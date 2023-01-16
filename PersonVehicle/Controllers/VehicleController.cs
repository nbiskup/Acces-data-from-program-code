using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonVehicle.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ModelContainer db = new ModelContainer();

        ~VehicleController()
        {
            db.Dispose();
        }

        public ActionResult Index()
        {
            return View(db.Vehicles);
        }


        public ActionResult Details(int? id)
        {
            return CommonAction(id);
        }


        public ActionResult Create()
        {
            ViewBag.PersonIDPerson = new SelectList(db.People, "IDPerson", "FirstName");
            return View();
        }


        [HttpPost]
        public ActionResult Create([Bind(Include = "IDVehicle, Brand, Model, Kilometers, Color, PersonIDPerson")] Vehicle vehicle, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                vehicle.UploadedFiles = new List<UploadedFile>();

                AddFiles(vehicle, files);

                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonIDPerson = new SelectList(db.People, "IDPerson", "FirstName", vehicle.PersonIDPerson);
            return View(vehicle);
        }


        public ActionResult Edit(int? id)
        {
            return CommonAction(id);
        }


        [HttpPost]
        public ActionResult Edit([Bind(Include = "IDVehicle, Brand, Model, Kilometers, Color, PersonIDPerson")] int id, IEnumerable<HttpPostedFileBase> files)
        {
            Vehicle vehicle = db.Vehicles.Find(id);

            if (TryUpdateModel(vehicle, "",
                new string[] {
                    nameof(Vehicle.Brand),
                    nameof(Vehicle.Model),
                    nameof(Vehicle.Color),
                    nameof(Vehicle.Kilometers)
                }))
            {
                AddFiles(vehicle, files);

                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                RedirectToAction("Index");
            }

            ViewBag.PersonIDPerson = new SelectList(db.People, "IDPerson", "FirstName", vehicle.PersonIDPerson);
            return View(vehicle);
        }


        [HttpGet]
        public ActionResult Owner(int id)
        {
            Person person = db.People.FirstOrDefault(p => p.IDPerson == id);
            if (person == null)
                return HttpNotFound();

            return View(person.Vehicles);

        }


        private void AddFiles(Vehicle vehicle, IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var picture = new UploadedFile
                    {
                        Name = file.FileName,
                        ContentType = file.ContentType,
                    };
                    using (var reader = new System.IO.BinaryReader(file.InputStream))
                    {
                        picture.Content = reader.ReadBytes(file.ContentLength);
                    }
                    vehicle.UploadedFiles.Add(picture);
                }
            }
        }


        public ActionResult Delete(int? id)
        {
            return CommonAction(id);
        }


        private ActionResult CommonAction(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);


            Vehicle vehicle = db.Vehicles
                .Include(v => v.UploadedFiles)
                .SingleOrDefault(v => v.IDVehicle == id);

            if (vehicle == null)
                return HttpNotFound();


            ViewBag.PersonIDPerson = new SelectList(db.People, "IDPerson", "FirstName", vehicle.PersonIDPerson);
            return View(vehicle);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            db.Files.RemoveRange(db.Files.Where(f => f.VehicleIDVehicle == id));
            db.Vehicles.Remove(db.Vehicles.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}