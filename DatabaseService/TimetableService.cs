using Lab5.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Lab5.DatabaseService
{
    public class TimetableService
    {

        private static Data_baseContext db = Program.DB;

        public static void Create(Timetable model)
        {
            if (model.Lesson == null)
            {
                throw new Exception("Column Lesson could not be null");
            }
            if (model.ExamDate == null)
            {
                throw new Exception("Column ExamDate could not be null");
            }           
            if (!model.TeacherId.HasValue)
            {
                throw new Exception("Column TeacherId could not be null");
            }
            if (!model.SpecialityId.HasValue)
            {
                throw new Exception("Column SpecialityId could not be null");
            }
            if (!model.DisciplineId.HasValue)
            {
                throw new Exception("Column DisciplineId could not be null");
            }
            if (!model.ClassroomId.HasValue)
            {
                throw new Exception("Column ClassroomId could not be null");
            }

            Timetable timetable =
                new Timetable()
                {
                    Lesson = model.Lesson,
                    ExamDate = model.ExamDate,
                    TeacherId = model.TeacherId,
                    SpecialityId = model.SpecialityId,
                    DisciplineId = model.DisciplineId,
                    ClassroomId = model.ClassroomId
                };

            db.Timetables.Add(timetable);
            db.SaveChanges();
        }

        public static List<Timetable> Read(Timetable model, int pageSize = Int32.MaxValue, int currentPage = 0)
        {
            List<Timetable> answer = null;

            if (model.TeacherId.HasValue && model.DisciplineId.HasValue && model.ClassroomId.HasValue && model.SpecialityId.HasValue)
            {
                answer =
                    db.Timetables
                    .Where(rec => rec.TeacherId == model.TeacherId && rec.DisciplineId == model.DisciplineId
                     && rec.ClassroomId == model.ClassroomId && rec.SpecialityId.HasValue == model.SpecialityId.HasValue)
                    .Include(rec => rec.Teacher)
                    .Include(rec => rec.Discipline)
                    .Include(rec => rec.Classroom)
                    .Include(rec => rec.Speciality)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                answer =
                    db.Timetables
                    .Where(rec => rec.Id == model.Id)
                    .Include(rec => rec.Teacher)
                    .Include(rec => rec.Discipline)
                    .Include(rec => rec.Classroom)
                    .Include(rec => rec.Speciality)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }

            return answer;
        }

        public static void Update(Timetable model)
        {
            Timetable Timetable = db.Timetables.FirstOrDefault(rec => rec.Id == model.Id);

            Timetable.Lesson = model.Lesson;
            Timetable.ExamDate = model.ExamDate;
            Timetable.TeacherId = model.TeacherId;
            Timetable.DisciplineId = model.DisciplineId;
            Timetable.ClassroomId = model.ClassroomId;
            Timetable.SpecialityId = model.SpecialityId;

            db.SaveChanges();
        }
        public static (int, int) ReadUsedTeachers()
        {
            var answer =
                db.Timetables
                .Include(rec => rec.Teacher)
                .GroupBy(rec => rec.Teacher.Id)
                .Select(m => new
                {
                    idp = m.Key,
                    count = m.Sum(rec => rec.Count)
                })
                .OrderByDescending(rec => rec.count)
                .First();

            return (answer.idp, answer.count);
        }

        public static void Delete(Timetable model)
        {
            Timetable Timetable = db.Timetables.FirstOrDefault(rec => rec.Id == model.Id);

            db.Timetables.Remove(Timetable);
            db.SaveChanges();
        }
    }
}
