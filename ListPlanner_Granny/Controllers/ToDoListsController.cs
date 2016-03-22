using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ListPlanner_Granny.Models;
using ListItem = ListPlanner_Granny.Models.ListItem;

namespace ListPlanner_Granny.Controllers
{
    public class ToDoListsController : Controller
    {
        private HsmDbContext _context;

        const string ToDoListCacheKeyPrefix = "ToDoListCache_";
        const string ToDoByUserCacheKeyPrefix = "ToDoByUserCache_";

        //private IMemoryCache cache;
        //private readonly ILogger<ToDoListsController> _logger;

        public ToDoListsController()
        {
            _context = new HsmDbContext();
            //this.cache = cache;
            //_logger = logger;
            //_logger.LogDebug("Hejsa");
            //_logger.LogInformation("Processing GET request for values.");
        }

        // GET: ToDoLists
        public ActionResult Index()
        {
            ViewBag.Title = "ToDoLists";
            return View();
        }

    }
    

    //her

    //og her
    [System.Web.Mvc.Route("api/[controller]")]
    public class ToDoListController : Controller
    {

        #region Dummy Data
        IList<ToDoList> toDoLists = new List<ToDoList>
        {
        };
        #endregion

    //    // /api/products
    //    [System.Web.Mvc.HttpGet]
    //    public IEnumerable<ToDoList> GetAllToDoLists()
    //    {
    //        return toDoLists;
    //    }

    //    // /api/products/1
    //    [System.Web.Mvc.HttpGet("{id}")]
    //    public ViewResult GetProduct(int id)
    //    {
    //        var toDoList = toDoLists.FirstOrDefault((p) => p.ToDoListID == id);
    //        if (toDoList == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return new ObjectResult(toDoList);
    //    }
    }





}

