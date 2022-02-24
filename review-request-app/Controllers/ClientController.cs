using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using review_request_app.Core;
using review_request_app.Core.Domain;
using review_request_app.Models;
using System;
using System.IO;
using System.Net;

namespace review_request_app.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ClientController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        } 
        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Page(int id)
        {
            if(id > 0) {
                return View(_unitOfWork.Clients.Get(id));
            }
            return RedirectToAction("Index", "Home");
        } 
        
        [HttpPost]
        public IActionResult Create(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (model.Logo != null)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploadFolder");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Logo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Logo.CopyTo(new FileStream(filePath, FileMode.Create));
                    model.LogoPath = uniqueFileName;
                }

                _unitOfWork.Clients.Add(model);
                _unitOfWork.Complete();
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            ClientViewModel model = _unitOfWork.Clients.Get(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (model.Logo != null)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploadFolder");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Logo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Logo.CopyTo(new FileStream(filePath, FileMode.Create));
                    model.LogoPath = uniqueFileName;
                }

                _unitOfWork.Clients.Update(model);
                _unitOfWork.Complete();
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Client client = new Client { Id = id };
            _unitOfWork.Clients.Remove(client);
            _unitOfWork.Complete();

            return RedirectToAction("Index", "Home");
        }

    }
}
