using DapperDemo.Data;
using DapperDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemo.Repository
{
    public class CompanyRepositoryEF : ICompanyRepository
    {
        private readonly AppDbContext _context;

        public CompanyRepositoryEF(AppDbContext context)
        {
            _context = context;
        }
        public Company Add(Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();
            return company;
        }

        public Company Find(int id)
        {
            var company = _context.Companies.FirstOrDefault(c => c.CompanyId == id);
            return company;
        }

        public List<Company> GetAll()
        {
            return _context.Companies.ToList();
        }

        public void Remove(int id)
        {
            var company = _context.Companies.FirstOrDefault(c => c.CompanyId == id);
            _context.Companies.Remove(company);
            _context.SaveChanges();
            return;
        }

        public Company Update(Company company)
        {
            _context.Companies.Update(company);
            _context.SaveChanges();
            return company;
        }
    }
}
