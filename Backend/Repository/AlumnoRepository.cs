using Backend.Repository.Contracts;
using Backend.Service.Contracts;
using Backend.Domain;
using Dapper;
using System.Net;
using System;
using Microsoft.Data.SqlClient;
using System.Data;
using Azure.Core;
using log4net;
using Serilog;

namespace Backend.Repository
{
    public class AlumnoRepository : IAlumnoRepository
    {
        private readonly ILog Logger;

        public AlumnoRepository()
        {
            Logger = LogManager.GetLogger("AlumnoRepository");
            log4net.GlobalContext.Properties["ProcessName"] = "AlumnoRepository";
        }

        public List<User> GetAlumnoById(int Id)
        {
            try
            {
                Logger.Info($"Getting Alumno by Id: {Id}");

                IConfigurationRoot _configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                string connString = _configuration.GetConnectionString("BackendDatabase")!;

                var parameters = new DynamicParameters();
                parameters.Add("@Id", Id, DbType.AnsiString, ParameterDirection.Input, Id);

                var sql = "select * from Alumno WHERE Id = @Id";
                var products = new List<User>();
                using (var connection = new SqlConnection(connString))
                {
                    connection.Open();
                    products = connection.Query<User>(sql, parameters).ToList();
                }

                if (products.Count == 0)
                    Logger.Warn($"No Alumno found with Id: {Id}");
                else
                    Logger.Info($"Found {products.Count} Alumno(s) with Id: {Id}");

                return products;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in GetAlumnoById for Id {Id}", ex);
                throw;
            }
        }

        public List<User> IncribirseTaller(TallerRequest request)
        {
            try
            {
                Logger.Info($"User {request.UsuarioId} attempting to enroll in Taller {request.TallerId}");

                IConfigurationRoot _configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                string connString = _configuration.GetConnectionString("BackendDatabase")!;

                var parameters = new DynamicParameters();
                parameters.Add("@UsuarioId", request.UsuarioId, DbType.AnsiString, ParameterDirection.Input, request.UsuarioId);
                parameters.Add("@TallerId", request.TallerId, DbType.AnsiString, ParameterDirection.Input, request.TallerId);

                var sql = "update Alumno set tallerId = @TallerId WHERE id = @UsuarioId; select * from Alumno WHERE id = @UsuarioId";

                var products = new List<User>();
                using (var connection = new SqlConnection(connString))
                {
                    connection.Open();
                    products = connection.Query<User>(sql, parameters).ToList();
                }

                if (products.Count == 0)
                    Logger.Warn($"User {request.UsuarioId} failed to enroll in Taller {request.TallerId}");
                else
                    Logger.Info($"User {request.UsuarioId} successfully enrolled in Taller {request.TallerId}");

                return products;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in IncribirseTaller for UsuarioId {request.UsuarioId}, TallerId {request.TallerId}", ex);
                throw;
            }
        }
    }
}