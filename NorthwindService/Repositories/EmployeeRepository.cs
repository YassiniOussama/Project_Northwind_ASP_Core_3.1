using Microsoft.EntityFrameworkCore.ChangeTracking;
using NorthwinDB;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
namespace NorthwindService.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        //utiliser un champ de dictionnaire statique qui est thread-safe
        //pour mettre les clients en cache
        private static ConcurrentDictionary<string, Employee> employeesCache;
        // utiliser un champ de contexte de données d'instance parce qu'il ne doit pas
        //être mis en cache en raison de leur mise en cache interne
        private Northwind db;

        public EmployeeRepository(Northwind db)
        {
            this.db = db;
            //pré - chargement des clients de la base de données comme un dictionnaire normal avec
            // CustomerID comme clé, puis conversion en un dictionnaire thread-safe
            if(employeesCache == null)
            {
                employeesCache = new ConcurrentDictionary
                <string, Employee>(
                db.Employees.ToDictionary(e => e.EmployeeID));
            }
        }
        public async Task<Employee> CreateAsync(Employee e)
        {
            e.EmployeeID = e.EmployeeID.ToUpper();
            EntityEntry<Employee> added = await db.Employees.AddAsync(e);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                // si le client est nouveau, ajoute le à la cache, ou mette à jour la cache
                return employeesCache.AddOrUpdate(e.EmployeeID, e, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<Employee>> RetrieveAllAsync()
        {
            // on lit de la cache pour la performance
            return Task.Run<IEnumerable<Employee>>(() => employeesCache.Values);
        }
        public Task<Employee> RetrieveAsync(string id)
        {
            return Task.Run(() =>
            {
                // on lit de la cache pour la performance
                id = id.ToUpper();
                Employee e;
                employeesCache.TryGetValue(id, out e);
                return e;
            });
        }
        private Employee UpdateCache(string id, Employee e)
        {
            Employee old;
            if (employeesCache.TryGetValue(id, out old))
            {
                if (employeesCache.TryUpdate(id, e, old))
                {
                    return e;
                }
            }
            return null;
        }
        public async Task<Employee> UpdateAsync(string id, Employee e)
        {
            id = id.ToUpper();
            e.EmployeeID = e.EmployeeID.ToUpper();
            db.Employees.Update(e);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(id, e);
            }
            return null;
        }
        public async Task<bool?> DeleteAsync(string id)
        {
            id = id.ToUpper();
            Employee e = db.Employees.Find(id);
            db.Employees.Remove(e);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                return employeesCache.TryRemove(id, out e);
            }
            else
            {
                return null;
            }
        }
    }
}