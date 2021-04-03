using DevServ.Core.Entities;
using System;
using System.Collections.Generic;

namespace DevServ.Web.ApiModels
{
    public class SkillDto
    {
        public string Name { get; set; }
        public int YearsOfExperience { get; set; }

        internal static IEnumerable<SkillDto> FromSkills(ICollection<Skill> skills)
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
}