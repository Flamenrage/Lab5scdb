using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5.DatabaseModels
{
    [Table("speciality")]
    public class Speciality
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("specialityname")]
        public string Name { get; set; }

        public List<Timetable> Timetables { get; set; }
    }
}
