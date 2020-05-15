using Lab5.DatabaseModels;
using Lab5.DatabaseService;
using System;
using System.Linq;

namespace Lab5
{
    class Program
    {
        public static readonly Data_baseContext DB = new Data_baseContext("localhost", "5432", "postgres", "exam");

        static void Main(string[] args)
        {
            int[] common = new int[15];
            int[,] general = new int[15, 15];

                int[] tries = run();
                for (int j = 0; j < 15; j++)
                {
                    common[j]= tries[j];
                }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("ScriptInsert0: " + general[10, 0]);
            Console.WriteLine("ScriptInsert1: " + general[10, 1]);
            Console.WriteLine("ScriptInsert2: " + general[10, 2]);
            Console.WriteLine("ScriptInsert3: " + general[10, 3]);
            Console.WriteLine("ScriptRead0: " + general[10, 4]);
            Console.WriteLine("ScriptRead1: " + general[10, 5]);
            Console.WriteLine("ScriptRead2: " + general[10, 6]);
            Console.WriteLine("ScriptUpdate0: " + general[10, 7]);
            Console.WriteLine("ScriptUpdate1: " + general[10, 8]);
            Console.WriteLine("ScriptUpdate2: " + general[10, 9]);
            Console.WriteLine("ScriptCustom0: " + general[10, 10]);
            Console.WriteLine("ScriptCustom1: " + general[10, 11]);
            Console.WriteLine("ScriptCustom2: " + general[10, 12]);
            Console.WriteLine("ScriptDelete0: " + general[10, 13]);
            Console.WriteLine("ScriptDelete1: " + general[10, 14]);
            Console.WriteLine("ScriptDelete2: " + general[10, 15]);
        }

