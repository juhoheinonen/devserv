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
        [EmailAddress]
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
                LastName = developer.LastName,
                Description = developer.Description,
                Email = developer.Email,
                PhoneNumber = developer.PhoneNumber,
                SocialSecurityNumber = developer.SocialSecurityNumber,
                HomePage = developer.HomePage,
                OpenToWork = developer.OpenToWork,
                //IsDeleted = developer.IsDeleted,
                Skills = SkillDto.FromSkills(developer.Skills)
            };
        }

        public static Developer ToDeveloper(DeveloperDto developerDto)
        {
            return new Developer()
            {
                Id = developerDto.Id,
                FirstName = developerDto.FirstName,
                LastName = developerDto.LastName,
                Description = developerDto.Description,
                Email = developerDto.Email,
                PhoneNumber = developerDto.PhoneNumber,
                SocialSecurityNumber = developerDto.SocialSecurityNumber,
                HomePage = developerDto.HomePage,
                OpenToWork = developerDto.OpenToWork,                
                Skills = SkillDto.ToSkills(developerDto.Skills)
            };
        }
    }
}
