using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Framework.Models;
using Newtonsoft.Json;

namespace Framework.Core.Domain
{
    [Table("Student")]
    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModeifiedDate { get; set; }

        public static implicit operator Student(StudentModel model)
        {
            return new Student()
            {
                FullName = model.FirstName + " " + model.LastName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                CreatedDate = model.CreatedDate,
                ModeifiedDate = model.ModeifiedDate
            };
        }
    }

    public class StudentMap : EntityTypeConfiguration<Student>
    {
        public StudentMap()
        {
            ToTable("Student");
            HasKey(x => x.Id);
            Property(x => x.FirstName).IsRequired().HasMaxLength(30);
            Property(x => x.LastName).IsRequired().HasMaxLength(30);
            Property(x => x.FullName).HasMaxLength(256);
            Property(x => x.DateOfBirth);
            Property(x => x.CreatedDate);
            Property(x => x.ModeifiedDate);
        }
    }
}
