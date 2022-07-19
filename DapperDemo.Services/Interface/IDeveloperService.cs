using DapperDemo.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemo.Services.Interface
{
    public interface IDeveloperService
    {
        Task<IEnumerable<Developer>> GetAllDevelopersAsync();
        Task<Developer> GetDeveloperByIdAsync(int Id);
        Task<Developer> GetDeveloperByEmail(string Email);
        void AddDeveloper(Developer developer);
        void UpdateDeveloper(Developer developer);
        void DeleteDeveloper(int Id);
    }
}
