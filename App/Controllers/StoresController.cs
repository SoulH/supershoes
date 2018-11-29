using App.Models;
using App.Utils;
using Models;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace App.Controllers
{
    public class StoresController : Controller
    {
        // GET: Stores
        public async Task<ActionResult> Index(Listing listing)
        {
            ViewBag.User = this.GetUser(User.Identity);
            ViewBag.Controller = this.GetName();
            ViewBag.StrSearch = listing.Name;
            var url = string.Join("/", ConfigurationManager.AppSettings["API_URL"], $"stores?take={listing.Take}&skip={listing.Skip}" + (string.IsNullOrEmpty(listing.Name)? "" : $"&name={listing.Name}"));
            HttpResponseMessage response = await DefaultApiClient.Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<StoresResponseModel>();
                if (content.Success)
                    return View(content.Stores);
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
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                ViewBag.User = this.GetUser(User.Identity);
                ViewBag.Controller = this.GetName();
                var model = new StoreModel()
                {
                    Name = collection.Get("Name"),
                    Address = collection.Get("Address")
                };
                var url = string.Join("/", ConfigurationManager.AppSettings["API_URL"], $"stores");
                HttpResponseMessage response = await DefaultApiClient.Client.PostAsJsonAsync(url, model);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsAsync<ResponseModelBase>();
                    if (!content.Success)
                        return View();
                }
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        // GET: Stores/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.User = this.GetUser(User.Identity);
            ViewBag.Controller = this.GetName();
            var url = string.Join("/", ConfigurationManager.AppSettings["API_URL"], $"stores/{id}");
            HttpResponseMessage response = await DefaultApiClient.Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<StoreResponseModel>();
                if (content.Success)
                    return View(content.Store);
            }
            return View();
        }

        // POST: Stores/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            try
            {
                ViewBag.User = this.GetUser(User.Identity);
                ViewBag.Controller = this.GetName();
                var model = new StoreModel()
                {
                    Id = int.Parse(collection.Get("Id")),
                    Name = collection.Get("Name"),
                    Address = collection.Get("Address")
                };
                var url = string.Join("/", ConfigurationManager.AppSettings["API_URL"], $"stores/{id}");
                HttpResponseMessage response = await DefaultApiClient.Client.PutAsJsonAsync(url, model);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsAsync<ResponseModelBase>();
                    if (!content.Success)
                        return View();
                }
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        // GET: Stores/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ViewBag.User = this.GetUser(User.Identity);
                ViewBag.Controller = this.GetName();
                var url = string.Join("/", ConfigurationManager.AppSettings["API_URL"], $"stores/{id}");
                HttpResponseMessage response = await DefaultApiClient.Client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsAsync<ResponseModelBase>();
                    if (!content.Success)
                        return View();
                }
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        // POST: Stores/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            return RedirectToAction("Index");
        }
    }
}
