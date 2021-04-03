using DevServ.SharedKernel;
using DevServ.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevServ.Core.Entities
{
    public class Developer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string HomePage { get; set; }
        public bool OpenToWork { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Skill> Skills { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Developer developer &&
                   Id == developer.Id &&
                   FirstName == developer.FirstName &&
                   LastName == developer.LastName &&
                   Description == developer.Description &&
                   Email == developer.Email &&
                   PhoneNumber == developer.PhoneNumber &&
                   SocialSecurityNumber == developer.SocialSecurityNumber &&
                   HomePage == developer.HomePage &&
                   OpenToWork == developer.OpenToWork &&
                   IsDeleted == developer.IsDeleted &&
                   Skills.SequenceEqual(developer.Skills);                
        }
    }    
}
