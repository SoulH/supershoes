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
        // GET: Articles
        public async Task<ActionResult> Index(Listing listing)
        {
            ViewBag.User = this.GetUser(User.Identity);
            ViewBag.Controller = this.GetName();
            ViewBag.StrSearch = listing.Name;
            ViewBag.StoreId = 0;
            var url = string.Join("/", ConfigurationManager.AppSettings["API_URL"], "stores");
            HttpResponseMessage response = await DefaultApiClient.Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<StoresResponseModel>();
                if (content.Success)
                    ViewBag.Stores = content.Stores;
            }
            url = string.Join("/", ConfigurationManager.AppSettings["API_URL"], $"articles?take={listing.Take}&skip={listing.Skip}" + (string.IsNullOrEmpty(listing.Name) ? "" : $"&name={listing.Name}"));
            response = await DefaultApiClient.Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<ArticlesResponseModel>();
                if (content.Success)
                    return View(content.Articles);
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
            HttpResponseMessage response = await DefaultApiClient.Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<StoresResponseModel>();
                if (content.Success)
                    ViewBag.Stores = content.Stores;
            }
            url = string.Join("/", ConfigurationManager.AppSettings["API_URL"], $"articles/store/{listing.StoreId}?take={listing.Take}&skip={listing.Skip}" + (string.IsNullOrEmpty(listing.Name) ? "" : $"&name={listing.Name}"));
            response = await DefaultApiClient.Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<ArticlesResponseModel>();
                if (content.Success)
                    return View("Index",content.Articles);
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
            HttpResponseMessage response = await DefaultApiClient.Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<StoresResponseModel>();
                if (content.Success)
                    ViewBag.Stores = content.Stores;
            }
            return View();
        }

        // POST: Articles/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                ViewBag.User = this.GetUser(User.Identity);
                ViewBag.Controller = this.GetName();
                var model = new ArticleModel()
                {
                    StoreId = int.Parse(collection.Get("StoreId")),
                    Name = collection.Get("Name"),
                    Description = collection.Get("Description"),
                    Price = decimal.Parse(collection.Get("Price")),
                    TotalInShelf = int.Parse(collection.Get("TotalInShelf")),
                    TotalInVault = int.Parse(collection.Get("TotalInVault"))
                };
                var url = string.Join("/", ConfigurationManager.AppSettings["API_URL"], $"articles");
                HttpResponseMessage response = await DefaultApiClient.Client.PostAsJsonAsync(url,model);
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

        // GET: Articles/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.User = this.GetUser(User.Identity);
            ViewBag.Controller = this.GetName();
            var url = string.Join("/", ConfigurationManager.AppSettings["API_URL"], $"articles/{id}");
            HttpResponseMessage response = await DefaultApiClient.Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<ArticleResponseModel>();
                if (content.Success)
                    return View(content.Article);
            }
            return View();
        }

        // POST: Articles/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            try
            {
                ViewBag.User = this.GetUser(User.Identity);
                ViewBag.Controller = this.GetName();
                var model = new ArticleModel()
                {
                    Id = int.Parse(collection.Get("Id")),
                    StoreId = int.Parse(collection.Get("StoreId")),
                    Name = collection.Get("Name"),
                    Description = collection.Get("Description"),
                    Price = decimal.Parse(collection.Get("Price")),
                    TotalInShelf = int.Parse(collection.Get("TotalInShelf")),
                    TotalInVault = int.Parse(collection.Get("TotalInVault"))
                };
                var url = string.Join("/", ConfigurationManager.AppSettings["API_URL"], $"articles/{id}");
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

        // GET: Articles/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ViewBag.User = this.GetUser(User.Identity);
                ViewBag.Controller = this.GetName();
                var url = string.Join("/", ConfigurationManager.AppSettings["API_URL"], $"articles/{id}");
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

        // POST: Articles/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            return RedirectToAction("Index");
        }
    }
}
