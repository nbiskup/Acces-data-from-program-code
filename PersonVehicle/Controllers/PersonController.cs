using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonVehicle.Controllers
{
    public class PersonController : Controller
    {
        private readonly ModelContainer db = new ModelContainer();
        ~PersonController()
        {
            db.Dispose();
        }


        public ActionResult Index()
        {
            return View(db.People);
        }


        public ActionResult Details(int id)
        {
            return CommonAction(id);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                person.Vehicles = new List<Vehicle>();

                db.People.Add(person);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            return CommonAction(id);
        }


        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Person person = db.People.Find(id);

            if (TryUpdateModel(person, "",
                new string[]
                {
                    nameof(Person.FirstName),
                    nameof(Person.LastName),
                    nameof(Person.Address),
                    nameof(Person.Age)
                }))
            {

                db.Entry(person).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }



        public ActionResult Delete(int id)
        {
            return CommonAction(id);
        }


        private ActionResult CommonAction(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Person person = db.People        // MOZDA TRIBA INCLUDATI VEHICLE PREKO METODE Include()
                .SingleOrDefault(p => p.IDPerson == id);

            if (person == null)
                return HttpNotFound();

            return View(person);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            db.Vehicles.RemoveRange(db.Vehicles.Where(v => v.PersonIDPerson == id));
            db.People.Remove(db.People.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
