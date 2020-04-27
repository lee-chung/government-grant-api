using System.Collections.Generic;

namespace GovernmentGrantAPI.Model
{
    public class Household
    {
        public int Id { get; set; }

        public HousingType HousingType { get; set; }

        public ICollection<FamilyMember> FamilyMembers { get; set; }
    }
}
