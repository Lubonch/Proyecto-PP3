﻿using Backend.Repository.Contracts;
using Backend.Service.Contracts;
using Backend.Domain;
using Dapper;
using System.Net;
using System;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Backend.Repository
{
    public class TallerRepository : ITallerRepository
    {
        public TallerRepository()
        {

        }
        public List<Taller> GetAllTalleres()
        {

            IConfigurationRoot _configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

            string connString = _configuration.GetConnectionString("BackendDatabase")!;
            //var connString = app.Configuration.GetConnectionString("MangaCountDatabase");
            var sql = "select * from Taller where Activo = 1";
            var products = new List<Taller>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                products = connection.Query<Taller>(sql).ToList();
            }

            return products;
        }

    }
}
