﻿using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Branch { get; set; }

        

    }
}
