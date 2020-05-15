using Lab5.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lab5.DatabaseService
{
    public class SpecialityService
    {
        private static Data_baseContext db = Program.DB;

        public static void Create(Speciality model)
        {
            if (model.Name == null)
            {
                throw new Exception("Column Name could not be null");
            }

            db.Specialities.Add(
                new Speciality()
                {
                    Name = model.Name,
                });
            db.SaveChanges();
        }

        public static List<Speciality> Read(Speciality model, int pageSize = Int32.MaxValue, int currentPage = 0)
        {
            List<Speciality> answer = null;

            if (model.Name != null)
            {
                    answer =
                    db.Specialities
                    .Where(rec => rec.Name == model.Name)
                    .Include(rec => rec.Timetables)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                answer =
                    db.Specialities
                    .Include(rec => rec.Timetables)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }

            return answer;
        }
        public static void Update(Speciality model)
        {
            Speciality mod = db.Specialities.FirstOrDefault(rec => rec.Name == model.Name);

            db.SaveChanges();
        }


        public static void Delete(Speciality model)
        {
            Speciality speciality = db.Specialities.FirstOrDefault(rec => rec.Name == model.Name);

            db.Specialities.Remove(speciality);
            db.SaveChanges();
        }
    }
}
