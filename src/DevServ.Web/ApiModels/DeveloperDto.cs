using DevServ.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevServ.Web.ApiModels
{
    public class DeveloperDto
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Description { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string SocialSecurityNumber { get; set; }
        public string HomePage { get; set; }
        public bool OpenToWork { get; set; }

        public IEnumerable<SkillDto> Skills { get; set; }

        public static DeveloperDto FromDeveloper(Developer developer)
        {
            return new DeveloperDto()
            {
                Id = developer.Id,
                FirstName = developer.FirstName,
                LastName = developer.FirstName,
                Description = developer.FirstName,
                Email = developer.FirstName,
                PhoneNumber = developer.FirstName,
                SocialSecurityNumber = developer.SocialSecurityNumber,
                HomePage = developer.HomePage,
                OpenToWork = developer.OpenToWork,
                Skills = SkillDto.FromSkills(developer.Skills)
            };
        }
    }
}
