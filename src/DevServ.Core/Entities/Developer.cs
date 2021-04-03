using DevServ.SharedKernel;
using DevServ.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevServ.Core.Entities
{
    public class Developer : BaseEntity, IAggregateRoot
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }         
        public string SocialSecurityNumber { get; set; }
        public string HomePage { get; set; }
        public bool OpenToWork { get; set; }
        
        public ICollection<Skill> Skills { get; set; }
    }
}
