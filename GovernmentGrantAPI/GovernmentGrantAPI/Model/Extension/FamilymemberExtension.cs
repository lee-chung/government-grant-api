using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovernmentGrantAPI.Model.Extension
{
    public static class FamilymemberExtension
    {
        public static int GetAge(this FamilyMember familyMember)
        {
            int age = 0;
            age = DateTime.Now.Year - familyMember.DOB.Year;
            if (DateTime.Now.DayOfYear < familyMember.DOB.DayOfYear)
                age = age - 1;

            return age;
        }
    }
}
