using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewReferenceOfIndividuals.IReferenceIndividualService;
using NewReferenceOfIndividuals.Models;
using static NewReferenceOfIndividuals.ReferenceIndividualService.Helper.PersonList;

namespace NewReferenceOfIndividuals.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReferenceIndividual _logger;
        string dbPath;

        public HomeController(IReferenceIndividual logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string identity, string personalNumber, int page=1)
        {
            var persons = _logger.GetAllPersons(new ReferenceIndividualService.Helper.GetFilter
            {
                 FindIdentificator= identity,
                  FindPersonalNumber= personalNumber,
                   Page=page,
                    PageSize=4
            });
            return View(persons);
        }

        public IActionResult persona(int x = 3, int y = 3)
        {
            var tt = _logger.GetFullPerson(x, y);

            return View(tt);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("PeopelId,Identificator, FirstNameGeo,FirstNameEn, LastNameGeo, LastNameEn, PersonalNumber, BirthDate, Addres, Mobile1, Mobile2, Picture, KinshipIdentificator, WhoIs")] Person person, List<IFormFile> files)
        {
            if(ModelState.IsValid)
            {

                Guid gid = Guid.NewGuid();
                person.Identificator = gid.ToString();

                var mm = UploadPicture(files);

                string addres = SplitName(mm);


                person.Picture = addres;

                string value =_logger.AddPerson(person);


                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Details(string Identificator)
        {
            if (Identificator == null )
            {
                ViewData["shecdoma"] = "არ არის ინდეთიფიკატორი!";
                return RedirectToAction(nameof(Index));
            }

            var brendi = _logger.GetPerson( Identificator);
            if (brendi == null)
            {
                ViewData["shecdoma"] = "არ არის ჩანაწერი!";
                return RedirectToAction(nameof(Index));
            }

            return View(brendi);
        }

        public IActionResult Edit(string Identificator)
        {
            if(Identificator == null)
            {
                return NotFound();
            }

            Person person = _logger.GetPerson(Identificator);

            if(person==null)
            {
                return NotFound();
            }
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit([Bind("PeopelId, Identificator, FirstNameGeo,FirstNameEn, LastNameGeo, LastNameEn, PersonalNumber, BirthDate, Addres, Mobile1, Mobile2, Picture, KinshipIdentificator, WhoIs")] Person person, List<IFormFile> files)
        {
            if (files.Count != 0)
            {
                var personName = person.Picture;
                DeletePicture(personName);
                var pictureName = UploadPicture(files);

                person.Picture = SplitName(pictureName);
            }

            _logger.EditPerson(person);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(string identity)
        {
            if(identity==null)
            {
                return NotFound();
            }

            Person person  = _logger.GetPerson(identity);

            if(person==null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string Identificator)
        {
            _logger.DeletePerson(Identificator);
            return RedirectToAction(nameof(Index));
        }

        public string UploadPicture(List<IFormFile> files)
        {
            try
            {
                var file = Request.Form.Files[0];

                
                Guid gid = Guid.NewGuid();

                string FileName = (gid.ToString() + file.FileName.Remove(0, file.FileName.IndexOf('.'))).ToString();

                var folderName = Path.Combine("wwwroot","Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);


                if (file.Length > 0)
                {
                    //var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, FileName);
                    dbPath = Path.Combine(folderName, FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return  dbPath;
                }
                else
                {
                    return "Error Message";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public void DeletePicture(string fileAdres)
        {
           _logger.DeletePicture(fileAdres);
        }

        public string name(string mm)
        {
           return _logger.name(mm);
        }

        public string SplitName(string mm)
        {
            var tt = mm.Split("\\");

            string addresPathe = "";


            ///Resources/Images/20170625_135101.jpg


            for (var i = 1; i < tt.Length; i++)
            {
                addresPathe += "/" + tt[i];
            };



            string addres = addresPathe;

            return addres;
        }
      

    }
}
