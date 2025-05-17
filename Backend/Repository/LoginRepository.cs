using Backend.Repository.Contracts;
using Backend.Service.Contracts;
using Backend.Domain;
using Dapper;
using System.Net;
using System;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Backend.Repository
{
    public class LoginRepository : ILoginRepository
    {
        public LoginRepository()
        {

        }
        public List<User> Login(LoginRequest request)
        {

            IConfigurationRoot _configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

            string connString = _configuration.GetConnectionString("BackendDatabase")!;
            //var connString = app.Configuration.GetConnectionString("MangaCountDatabase");
            
            var parameters = new DynamicParameters();
            parameters.Add("@Usuario", request.Usuario, DbType.AnsiString, ParameterDirection.Input, request.Usuario.Length);
            parameters.Add("@Password", request.Password, DbType.AnsiString, ParameterDirection.Input, request.Password.Length);


            var sql = "select * from Alumno WHERE Usuario = @Usuario AND Password = @Password";
            var products = new List<User>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                products = connection.Query<User>(sql,parameters).ToList();
            }

            return products;
        }
    }
}
