﻿using IntranetPortal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;

namespace IntranetPortal.Controllers
{
    public class HomeController : Controller

    {
        private IntranetDBContext myContext = new IntranetDBContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        public IActionResult Index()
        {
            return View();
        }
        public   async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task< IActionResult> Processlogin(LoginViewModel model, string ReturnUrl)
        {
            //if (ModelState.IsValid)
            //{
                string Password = "tasac@123";
                string ContentManagers = "0";
                var encryptedPass = getHashedMD5Password(Password);
                var getUser = myContext.Users.Include("Designations").SingleOrDefault(t => t.PFNumber == model.PFNumber && t.Password == encryptedPass );
                var getRoles = myContext.UserRoles.SingleOrDefault(t => t.PFNumber == model.PFNumber && t.RoleId==13 );
                    if(getRoles != null)
                        {
                              ContentManagers = getRoles.RoleId.ToString();
                        }
          

            if (getUser != null)
                {



                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {

                    var userClaims = new List<Claim>()
                {

                    new Claim(ClaimTypes.Name  , getUser.FirstName+" "+ getUser.LastName ),
                    new Claim(ClaimTypes.SerialNumber  , getUser.PFNumber),
                    new Claim(ClaimTypes.Email, getUser.Email),
                    new Claim(ClaimTypes.DateOfBirth, getUser.DateOfBirth.ToString()),
                    new Claim("SectionCode", getUser.Designations.SectionCode),
                    new Claim("DesignationCode", getUser.DesignationCode),
                    new Claim("DepartmentCode", getUser.Designations.DepartmentCode),
                    new Claim("IsSupervisor", getUser.Designations.SupervisoryPostion.ToString()),
                    new Claim("IsAdmin", getUser.Designations.SectionCode.ToString()),
                    new Claim("IsAContentManager", ContentManagers),
                 };
                    var userIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                    await HttpContext.SignInAsync(userPrincipal);
                        return   RedirectToAction("Index", "StaffPage");
                    }
                }
            ViewBag.Error = "Invalid login attempt. or Account is locked";
            return View("Login");
        }




        

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}