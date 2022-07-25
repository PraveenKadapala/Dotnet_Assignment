using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Courses
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CourseName { get; set; }

        [Required]
        public int CoursePrice { get; set; }

        [Required]
        public string CourseDuration { get; set; }
        [Required]

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [ForeignKey("Teachers")]
        public int TeacherId { get; set; }
    }
}
