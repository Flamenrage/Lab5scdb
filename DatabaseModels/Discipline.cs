using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5.DatabaseModels
{
    [Table("discipline")]
    public class Discipline
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("disciplinename")]
        public string Name { get; set; }

        public List<Timetable> Timetables { get; set; }
    }
}
