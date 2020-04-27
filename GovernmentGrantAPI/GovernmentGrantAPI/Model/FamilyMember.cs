using System;

namespace GovernmentGrantAPI.Model
{
    public class FamilyMember
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Gender Gender { get; set; }

        public MaritalStatus MaritalStatus { get; set; }

        public string Spouse { get; set; }

        public OccupationType OccupationType { get; set; }

        public double AnnualIncome { get; set; }

        public DateTime DOB { get; set; }
    }
}
