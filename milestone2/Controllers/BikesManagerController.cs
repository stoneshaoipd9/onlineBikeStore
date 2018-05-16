using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using milestone2;
using milestone2.Models;

namespace milestone2.Controllers
{
    public class BikesManagerController : Controller
    {
        private EntityContext db = new EntityContext();

        // GET: BikesManager
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductCategory).Include(p => p.ProductModel);
    
            return View(products.ToList());
        }

        // GET: BikesManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Show(int? id)
        {
            Product product = db.Products.Find(id);
            byte[] imageByte = (byte[])product.ThumbNailPhoto;
            return File(imageByte, "image/jpg");
        }

        // GET: BikesManager/Create
        public ActionResult Create()
        {
            var bikeList = from p in db.ProductCategories
                           where p.ParentProductCategoryID == 1
                           select p;

            var validProducts = (from bikes in db.vProductAndDescriptions
                                 where bikes.Culture == "en"
                                     && (bikes.ProductCategoryID == 5
                                     || bikes.ProductCategoryID == 6
                                     || bikes.ProductCategoryID == 7)
                                 select new
                                 {
                                     ProductModel = bikes.ProductModel,
                                     Description = bikes.Description,
                                     ProductModelID = bikes.ProductModelID
                                 }).Distinct().ToList();


            List<BikeListModel> bikeValidList = new List<BikeListModel>();
            foreach (var valids in validProducts)
            {
                bikeValidList.Add(new BikeListModel(valids.ProductModel, valids.Description, valids.ProductModelID));
            }

            ViewBag.ProductCategoryID = new SelectList(bikeList, "ProductCategoryID", "Name");
            ViewBag.BikeModelID = new SelectList(bikeValidList, "ProductModelID", "ProductModel");
            return View();
        }

        // POST: BikesManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,ProductNumber,Color,StandardCost,ListPrice,Size,Weight,ProductCategoryID,ProductModelID,SellStartDate,SellEndDate,DiscontinuedDate,ThumbNailPhoto,ThumbnailPhotoFileName,rowguid,ModifiedDate")] Product product)
        {
            bool flag = true;
 
            product.rowguid = Guid.NewGuid();
            product.ModifiedDate = DateTime.Now;

            var nameList = from p in db.Products
                           where p.Name == product.Name
                           select p;
            if (nameList.Any())
            {
                ViewBag.errorName = "The Name is already exist!";
                flag = false;
            }
            else
            {
                ViewBag.errorName = null;
            }

            var numberList = from p in db.Products
                           where p.ProductNumber == product.ProductNumber
                           select p;
            if (numberList.Any())
            {
                ViewBag.errorNumber = "The Number is already exist!";
                flag = false;
            }
            else
            {
                ViewBag.errorNumber = null;
            }

            if (ModelState.IsValid && flag)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var bikeList = from p in db.ProductCategories
                           where p.ParentProductCategoryID == 1
                           select p;

            int selectedValue = Int32.Parse(Request.Form["ProductCategoryID"].ToString());

            var validProducts = (from bikes in db.vProductAndDescriptions
                                 where bikes.Culture == "en"
                                     && bikes.ProductCategoryID == selectedValue
                                 select new
                                 {
                                     ProductModel = bikes.ProductModel,
                                     Description = bikes.Description,
                                     ProductModelID = bikes.ProductModelID
                                 }).Distinct().ToList();

            List<BikeListModel> bikeValidList = new List<BikeListModel>();
            foreach (var valids in validProducts)
            {
                bikeValidList.Add(new BikeListModel(valids.ProductModel, valids.Description, valids.ProductModelID));
            }

            ViewBag.ProductCategoryID = new SelectList(bikeList, "ProductCategoryID", "Name", product.ProductCategoryID);
            ViewBag.BikeModelID = new SelectList(bikeValidList, "ProductModelID", "ProductModel", product.ProductModelID);
            return View(product);
        }

        // GET: BikesManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name", product.ProductCategoryID);
            ViewBag.ProductModelID = new SelectList(db.ProductModels, "ProductModelID", "Name", product.ProductModelID);
            return View(product);
        }

        // POST: BikesManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Name,ProductNumber,Color,StandardCost,ListPrice,Size,Weight,ProductCategoryID,ProductModelID,SellStartDate,SellEndDate,DiscontinuedDate,ThumbNailPhoto,ThumbnailPhotoFileName,rowguid,ModifiedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name", product.ProductCategoryID);
            ViewBag.ProductModelID = new SelectList(db.ProductModels, "ProductModelID", "Name", product.ProductModelID);
            return View(product);
        }

        // GET: BikesManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: BikesManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
