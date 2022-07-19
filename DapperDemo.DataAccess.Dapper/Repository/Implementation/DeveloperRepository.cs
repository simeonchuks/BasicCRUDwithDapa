using Dapper;
using DapperDemo.Core.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemo.DataAccess.Dapper.Repository.Implementation
{
    public class DeveloperRepository : IDeveloperRepository
    {
        //private IDbConnection _dbConnection;
        //public DeveloperRepository(IConfiguration configuration)
        //{
        //    _dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));  The easeir way to read configuration
        //}

        protected readonly IConfiguration _configuration;
        public DeveloperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection connection
        {
            get
            {
                return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        public void AddDeveloper(Developer developer)
        {
            try
            {
                using(IDbConnection dbConnection = connection)
                {
                    dbConnection.Open();
                    string query = "INSERT INTO Developers(DeveloperName, Email, GithubURL, ImageURL, Department, JoinDate)" +
                                   "VALUES(@DeveloperName, @Email, @GithubURL, @ImageURL, @Department, @JoinDate);"
                                    + "Select CAST(SCOPE_IDENTITY() as int); ";

                    dbConnection.Execute(query, developer);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteDeveloper(int Id)
        {
            try
            {
                using(IDbConnection dbConnection = connection)
                {
                    dbConnection.Open();
                    string query = @"DELETE FROM Developers WHERE Id = @Id";
                    dbConnection.Execute(query, new { Id = Id });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Developer>> GetAllDevelopersAsync()
        {
            try
            {
                using (IDbConnection dbConnection = connection)
                {
                    dbConnection.Open();
                    string query = @"SELECT Id, DeveloperName, Email, GithubURL, ImageURL, Department, JoinDate FROM Developers";
                    return await dbConnection.QueryAsync<Developer>(query);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Developer> GetDeveloperByEmail(string Email)
        {
            try
            {
                using (IDbConnection dbConnection = connection)
                {
                    dbConnection.Open();
                    string query = @"SELECT * FROM Developers WHERE Email = @Email";
                    return await dbConnection.QueryFirstOrDefaultAsync<Developer>(query, new { Email = Email });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Developer> GetDeveloperByIdAsync(int Id)
        {
            try
            {
                using (IDbConnection dbConnection = connection)
                {
                    dbConnection.Open();
                    string query = @"SELECT * FROM Developers WHERE Id = @Id";
                    return await dbConnection.QueryFirstOrDefaultAsync<Developer>(query, new { Id = Id });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateDeveloper(Developer developer)
        {
            try
            {
                using (IDbConnection dbConnection = connection)
                {
                    dbConnection.Open();
                    string query = @"UPDATE Dvevelopers SET DeveloperName=@DeveloperName, Email=@Email, 
                                                            GithubURL=@GithubURL, ImageURL=@ImageURL, Department=@Department, JoinDate=@JoinDate";
                    dbConnection.Execute(query, developer);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
