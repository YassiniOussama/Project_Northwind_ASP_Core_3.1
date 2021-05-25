using Microsoft.EntityFrameworkCore.ChangeTracking;
using NorthwinDB;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
namespace NorthwindService.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        //utiliser un champ de dictionnaire statique qui est thread-safe
        //pour mettre les clients en cache
        private static ConcurrentDictionary<string, Customer> customersCache;
        // utiliser un champ de contexte de données d'instance parce qu'il ne doit pas
        //être mis en cache en raison de leur mise en cache interne
        private Northwind db;

        public CustomerRepository(Northwind db)
        {
            this.db = db;
            //pré - chargement des clients de la base de données comme un dictionnaire normal avec
            // CustomerID comme clé, puis conversion en un dictionnaire thread-safe
            if(customersCache == null)
            {
                customersCache = new ConcurrentDictionary
                <string, Customer>(
                db.Customers.ToDictionary(c => c.CustomerID));
            }
        }
        public async Task<Customer> CreateAsync(Customer c)
        {
            c.CustomerID = c.CustomerID.ToUpper();
            EntityEntry<Customer> added = await db.Customers.AddAsync(c);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                // si le client est nouveau, ajoute le à la cache, ou mette à jour la cache
                return customersCache.AddOrUpdate(c.CustomerID, c, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<Customer>> RetrieveAllAsync()
        {
            // on lit de la cache pour la performance
            return Task.Run<IEnumerable<Customer>>(() => customersCache.Values);
        }
        public Task<Customer> RetrieveAsync(string id)
        {
            return Task.Run(() =>
            {
                // on lit de la cache pour la performance
                id = id.ToUpper();
                Customer c;
                customersCache.TryGetValue(id, out c);
                return c;
            });
        }
        private Customer UpdateCache(string id, Customer c)
        {
            Customer old;
            if (customersCache.TryGetValue(id, out old))
            {
                if (customersCache.TryUpdate(id, c, old))
                {
                    return c;
                }
            }
            return null;
        }
        public async Task<Customer> UpdateAsync(string id, Customer c)
        {
            id = id.ToUpper();
            c.CustomerID = c.CustomerID.ToUpper();
            db.Customers.Update(c);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(id, c);
            }
            return null;
        }
        public async Task<bool?> DeleteAsync(string id)
        {
            id = id.ToUpper();
            Customer c = db.Customers.Find(id);
            db.Customers.Remove(c);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                return customersCache.TryRemove(id, out c);
            }
            else
            {
                return null;
            }
        }
    }
}