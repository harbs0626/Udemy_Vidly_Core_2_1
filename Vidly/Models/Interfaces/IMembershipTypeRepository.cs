using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Models.Interfaces
{
    public interface IMembershipTypeRepository
    {
        IQueryable<MembershipType> MembershipTypes { get; }

        void SaveMembershipType(MembershipType membership);

    }
}
