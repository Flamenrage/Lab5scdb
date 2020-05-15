using Lab5.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lab5.DatabaseService
{
    public class TeacherService
    {
        private static Data_baseContext db = Program.DB;

        public static void Create(Teacher model)
        {
            if (model.Name == null)
            {
                throw new Exception("Column Name could not be null");
            }
            if (!model.PassportData.HasValue)
            {
                throw new Exception("Column passposrt_data could not be null");
            }

            db.Teachers.Add(
                new Teacher()
                {
                    Name = model.Name,
                    PassportData = model.PassportData
                });
            db.SaveChanges();
        }

        public static List<Teacher> Read(Teacher model, int pageSize = Int32.MaxValue, int currentPage = 0)
        {
            List<Teacher> answer = null;

            if (model.Name != null)
            {
                    answer =
                    db.Teachers
                    .Where(rec => rec.Name == model.Name)
                    .Include(rec => rec.Timetables)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                answer =
                    db.Teachers
                    .Include(rec => rec.Timetables)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }

            return answer;
        }

        public static void Update(Teacher model)
        {
            Teacher teacher = db.Teachers.FirstOrDefault(rec => rec.Name == model.Name);

            teacher.PassportData = model.PassportData;

            db.SaveChanges();
        }

        public static void Delete(Teacher model)
        {
            Teacher teacher = db.Teachers.FirstOrDefault(rec => rec.Name == model.Name);

            db.Teachers.Remove(teacher);
            db.SaveChanges();
        }
    }
}
