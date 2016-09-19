using System;

namespace Framework.Models
{
    public class StudentModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModeifiedDate { get; set; }
    }
}
