using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5.DatabaseModels
{
    [Table("classroom")]
    public class Classroom
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("classroomname")]
        public string Name { get; set; }

        public List<Timetable> Timetables { get; set; }
    }
}
