﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GovernmentGrantAPI.Model;
using GovernmentGrantAPI.Model.Extension;
using GovernmentGrantAPI.Model.Dto;

namespace GovernmentGrantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseholdsController : ControllerBase
    {
        private readonly HouseholdContext _context;

        public HouseholdsController(HouseholdContext context)
        {
            _context = context;
        }

        // GET: api/Households
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Household>>> GetHouseholds(
            int? hasMemberYoungerThan,
            int? hasMemberOlderThan,
            double? householdIncomeLessThan,
            bool? hasHusbandAndWife,
            HousingType? housingType)
        {
            var households =  await _context.Households.Include(h => h.FamilyMembers).ToListAsync();

            if (hasMemberYoungerThan.HasValue)
            {
                households = households.
                                Where(h => h.FamilyMembers.Any(f => f.GetAge() < hasMemberYoungerThan.Value)).
                                ToList();
            }

            if (hasMemberOlderThan.HasValue)
            {
                households = households.
                                Where(h => h.FamilyMembers.Any(f => f.GetAge() > hasMemberOlderThan.Value)).
                                ToList();
            }

            if (householdIncomeLessThan.HasValue)
            {
                households = households.
                                Where(h=> h.GetAnnualIncome() < householdIncomeLessThan.Value).
                                ToList();
            }

            if (hasHusbandAndWife.HasValue && hasHusbandAndWife.Value)
            {
                households = households.
                                Where(h => h.HasHusbandAndWife()).
                                ToList();
            }

            if (housingType.HasValue)
            {
                households = households.Where(h => h.HousingType == housingType.Value)
                             .ToList();
            }

            return households;
        }

        // GET: api/Households/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Household>> GetHousehold(int id)
        {
            var households = await _context.Households.Include(h => h.FamilyMembers).ToListAsync();

            var household = households.Where(h => h.Id == id).FirstOrDefault();

            if (household == null)
            {
                return NotFound();
            }

            return household;
        }

        [HttpPost]
        public async Task<ActionResult<Household>> PostHousehold(AddHouseholdDto addHouseholdDto)
        {
            var household = new Household
            {
                FamilyMembers = new List<FamilyMember>(),
                HousingType = addHouseholdDto.HousingType
            };

            _context.Households.Add(household);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHousehold), new { id = household.Id }, household);
        }

        [HttpPost("addfamilymember")]
        public async Task<ActionResult<Household>> AddFamilyMember(AddFamilyMemberDto addFamilyMemberDto)
        {
            if (!HouseholdExists(addFamilyMemberDto.HouseholdId))
            {
                return NotFound();
            }
            var households = await _context.Households.Include(h => h.FamilyMembers).ToListAsync();

            var household = households.First(h => h.Id == addFamilyMemberDto.HouseholdId);

            var nextFamilyMemberId = households.SelectMany(h => h.FamilyMembers).Select(f => f.Id).DefaultIfEmpty(0).Max() + 1;

            var familyMember = new FamilyMember()
            {
                Id = nextFamilyMemberId,
                Name = addFamilyMemberDto.Name,
                Gender = addFamilyMemberDto.Gender,
                MaritalStatus = addFamilyMemberDto.MaritalStatus,
                Spouse = addFamilyMemberDto.Spouse,
                OccupationType = addFamilyMemberDto.OccupationType,
                AnnualIncome = addFamilyMemberDto.AnnualIncome,
                DOB = DateTime.Parse(addFamilyMemberDto.DOB)
            };

            household.FamilyMembers.Add(familyMember);

            _context.Entry(household).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return CreatedAtAction(nameof(GetHousehold), new { id = household.Id }, household);
        }

        private bool HouseholdExists(int id)
        {
            return _context.Households.Any(e => e.Id == id);
        }
    }
}
