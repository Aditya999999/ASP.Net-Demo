using DP.Web.Data;
using DP.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DP.xUnitTestProject
{
    internal static class DbContextMocker
    {
        //Note: 
        //  Since all tests run parallely,
        //  ensure that test method gets its own locally scoped Inmemory database

        public static ApplicationDbContext GetApplicationDbContext(string database)
        {
            //Create a fresh service provider for the Inmemory database instance.
            var serviceProvider = new ServiceCollection()
                      .AddEntityFrameworkInMemoryDatabase()
                      .BuildServiceProvider();
            
            // Create a new options instance,
            // telling the context to use InMemory database and the new service provider.
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(database)
                            .UseInternalServiceProvider(serviceProvider)
                            .Options;

            //Create the instance of the DbContext (would be an Inmemory database)
            // Note: It will use the Schema as defined in the Data and Models folders
            
            var dbContext = new ApplicationDbContext(options);

            // Add entities to the inmemory database
            dbContext.SeedData();

            return dbContext;
        }
        internal static readonly Complainer[] TestData_Complainers
            =
        {
            new Complainer
            {
                ComplainerId = 1,
                FirstName = "Aditya"
            },
            new Complainer
            {
                ComplainerId = 2,
                FirstName = "Amit"
            },
            new Complainer
            {
                ComplainerId = 3,
                FirstName = "Aman"
            },
            new Complainer
            {
                ComplainerId = 4,
                FirstName = "Saurabh"
            }
        };
        ///<summary>
        /// An extension method for the DbContext Object.
        /// </summary>
        /// <param name="context"></param>

        private static void SeedData(this ApplicationDbContext context)
        {
            context.Complainers.AddRange(TestData_Complainers);
            context.SaveChanges();
        }
    }
}
