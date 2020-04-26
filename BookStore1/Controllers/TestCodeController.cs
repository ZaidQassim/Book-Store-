using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore1.Controllers
{

    [Route("Api/[controller]")]
    [ApiController]
    public class TestCodeController : Controller
    {
        BookStoreDbContext db = new BookStoreDbContext();

        public IActionResult Index()
        {
            return View();

        }

       // to return the  List from Data To View
        public IActionResult test()
        {
            var model = new List<MyData>
            {
                new MyData{Id=1, value="ksjah"},
                new MyData{Id=2, value="wdfhodhfiowefio"}
            };
              
            return View(model);
        }

        public ObjectResult Myobject()
        {
            // call class
            var myModel = new MyData
            {
                Id = 1,
                value = "zaid qassim"
            };
            return new ObjectResult(myModel);
        }

        public JsonResult Myobjectjson()
        {
            // call class
            var myModel = new MyData
            {
                Id = 1,
                value = "zaid qassimdsgadsgdfgadfgdfgadf"
            };
            return Json (myModel);
        }

        public string query()
        {
            var selectq = db.Books.FromSql("selecte * from  Books").ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var item in selectq)
            {
                sb.Append(item.Title + "\n");
            }
            return sb.ToString();
        }


    }
}