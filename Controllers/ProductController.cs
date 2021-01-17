using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using crud_web_application.Models;
using crud_web_application.Storage;

namespace crud_web_application.Controllers
{
    public class ProductController : Controller
    {
        List<Product> products;

        FileManager fileManager;

        public ProductController() {
            fileManager = new FileManager();
            products = fileManager.ReadFromFile();
            if (products == null) {
                products.Add(new Product(1, "iPhone 6s Plus", 400.0D));
                products.Add(new Product(2, "iPhone 7 Plus", 450.0D));
                products.Add(new Product(3, "iPhone 8s Plus", 500.0D));
                products.Add(new Product(4, "iPhone 10 Pro", 550.9D));
                products.Add(new Product(5, "iPhone 11 Pro Max", 1.100D));
                fileManager.WriteToFile(products);
            }
        }
        public IActionResult Index() {
            ViewData.Add("products", products);
            return View();
        }
        public ActionResult<Product> Edit(int id) {
            foreach (var item in products) {
                if (item.Id == id) {
                    return View(item);
                }
            }
            TempData["message"] = "Khong co san pham nao co id: " + id;
            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public IActionResult Edit(Product productToUpdate) {
            Console.WriteLine("AnhNBT: " + productToUpdate.ToString());
            if (ModelState.IsValid) {
                int indexOfProduct = products.FindIndex(item => item.Id == productToUpdate.Id);
                if (indexOfProduct != -1)
                {
                    products[indexOfProduct] = productToUpdate;
                    
                    fileManager.WriteToFile(products);

                    TempData["message"] = "Sua san pham " + productToUpdate.Name + " thanh cong!";
                    return RedirectToAction("Index", "Product");
                } else {
                    TempData["message"] = "Khong sua duoc san pham: " + productToUpdate.Name;
                    return RedirectToAction("Index", "Product");
                }
            }
            return View(productToUpdate);
        }

        public IActionResult Details(int id) {
            foreach (var item in products) {
                if (item.Id == id) {
                    Console.WriteLine("found: " + item.Name);
                    return View(item);
                }
            }
            TempData["message"] = "Khong co san pham nao co id: " + id;
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Delete(int id) {
            int indexOfProduct = products.FindIndex(item => item.Id == id);
            if (indexOfProduct != -1) {
                Console.WriteLine("found: " + products[indexOfProduct].Name);
                return View(products[indexOfProduct]);
            }
            TempData["message"] = "Khong co san pham nao co id: " + id;
            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public IActionResult DeleteById(int id) {
            int indexOfProduct = products.FindIndex(item => item.Id == id);
            if (indexOfProduct != -1) {
                products.RemoveAt(indexOfProduct);

                fileManager.WriteToFile(products);

                TempData["message"] = "Xoa san pham thanh cong.";
            } else {
                TempData["message"] = "Khong xoa duoc san pham co id: " + id;
            }
            return Redirect("~/Product");
        }
    }
}