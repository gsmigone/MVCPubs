using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPubs.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVCPubs.Controllers
{
    public class StoreController : Controller
    {
        private readonly pubsContext _context;

        public StoreController(pubsContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View("Index", _context.Stores.ToList());
        }

        public IActionResult Create()
        {
            Stores stores = new Stores();
            return View("Create", stores);
        }

        [HttpPost]
        public IActionResult Create(Stores stores)
        {
            _context.Add(stores);
            _context.SaveChanges();
            return RedirectToAction("Index")
;
        }
        
        
        [HttpGet]
        public ActionResult Edit(string id)
        {

            Stores store = _context.Stores.Find(id);
            
            return View("Edit", store);

        }
        

        [HttpPost]

        public ActionResult Edit(Stores store)
        {

            if (ModelState.IsValid)
            {

                _context.Entry(store).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(store);

        }


        public IActionResult Delete(string id)
        {
            var store = _context.Stores.SingleOrDefault(m => m.StorId == id);
            _context.Stores.Remove(store);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/stores/{city?}/{state?}")]
        public IActionResult FiltrarPorCiudad(string city, string state)
        {
            List<Stores> stores = (from s in _context.Stores where s.City == city && s.State== state select s).ToList();
            return View("Index", stores);
        }



    }
}
