using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoinMemoryListWithEF.Entity;
using EntityFramework.MemoryJoin;

namespace JoinMemoryListWithEF
{
    class Program
    {
        static void Main(string[] args)
        {
            var coachs = GetCoachs();           

            ReviewManagementEntities context = new ReviewManagementEntities();
            var reviews = context.Reviews.ToList();
            var memmoryCoach = context.FromLocalList(coachs);

            var results = (from review in context.Reviews
                          join coach in memmoryCoach
                          on review.CoachUserID equals coach.CoachID
                          select new
                          {
                              review.ReviewID,
                              review.CoachUserID,
                              coach.Name
                          }).ToList();
            foreach (var review in results)
            {
                Console.WriteLine($"ReviewID: {review.ReviewID}, CoachUserID: {review.CoachUserID}, CoachName: {review.Name}");
            }

            Console.ReadLine();
        }

        private static List<Coach> GetCoachs()
        {
            return new List<Coach>()
            {
                new Coach() { CoachID = 149, Name="ABC" },
                new Coach() { CoachID = 160, Name="OK" }
            };
        }
    }
}
