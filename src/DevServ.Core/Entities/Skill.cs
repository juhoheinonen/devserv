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
        public Developer Developer { get; set; }
        public string Name { get; set; }
        public int YearsOfExperience { get; set; }
    }
}
