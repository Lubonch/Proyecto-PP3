using Backend.Repository.Contracts;
using Microsoft.AspNetCore.Identity;
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


            var parameters = new DynamicParameters();
            parameters.Add("@Usuario", request.Usuario, DbType.AnsiString, ParameterDirection.Input, request.Usuario.Length);

            var sql = "select * from Alumno WHERE Usuario = @Usuario";
            List<User> users;
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                users = connection.Query<User>(sql, parameters).ToList();
            }

            if (users.Count == 0)
                return new List<User>();

            var user = users.First();
            var passwordHasher = new PasswordHasher<string>();
            var result = passwordHasher.VerifyHashedPassword(null, user.Password, request.Password);

            if (result == PasswordVerificationResult.Success)
                return new List<User> { user };

            return new List<User>();
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

            var passwordHasher = new PasswordHasher<string>();
            string hashedPassword = passwordHasher.HashPassword(null, dataRegistro.Password);


            var parameters = new DynamicParameters();
            parameters.Add("@Usuario", dataRegistro.Usuario, DbType.AnsiString, ParameterDirection.Input, dataRegistro.Usuario.Length);
            parameters.Add("@Password", hashedPassword, DbType.AnsiString, ParameterDirection.Input, hashedPassword.Length);
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

        public void updateHash() 
        {
            IConfigurationRoot _configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

            string connString = _configuration.GetConnectionString("BackendDatabase")!;

            var passwordHasher = new PasswordHasher<string>();

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                var users = connection.Query<(int Id, string Password)>("SELECT Id, Password FROM Alumno").ToList();

                foreach (var user in users)
                {
                    // Skip if already hashed (optional: check hash format)
                    if (user.Password.StartsWith("AQAAAA")) continue;

                    string hashed = passwordHasher.HashPassword(null, user.Password);
                    connection.Execute("UPDATE Alumno SET Password = @Password WHERE Id = @Id",
                        new { Password = hashed, Id = user.Id });
                }
            }
        }
    }
}
