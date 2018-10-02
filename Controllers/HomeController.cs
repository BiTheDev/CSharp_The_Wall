using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWall.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TheWall.Controllers
{
    public class HomeController : Controller
    {
        private WallContext _context;
        public HomeController(WallContext context)
        {
            _context = context;
        }
         public IActionResult Index()
        {
            return View("index");
        }

        [HttpPost("RegisterProcess")]
        public IActionResult Register(RegisterViewModel user){
                // var userList = _context.users.Where(p => p.email== user.email).FirstOrDefault();
                // if(user.email == userList.email){
                //     ModelState.AddModelError("email", "email existed");
                // }
                if(ModelState.IsValid){
                PasswordHasher<RegisterViewModel> Hasher = new PasswordHasher<RegisterViewModel>();
                user.password = Hasher.HashPassword(user, user.password);
                users User = new users(){
                    first_name = user.first_name,
                    last_name = user.last_name,
                    email = user.email,
                    password = user.password,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };
                _context.Add(User);
                _context.SaveChanges();
                return RedirectToAction("Wall");
            }else{
                return View("index");
            }
        }


        [HttpGet("Login")]
        public IActionResult LoginPage(){

            return View("login");
        }

        [HttpPost("LoginProcess")]
        public IActionResult Login(LoginViewModel User){
            if(ModelState.IsValid){
                List<users> users = _context.users.Where(p => p.email== User.Email).ToList();
                foreach (var user in users)
                {
                    if(user != null && User.Password != null)
                        {
                            var Hasher = new PasswordHasher<users>();
                            if( 0 !=Hasher.VerifyHashedPassword(user, user.password, User.Password)){
                                HttpContext.Session.SetInt32("Id", (int)user.id);
                                int? id = HttpContext.Session.GetInt32("Id");

                            return RedirectToAction("Wall");
                        }
                    }else{
                        return View("login");
                    }
                }       
            }
            return View("login");
        }

        [HttpGet("wall")]
        public IActionResult Wall(){
            int? id = HttpContext.Session.GetInt32("Id");
            List<messages> allMessges = _context.messages.Include(p=>p.user).OrderByDescending(p=>p.created_at).ToList();
            List<comments> allComments = _context.comments.Include(p=>p.user).ToList();
            var userinfo = _context.users.Where(p=>p.id == id).FirstOrDefault();
            ViewBag.userinfo = userinfo;
            ViewBag.allMessages = allMessges;
            ViewBag.allComments = allComments;
            return View("wall");
        }

        [HttpPost("CreateMessage")]
        public IActionResult CreateMessage(string message){
            int? id = HttpContext.Session.GetInt32("Id");
            messages NewMessage = new messages(){
                usersid = (long)id,
                message = message,
                created_at = DateTime.Now,
                updated_at = DateTime.Now

            };
            _context.Add(NewMessage);
            _context.SaveChanges();
            return RedirectToAction("Wall");
        }

        [HttpPost("CreateComment")]
        public IActionResult CreateComment(string comment, int messageid){
            int? id = HttpContext.Session.GetInt32("Id");
            comments NewComment = new comments(){
                usersid = (long)id,
                messagesid = (long)messageid,
                comment = comment,
                created_at = DateTime.Now,
                updated_at = DateTime.Now

            };
            _context.Add(NewComment);
            _context.SaveChanges();
            return RedirectToAction("Wall");
        }

        [HttpPost("DeleteMsg")]
        public IActionResult DeleteMsg(int messageid){
            int? id = HttpContext.Session.GetInt32("Id");
            var Usercmt = _context.comments.Where(p=>p.messagesid == messageid).ToList();
            var UserMsg = _context.messages.Where(p=>p.id == messageid).FirstOrDefault();
            DateTime now = DateTime.Now;
            TimeSpan diff = now - UserMsg.created_at;
            
            if(diff.Minutes < 30){
                foreach(var item in Usercmt){
                    _context.comments.Remove(item);
                }
            
                if((long)id == UserMsg.usersid){
                    
                    _context.messages.Remove(UserMsg);
                }
            }
            else{
                ViewBag.error = "You cant delete message after 30 minutes";
            }
            _context.SaveChanges();
            return RedirectToAction("Wall");
        }


        [HttpPost("DeleteCmt")]
        public IActionResult DeleteCmt(int messageid, int cmtid){
            int? id = HttpContext.Session.GetInt32("Id");
            System.Console.WriteLine("===========");
            System.Console.WriteLine(id);
            System.Console.WriteLine(messageid);
            System.Console.WriteLine(cmtid);
            System.Console.WriteLine("==========");
            var UserMsgCmt = _context.comments.Where(p=>p.id == (long)cmtid).FirstOrDefault();

            //delete comment
            if(((long)messageid == UserMsgCmt.messagesid) && ((long)id == UserMsgCmt.usersid)){
                _context.comments.Remove(UserMsgCmt);
                _context.SaveChanges();
            }
            return RedirectToAction("Wall");
        }
        [HttpGet("logout")]
        public IActionResult logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}
