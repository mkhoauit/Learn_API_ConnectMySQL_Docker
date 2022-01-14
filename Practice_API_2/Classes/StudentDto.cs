using System;

namespace Practice_API_2.Classes
{
    public class StudentDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool isDeleted { get; set; }
    }
}