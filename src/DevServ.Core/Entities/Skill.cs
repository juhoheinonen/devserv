using DevServ.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevServ.Core.Entities
{
    public class Skill : BaseEntity
    {
        public string Name { get; set; }
        public int YearsOfExperience { get; set; }

        public int DeveloperId { get; set; }
        public Developer Developer { get; set; }

        public Skill()
        {

        }

        public Skill(string name, int yearsOfExperience)
        {
            Name = name;
            YearsOfExperience = yearsOfExperience;
        }

        public Skill(int developerId, string name, int yearsOfExperience): this(name, yearsOfExperience)
        {
            DeveloperId = developerId;
        }

        public override bool Equals(object obj)
        {
            return obj is Skill skill &&
                   DeveloperId == skill.DeveloperId &&
                   Name == skill.Name &&
                   YearsOfExperience == skill.YearsOfExperience;
        }
    }
}
