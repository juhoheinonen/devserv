using DevServ.Core.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;
using System.Threading.Tasks;
using Xunit;

namespace DevServ.Infrastructure.Tests
{
    public class DeveloperRepositoryTests
    {
        private IConfiguration _configuration;

        public DeveloperRepositoryTests()
        {
            _configuration = InitConfiguration();
        }

        [Fact]
        public async Task GetByIdAsync_ExistingDeveloper_ReturnsCorrectDeveloper()
        {
            using (var context = CreateTestContext())
            {
                var sut = new DeveloperRepository(context);
                var actual = await sut.GetByIdAsync(2);

                var expected = new Developer
                {
                    Id = 2,
                    FirstName = "Teemu",
                    LastName = "Testaaja",
                    Email = "teemu.testaaja@testi.fi",
                    SocialSecurityNumber = "090287-499Y",
                    Description = "Kokenut frontend-kehittäjä.",
                    HomePage = "https://www.mikrobitti.fi",
                    OpenToWork = true,
                    PhoneNumber = "+358 23244 2234",
                    IsDeleted = false,
                    Skills = new System.Collections.Generic.List<Skill>
                    {
                        new Skill(2, "JavaScript", 9),
                        new Skill(2, "VueJs", 7),
                        new Skill(2, "Css", 5),
                        new Skill(2, "Gimp", 8)
                    }
                };

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public async Task GetByIdAsync_DeletedDeveloper_ReturnsNull()
        {
            using (var context = CreateTestContext())
            {
                var sut = new DeveloperRepository(context);
                var actual = await sut.GetByIdAsync(3);

                Assert.Null(actual);
            }
        }

        [Fact]
        public async Task GetByIdAsync_NotExistingDeveloper_ReturnsNull()
        {
            using (var context = CreateTestContext())
            {
                var sut = new DeveloperRepository(context);
                var actual = await sut.GetByIdAsync(5);

                Assert.Null(actual);
            }
        }

        [Fact]
        public async Task ListAsync_NormalOperation_ReturnsAllExceptDeletedDeveloper()
        {
            using (var context = CreateTestContext())
            {
                var sut = new DeveloperRepository(context);
                var actual = await sut.ListAsync();

                Assert.Equal(2, actual.Count);
                Assert.Equal(1, actual[0].Id);
                Assert.Equal(2, actual[1].Id);
            }
        }

        private IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return config;
        }

        private DevServDbContext CreateTestContext()
        {
            var databaseFilePath = _configuration.GetSection("AppSettings")["DatabaseFilePath"];
            if (System.IO.File.Exists(databaseFilePath))
            {
                System.IO.File.Delete(databaseFilePath);
            }

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<DevServDbContext>().
                 UseSqlite(_configuration.GetConnectionString("SqliteConnection"));

            var context = new DevServDbContext(dbContextOptionsBuilder.Options);
            context.Database.EnsureCreated();
            TestDataInitializer.PopulateTestData(context);

            return context;
        }
    }
}
