using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5.DatabaseModels
{
    [Table("timetable")]
    public class Timetable
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("teacher_id")]
        public int? TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        [Required]
        [Column("classroom_id")]
        public int? ClassroomId { get; set; }

        public Classroom Classroom { get; set; }

        [Required]
        [Column("discipline_id")]
        public int? DisciplineId { get; set; }

        public Discipline Discipline { get; set; }

        [Required]
        [Column("speciality_id")]
        public int? SpecialityId { get; set; }

        public Speciality Speciality { get; set; }

        [Required]
        [Column("exam_date")]
        public DateTime? ExamDate { get; set; }

        [Required]
        [Column("lesson")]
        public int Lesson { get; set; }

    }
}
