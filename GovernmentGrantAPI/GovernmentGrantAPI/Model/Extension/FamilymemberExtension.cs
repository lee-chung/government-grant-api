using System;

namespace GovernmentGrantAPI.Model.Extension
{
    public static class FamilymemberExtension
    {
        public static int GetAge(this FamilyMember familyMember)
        {
            var age = DateTime.Now.Year - familyMember.DOB.Year;
            if (DateTime.Now.DayOfYear < familyMember.DOB.DayOfYear)
            {
                age -= 1;
            }

            return age;
        }
    }
}
