using DevServ.Core.Entities;
using DevServ.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevServ.Infrastructure
{
    public class DeveloperFilter : IFilter<Developer>
    {
        private List<string> _requiredSkillsNames;

        public DeveloperFilter(List<string> requiredSkillsNames)
        {
            _requiredSkillsNames = requiredSkillsNames;
        }

        public bool Filter(Developer entity)
        {
            var developerSkillNames = (entity.Skills ?? new List<Skill>()).Select(s => (s.Name ?? string.Empty));            

            foreach (var requiredSkill in _requiredSkillsNames)
            {
                if (!developerSkillNames.Any(d => d.Equals(requiredSkill, StringComparison.InvariantCultureIgnoreCase)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
