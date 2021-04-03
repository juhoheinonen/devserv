using DevServ.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevServ.Web.ApiModels
{
    public class SkillDto
    {        
        public string Name { get; set; }
        public int YearsOfExperience { get; set; }

        internal static IEnumerable<SkillDto> FromSkills(ICollection<Skill> skills)
        {
            if (skills != null)
            {
                foreach (var skill in skills)
                {
                    yield return new SkillDto
                    {                        
                        Name = skill.Name,
                        YearsOfExperience = skill.YearsOfExperience
                    };
                }
            }
        }

        internal static List<Skill> ToSkills(IEnumerable<SkillDto> skillsDtos)
        {
            if (skillsDtos == null)
            {
                return new List<Skill>();
            }

            var skills = skillsDtos.Select(s => new Skill { Name = s.Name, YearsOfExperience = s.YearsOfExperience }).ToList();

            return skills;
        }
    }
}