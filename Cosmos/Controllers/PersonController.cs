using Cosmos.Dao;
using Cosmos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Cosmos.Controllers
{
    public class PersonController : Controller
    {
        private static readonly ICosmosDbService service = CosmosDbServiceProvider.CosmosDbService;


        public async Task<ActionResult> Index()
        {
            return View(await service.GetPeopleAsync("SELECT * FROM Person"));
        }



        public async Task<ActionResult> Details(string id) => await ShowPerson(id);

      
        public ActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public async Task<ActionResult> Create(Person person)
        {
            if (ModelState.IsValid)
            {
                person.Id = Guid.NewGuid().ToString();
                await service.AddPersonAsync(person);
                return RedirectToAction("Index");
            }
            return View(person);
            
        }


        public async Task<ActionResult> Edit(string id) => await ShowPerson(id);


        [HttpPost]
        public async Task<ActionResult> Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                await service.UpadtePersonAsync(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }


        public async Task<ActionResult> Delete(string id) => await ShowPerson(id);


        [HttpPost]
        public async Task<ActionResult> Delete(Person person)
        {
            await service.DeletePersonAsync(person);
            return RedirectToAction("Index");
        }


        private async Task<ActionResult> ShowPerson(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadGateway);

            var person = await service.GetPersonAsync(id);

            if (person == null)
                return HttpNotFound();

            return View(person);
        }


    }
}
