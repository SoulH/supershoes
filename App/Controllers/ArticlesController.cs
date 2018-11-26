using App.Models;
using App.Utils;
using Models;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace App.Controllers
{
    public class ArticlesController : Controller
    {
        readonly HttpClient client = new HttpClient();

        // GET: Articles
        public async Task<ActionResult> Index(Listing listing)
        {
            ViewBag.User = this.GetUser(User.Identity);
            ViewBag.Controller = this.GetName();
            ViewBag.StrSearch = listing.Name;
            ViewBag.StoreId = 0;
            var url = string.Join("/", ConfigurationManager.AppSettings["API_URL"], "stores");
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<ResponseModel<StoreModel>>();
                if (content.Success)
                    ViewBag.Stores = content.Data;
            }
            url = string.Join("/", ConfigurationManager.AppSettings["API_URL"], $"articles?take={listing.Take}&skip={listing.Skip}" + (string.IsNullOrEmpty(listing.Name) ? "" : $"&name={listing.Name}"));
            response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<ResponseModel<ArticleModel>>();
                if (content.Success)
                    return View(content.Data);
            }
            return RedirectToAction("Index", "Home", null);
        }

        // GET: Articles
        public async Task<ActionResult> Search(ListArticlesByStore listing)
        {
            ViewBag.User = this.GetUser(User.Identity);
            ViewBag.Controller = this.GetName();
            ViewBag.StrSearch = listing.Name;
            ViewBag.StoreId = listing.StoreId;
            var url = string.Join("/", ConfigurationManager.AppSettings["API_URL"], "stores");
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<ResponseModel<StoreModel>>();
                if (content.Success)
                    ViewBag.Stores = content.Data;
            }
            url = string.Join("/", ConfigurationManager.AppSettings["API_URL"], $"articles/store/{listing.StoreId}?take={listing.Take}&skip={listing.Skip}" + (string.IsNullOrEmpty(listing.Name) ? "" : $"&name={listing.Name}"));
            response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<ResponseModel<ArticleModel>>();
                if (content.Success)
                    return View("Index",content.Data);
            }
            return RedirectToAction("Index", "Home", null);
        }

        // GET: Articles/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.User = this.GetUser(User.Identity);
            ViewBag.Controller = this.GetName();
            return View();
        }

        // GET: Articles/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.User = this.GetUser(User.Identity);
            ViewBag.Controller = this.GetName();
            var url = string.Join("/", ConfigurationManager.AppSettings["API_URL"], $"stores?take=0&skip=0");
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<ResponseModel<StoreModel>>();
                if (content.Success)
                    ViewBag.Stores = content.Data;
            }
            return View();
        }

        // POST: Articles/Create
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

        // GET: Articles/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.User = this.GetUser(User.Identity);
            ViewBag.Controller = this.GetName();
            var url = string.Join("/", ConfigurationManager.AppSettings["API_URL"], $"articles/{id}");
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<ResponseModel<ArticleModel>>();
                if (content.Success)
                    return View(content.Data[0]);
            }
            return View();
        }

        // POST: Articles/Edit/5
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

        // GET: Articles/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.User = this.GetUser(User.Identity);
            ViewBag.Controller = this.GetName();
            return View();
        }

        // POST: Articles/Delete/5
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
