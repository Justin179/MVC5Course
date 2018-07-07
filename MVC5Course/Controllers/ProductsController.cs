using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class ProductsController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: Products
        public ActionResult Index()
        {
            // 資料存進data
            var data = db.Product
                .OrderByDescending(p=>p.ProductId)
                .Take(10)
                .ToList();
            // 把資料傳回前端
            return View(data);
        }

        public ActionResult Index2()
        {
            // 透過程式把資料從model搬到ViewModel
            var data = db.Product
                .Where(p => p.Active == true)
                .OrderByDescending(p => p.ProductId)
                .Take(10)
                .Select(p => new ProductViewModel()
                {
                    ProductName = p.ProductName,
                    ProductId = p.ProductId,
                    Price = p.Price,
                    Stock = p.Stock
                });

            return View(data);
        }


        public ActionResult AddNewProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewProduct(ProductViewModel data)
        {
            if (!ModelState.IsValid)
            {

                return View();
            }

            var product = new Product()
            {
                // 轉(productId會自動編號，所以不管打什麼都可以)
                ProductId = data.ProductId,
                Active = true,
                Price = data.Price,
                Stock = data.Stock,
                ProductName = data.ProductName
            };
            this.db.Product.Add(product); // 對物件集合進行操作(記憶體作業)
            this.db.SaveChanges(); // 寫進db


            return RedirectToAction("Index2");
        }

        public ActionResult EditOne(int id)
        {
            // 先把資料準備好(先寫action再寫view)
            var data = db.Product.Find(id);

            return View(data);
        }

        [HttpPost]
        public ActionResult EditOne(int id, ProductViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }

            var one = db.Product.Find(id);

            one.ProductName = data.ProductName;
            one.Stock = data.Stock;
            one.Price = data.Price;

            db.SaveChanges();

            return RedirectToAction("Index2");
        }


        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
