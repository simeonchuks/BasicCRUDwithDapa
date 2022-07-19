using DapperDemo.Core.Model;
using DapperDemo.DataAccess.Dapper.Repository;
using DapperDemo.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemo.Services.Implementation
{
    public class DeveloperService : IDeveloperService 
    {
        private readonly IDeveloperRepository _developerRepository;

        public DeveloperService(IDeveloperRepository developerRepository)
        {
            _developerRepository = developerRepository;
        }

        public void AddDeveloper(Developer developer)
        {
            _developerRepository.AddDeveloper(developer);
        }

        public void DeleteDeveloper(int Id)
        {
            _developerRepository.DeleteDeveloper(Id);
        }

        public async Task<IEnumerable<Developer>> GetAllDevelopersAsync()
        {
            return await _developerRepository.GetAllDevelopersAsync();
        }

        public async Task<Developer> GetDeveloperByEmail(string Email)
        {
            return await _developerRepository.GetDeveloperByEmail(Email);
        }

        public async Task<Developer> GetDeveloperByIdAsync(int Id)
        {
            return await _developerRepository.GetDeveloperByIdAsync(Id);
        }

        public void UpdateDeveloper(Developer developer)
        {
            _developerRepository.UpdateDeveloper(developer);
        }
    }
}
