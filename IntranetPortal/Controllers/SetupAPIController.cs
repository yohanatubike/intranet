using IntranetPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevExtreme.AspNet.Data;
using Newtonsoft.Json;
using DevExtreme.AspNet.Mvc;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace IntranetPortal.Controllers
{
    //[Authorize]
    public class SetupAPIController : Controller
    {

        private IntranetDBContext myContext = new IntranetDBContext();
        string ValidationErrorMessage = null;

        #region for roles  

        [HttpGet]
        public async Task<IActionResult> GetGroups(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.Roles, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return  Content( resultJson, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> GetPermissions(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.Permissions, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        [HttpPost]
        public async Task<IActionResult> AddNewPermisson(string values)
        {
            var newPermission = new Permission();

            JsonConvert.PopulateObject(values, newPermission);
            //if (!TryValidateModel(newPermission))

            //    return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            newPermission.CreatedBy = "mustafa.juma@tasac.go.tz";
            newPermission.CreatedDate = DateTime.Now;
            myContext.Permissions.Add(newPermission);
            await myContext.SaveChangesAsync();
            return Ok(newPermission);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewRole (string values)
        {
            var newRole = new Role();

            JsonConvert.PopulateObject(values, newRole);
            if (!TryValidateModel(newRole))

            return  BadRequest(ValidationErrorMessage ="Failed to save details due to validation error");
            newRole.CreatedBy = "mustafa.juma@tasac.go.tz";
            newRole.CretaedDate = DateTime.Now;
            newRole.RoleName.ToUpper();

           myContext.Roles.Add(newRole);
           await myContext.SaveChangesAsync();

            return  Ok(newRole);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(int key, string values)
        {
            var RoleDetails = await myContext.Roles.FirstOrDefaultAsync(item => item.Id == key);
            JsonConvert.PopulateObject(values, RoleDetails);

            if (!TryValidateModel(RoleDetails))
                return BadRequest(ValidationErrorMessage);

            await myContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task RemoveRole(int key)
        {
            var RoleDetails = await myContext.Roles.FirstOrDefaultAsync(item => item.Id == key);
            myContext.Roles.Remove(RoleDetails);
            await  myContext.SaveChangesAsync();
        }

        #endregion roles management

        #region for Department
        [HttpGet]
        public async Task<IActionResult> GetDepartments(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.Departments, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(string values)
        {
            var newDepartment = new  Department();

            JsonConvert.PopulateObject(values, newDepartment);
            if (!TryValidateModel(newDepartment))
            
            return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            newDepartment.CreatedBy = "mustafa.juma@tasac.go.tz";
            newDepartment.CreatedDate = DateTime.UtcNow;
            newDepartment.DepartmentCode.ToUpper();
            newDepartment.DepartmentName.ToUpper();

            myContext.Departments.Add(newDepartment);
            await myContext.SaveChangesAsync();

            return Ok(newDepartment);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDepartment(string key, string values)
        {
            var DepartmentDetails = await myContext.Departments.FirstOrDefaultAsync(item => item.DepartmentCode == key);
            JsonConvert.PopulateObject(values, DepartmentDetails);

            if (!TryValidateModel(DepartmentDetails))

                return BadRequest(ValidationErrorMessage);

            await myContext.SaveChangesAsync();
            return Ok();
        }
        #endregion  for Department

        #region  for Section
        [HttpGet]
        public async Task<IActionResult> GetSection(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.Sections, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> AddSection(string values)
        {
            var newSection = new Section();
            JsonConvert.PopulateObject(values, newSection);
            if (!TryValidateModel(newSection))
            { 

            return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            
            }
            newSection.CreatedBy = "mustafa.juma@tasac.go.tz";
            newSection.CreatedDate = DateTime.Now;
            newSection.DepartmentCode.ToUpper();
            newSection.SectionCode.ToUpper();
            myContext.Sections.Add(newSection);
            await myContext.SaveChangesAsync();
            return Ok(newSection);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSection(string key, string values)
        {
            var sectionDetails = await myContext.Sections.FirstOrDefaultAsync(item => item.SectionCode == key);
            JsonConvert.PopulateObject(values, sectionDetails);

            if (!TryValidateModel(sectionDetails))
                return BadRequest(ValidationErrorMessage);

            await myContext.SaveChangesAsync();
            return Ok();
        }
        #endregion for Section

        #region Password encryption zone
        public static string getHashedMD5Password(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        #endregion

        #region Designation Management
        [HttpGet]
        public async Task<IActionResult> GetDesignation(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.Designations, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return  Content(resultJson, "application/json");
        }
        public async Task<IActionResult> GetSupervisoryDesignation(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.Designations.Where(t=>t.SupervisoryPostion ==true), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        [HttpPost]
        public async Task<IActionResult> AddDesignation(string values)
        {
            var newDesignation = new Designation();
            JsonConvert.PopulateObject(values, newDesignation);
            if (!TryValidateModel(newDesignation))
            {

                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");

            }
            newDesignation.CreatedBy = "mustafa.juma@tasac.go.tz";
            newDesignation.CreatedDate = DateTime.Now;
            newDesignation.DepartmentCode.ToUpper();
            newDesignation.SectionCode.ToUpper();
            newDesignation.StaffDesignation.ToUpper();
            newDesignation.DesignationCode.ToUpper();
            myContext.Designations.Add(newDesignation);
            await myContext.SaveChangesAsync();
            return Ok(newDesignation);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDesignation(string key, string values)
        {
            var designationDetails = await myContext.Designations.FirstOrDefaultAsync(item => item.DesignationCode == key);
            JsonConvert.PopulateObject(values, designationDetails);

            if (!TryValidateModel(designationDetails))

                return BadRequest(ValidationErrorMessage);

            await myContext.SaveChangesAsync();
            return Ok();
        }
        #endregion

        #region  Users Management
        [HttpGet]
        public async Task<IActionResult> GetListOfUsers(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.Users.Include("Designations"), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> AddNewUser(string values)
        {
            var newUser = new  User();
            JsonConvert.PopulateObject(values, newUser);
           
            newUser.CreatedBy = "mustafa.juma@tasac.go.tz";
            newUser.CreatedDate = DateTime.Now;
            myContext.Users.Add(newUser);
            await myContext.SaveChangesAsync();
            return Ok(newUser);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(string key, string values)
        {
            var  userDetails = await myContext.Users.FirstOrDefaultAsync(item => item.PFNumber == key);
            JsonConvert.PopulateObject(values, userDetails);
            await myContext.SaveChangesAsync();
            return Ok();
        }
        #endregion

        #region  User's Role
        [HttpGet]
        public async Task<IActionResult> GetListOfUserRoles(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.UserRoles, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> AssignUserRole(string values)
        {
            var newUserRole = new UserRole();
            JsonConvert.PopulateObject(values, newUserRole);

            newUserRole.CreatedBy = "mustafa.juma@tasac.go.tz";
            newUserRole.CreatedDate = DateTime.Now;
            myContext.UserRoles.Add(newUserRole);
            await myContext.SaveChangesAsync();
            return Ok(newUserRole);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserRole(string key, string values)
        {
            var userDetails = await myContext.Users.FirstOrDefaultAsync(item => item.PFNumber == key);
            JsonConvert.PopulateObject(values, userDetails);
            await myContext.SaveChangesAsync();
            return Ok();
        }
        #endregion
    }
}
