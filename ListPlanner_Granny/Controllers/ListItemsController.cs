using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using ListPlanner_Granny.Models;

namespace ListPlanner_Granny.Controllers
{
    public class ListItemsController : ApiController
    {
        private HsmDbContext _context;

        public ListItemsController()
        {
            _context = new HsmDbContext();    

        }

 
        // POST: ListItems/Create
        [System.Web.Mvc.HttpPost]
        // [ValidateAntiForgeryToken]
        public IHttpActionResult Create([FromBody]ListItem listItem)
        {
            if (ModelState.IsValid)
            {
              
                _context.ListItem.Add(listItem);
                _context.SaveChanges();
                return Json(new AjaxResponse { IsSuccess = true });
            }

         

            var errorList = ModelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

            var error = new
            {
                //ErrorCount = ModelState.ErrorCount,
                Errors = errorList,
                Message = "Ret fejlen"
            };

            return Json(new AjaxResponse
            {
                IsSuccess = false,
                Errors = errorList.Select(x => string.Join(",", x.Value)).ToList(),
                Message = "An error occured!"
            });

        }

        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Update([FromBody]ListItem listItem)
        {
            if (ModelState.IsValid)
            {

                //_context.ListItem.Update(listItem);
                _context.SaveChanges();
                return Json(new AjaxResponse { IsSuccess = true });
            }



            var errorList = ModelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

            var error = new
            {
                //ErrorCount = ModelState.ErrorCount,
                Errors = errorList,
                Message = "Ret fejlen"
            };

            return Json(new AjaxResponse
            {
                IsSuccess = false,
                Errors = errorList.Select(x => string.Join(",", x.Value)).ToList(),
                Message = "An error occured!"
            });

        }
        
        

        // GET: ListItems/DeleteItems/5         we use a simular in ToDoListsController
        [System.Web.Mvc.ActionName("DeleteItems")]
        public IHttpActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ListItem listItem = _context.ListItem.Single(m => m.ListItemID == id);
            if (listItem == null)
            {
                return NotFound();

            }

            return Ok(listItem);
        }

        // POST: ListItems/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteItems")]
      //  [ValidateAntiForgeryToken]
        public IHttpActionResult DeleteItemsConfirmed(int id)
        {
            ListItem listItem = _context.ListItem.Single(m => m.ListItemID == id);
            _context.ListItem.Remove(listItem);
            _context.SaveChanges();
            return Json("OK");
        }
    }
}
