using Lab5.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lab5.DatabaseService
{
    public class ClassroomService
    {
        private static Data_baseContext db = Program.DB;

        public static void Create(Classroom model)
        {
            if (model.Name == null)
            {
                throw new Exception("Column Name could not be null");
            }

            db.Classrooms.Add(
                new Classroom()
                {
                    Name = model.Name,
                });
            db.SaveChanges();
        }
        public static void Update(Classroom model)
        {
            Classroom teacher = db.Classrooms.FirstOrDefault(rec => rec.Name == model.Name);

            db.SaveChanges();
        }
        public static List<Classroom> Read(Classroom model, int pageSize = Int32.MaxValue, int currentPage = 0)
        {
            List<Classroom> answer = null;

            if (model.Name != null)
            {
                answer =
                db.Classrooms
                .Where(rec => rec.Name == model.Name)
                .Include(rec => rec.Timetables)
                .Skip(pageSize * currentPage)
                .Take(pageSize)
                .ToList();
            }
            else
            {
                answer =
                    db.Classrooms
                    .Include(rec => rec.Timetables)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }

            return answer;
        }

        public static void Delete(Classroom model)
        {
            Classroom classroom = db.Classrooms.FirstOrDefault(rec => rec.Name == model.Name);

            db.Classrooms.Remove(classroom);
            db.SaveChanges();
        }
    }
}
