using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ECommerceWeb.Controllers
{
    public class SliderController : Controller
    {
        dbEcommerceEntities _context;
        public SliderController()
        {
            _context=new dbEcommerceEntities();
        }
        public ActionResult SlidesList()
        {
            var slides = _context.Tble_SlideImage.ToList();
            return View(slides);
        }
        public ActionResult Details(int? id)
        {
            var slide = _context.Tble_SlideImage.Find(id);
            return View(slide);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file,Tble_SlideImage slides)
        {
            if(file!=null)
            {
                string filename = Path.GetFileName(file.FileName);
                string _filename = DateTime.Now.ToString("yymmssfff") + filename;
                string extension = Path.GetExtension(file.FileName);
                string path = Path.Combine(Server.MapPath("~/SliderImg/"), _filename);
                slides.slideImage = "~/SliderImg/" + _filename;
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    if (file.ContentLength <= 1000000)
                    {
                        _context.Tble_SlideImage.Add(slides);
                        if (_context.SaveChanges() > 0)
                        {
                            file.SaveAs(path);
                            ModelState.Clear();
                        }
                    }
                    else
                    {
                        ViewBag.msg = "File size must be Equal or greater than 1MB";
                    }
                }
                else
                {
                    ViewBag.msg = "Invalid File Type";
                }
            }
            else
            {
                ViewBag.msg = "File not Uploaded";
                return View("Create");
            }
            
            return RedirectToAction("SlidesList");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var slide = _context.Tble_SlideImage.Find(id);

            Session["imgPath"] = slide.slideImage;

            if (slide == null)
                return HttpNotFound();

            
            return View(slide);
        }

        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, Tble_SlideImage slides)
        {
            
                if (file != null && slides.SlideId>0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string _filename = DateTime.Now.ToString("yymmssfff") + filename;
                    string extension = Path.GetExtension(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/SliderImg/"), _filename);
                    slides.slideImage = "~/SliderImg/" + _filename;
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (file.ContentLength <= 1000000)
                        {
                            _context.Entry(slides).State = System.Data.Entity.EntityState.Modified;

                            if (_context.SaveChanges() > 0)
                            {
                                file.SaveAs(path);
                                TempData["msg"] = "Data Updated";
                                return RedirectToAction("SlidesList");
                            }
                        }
                        else
                        {
                            ViewBag.msg = "File size must be Equal or greater than 1MB";
                        }
                    }
                    else
                    {
                        ViewBag.msg = "Invalid File Type";
                    }
                }
                else
                {
                    slides.slideImage= Session["imgPath"].ToString();
                    _context.Entry(slides).State = System.Data.Entity.EntityState.Modified;
                    if (_context.SaveChanges() > 0)
                    {
                        TempData["msg"] = "Data Updated";
                        return RedirectToAction("SlidesList");
                    }
                }
            


            return RedirectToAction("SlidesList");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tble_SlideImage slides = _context.Tble_SlideImage.Find(id);

            if (slides == null)
                return HttpNotFound();

            _context.Tble_SlideImage.Remove(slides);
            _context.SaveChanges();

            return RedirectToAction("SlidesList");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}