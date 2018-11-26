using App.Models;
using App.Utils;
using Models;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace App.Controllers
{
    public class StoresController : Controller
    {
        readonly HttpClient client = new HttpClient();

        // GET: Stores
        public async Task<ActionResult> Index(Listing listing)
        {
            ViewBag.User = this.GetUser(User.Identity);
            ViewBag.Controller = this.GetName();
            ViewBag.StrSearch = listing.Name;
            var url = string.Join("/", ConfigurationManager.AppSettings["API_URL"], $"stores?take={listing.Take}&skip={listing.Skip}" + (string.IsNullOrEmpty(listing.Name)? "" : $"&name={listing.Name}"));
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<ResponseModel<StoreModel>>();
                if (content.Success)
                    return View(content.Data);
            }
            return RedirectToAction("Index","Home",null);
        }

        // GET: Stores/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.User = this.GetUser(User.Identity);
            ViewBag.Controller = this.GetName();
            return View();
        }

        // GET: Stores/Create
        public ActionResult Create()
        {
            ViewBag.User = this.GetUser(User.Identity);
            ViewBag.Controller = this.GetName();
            return View();
        }

        // POST: Stores/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Stores/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.User = this.GetUser(User.Identity);
            ViewBag.Controller = this.GetName();
            var url = string.Join("/", ConfigurationManager.AppSettings["API_URL"], $"stores/{id}");
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<ResponseModel<StoreModel>>();
                if (content.Success)
                    return View(content.Data[0]);
            }
            return View();
        }

        // POST: Stores/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Stores/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.User = this.GetUser(User.Identity);
            ViewBag.Controller = this.GetName();
            return View();
        }

        // POST: Stores/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
