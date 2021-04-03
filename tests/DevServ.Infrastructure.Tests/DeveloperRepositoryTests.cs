using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using Xunit;

namespace DevServ.Infrastructure.Tests
{
    public class DeveloperRepositoryTests
    {
        private DevServDbContext CreateTestContext()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<DevServDbContext>().
                            UseSqlite(CreateInMemoryDatabase());

            return new DevServDbContext(dbContextOptionsBuilder.Options);
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        [Fact]
        public void Test1()
        {
            using (var context = CreateTestContext())
            {
                TestDataInitializer.PopulateTestData(context);

            }
        }
    }
}
