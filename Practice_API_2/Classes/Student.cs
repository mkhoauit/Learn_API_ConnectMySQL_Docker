using System;
using System.Collections.Generic;

namespace Practice_API_2.Classes
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool isDeleted { get; set; }
        public List<StudentClass> StudentClasses { get; set; }
    }

   
}