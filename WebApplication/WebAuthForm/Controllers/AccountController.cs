    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.Security;
    using System.Web.Mvc;
    using BusinessLayerLibrary.Common;
    using BusinessLayerLibrary.DAL;

    using BusinessLayerLibrary.Domain.Model;
    using BusinessLayerLibrary.Facades;
    using WebAuthForm.Models;

    namespace WebAuthForm.Controllers
    {
        public class AccountController : Controller
        {

        
        public UserFacade UserService { get; set; }
       

        public AccountController(UserFacade userFac)
        {
            this.UserService = userFac;
        }
        public AccountController()
        {
          
        }


        public ActionResult Login()
        {
                return View();
        }

            [HttpPost]
            [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
            {
               

                if (ModelState.IsValid)
                {
                    var user = UserService.Validate(model.Login, model.Password);
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                    }
                }
                return View(model);
        }
        
            public ActionResult Register()
            {
                return View();
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Register(RegisterModel model)
            {
               /* if (ModelState.IsValid)
                {
                    User user = null;
                    using (UserContext db = new UserContext())
                    {
                        user = db.Users.FirstOrDefault(u => u.Email == model.Name);
                    }
                    if (user == null)
                    {
                        // создаем нового пользователя
                        using (UserContext db = new UserContext())
                        {
                          //  db.Users.Add(new User { Name = model.Name });
                            db.SaveChanges();

                            user = db.Users.Where(u => u.Name == model.Name && u.Password == model.Password).FirstOrDefault();
                        }
                        // если пользователь удачно добавлен в бд
                        if (user != null)
                        {
                            FormsAuthentication.SetAuthCookie(model.Name, true);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                    }
                }*/

                return View(model);
            }
            public ActionResult Logoff()
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
            }
        }
    }
