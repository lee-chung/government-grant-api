using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovernmentGrantAPI.Model.Extension
{
    public static class HouseholdExtension
    {
        public static double GetAnnualIncome(this Household household)
        {
            return household.FamilyMembers.Select(f => f.AnnualIncome).Sum();
        }

        public static bool HasHusbandAndWife(this Household household)
        {
            var marriedMembers = household.FamilyMembers.Where(f => f.MaritalStatus == MaritalStatus.Married).ToList();

            for (var i = 0; i < marriedMembers.Count; i++)
            {
                for (var j = 0; j < marriedMembers.Count; j++)
                {
                    if (marriedMembers[i].Name == marriedMembers[j].Spouse &&
                        marriedMembers[j].Name == marriedMembers[i].Spouse)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
