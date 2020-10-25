using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();

            context.Database.Migrate();

            if (!context.MembershipTypes.Any())
            {
                context.MembershipTypes.AddRange(
                    new MembershipType
                    {
                        Name = "Pay As You Go",
                        SignUpFee = 0,
                        DurationInMonths = 0,
                        DiscountRate = 0
                    },
                    new MembershipType
                    {
                        Name = "Monthly",
                        SignUpFee = 30,
                        DurationInMonths = 1,
                        DiscountRate = 10
                    },
                    new MembershipType
                    {
                        Name = "Quarterly",
                        SignUpFee = 90,
                        DurationInMonths = 3,
                        DiscountRate = 15
                    },
                    new MembershipType
                    {
                        Name = "Annual",
                        SignUpFee = 300,
                        DurationInMonths = 12,
                        DiscountRate = 20
                    }
                );

                context.SaveChanges();
            }
            
            if (!context.Genres.Any())
            {
                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Action"
                    },
                    new Genre
                    {
                        Name = "Thriller"
                    },
                    new Genre
                    {
                        Name = "Family"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    },
                    new Genre
                    {
                        Name = "Comedy"
                    }
                );

                context.SaveChanges();
            }
        }
    }

}
