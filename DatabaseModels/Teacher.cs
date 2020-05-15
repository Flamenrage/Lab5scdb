using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5.DatabaseModels
{
    [Table("teacher")]
    public class Teacher
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("fullname")]
        public string Name { get; set; }

        [Required]
        [Column("passport_data")]
        public int? PassportData { get; set; }

        public List<Timetable> Timetables { get; set; }
    }
}
