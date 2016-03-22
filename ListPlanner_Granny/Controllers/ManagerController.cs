using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using ListPlanner_Granny.Models;
using ListItem = ListPlanner_Granny.Models.ListItem;

namespace ListPlanner_Granny.Controllers
{

    [Route("api/todolist")]
    public class ManagerController : ApiController
    {
        private readonly HsmDbContext _context;

        const string ToDoListCacheKeyPrefix = "ToDoListCache_";
        const string ToDoByUserCacheKeyPrefix = "ToDoByUserCache_";

        //private IMemoryCache cache;
        //private readonly ILogger<ToDoListsController> _logger;

        public ManagerController()
        {
            _context = new HsmDbContext();

            //this.cache = cache;
            //_logger = logger;
            //_logger.LogDebug("Hejsa");
            //_logger.LogInformation("Processing GET request for values.");
        }


        //JUBII,...mig i røven!  haha
        //    [LogActionFilter]
        //    [HsmCache(CacheKey = ToDoListCacheKeyPrefix, Duration = 60)]
        //[Route("ToDoLists")] // kun for at vise at det er sådan du "mapper" navn/url tol api-action'en :) ok
        public ICollection<ToDoList> Get()
        {
            // nu kigger .NET på requestet (eller BØR gøre det), og returnerer XML, JSON eller CSV alt after hvad vi beder om i requestet

            var todolist = _context.ToDoList.Include(x => x.ListItem).ToList();
            return todolist;
        }
      

        //     [HsmCache(CacheKey = ToDoByUserCacheKeyPrefix, Duration = 60)]
        //[Route("ToDoByUser")]
        public IHttpActionResult GetToDoByUser(int userId)
        {
            IList<ToDoList> todolist;

            //var cacheKey = GetCacheKey(ToDoByUserCacheKeyPrefix, userId);
            //cache.TryGetValue(cacheKey, out todolist);

            //if (todolist == null)
            //{
            todolist = _context.ToDoList.Where(t => t.UserID == userId).ToList();

            //    cache.Set(cacheKey, todolist, new MemoryCacheEntryOptions
            //    {
            //        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5)
            //    });
            //}

            return Json(todolist);
        }


        // POST: ToDoLists/Create
        [System.Web.Mvc.HttpPost]
        // [ValidateAntiForgeryToken]
        public IHttpActionResult Create([FromBody]ToDoList toDoList)
        {
            if (ModelState.IsValid)
            {
                _context.ToDoList.Add(toDoList);
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


        // GET: ToDoLists/Delete/5
        public IHttpActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ToDoList toDoList = _context.ToDoList.Single(m => m.ToDoListID == id);
            if (toDoList == null)
            {
                return NotFound();
            }

            return Ok();
        }

        // POST: ToDoLists/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public IHttpActionResult DeleteConfirmed(int id)
        {
            ToDoList toDoList = _context.ToDoList.Single(m => m.ToDoListID == id);
            _context.ToDoList.Remove(toDoList);
            _context.SaveChanges();
            return Json("OK");
        }
        //_____________________________________________Items

        // GET: /todolists/DeleteItems/5
        public IHttpActionResult GetDeleteItems(int? id)
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

            return Ok();
        }

        // POST: ListItems/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteItems")]        
        //[ValidateAntiForgeryToken]
        public IHttpActionResult DeleteItemsConfirmed(int id)
        {
            ListItem listItem = _context.ListItem.Single(m => m.ListItemID == id);
            _context.ListItem.Remove(listItem);
            _context.SaveChanges();
            return Json("OK");
        }

        //______________________________________________



        private string GetCacheKey(string prefix, object key)
        {
            return prefix + key;
        }
    }
}
