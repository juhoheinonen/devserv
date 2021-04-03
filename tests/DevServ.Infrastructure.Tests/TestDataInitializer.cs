using DevServ.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevServ.Infrastructure.Tests
{
    public class TestDataInitializer
    {
        private static readonly Developer Developer1 = new Developer()
        {
            FirstName = "Petri",
            LastName = "Testinen",
            Email = "petri.testinen@testi.fi",
            SocialSecurityNumber = "230578-481H",
            Description = "Kokenut pelinkehittäjä, kuuluisa strategiapelistä X.",
            HomePage = "https://www.mikrobitti.fi",
            OpenToWork = true,
            PhoneNumber = "+358 470123 2142",
            IsDeleted = false,
            Skills = new System.Collections.Generic.List<Skill>
            {
                new Skill("C#", 10),
                new Skill("Angular", 4),
                new Skill("Sql", 8),
                new Skill("Azure", 5),
            }
        };

        public static readonly Developer Developer2 = new Developer()
        {
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
                new Skill("JavaScript", 9),
                new Skill("VueJs", 7),
                new Skill("Css", 5),
                new Skill("Gimp", 8)
            }
        };

        private static readonly Developer Developer3 = new Developer()
        {
            FirstName = "Sulo",
            LastName = "Surffaaja",
            Email = "sulo@testiemail.fi",
            SocialSecurityNumber = "230578-481H",
            Description = "Kokenut AWS-kehittäjä, osaa myös pen testausta.",
            HomePage = "https://www.isc2.com",
            OpenToWork = false,
            PhoneNumber = "+358 422123 2111",
            IsDeleted = true,
            Skills = new System.Collections.Generic.List<Skill>
            {
                new Skill("c", 10),
                new Skill("Java", 7),
                new Skill("AWS", 8),
                new Skill("Pen testing", 3)
            }
        };

        public static void PopulateTestData(DevServDbContext dbContext)
        {
            foreach (var item in dbContext.Developers)
            {
                dbContext.Remove(item);
            }
            dbContext.SaveChanges();

            dbContext.Developers.Add(Developer1);
            dbContext.Developers.Add(Developer2);
            dbContext.Developers.Add(Developer3);

            dbContext.SaveChanges();
        }
    }

}
