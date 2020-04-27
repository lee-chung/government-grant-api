using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovernmentGrantAPI.Model.Dto
{
    public class AddFamilyMemberDto
    {
        public int HouseholdId { get; set; }

        public string Name { get; set; }

        public Gender Gender { get; set; }

        public MaritalStatus MaritalStatus { get; set; }

        public string Spouse { get; set; }

        public OccupationType OccupationType { get; set; }

        public double AnnualIncome { get; set; }

        public string DOB { get; set; }
    }
}
