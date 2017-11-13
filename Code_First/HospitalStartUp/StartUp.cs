using P01_HospitalDatabase.Data;
using P01_HospitalDatabase;
using P01_HospitalDatabase.Initializer;
using P01_HospitalDatabase.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace HospitalStartUp
{
    public class StartUp
    {
        public static void Main()
        {
            var dbContext = new HospitalContext();

            using (dbContext)
            {
                // recomended to create database with Migrate()
                // dbContext.Database.Migrate();
                ////dbContext.Database.EnsureCreated();

                // Fills the db with random data
                // DatabaseInitializer.InitialSeed(dbContext);
            }
        }
    }
}