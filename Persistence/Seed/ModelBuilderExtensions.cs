using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Seed
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Seed admin
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = 1,
                    FirstName = "Danny",
                    LastName = "Brown",
                    EmailAddress = "admin@example.com",
                    Password = "admin123",
                    DateOfBirth = new DateTime(1990, 1, 1)
                }
            );

            // Seed doctors
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    EmailAddress = "john.doe@example.com",
                    Password = "doctor123",
                    DateOfBirth = new DateTime(1985, 5, 10),
                    Specialization = Specialization.Pediatrician
                },
                new Doctor
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    EmailAddress = "jane.smith@example.com",
                    Password = "doctor456",
                    DateOfBirth = new DateTime(1992, 8, 20),
                    Specialization = Specialization.Psychiatrist
                }
            );

            // Seed patients
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = 1,
                    FirstName = "Mike",
                    LastName = "Johnson",
                    MedicalHistory = "Some medical history",
                    EmailAddress = "mike.johnson@example.com",
                    Password = "patient123",
                    DateOfBirth = new DateTime(1995, 4, 15)
                },
                new Patient
                {
                    Id = 2,
                    FirstName = "Emily",
                    LastName = "Wilson",
                    MedicalHistory = "Some medical history",
                    EmailAddress = "emily.wilson@example.com",
                    Password = "patient456",
                    DateOfBirth = new DateTime(1988, 12, 5)
                },
                new Patient
                {
                    Id = 3,
                    FirstName = "Sarah",
                    LastName = "Brown",
                    MedicalHistory = "Some medical history",
                    EmailAddress = "sarah.brown@example.com",
                    Password = "patient789",
                    DateOfBirth = new DateTime(1990, 9, 25)
                },
                new Patient
                {
                    Id = 4,
                    FirstName = "David",
                    LastName = "Miller",
                    MedicalHistory = "Some medical history",
                    EmailAddress = "david.miller@example.com",
                    Password = "patientabc",
                    DateOfBirth = new DateTime(1982, 3, 12)
                }
            );

            // Seed appointment
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = 1,
                    Date = DateTime.Now.AddDays(1),
                    Description = "Appointment 1 - doc 2, pat 4",
                    PatientId = 4,
                    DoctorId = 2
                },
                new Appointment
                {
                    Id = 2,
                    Date = DateTime.Now.AddDays(38),
                    Description = "Appointment 2 - doc 2, pat 3",
                    PatientId = 3,
                    DoctorId = 2
                },
                new Appointment
                {
                    Id = 3,
                    Date = DateTime.Now.AddDays(42),
                    Description = "Appointment 3 - doc 2, pat 4",
                    PatientId = 4,
                    DoctorId = 2
                },
                new Appointment
                {
                    Id = 4,
                    Date = DateTime.Now.AddDays(27),
                    Description = "Appointment 4 - doc 1, pat 1",
                    PatientId = 1,
                    DoctorId = 1
                }
            );

            // Seed prescription
            modelBuilder.Entity<Prescription>().HasData(
                new Prescription
                {
                    Id = 1,
                    CreatedAt = DateTime.Now,
                    Medicines = "Medicine 1",
                    Description = "Prescription 1 - doc 2, pat 4",
                    PatientId = 4,
                    DoctorId = 2
                },
                new Prescription
                {
                    Id = 2,
                    CreatedAt = DateTime.Now.AddDays(-22),
                    Medicines = "Medicine 2",
                    Description = "Prescription 2 - doc 1, pat 1",
                    PatientId = 1,
                    DoctorId = 1
                }, new Prescription
                {
                    Id = 3,
                    CreatedAt = DateTime.Now.AddDays(-13),
                    Medicines = "Medicine 3",
                    Description = "Prescription 1 - doc 2, pat 3",
                    PatientId = 4,
                    DoctorId = 2
                }
            );
            modelBuilder.Entity<Notice>().HasData(
               new Notice
               {
                   Id = 1,
                   Content = "First note.",
               }
            );
        }
    }
}
