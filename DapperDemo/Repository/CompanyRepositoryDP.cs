using Dapper;
using DapperDemo.Data;
using DapperDemo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemo.Repository
{
    public class CompanyRepositoryDP : ICompanyRepository
    {
        private IDbConnection db;
        public CompanyRepositoryDP(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public Company Add(Company company)
        {
            //Thi query creates an object in the db and returns the created object.
            var sql = "INSERT INTO Companies(Name, Address, City, State, PostalCode) Values (@Name, @Address, @City, @State, @PostalCode);"
                        + "Select CAST(SCOPE_IDENTITY() as int); ";
            var id = db.Query<int>(sql, company).Single();
            company.CompanyId = id;
            return company;
            
            //MANUEL MAPPING BELOW......
            //Pass the Id as a new Object 
            //var id = db.Query<int>(sql, new
            //{
            //    @Name = company.Name,
            //    @Address = company.Address,
            //    @City = company.City,
            //    @State = company.State,
            //    @PostalCode = company.PostalCode
            //}).Single();

            //company.CompanyId = id;
            //return company;
        }

        public Company Find(int id)
        {
            var sql = "SELECT * FROM Companies WHERE CompanyId = @CompanyId";
            var response = db.Query<Company>(sql, new { @CompanyId = id }).Single();
            return response;
        }

        public List<Company> GetAll()
        {
            var sql = "SELECT * FROM Companies";
            return db.Query<Company>(sql).ToList(); 
        }

        public void Remove(int id)
        {
            var sql = "DELETE FROM Companies WHERE CompanyId = @Id";
            db.Execute(sql, new { id });
        }

        public Company Update(Company company)
        {
            var sql = "UPDATE Companies " +
                "SET Name = @Name, Address = @Address, City = @City, State = @State, PostalCode = @PostalCode WHERE CompanyId = @CompanyId";
            db.Execute(sql, company);
            return company;
        }
    }
}
