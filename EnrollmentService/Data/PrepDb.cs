using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EnrollmentService.Data
{
    public static class PrepDb
    {
        public static void PrePopulation(IApplicationBuilder app, bool isProd)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            };
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if(isProd)
            {  
                Console.Write("--> Menjalankan migrasi");
                try
                {
                     context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Gagal Menjalankan Migrasi dengan error: {ex.Message}");
                }
            }

            if(!context.Students.Any())
            {
                Console.WriteLine("--> Seeding data Student...");
                context.Students.AddRange(
                    new Student{FirstName="Muhammad",LastName="Kifahi",EnrollmentDate=DateTime.Parse("2021-12-12")},
                    new Student{FirstName="Clint",LastName="Barton",EnrollmentDate=DateTime.Parse("2021-10-12")},
                    new Student{FirstName="Peter",LastName="Parker",EnrollmentDate=DateTime.Parse("2021-12-12")},
                    new Student{FirstName="Tony",LastName="Stark",EnrollmentDate=DateTime.Parse("2021-12-12")},
                    new Student{FirstName="Bruce",LastName="Banner",EnrollmentDate=DateTime.Parse("2021-12-12")}
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> Already have Students data...");
            }

            if(!context.Courses.Any())
            {
                Console.WriteLine("--> Seeding data Course...");
                context.Courses.AddRange(
                    new Course{Title="Cloud Fundamentals",Credits=3},
                    new Course{Title="Microservices Architecture",Credits=3},
                    new Course{Title="Frontend Programming",Credits=3},
                    new Course{Title="Backend RESTful API",Credits=3},
                    new Course{Title="Entity Frmework Core",Credits=3}
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> Already have Courses data...");
            }
            
            if(!context.Enrollments.Any())
            {
                Console.WriteLine("--> Seeding data Enrollment...");
                context.Enrollments.AddRange(
                    new Enrollment{StudentID=1,CourseID=1,Grade=Grade.A},
                    new Enrollment{StudentID=1,CourseID=2,Grade=Grade.B},
                    new Enrollment{StudentID=1,CourseID=3,Grade=Grade.C},
                    new Enrollment{StudentID=2,CourseID=1,Grade=Grade.C},
                    new Enrollment{StudentID=2,CourseID=2,Grade=Grade.C},
                    new Enrollment{StudentID=2,CourseID=3,Grade=Grade.C},
                    new Enrollment{StudentID=3,CourseID=1,Grade=Grade.A},
                    new Enrollment{StudentID=3,CourseID=2,Grade=Grade.B},
                    new Enrollment{StudentID=3,CourseID=3,Grade=Grade.C}
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> Already have Enrollments data...");
            }
        }
    }
}