using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Models.Interfaces;

namespace Vidly.Models.EntityFrameworks
{
    public class EFMembershipTypeRepository : IMembershipTypeRepository
    {
        private ApplicationDbContext _context;

        public EFMembershipTypeRepository(ApplicationDbContext ctx)
        {
            this._context = ctx;
        }

        public IQueryable<MembershipType> MembershipTypes => this._context.MembershipTypes;

        public void SaveMembershipType(MembershipType membershipType)
        {
            if (membershipType.Id == 0)
            {
                this._context.MembershipTypes.Add(membershipType);
            }
            else
            {
                MembershipType membershipTypeEntry = this._context.MembershipTypes
                    .FirstOrDefault(m => m.Id == membershipType.Id);

                if (membershipTypeEntry != null)
                {
                    membershipTypeEntry.SignUpFee = membershipType.SignUpFee;
                    membershipTypeEntry.DurationInMonths = membershipType.DurationInMonths;
                    membershipTypeEntry.DiscountRate = membershipType.DiscountRate;
                }
            }

            this._context.SaveChanges();
        }
    }
}
