using Lab5.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Lab5.DatabaseService
{
    public class DisciplineService
    {
        private static Data_baseContext db = Program.DB;

        public static void Create(Discipline model)
        {
            if (model.Name == null)
            {
                throw new Exception("Column Name could not be null");
            }

            db.Disciplines.Add(
                new Discipline()
                {
                    Name = model.Name,
                });
            db.SaveChanges();
        }
        public static void Update(Discipline model)
        {
            Discipline mod = db.Disciplines.FirstOrDefault(rec => rec.Name == model.Name);

            db.SaveChanges();
        }
        public static List<Discipline> Read(Discipline model, int pageSize = Int32.MaxValue, int currentPage = 0)
        {
            List<Discipline> answer = null;

            if (model.Name != null)
            {
                answer =
                db.Disciplines
                .Where(rec => rec.Name == model.Name)
                .Include(rec => rec.Timetables)
                .Skip(pageSize * currentPage)
                .Take(pageSize)
                .ToList();
            }
            else
            {
                answer =
                    db.Disciplines
                    .Include(rec => rec.Timetables)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }

            return answer;
        }

        public static void Delete(Discipline model)
        {
            Discipline discipline = db.Disciplines.FirstOrDefault(rec => rec.Name == model.Name);

            db.Disciplines.Remove(discipline);
            db.SaveChanges();
        }
    }
}
