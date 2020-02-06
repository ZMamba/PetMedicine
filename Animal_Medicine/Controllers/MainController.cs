using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Animal_Medicine.Data;
using Animal_Medicine.Models;
using Animal_Medicine.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace Animal_Medicine.Controllers
{
    public class MainController : Controller
    {
        private readonly AnimalMedicineContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public MainController(AnimalMedicineContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            string petType,
            char cChar,
            int? pageIndex)
        {
            ViewData["CurrentSort"] = sortOrder;

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var disease = from d in _context.Diseases select d;
            if (!String.IsNullOrEmpty(searchString))
            {
                disease = disease.Where(s => s.Title.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(petType))
            {
                disease = disease.Where(s => s.PetType.Title == petType);
            }
            if (Char.IsLetterOrDigit(cChar))
            {
                disease = disease.Where(d => d.Title.Substring(0, 1).Contains(cChar));
            }

            var pageSize = 6;

            var paginatedDisease = new PaginatedList<Disease>(
                disease.Skip(((pageIndex ?? 1) - 1) * pageSize).Take(pageSize).ToList(),
                disease.Count(),
                pageIndex ?? 1,
                pageSize);


            var mainViewModel = new MainViewModel {
                PetTypes = _context.PetTypes.ToList(),
                Diseases = paginatedDisease
            };

            return View(mainViewModel);
        }

        [Authorize(Roles ="admin")]
        //GET CREATE
        public IActionResult CreateDisease()
        {
            var createDiseaseViewModel = new CreateDiseaseViewModel();
            createDiseaseViewModel.PetTypes = _context.PetTypes.ToList();

            return View(createDiseaseViewModel);
        }
        //POST CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDisease([Bind("Title, Description, Tags, PetTypeId")] CreateDiseaseViewModel diseaseViewModel, IFormFile pic)
        {
            if (ModelState.IsValid)
            {
                if (pic != null)
                {
                    var fileName = Path.Combine(_hostingEnvironment.WebRootPath, "images\\diseases\\" + Path.GetFileName(pic.FileName));
                    pic.CopyTo(new FileStream(fileName, FileMode.Create));
                }

                var disease = new Disease
                {
                    Title = diseaseViewModel.Title,
                    Description = diseaseViewModel.Description,
                    Tags = diseaseViewModel.Tags,
                    PetTypeId = diseaseViewModel.PetTypeId,
                    ImageName = pic.FileName
                };
                _context.Add(disease);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(diseaseViewModel);
        }

        //POST Deleta Disease Action
        [HttpPost, ActionName("DeleteDisease")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDiseaseConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var disease = await _context.Diseases.FirstOrDefaultAsync(d => d.Id == id);
            if (disease == null)
            {
                return NotFound();
            }
            _context.Remove(disease);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET Method EDIT
        public async Task<IActionResult> EditDisease(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disease = await _context.Diseases.FindAsync(id);

            if (disease == null)
            {
                return NotFound();
            }

            var editDiseaseViewModel = new EditDiseaseViewModel
            {
                PetTypes = _context.PetTypes.ToList(),
                Id = disease.Id,
                Title = disease.Title,
                Description = disease.Description,
                PetTypeId = disease.PetTypeId
            };

            return View(editDiseaseViewModel);
        }

        //POST Method EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<IActionResult> EditDisease(int id, [Bind("Title,Description,PetTypeId")] EditDiseaseViewModel editDiseaseViewModel)
        {
            var disease = await _context.Diseases.FirstOrDefaultAsync(d => d.Id == id);

            if (disease == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(disease))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {

                    throw;
                }
                return RedirectToAction("Index");
            }

            return View(editDiseaseViewModel);
        }

        public async Task<IActionResult> DetailsDisease(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disease = await _context.Diseases.Include(p => p.PetType).FirstOrDefaultAsync(d => d.Id == id);


            if (disease == null)
            {
                return NotFound();
            }

            return View(disease);
        }


        public IActionResult TestAction()
        {
            return View();
        }
    }
}