using System.Collections.Generic;

namespace Practice_API_2.Classes
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string Teacher { get; set; }
        public bool isDeleted { get; set; }
        public List<StudentClass> StudentClasses { get; set; }
    }
}