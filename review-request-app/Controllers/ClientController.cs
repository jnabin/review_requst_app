using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using review_request_app.Core;
using review_request_app.Core.Domain;
using review_request_app.Models;
using System;
using System.IO;
using System.Threading.Tasks;

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
                var model = _unitOfWork.Clients.Get(id);
                ViewBag.Image = ViewImage(model.Logo);
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        } 
        
        [HttpPost]
        public async Task<IActionResult> Create(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.LogoFile == null)
                {
                    ModelState.AddModelError("LogoFile", "Logo is required");
                    return View(model);
                }

                using (var memoryStream = new MemoryStream())
                {
                    await model.LogoFile.CopyToAsync(memoryStream);

                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        model.Logo = memoryStream.ToArray();
                    }
                    else
                    {
                        ModelState.AddModelError("File", "The file is too large.");
                        return View(model);
                    }
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
            ViewBag.Image = ViewImage(model.Logo);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.LogoFile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.LogoFile.CopyToAsync(memoryStream);

                        // Upload the file if less than 2 MB
                        if (memoryStream.Length < 2097152)
                        {
                            model.Logo = memoryStream.ToArray();
                        }
                        else
                        {
                            ModelState.AddModelError("File", "The file is too large.");
                            return View(model);
                        }
                    }
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

        [NonAction]
        private string ViewImage(byte[] arrayImage)

        {

            string base64String = Convert.ToBase64String(arrayImage, 0, arrayImage.Length);

            return "data:image/png;base64," + base64String;

        }

    }
}
