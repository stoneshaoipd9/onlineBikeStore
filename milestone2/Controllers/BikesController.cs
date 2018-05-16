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
    public class BikesController : Controller
    {
        private EntityContext db = new EntityContext();

        // GET: Bikes
        public ActionResult Index()
        {
            //var productCategories = db.ProductCategories.Include(p => p.ProductCategory2);
            //return View(productCategories.ToList());
            var bikeList = from p in db.ProductCategories
                           where p.ParentProductCategoryID == 1
                           select p;
            return View(bikeList);
        }

        public ActionResult Road()
        {
            var validRoadProducts = (from bikes in db.vProductAndDescriptions
                                     where bikes.Culture == "en"
                                         && bikes.SellEndDate == null
                                         && bikes.ProductCategoryID == 6
                                     select new
                                     {
                                         ProductModel = bikes.ProductModel,
                                         Description = bikes.Description,
                                         ProductModelID = bikes.ProductModelID
                                     }).Distinct().ToList();


            List<BikeListModel> bikeList = new List<BikeListModel>();

            foreach (var valids in validRoadProducts)
            {
                bikeList.Add(new BikeListModel(valids.ProductModel, valids.Description, valids.ProductModelID));
            }

            return View(bikeList);
        }

        public ActionResult Mountain()
        {
            var validMountainProducts = (from bikes in db.vProductAndDescriptions
                                     where bikes.Culture == "en"
                                         && bikes.SellEndDate == null
                                         && bikes.ProductCategoryID == 5
                                     select new
                                     {
                                         ProductModel = bikes.ProductModel,
                                         Description = bikes.Description,
                                         ProductModelID = bikes.ProductModelID
                                     }).Distinct().ToList();


            List<BikeListModel> bikeList = new List<BikeListModel>();

            foreach (var valids in validMountainProducts)
            {
                bikeList.Add(new BikeListModel(valids.ProductModel, valids.Description, valids.ProductModelID));
            }

            return View(bikeList);
        }

        public ActionResult Touring()
        {
            var validRoadProducts = (from bikes in db.vProductAndDescriptions
                                     where bikes.Culture == "en"
                                         && bikes.SellEndDate == null
                                         && bikes.ProductCategoryID == 7
                                     select new
                                     {
                                         ProductModel = bikes.ProductModel,
                                         Description = bikes.Description,
                                         ProductModelID = bikes.ProductModelID
                                     }).Distinct().ToList();


            List<BikeListModel> bikeList = new List<BikeListModel>();

            foreach (var valids in validRoadProducts)
            {
                bikeList.Add(new BikeListModel(valids.ProductModel, valids.Description, valids.ProductModelID));
            }

            return View(bikeList);
        }

        // GET: Bikes/Details/5
        public ActionResult Details(int? id)
        {
            var detailList = from d in db.Products
                             where d.ProductModelID == id
                             select d;
            
            return View(detailList);
        }
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ProductCategory productCategory = db.ProductCategories.Find(id);
        //    if (productCategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(productCategory);
        //}

        // GET: Bikes/Create
        public ActionResult Create()
        {
            ViewBag.ParentProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name");
            return View();
        }

        // POST: Bikes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductCategoryID,ParentProductCategoryID,Name,rowguid,ModifiedDate")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                db.ProductCategories.Add(productCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParentProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name", productCategory.ParentProductCategoryID);
            return View(productCategory);
        }

        // GET: Bikes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name", productCategory.ParentProductCategoryID);
            return View(productCategory);
        }

        // POST: Bikes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductCategoryID,ParentProductCategoryID,Name,rowguid,ModifiedDate")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name", productCategory.ParentProductCategoryID);
            return View(productCategory);
        }

        // GET: Bikes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // POST: Bikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductCategory productCategory = db.ProductCategories.Find(id);
            db.ProductCategories.Remove(productCategory);
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
