using Business;
using Project;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Connect
    {
        private static String cliComConnectionString = GetConnectString();

        internal static String ConnectionString
        {
            get => cliComConnectionString;
        }

        private static String GetConnectString()
        {
            SqlConnectionStringBuilder cs = new SqlConnectionStringBuilder();
            cs.DataSource = "(local)";
            cs.InitialCatalog = "College1en";
            cs.UserID = "sa";
            cs.Password = "sysadm";
            return cs.ConnectionString;

        }
    }
    internal class DataTables
    {
        private static SqlDataAdapter adapterStudents = InitAdapterStudents();
        private static SqlDataAdapter adapterEnrollments = InitAdapterEnrollments();
        private static SqlDataAdapter adapterCourses = InitAdapterCourses();
        private static SqlDataAdapter adapterPrograms = InitAdapterPrograms();

        private static DataSet ds = InitDataSet();

        private static SqlDataAdapter InitAdapterPrograms()
        {
            SqlDataAdapter r = new SqlDataAdapter(
           "SELECT * FROM Programs ORDER BY ProgId",
               Connect.ConnectionString);

            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;
        }
        private static SqlDataAdapter InitAdapterCourses()
        {
            SqlDataAdapter r = new SqlDataAdapter(
           "SELECT * FROM Courses ORDER BY CId",
               Connect.ConnectionString);

            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;
        }
        private static SqlDataAdapter InitAdapterStudents()
        {
            SqlDataAdapter r = new SqlDataAdapter(
           "SELECT * FROM Students ORDER BY StId",
               Connect.ConnectionString);

            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;

        }
        private static SqlDataAdapter InitAdapterEnrollments()
        {
            SqlDataAdapter r = new SqlDataAdapter(
          "SELECT * FROM Enrollments ORDER BY StId, CId",
              Connect.ConnectionString);

            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;

        }
        private static DataSet InitDataSet()
        {
            DataSet ds = new DataSet();
            loadPrograms(ds);
            loadCourses(ds);
            loadStudents(ds);
            loadEnrollments(ds);
            createFKCourses(ds);
            createFKStudents(ds);
            createFKEnrollments(ds);
            return ds;
        }
        private static void loadPrograms(DataSet ds)
        {
            adapterPrograms.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapterPrograms.Fill(ds, "Programs");
        }
        private static void loadCourses(DataSet ds)
        {
            adapterCourses.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapterCourses.Fill(ds, "Courses");
        }

        private static void loadStudents(DataSet ds)
        {
            adapterStudents.MissingSchemaAction= MissingSchemaAction.AddWithKey;
            adapterStudents.Fill(ds, "Students");

        }
  
        private static void loadEnrollments(DataSet ds)
        {
            adapterEnrollments.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapterEnrollments.Fill(ds, "Enrollments");
        }
        private static void createFKCourses(DataSet ds)
        {
            ForeignKeyConstraint myFK01 = new ForeignKeyConstraint("MyFK01",
                new DataColumn[]{
                    ds.Tables["Programs"].Columns["ProgId"],
                },
                new DataColumn[] {
                    ds.Tables["Courses"].Columns["ProgId"],
                }
            );
            myFK01.DeleteRule = Rule.None;
            myFK01.UpdateRule = Rule.Cascade;
            ds.Tables["Courses"].Constraints.Add(myFK01);
        }
        private static void createFKStudents(DataSet ds)
        {
            ForeignKeyConstraint myFK01 = new ForeignKeyConstraint("MyFK01",
                new DataColumn[]{
                    ds.Tables["Programs"].Columns["ProgId"],
                },
                new DataColumn[] {
                    ds.Tables["Students"].Columns["ProgId"],
                }
            );
            myFK01.DeleteRule = Rule.None;
            myFK01.UpdateRule = Rule.Cascade;
            ds.Tables["Students"].Constraints.Add(myFK01);
        }
        private static void createFKEnrollments(DataSet ds)
        {

            ForeignKeyConstraint myFK01 = new ForeignKeyConstraint("MyFK01",
                new DataColumn[]{
                      ds.Tables["Students"].Columns["StId"]
                },
                new DataColumn[] {
                      ds.Tables["Enrollments"].Columns["StId"]
                }
            );
            myFK01.DeleteRule = Rule.Cascade;
            myFK01.UpdateRule = Rule.Cascade;
            ds.Tables["Enrollments"].Constraints.Add(myFK01);

            ForeignKeyConstraint myFK02 = new ForeignKeyConstraint("MyFK02",
              new DataColumn[]{
                      ds.Tables["Courses"].Columns["CId"]
              },
              new DataColumn[] {
                      ds.Tables["Enrollments"].Columns["CId"]
              }
          );
            myFK02.DeleteRule = Rule.None;
            myFK02.UpdateRule = Rule.Cascade;
            ds.Tables["Enrollments"].Constraints.Add(myFK02);
        }
        internal static SqlDataAdapter getAdapterPrograms()
        {
            return adapterPrograms;
        }
        internal static SqlDataAdapter getAdapterCourses()
        {
            return adapterCourses;
        }
        internal static SqlDataAdapter getAdapterStudents()
        {
            return adapterStudents;
        }
        internal static SqlDataAdapter getAdapterEnrollments()
        {
            return adapterEnrollments;
        }
        internal static DataSet getDataSet()
        {
            return ds;
        }
    }
    internal class Programs
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterPrograms();
        private static DataSet ds = DataTables.getDataSet();

        internal static DataTable GetPrograms()
        {
            return ds.Tables["Programs"];
        }

        internal static int UpdatePrograms()
        {
            if (!ds.Tables["Programs"].HasErrors)
            {
                return adapter.Update(ds.Tables["Programs"]);
            }
            else
            {
                return -1;
            }
        }
    }

    internal class Courses
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterCourses();
        private static DataSet ds = DataTables.getDataSet();


        internal static DataTable GetCourses()
        {
            return ds.Tables["Courses"];
        }

        internal static int UpdateCourses()
        {
            if (!ds.Tables["Courses"].HasErrors)
            {
                return adapter.Update(ds.Tables["Courses"]);
            }
            else
            {
                return -1;
            }
        }
    }
   internal class Students
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterStudents();
        private static DataSet ds = DataTables.getDataSet();

        internal static DataTable GetStudents()
        {
            return ds.Tables["Students"];
        }

        internal static int UpdateStudents()
        {
            if (!ds.Tables["Students"].HasErrors)
            {
                return adapter.Update(ds.Tables["Students"]);
            }
            else
            {
                return -1;
            }
        }
    }

    internal class Enrollments
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterEnrollments();
        private static DataSet ds = DataTables.getDataSet();

        private static DataTable displayEnrollment = null;


        internal static DataTable GetDisplayEnrollments()
        {

            ds.Tables["Enrollments"].AcceptChanges();

            var query = (
                   from enroll in ds.Tables["Enrollments"].AsEnumerable()
                   from student in ds.Tables["Students"].AsEnumerable()
                   from course in ds.Tables["Courses"].AsEnumerable()
                   from programs in ds.Tables["Programs"].AsEnumerable()
                   where enroll.Field<string>("StId") == student.Field<string>("StId")
                   where enroll.Field<string>("CId") == course.Field<string>("CId")
                   where programs.Field<string>("ProgId") == student.Field<string>("ProgId")
                   select new
                   {
                       StId = student.Field<string>("StId"),
                       StName = student.Field<string>("StName"),
                       CId = course.Field<string>("CId"),
                       CName = course.Field<string>("CName"),
                       FinalGrade = enroll.Field<Nullable<int>>("FinalGrade"),
                       ProgId = programs.Field<string>("ProgId"),
                       ProgName = programs.Field<string>("ProgName")
                   });

            DataTable result = new DataTable();
            result.Columns.Add("StId");
            result.Columns.Add("StName");
            result.Columns.Add("CId");
            result.Columns.Add("CName");
            result.Columns.Add("FinalGrade");
            result.Columns.Add("ProgId");
            result.Columns.Add("ProgName");


            foreach (var x in query)
            {
                object[] allFields = { x.StId, x.StName, x.CId, x.CName, x.FinalGrade, x.ProgId, x.ProgName };
                result.Rows.Add(allFields);
            }
            displayEnrollment = result;
            return displayEnrollment;
        }
        internal static int InsertData(string[] a)
        {
            //verify if student already in course
            //a: enrollPK
            var test = (
                
                   from enroll in ds.Tables["Enrollments"].AsEnumerable()
                   where enroll.Field<string>("StId") == a[0]
                   where enroll.Field<string>("CId") == a[1]
                   select enroll);
            if (test.Count() > 0)
            {
                Project.Form1.DALMessage("This enrollment already exists");
                return -1;
            }


            //check student having course belongs to his program -> insert
            var test2 = (
                    from student in ds.Tables["Students"].AsEnumerable()
                    where student.Field<string>("StId") == a[0]
                    select new
                    {
                        ProgId = student.Field<string>("ProgId")
                    });

            var test3 = (
                    from course in ds.Tables["Courses"].AsEnumerable()
                    where course.Field<string>("CId") == a[0] //1
                    select new
                    {
                        ProgId = course.Field<string>("ProgId")
                    });

            foreach(var x in test2)
            {
                foreach(var y in test3)
                {
                    if(x.ProgId != y.ProgId)
                    {
                        Form1.BLLMessage("The student can not be inserted in a course that is not belongs to his/her program");
                        return -1;
                    }
                }
            }

            try
            {
                DataRow line = ds.Tables["Enrollments"].NewRow();
                line.SetField("StId", a[0]);
                line.SetField("CId", a[1]);
                ds.Tables["Enrollments"].Rows.Add(line);

                adapter.Update(ds.Tables["Enrollments"]);

                if (displayEnrollment != null)
                {
                    var query = (
                           from emp in ds.Tables["Students"].AsEnumerable()
                           from proj in ds.Tables["Courses"].AsEnumerable()
                           from programs in ds.Tables["Programs"].AsEnumerable()
                           where emp.Field<string>("StId") == a[0]
                           where proj.Field<string>("CId") == a[1]
                           where programs.Field<string>("ProgId") == a[2]
                           select new
                           {
                               StId = emp.Field<string>("StId"),
                               StName = emp.Field<string>("StName"),
                               CId = proj.Field<string>("CId"),
                               CName = proj.Field<string>("CName"),
                               FinalGrade = line.Field<Nullable<int>>("FinalGrade"),
                               ProgId = programs.Field<string>("ProgId"),
                               ProgName = programs.Field<string>("ProgName")
                           });

                    var r = query.Single();
                    displayEnrollment.Rows.Add(new object[] { r.StId, r.StName, r.CId, r.CName, r.FinalGrade, r.ProgId, r.ProgName });
                }
                return 0;
            }
            catch (Exception)
            {
                Project.Form1.DALMessage("Insertion / Update rejected");
                return -1;
            }
        }
    
        internal static int DeleteData(List<string[]> lId)
        {
            try
            {
                var lines = ds.Tables["Enrollments"].AsEnumerable()
                                .Where(s =>
                                   lId.Any(x => (x[0] == s.Field<string>("StId") && x[1] == s.Field<string>("CId"))));

                
                foreach (var line in lines)
                {
                    line.Delete();
                }

                adapter.Update(ds.Tables["Enrollments"]);

                if (displayEnrollment != null)
                {
                    foreach (var p in lId)
                    {
                        var r = displayEnrollment.AsEnumerable()
                                .Where(s => (s.Field<string>("StId") == p[0] && s.Field<string>("CId") == p[1]))
                                .Single();
                        displayEnrollment.Rows.Remove(r);
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                Project.Form1.DALMessage("Update / Deletion rejected");
                return -1;
            }
        }
        internal static int UpdateFinalGrade(string[] a, Nullable<int> finalgrade)
        {
            try
            {
                var line = ds.Tables["Enrollments"].AsEnumerable()
                                    .Where(s =>
                                      (s.Field<string>("StId") == a[0] && s.Field<string>("CId") == a[1]))
                                    .Single();

                line.SetField("FinalGrade", finalgrade);

                adapter.Update(ds.Tables["Enrollments"]);

                if (displayEnrollment != null)
                {
                    var r = displayEnrollment.AsEnumerable()
                                    .Where(s =>
                                      (s.Field<string>("StId") == a[0] && s.Field<string>("CId") == a[1]))
                                    .Single();
                    r.SetField("FinalGrade", finalgrade); 
                }
                return 0;
            }
            catch (Exception)
            {
                Project.Form1.DALMessage("Update / Deletion rejected");
                return -1;
            }
        }
    }
}
