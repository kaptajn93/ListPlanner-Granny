 using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ListPlanner_Granny.Models;

namespace ListPlanner_Granny.Controllers
{
    
    public class UsersController : ApiController
    {

        private HsmDbContext _context;


        public UsersController()
        {
            //_context = new ApplicationDbContext();
            _context = new HsmDbContext();

        }

        //// GET: Users
        //public ViewResult Index()
        //{
        //    return View(_context.User.ToList());
        //}

        
        public ICollection<User> GetList()
        {
            var users = _context.User.ToList();
            return users;
        }

        // POST: Users/Create
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public IHttpActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.User.Add(user);
                _context.SaveChanges();
                return Ok("User successfully created");
            }
            return NotFound();
        }


        // GET: Users/Delete/5
        [System.Web.Mvc.ActionName("Delete")]
        public IHttpActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = _context.User.Single(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: Users/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IHttpActionResult DeleteConfirmed(int id)
        {
            User user = _context.User.Single(m => m.UserID == id);
            _context.User.Remove(user);
            _context.SaveChanges();
            return Ok("Successfully deleted user!");
        }
    }



}
