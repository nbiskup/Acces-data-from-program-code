using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonVehicleCollection.Controllers
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
            return View(db.VehicleSet);
        }


        public ActionResult Details(int? id)
        {
            return CommonAction(id);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Vehicle vehicle, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                vehicle.UploadedFiles = new List<UploadedFile>();

                AddFiles(vehicle, files);

                db.VehicleSet.Add(vehicle);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
                

        public ActionResult Edit(int? id)
        {
            return CommonAction(id);
        }


        [HttpPost]
        public ActionResult Edit(int id, IEnumerable<HttpPostedFileBase> files)
        {
            Vehicle vehicle = db.VehicleSet.Find(id);

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

            return View(vehicle);
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


            Vehicle vehicle = db.VehicleSet                
                .SingleOrDefault(v => v.IDVehicle == id);

            if (vehicle == null)
                return HttpNotFound();

            return View(vehicle);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            db.UploadedFiles.RemoveRange(db.UploadedFiles.Where(f => f.VehicleIDVehicle == id));
            db.VehicleSet.Remove(db.VehicleSet.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
