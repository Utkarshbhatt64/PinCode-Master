using PinCode_Master.DAL;
using PinCode_Master.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

 

namespace PinCode_Master.Controllers
{
    public class HomeController : Controller
    {

        Product_DAL _productDAL = new Product_DAL();
        public ActionResult Index()
        {
            List<Product> productList = _productDAL.GetAllProducts();
            return View(productList);
            //try
            //{
            //    var products = _productDAL.Products.ToList();
            //    return View(products);
            //    var productList = _productDAL.GetAllProducts();

            //    if (productList.Count == 0)
            //    {
            //        TempData["InfoMessage"] = "Currently products not available in the Database.";
            //    }
            //    return View(productList);
            //}
            //catch (Exception ex)
            //{
            //    TempData["ErrorMessage"] = ex.Message;
            //    return View();
            //}
            //return View();
        }
        //public IActionResult ExportToExcel()
        //{
        //    var products = _productDAL.GetAllProducts(); // Adjust to get your product data

        //    var data = products.Select(p => new
        //    {
        //        ProductName = p.Pincode,
        //        Price = p.City,
        //        Qty = p.State,
        //        Remarks = p.Country
        //    });

        //    return Json(data);
        //}

        public ActionResult Edit(int id)
        {
            Product product = _productDAL.GetProductsByID(id)[0];
            return View(product);
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateProducts(Product product)
        {
            try
            {
                int id = 0;

                if (ModelState.IsValid)
                {
                    id = _productDAL.UpdateProducts(product);

                    if (id > 0)
                    {
                        TempData["SuccessMessage"] = "Product details updated successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update the product.";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ID == 0)
                {
                    // New product, insert it
                    _productDAL.InsertProducts(product);
                }
                else
                {
                    // Existing product, update it
                    _productDAL.UpdateProducts(product);
                }

                TempData["SuccessMessage"] = "Product saved successfully.";
                return RedirectToAction("Index");
            }

            // If ModelState is not valid, redisplay the form
            return View("Edit", product);
        }

        public ActionResult Delete(int id)
        {
            _productDAL.DeleteProducts(id);
            TempData["SuccessMessage"] = "Product deleted successfully.";
            return RedirectToAction("Index");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}