        static int[] run()
        {
            int[] times = new int[15];

            times[0] = AddScript0();
            times[1] = AddScript1();
            times[2] = AddScript2();
            times[3] = AddScript3();
            times[4] = ReadScript0();
            times[5] = ReadScript1();
            times[6] = ReadScript2();
            times[7] = UpdateScript0();
            times[8] = UpdateScript1();
            times[8] = UpdateScript2();
            times[10] = SpecialScript0();
            times[11] = SpecialScript1();
            times[12] = SpecialScript2();
            times[13] = DeleteScript0();
            times[14] = DeleteScript1();
            times[15] = DeleteScript2();

            return times;
        }
        static int AddScript0()
        {
            Teacher model = new Teacher() { Name = "Random Teacher", PassportData = 456278 };

            DateTime startTime = DateTime.Now;
            TeacherService.Create(model);
            DateTime finishTime = DateTime.Now;
            return (int)(finishTime - startTime).TotalMilliseconds;
        }
        static int AddScript1()
        {
            Speciality model = new Speciality() { Name = "Random Speciality" };

            DateTime startTime = DateTime.Now;
            SpecialityService.Create(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }
        static int AddScript2()
        {
            var teacher = TeacherService.Read(new Teacher() { Name = "Random Teacher", PassportData = 456278}, 1, 0).First();
            var speciality = SpecialityService.Read(new Speciality() { Name = "Random Speciality" }, 1, 0).First();
            Timetable[] models = new Timetable[30];
            for (int i = 0; i < models.Length; i++)
            {
                models[i] =
                    new Timetable()
                    {
                        Lesson = 5,
                        ExamDate = DateTime.Now.AddDays(i),
                        TeacherId = teacher.Id,
                        SpecialityId = speciality.Id,
                        ClassroomId = 5,
                        DisciplineId = 5
                    };
            }
            DateTime startTime = DateTime.Now;
            foreach (var model in models)
            {
                TimetableService.Create(model);
            }
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }
        static int AddScript3()
        {
            Discipline model = new Discipline() { Name = "Random Discipline" };

            DateTime startTime = DateTime.Now;
            DisciplineService.Create(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }
        static int ReadScript0()
        {
            Speciality model = new Speciality() { Name = "Random Speciality" };

            DateTime startTime = DateTime.Now;
            Speciality mod = SpecialityService.Read(model, 1, 0).First();
            DateTime finishTime = DateTime.Now;

            Console.WriteLine("{0}: {1}", mod.Id, mod.Name);

            return (int)(finishTime - startTime).TotalMilliseconds;
        }
        static int ReadScript1()
        {
            Speciality speciality = SpecialityService.Read(new Speciality() { Name = "Random Speciality" }, 1, 0).First();

            // Предполагается, что действия до создания модели - это моделирование выбора пользователя

            Timetable time = new Timetable() { SpecialityId = speciality.Id };

            DateTime startTime = DateTime.Now;
            Timetable model = TimetableService.Read(time, 1, 0).First();
            var count = DB.Timetables.Count(t => model.SpecialityId == speciality.Id);
            DateTime finishTime = DateTime.Now;

            Console.WriteLine("{0}: {1}", model.SpecialityId, count);

            return (int)(finishTime - startTime).TotalMilliseconds;

        }
        static int ReadScript2()
        {
            var teacher = TeacherService.Read(new Teacher() { Name = "Random Teacher", PassportData = 456278 }, 1, 0).First();
            // Предполагается, что действия до создания модели - это моделирование выбора пользователя
            Timetable model = new Timetable() { TeacherId = teacher.Id};

            DateTime startTime = DateTime.Now;
            var models = TimetableService.Read(model);
            DateTime finishTime = DateTime.Now;

            foreach (var timetable in models)
            {
                Console.WriteLine("{0} {1}", timetable.ExamDate.Value.ToString("dd.MM.yyyy"), timetable.Lesson);
            }

            return (int)(finishTime - startTime).TotalMilliseconds;
        }
        static int UpdateScript0()
        {
           
            Teacher teacher = TeacherService.Read(new Teacher() { Name = "Random Teacher", PassportData = 456278 }, 1, 0).First();
            Teacher mod = new Teacher() { Name = teacher.Name, PassportData = 555190 };
            DateTime startTime = DateTime.Now;
            TeacherService.Update(mod);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }
        static int UpdateScript1()
        {
            Speciality speciality = SpecialityService.Read(new Speciality() { Name = "Random Speciality" }, 1, 0).First();
            Timetable mod = new Timetable() { Id = speciality.Id, Lesson = 6};

            DateTime startTime = DateTime.Now;
            TimetableService.Update(mod);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }
        static int UpdateScript2()
        {
            var teacher = TeacherService.Read(new Teacher() { Name = "Random Teacher", PassportData = 555190 }, 1, 0).First();
            var speciality = SpecialityService.Read(new Speciality() { Name = "Random Speciality" }, 1, 0).First();

            // Предполагается, что действия до создания модели - это моделирование выбора пользователя

            var models = TimetableService.Read(new Timetable() { TeacherId = teacher.Id, SpecialityId = speciality.Id});

            DateTime startTime = DateTime.Now;
            foreach (var model in models)
            {
                model.ClassroomId = 1;
                TimetableService.Update(model);
            }
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }
        static int DeleteScript0()
        {

            Teacher model = new Teacher() { Name = "Random Teacher", PassportData = 555190 };

            DateTime startTime = DateTime.Now;
            TeacherService.Delete(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }
        static int DeleteScript1()
        {
            Discipline model = new Discipline() { Name = "Random Discipline" };

            DateTime startTime = DateTime.Now;
            DisciplineService.Delete(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }
        static int DeleteScript2()
        {
            var speciality = SpecialityService.Read(new Speciality() { Name = "Random Speciality" }, 1, 0).First();

            // Предполагается, что действия до создания модели - это моделирование выбора пользователя

            var models = TimetableService.Read(new Timetable() { SpecialityId = speciality.Id });

            DateTime startTime = DateTime.Now;
            foreach (var model in models)
            {
                TimetableService.Delete(model);
            }
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }
        static int SpecialScript0()
        {

            var teacher = TeacherService.Read(new Teacher() { Name = "Random Teacher", PassportData = 555190 }, 1, 0).First();
            var speciality = SpecialityService.Read(new Speciality() { Name = "Random Speciality" }, 1, 0).First();
            DateTime startTime = DateTime.Now;
            Timetable model = new Timetable() { TeacherId = teacher.Id, SpecialityId = speciality.Id };

            var models = TimetableService.Read(model);

            foreach (var timetable in models)
            {
                Console.WriteLine("{0}: {1} {2} {3} {4}", timetable.Id, timetable.ExamDate.Value.ToString("dd.MM.yyyy"), timetable.Lesson, timetable.TeacherId, timetable.SpecialityId);
            }
            DateTime finishTime = DateTime.Now;
            return (int)(finishTime - startTime).TotalMilliseconds;
        }
        static int SpecialScript1()
        {
            DateTime date = new DateTime(2020, 5, 14).Date;
            DateTime startTime = DateTime.Now;
            var timetables = TimetableService.Read(new Timetable() { ExamDate = date});
            foreach (var time in timetables)
            {
                Console.WriteLine("{0}: {1} - {2} ", time.Id, time.ExamDate.Value.ToString("dd.MM.yyyy"), time.TeacherId);
            }
            DateTime finishTime = DateTime.Now;
            return (int)(finishTime - startTime).TotalMilliseconds;
        }
        static int SpecialScript2()
        {
            DateTime startTime = DateTime.Now;
            var teachers = TimetableService.ReadUsedTeachers();
            Console.WriteLine("{0}: {1}", teachers.Item1, teachers.Item2);
            DateTime finishTime = DateTime.Now;
            return (int)(finishTime - startTime).TotalMilliseconds;
        }
    }
}
