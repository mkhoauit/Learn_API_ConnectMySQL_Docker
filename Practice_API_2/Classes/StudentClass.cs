using System;

namespace Practice_API_2.Classes
{
    public class StudentClass
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public bool isDeleted { get; set; }
        
        public Student Student { get; set; }
        public Subject Subject { get; set; }
    }
}