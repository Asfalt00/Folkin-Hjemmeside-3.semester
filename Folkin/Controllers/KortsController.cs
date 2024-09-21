using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Folkin.Data;
using Folkin.Models;
using Folkin.KortMangment;
using Folkin.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Folkin.Controllers
{
    public class KortsController : Controller
    {
        private readonly IKortService _service;
        private readonly UserManager<IdentityUser> _userManager;

        public KortsController(IKortService service, UserManager<IdentityUser> userManager)
        {
            _service = service;
            _userManager = userManager;

        }

        // GET: Korts
        public async Task<IActionResult> Index()
        {
            var userid = _userManager.FindByEmailAsync(User.Identity.Name).Result.Id;
            List<Kort> AlleKort = new List<Kort>();
            var dataContext = await _service.GetAllAsync(k => k.Sillhouette);

            int[] KortIds = dataContext.Select(k => k.Id).ToArray();
            
            foreach (var id in KortIds)
            {
                var kort = await _service.GetKortByIdAsync(id);
                if(kort.SpillerId == userid)
                    AlleKort.Add(kort);
            }
            ViewBag.Users = _service.GetPlayers(userid);
            return View(AlleKort);
        }

        // GET: Korts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var KortDetail = await _service.GetKortByIdAsync(id);
            return View(KortDetail);

        }

        //GET: Korts/Create
        public async Task<IActionResult> Create()
        {
            var kortDropdownsData = await _service.GetKortDropdownsValues();
            ViewBag.Tags = new SelectList(kortDropdownsData.Tags, "Id", "Titel");
            ViewBag.Types = new SelectList(kortDropdownsData.Types, "Id", "Titel");
            ViewBag.Sillhouettes = new SelectList(kortDropdownsData.Sillhouettes, "Id", "Titel");
            return View();
        }

        // POST: Korts/Create
        [HttpPost]
        public async Task<IActionResult> Create(KortViweModls KortViweModls)
        {
            if (!ModelState.IsValid)
            {
                var kortDropdownsData = await _service.GetKortDropdownsValues();
                ViewBag.Tags = new SelectList(kortDropdownsData.Tags, "Id", "Titel");
                ViewBag.Types = new SelectList(kortDropdownsData.Types, "Id", "Titel");
                ViewBag.Sillhouettes = new SelectList(kortDropdownsData.Sillhouettes, "Id", "Titel");
                return View(KortViweModls);
            }
            string user = User.Identity.Name;
            await _service.AddNewKortAsync(KortViweModls, user);
            return RedirectToAction(nameof(Index));
        }

        // GET: Korts/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var KortDetalis = await _service.GetKortByIdAsync(id);
            if (KortDetalis == null) return View("NotFound");
      
            var respons = new KortViweModls()
            {
                Id = KortDetalis.Id,
                Beskivelse = KortDetalis.Beskivelse,
                SillhouetteId = KortDetalis.SillhouetteId,
                Abstract = KortDetalis.Abstract,
                Titel = KortDetalis.Titel,
                TagIds = KortDetalis.Korts_og_tags.Select(n => n.TagId).ToList(),
                TypeIds = KortDetalis.Korts_og_Types.Select(n => n.TypeId).ToList(),
            };

            var kortDropdowns = await _service.GetKortDropdownsValues();
            ViewBag.Tags = new SelectList(kortDropdowns.Tags, "Id", "Titel");
            ViewBag.Types = new SelectList(kortDropdowns.Types, "Id", "Titel");
            ViewBag.Sillhouettes = new SelectList(kortDropdowns.Sillhouettes, "Id", "Titel");

            return View(respons);

        }

        // POST: Korts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KortViweModls kortmodel)
        {
            if (id != kortmodel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var kortDropdowns = await _service.GetKortDropdownsValues();
                ViewBag.Tags = new SelectList(kortDropdowns.Tags, "Id", "Titel");
                ViewBag.Types = new SelectList(kortDropdowns.Types, "Id", "Titel");
                ViewBag.Sillhouettes = new SelectList(kortDropdowns.Sillhouettes, "Id", "Titel");
                return View(kortmodel);
            }
                await _service.UpdateKortAsync(kortmodel);
                return RedirectToAction(nameof(Index));
        }


        // GET: Korts/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var kortDetails = await _service.GetKortByIdAsync(id);
            if (kortDetails == null) return View("NotFound");
            return View(kortDetails);
        }

        // POST: Korts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kortDetails = await _service.GetByIdAsync(id);
            if (kortDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChangeOwner(int cardId, string newUserId)
        {
            await _service.ChangeCardOwner(cardId, newUserId);


            return RedirectToAction(nameof(Index));
        }


    }
        
}
