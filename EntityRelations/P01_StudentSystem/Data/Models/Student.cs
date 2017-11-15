using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace P01_StudentSystem.Data.Models
{
    public class Student
    {
        public Student()
        {
            this.StudentCourses = new List<StudentCourse>();
            this.HomeworkSubmissions = new List<HomeworkSubmission>();
        }

        public int StudentId { get; set; }
        public string Name { get; set; }

        [MaxLength(10), MinLength(10)]
        public string PhoneNumber { get; set; }

        public DateTime RegisteredOn { get; set; }
        public DateTime Birthday { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; }
        public ICollection<HomeworkSubmission> HomeworkSubmissions { get; set; }
    }
}