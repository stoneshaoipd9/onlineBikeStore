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
    public class GearController : Controller
    {
        private EntityContext db = new EntityContext();

        // GET: Gear
        public ActionResult Index()
        {
            var gearList = from p in db.ProductCategories
                           where p.ProductCategoryID > 1
                           && p.ProductCategoryID < 5
                           select p;
            return View(gearList);
        }

        public ActionResult Components()
        {
            var componentsList = from p in db.ProductCategories
                           where p.ParentProductCategoryID == 2
                           select p;
            return View(componentsList);
        }

        public ActionResult Clothing()
        {
            var clothingList = from p in db.ProductCategories
                           where p.ParentProductCategoryID == 3
                           select p;
            return View(clothingList);
        }

        public ActionResult Accessories()
        {
            var accessoriesList = from p in db.ProductCategories
                           where p.ParentProductCategoryID == 4
                           select p;
            return View(accessoriesList);
        }
        /*
        private List<BikeListModel> GetBikeList(int id) {
            var validMountainProducts = (from bikes in db.vProductAndDescriptions
                                         where bikes.Culture == "en"
                                             && bikes.SellEndDate == null
                                             && bikes.ProductCategoryID == id
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

            return bikeList;
        }
        
        public ActionResult Components()
        {
            return View(GetBikeList(2));
        }*/


        // GET: Gear/Details/5
        public ActionResult Details(int? id)
        {
            var detailList = from d in db.Products
                             where d.ProductCategoryID == id
                             && d.SellEndDate == null
                             select d;

            return View(detailList);
        }

        // GET: Gear/Create
        public ActionResult Create()
        {
            ViewBag.ParentProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name");
            return View();
        }

        // POST: Gear/Create
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

        // GET: Gear/Edit/5
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

        // POST: Gear/Edit/5
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

        // GET: Gear/Delete/5
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

        // POST: Gear/Delete/5
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
