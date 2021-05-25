using NorthwinDB;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NorthwindService.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateAsync(Employee e);
        Task<IEnumerable <Employee>> RetrieveAllAsync();
        Task<Employee> RetrieveAsync( string id);
        Task<Employee> UpdateAsync( string id, Employee e);
        Task<bool?> DeleteAsync( string id);
    }
}