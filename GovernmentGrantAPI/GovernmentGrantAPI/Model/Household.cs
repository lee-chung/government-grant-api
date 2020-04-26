using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovernmentGrantAPI.Model
{
    public class Household
    {
        public HousingType HousingType { get; set; }

        public IEnumerable<FamilyMember> FamilyMembers { get; set; }
    }
}
