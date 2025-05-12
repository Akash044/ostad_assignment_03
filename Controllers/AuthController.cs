using ostad_assignment_03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ostad_assignment_03.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(FormCollection formValues) {
            BaseMember baseMember = new BaseMember();

            string firstName = formValues["firstName"].ToString();
            string lastName = formValues["lastName"].ToString();
            string username = formValues["username"].ToString();
            string role = formValues["role"].ToString();
            string gender = formValues["gender"].ToString();
            string password = formValues["password"].ToString();
            string confirmPassword = formValues["confirmPassword"].ToString();

            if (password != confirmPassword) {
                ViewBag.isConfirmPasswordMatched = false;
                return View();
            }

            bool isExistingUser = baseMember.ValidateUsername(username);

            if (isExistingUser) {
                ViewBag.isUserAlreadyExisted = true;
                return View();
            }

            baseMember.FirstName = firstName;
            baseMember.LastName = lastName;
            baseMember.Username = username;
            baseMember.Role = role;
            baseMember.Gender = gender;
            baseMember.Password = password;

            int rowCount = baseMember.SaveUser();

            if (rowCount > 0) {
                Session["confirmationMsg"] = "User has successfully registered";
                return Redirect(Url.Action("UserList", "Dashboard"));
            }

            ViewBag.isUserCreated = false;
            return View();
        }
    }
}