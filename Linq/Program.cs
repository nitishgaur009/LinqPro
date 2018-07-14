using Linq.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            //var s = GetAllStudents();
            IList<int> intList1 = new List<int>() {
                                            10,
                                            8,
                                            25,
                                            15
                                        };

            IList<int> intList2 = new List<int>() {
                                            70,
                                            40,
                                            5,
                                            67
                                        };
            var result = intList1.Concat(intList2);

            var r9 = intList1.Aggregate<int, string>("int list : ", (str, int1) => str += int1.ToString() + ",");


            var result2 = intList1.Union(intList2).OrderBy(s1 => s1);

            //var result = strList1.Join(strList2, s1 => s1, s2 => s2, (s1, s2) => s1);
            int i;

            StudentDBEntities context = new StudentDBEntities();
            //var res = context.Students.Join(context.Teachers,
            //                                s => s.StandardID,
            //                                t => t.StandardId,
            //                                (s, t) => new
            //                                {
            //                                    s.StandardID,
            //                                    t.PersonId,
            //                                    t.Name,
            //                                    s.StudentName,
            //                                    t.Subject
            //                                });


            //var r2 = from s in context.Students
            //         join t in context.Teachers
            //         on s.StandardID equals t.StandardId
            //         into teacherGroup
            //         select new
            //         {
            //             teachers = teacherGroup,
            //             s.StandardID
            //         };

           // var r4 = r2.ToList();

            //var studentsLocal = context.Students.Select(s => 
            //                                            new LocalStudents()
            //                                            {
            //                                                StudentID = s.StudentID,
            //                                                StudentName = s.StudentName
            //                                            }).ToList();

            //var data = res.ToList();

            //var studentSum = context.Students.ToList().Sum(s =>
            //{
            //    if (s.Address.ToUpper() == "JIND")
            //        return 1;
            //    else
            //        return 0;
            //});

        //    var maxStudent = context.Students.ToList().Max().StudentName;


            //var resultFin = context.Students.Join(context.Teachers,
            //                                    s => s.StandardID,
            //                                    t => t.StandardId,
            //                                    (s, t) => new
            //                                    {
            //                                        s.StudentID,
            //                                        s.StudentName,
            //                                        s.StandardID,
            //                                        t.Name
            //                                    }).ToList();

            //var resultLeft = (from s in context.Students
            //                 join t in context.Teachers
            //                 on s.StandardID equals t.StandardId
            //                 into leftJoinGroup
            //                 from t in leftJoinGroup.DefaultIfEmpty()
            //                 select new
            //                 {
            //                     s.StudentID,
            //                     s.StudentName,
            //                     s.StandardID,
            //                     TName = t.Name != null ? t.Name : null 
            //                 });

            // var leftQuery = context.Students.Join(context.Teachers, s=> s.StandardID,
            // t=> t.StandardId)

            //  var rh = resultLeft.ToList();

            var twoInner1 = context.Students.Join(context.Addresses,
                                                     s => s.StudentID,
                                                     a => a.StudentID,
                                                     (s, a) => new
                                                     {
                                                         s.StudentID,
                                                         s.StudentName,
                                                         a.PermanentAddress
                                                 }).ToList();

            var twoInner2 = (from s in context.Students
                            join a in context.Addresses
                            on s.StudentID equals a.StudentID
                            select new
                            {
                                s.StudentID,
                                s.StudentName,
                                a.PermanentAddress
                            }).ToList();

            var threeInner1 = context.Students.Join(context.Addresses,
                                                    s => s.StudentID,
                                                    a => a.StudentID,
                                                    (s, a) => new
                                                    {
                                                        s.StudentID,
                                                        s.StudentName,
                                                        a.PermanentAddress
                                                    })
                                                    .Join(context.Marks.Where(m => m.StandardID == 10),
                                                        join1 => join1.StudentID,
                                                        m => m.StudentID,
                                                        (join1, m) => new
                                                        {
                                                            join1.StudentID,
                                                            join1.StudentName,
                                                            join1.PermanentAddress,
                                                            m.MarksObtained,
                                                            m.TotalMarks
                                                        });

            var threeInner2 = (from s in context.Students
                              join a in context.Addresses
                              on s.StudentID equals a.StudentID
                              join m in context.Marks.Where(m=> m.StandardID == 10)
                              on s.StudentID equals m.StudentID
                              into marksLeft
                              from mleft in marksLeft.DefaultIfEmpty()
                              select new
                              {
                                  s.StudentID,
                                  s.StudentName,
                                  a.PermanentAddress,
                                  MarksObtained = mleft.MarksObtained != null ? mleft.MarksObtained : null,
                                  TotalMarks = mleft.TotalMarks != null ? mleft.TotalMarks : null
                              });


            var twoLeft1 = from s in context.Students
                           join m in context.Marks
                           on s.StudentID equals m.StudentID
                           into marksLeft
                           from m in marksLeft.DefaultIfEmpty()
                           select new
                           {
                               s.StudentID,
                               s.StudentName,
                               MarksObtained = m.MarksObtained != null ? m.MarksObtained : null,
                               TotalMarks = m.TotalMarks != null ? m.TotalMarks : null
                           };


            List<Student> lstStd = new List<Student>();
            lstStd.Add(new Student()
            {
                StudentID = 1,
                StudentName = "a"                
            });
            lstStd.Add(new Student()
            {
                StudentID = 2,
                StudentName = "b"
            });
            lstStd.Add(new Student()
            {
                StudentID = 3,
                StudentName = "c"
            });

            List<Mark> marks = new List<Mark>();
            marks.Add(new Mark()
            {
                MarksID = 1,
                StudentID = 1,
                StandardID = 10,
                MarksObtained = 309,
                TotalMarks = 400
            });

            var leftLocal = (from s in lstStd
                             join m in marks
                             on s.StudentID equals m.StudentID
                             into marksF
                             from m in marksF.DefaultIfEmpty()
                             select new
                             {
                                 s.StudentID,
                                 Marks = m != null && m.MarksObtained != null ? m.MarksObtained : null
                            }).ToList();

            var query = lstStd.Where(s => s.StudentName == "a");
            var querydb = context.Students.Where(s => s.StudentName.ToLower().Contains("nitish"));
            querydb.ToList();
            query.ToList();
            Console.ReadLine();
        }

        public static List<Student> GetAllStudents()
        {
            //StudentDBEntities context = new StudentDBEntities();
            //var std = from s in context.Students
            //          where s.StudentName.Contains("a") && s.Address != null
            //          orderby s.StudentName descending
            //          orderby s.DOB
            //          select new
            //          {
            //              s.StudentID,
            //              s.FatherName
            //          };


            //var s2 = std.ToList();
            //var students = context.Students.Where(s => s.StudentName.Contains("a")
            //                                        && s.Address != null)
            //                                .OrderBy(s => s.StudentName).ThenBy(s => s.DOB)
            //                                .Select(s => new
            //                                {
            //                                    s.StudentID,
            //                                    s.FatherName
            //                                }).ToList();

            //var avg = context.Students.Average(s => s.StandardID);

            //var studentGroup1 = context.Students.ToLookup(s => s.StandardID).ToList();
            //var studentGroup = from s in context.Students
            //                   group s by s.StandardID;


            //foreach (var item in studentGroup1)
            //{
            //    Console.WriteLine("Students in standard : " + item.Key);

            //    foreach (Student student in item)
            //    {
            //        Console.WriteLine("Student Name : " + student.StudentName);
            //    }

            //    Console.WriteLine("\n");
            //}

            return null;
        }
    }

    class LocalStudents
    {
        public int StudentID { get; set; }

        public string StudentName { get; set; }
    }
}