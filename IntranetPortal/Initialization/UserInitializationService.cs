using IntranetPortal.Controllers;
using IntranetPortal.Initialization;
using IntranetPortal.Models;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq;

namespace IntranetPortal.Services
{
    public class UserInitializationService
    {
        private readonly IntranetDBContext _context;

        public UserInitializationService(IntranetDBContext context)
        {
            _context = context;
        }

        public void OnApplicationStarted()
        {
            // Carry out your initialisation.
            Console.WriteLine("test01");
            //List<User> users = LoadJson();
            //try
            //{
            //    _context.Users.AddRangeAsync(users);
            //    _context.SaveChangesAsync();
            //} catch(Exception e)
            //{
            //   Console.WriteLine( e.Message );
            //}
            LoadJson();
        }

        private string LoadDepartments(string name)
        {
            string code = GetDepCode(name);

            Department department = _context.Departments.Where(d => d.DepartmentCode == code).SingleOrDefault();
            if (department == null)
            {
                department = new Department();
                department.DepartmentName = name;
                department.DepartmentCode = code;
                department.CreatedBy = "yohana.tubike@tasac.go.tz";
                department.CreatedDate = DateTime.Now;
                _context.Departments.Add(department);
                _context.SaveChanges();
            }
            return department.DepartmentCode;
        }

        private string LoadDesignations(ExcelUser user)
        {
            string code = GetDesignationCode(user.Job);

            Designation designation = _context.Designations.Where(d => d.DesignationCode == code).SingleOrDefault();
            if (designation == null)
            {
                try
                {
                    designation = new Designation();
                    designation.StaffDesignation = user.Job;
                    designation.DesignationCode = code;
                    designation.CreatedBy = "yohana.tubike@tasac.go.tz";
                    designation.CreatedDate = DateTime.Now;
                    designation.DepartmentCode = LoadDepartments(user.Department);
                    designation.SectionCode = designation.DesignationCode;
                    designation.SupervisoryPostion = false;
                    _context.Designations.Add(designation);
                    _context.SaveChanges();
                } catch(Exception e)
                {
                    return e.Message;
                }
            }
            return designation.DesignationCode;
        }

        private string GetDesignationCode(string name)
        {
            string designationName = name.Replace("&", "").Replace("AND", "").Replace(" I", "").Replace(" II", "");
            string[] words = designationName.Split();
            string designationCode = "";
            foreach (string word in words)
            {
                if (word.Length > 0)
                {
                    designationCode = designationCode + word.Substring(0, 1);
                }
            }
            return designationCode;
        }

        private string GetDepCode(string name)
        {
            string depName = name.Replace("&", "");
            string[] words = depName.Split();
            string depCode = "";

            if (words.Last() == "DIRECTORATE")
            {
                words = ShiftString(words);
            }
                foreach (string word in words)
                {
                    if (word.Length > 0)
                    {
                        depCode = depCode + word.Substring(0, 1);
                    }
                }
            
            return depCode;
        }

        public string[] ShiftString(string[] t)
        {
            string[] first = t.Take(t.Length - 1).ToArray();
            string newDepName = t[t.Length - 1];

            return first.Prepend(newDepName).ToArray();
        }

        private void LoadJson()
        {
            //List<User> users = new List<User>();

            using (StreamReader r = new StreamReader("userlist.json"))
            {
                string json = r.ReadToEnd();
                List<ExcelUser> items = JsonConvert.DeserializeObject<List<ExcelUser>>(json);
                foreach (ExcelUser inUser in items)
                {
                    User userAvailable = _context.Users.Where(u => u.PFNumber == inUser.EmployeeCode).SingleOrDefault();

                    if (userAvailable == null)
                    {
                        string[] subs = inUser.EmployeeName.Split(' ');
                        string emailEnd = "@tasac.go.tz";
                        DateTime dt = DateTime.ParseExact(inUser.Birthdate, "dd-MMM-yyyy", CultureInfo.InvariantCulture);


                        User user = new User();
                        user.PFNumber = inUser.EmployeeCode;
                        user.FirstName = subs[0];
                        user.MiddleName = null;
                        user.LastName = subs[1];
                        user.DesignationCode = LoadDesignations(inUser);
                        user.Email = user.FirstName.ToLower() + "." + user.LastName.ToLower() + emailEnd;
                        user.CreatedDate = DateTime.Now;
                        user.CreatedBy = "yohana.tubike@tasac.go.tz";
                        user.ReportingTo = " ";
                        user.DutyStation = "HQ";
                        user.DateOfBirth = dt.ToUniversalTime();
                        user.BirthDay = dt.Day;
                        user.BirthMonth = dt.Month;
                        user.MobileNumber = "255 XXX XXXX";
                        user.Password = HomeController.getHashedMD5Password("tasac@123");

                        _context.Users.Add(user);
                        _context.SaveChanges();
                    }
                    //users.Add(user);
                }
            }
            //return users;
        }
    }
}
