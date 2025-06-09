using Backend.Repository.Contracts;
using Backend.Service.Contracts;
using Backend.Domain;
using Dapper;
using System.Net;
using System;
using Microsoft.Data.SqlClient;
using System.Data;
using Azure.Core;

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

        public Boolean Registro(Registro dataRegistro)
        {

            IConfigurationRoot _configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

            var FechaRegistro = DateTime.Now;
            var activo = true;
            var administrador = false;

            string connString = _configuration.GetConnectionString("BackendDatabase")!;
            //var connString = app.Configuration.GetConnectionString("MangaCountDatabase");

            var parameters = new DynamicParameters();
            parameters.Add("@Usuario", dataRegistro.Usuario, DbType.AnsiString, ParameterDirection.Input, dataRegistro.Usuario.Length);
            parameters.Add("@Password", dataRegistro.Password, DbType.AnsiString, ParameterDirection.Input, dataRegistro.Password.Length);
            parameters.Add("@Email", dataRegistro.Email, DbType.AnsiString, ParameterDirection.Input, dataRegistro.Email.Length);
            parameters.Add("@TallerId", null, DbType.AnsiString, ParameterDirection.Input);
            parameters.Add("@FechaRegistro", FechaRegistro, DbType.AnsiString, ParameterDirection.Input);
            parameters.Add("@Activo", activo, DbType.AnsiString, ParameterDirection.Input, dataRegistro.Email.Length);
            parameters.Add("@Administrador", administrador, DbType.AnsiString, ParameterDirection.Input);


            var sql = "insert into [dbo].[Alumno]([Usuario],[Password],[Email],[TallerId],[FechaRegistro],[Activo],[Administrador]) VALUES(@Usuario,@Password,@Email,@TallerId,@FechaRegistro,@Activo,@Administrador) SELECT SCOPE_IDENTITY();";
            var result = new List<User>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                result = connection.Query<User>(sql, parameters).ToList();
            }

            return result.Any();
        }
    }
}
