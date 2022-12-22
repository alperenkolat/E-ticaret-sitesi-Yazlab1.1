using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Driver;
using TicaretSitesi_yazlab.Models;
using TicaretSitesi_yazlab.Services;

namespace TicaretSitesi_yazlab.Controllers
{

    [Authorize]
    public class LaptopsController : Controller
    {

        private readonly LaptopService _laptopService;
        public LaptopsController(LaptopService laptopService) =>
       _laptopService = laptopService;
       
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(string searchString, string bttn, string[] checkBrand, string[] checkRam, string[] checkScreen, string[] checkGeneration, string[] checkOperatingSystem, float[] checkScore)
        {
          
            var laptop = await _laptopService.GetAsync();
            List<FilterDefinition<Laptop>> listSearch = new List<FilterDefinition<Laptop>>();
            if (!String.IsNullOrEmpty(searchString))
            {
                
                var SearchBrand = Builders<Laptop>.Filter.Where(x => x.Brand.Contains(searchString));
                listSearch.Add(SearchBrand);

                var SearchModelName = Builders<Laptop>.Filter.Where(x => x.ModelName.Contains(searchString));
                listSearch.Add(SearchModelName);

                var SearchWebName = Builders<Laptop>.Filter.Where(x => x.WebName.Contains(searchString));
                listSearch.Add(SearchWebName);

                var idAndStateFilter = Builders<Laptop>.Filter.Or(listSearch);
                var laptop1 = await _laptopService.GetWhereAsync(idAndStateFilter);
                if (laptop1.Count() != 0)
                {

                    laptop = laptop1;

                }
            }
            List<FilterDefinition<Laptop>> listFilter = new List<FilterDefinition<Laptop>>();

            if (checkBrand.Length != 0)
            {
                var filterBrand = Builders<Laptop>.Filter.In(x => x.Brand, checkBrand);
                listFilter.Add(filterBrand);



            }

            if (checkRam.Length != 0)
            {
                var filterRam = Builders<Laptop>.Filter.In(x => x.Ram, checkRam);
                listFilter.Add(filterRam);

            }
            if (checkScreen.Length != 0)
            {
                var filterScreen = Builders<Laptop>.Filter.In(x => x.ScreenSize, checkScreen);

                listFilter.Add(filterScreen);

            }
            if (checkGeneration.Length != 0)
            {
                var filterGeneration = Builders<Laptop>.Filter.In(x => x.OperatingType, checkGeneration);

                listFilter.Add(filterGeneration);

            }
            if (checkOperatingSystem.Length != 0)
            {
                var filterOperatingSystem = Builders<Laptop>.Filter.In(x => x.OperatingSystem, checkOperatingSystem);


                listFilter.Add(filterOperatingSystem);
            }
           
            
            
            if (checkScore.Length != 0)
            {

                var filterScore = Builders<Laptop>.Filter.Gte(x => x.Score, checkScore[checkScore.Length-1]);
                listFilter.Add(filterScore);


            }
            if (listFilter.Count != 0)
            {
                var idAndStateFilter = Builders<Laptop>.Filter.And(listFilter);
                laptop = await _laptopService.GetWhereAsync(idAndStateFilter);




            }





            if (bttn == "En Düşük Fiyat")
            {
                laptop = await _laptopService.SortGetAsync();
                laptop = laptop.OrderBy(x => x.Price).ToList();





            }
            else if (bttn == "En Yüksek Fiyat")
            {
                laptop = await _laptopService.SortRevGetAsync();
                laptop = laptop.OrderByDescending(x => x.Price).ToList();

            }
            else if (bttn == "En Yüksek Puan")
            {
                laptop = await _laptopService.SortScoreGetAsync();

            }


            if (laptop == null)
            {
                return NotFound();
            }

            var laptopss = new Laptops
            {
                partialLaptopslist = laptop,
                filtrerlaptopslist = await _laptopService.GetAsync()
            };
            return View(laptopss);

        }

        public async Task<IActionResult> AdminIndex(string searchString, string bttn, string[] checkBrand, string[] checkRam, string[] checkScreen, string[] checkGeneration, string[] checkOperatingSystem, float[] checkScore)
        {
            var laptop = await _laptopService.GetAsync();
            List<FilterDefinition<Laptop>> listSearch = new List<FilterDefinition<Laptop>>();
            if (!String.IsNullOrEmpty(searchString))
            {

                var SearchBrand = Builders<Laptop>.Filter.Where(x => x.Brand.Contains(searchString));
                listSearch.Add(SearchBrand);

                var SearchModelName = Builders<Laptop>.Filter.Where(x => x.ModelName.Contains(searchString));
                listSearch.Add(SearchModelName);

                var SearchWebName = Builders<Laptop>.Filter.Where(x => x.WebName.Contains(searchString));
                listSearch.Add(SearchWebName);

                var idAndStateFilter = Builders<Laptop>.Filter.Or(listSearch);
                var laptop1 = await _laptopService.GetWhereAsync(idAndStateFilter);
                if (laptop1.Count() != 0)
                {

                    laptop = laptop1;

                }
            }
            List<FilterDefinition<Laptop>> listFilter = new List<FilterDefinition<Laptop>>();

            if (checkBrand.Length != 0)
            {
                var filterBrand = Builders<Laptop>.Filter.In(x => x.Brand, checkBrand);
                listFilter.Add(filterBrand);



            }

            if (checkRam.Length != 0)
            {
                var filterRam = Builders<Laptop>.Filter.In(x => x.Ram, checkRam);
                listFilter.Add(filterRam);

            }
            if (checkScreen.Length != 0)
            {
                var filterScreen = Builders<Laptop>.Filter.In(x => x.ScreenSize, checkScreen);

                listFilter.Add(filterScreen);

            }
            if (checkGeneration.Length != 0)
            {
                var filterGeneration = Builders<Laptop>.Filter.In(x => x.OperatingType, checkGeneration);

                listFilter.Add(filterGeneration);

            }
            if (checkOperatingSystem.Length != 0)
            {
                var filterOperatingSystem = Builders<Laptop>.Filter.In(x => x.OperatingSystem, checkOperatingSystem);


                listFilter.Add(filterOperatingSystem);
            }



            if (checkScore.Length != 0)
            {

                var filterScore = Builders<Laptop>.Filter.Gte(x => x.Score, checkScore[checkScore.Length - 1]);
                listFilter.Add(filterScore);


            }
            if (listFilter.Count != 0)
            {
                var idAndStateFilter = Builders<Laptop>.Filter.And(listFilter);
                laptop = await _laptopService.GetWhereAsync(idAndStateFilter);




            }





            if (bttn == "En Düşük Fiyat")
            {
                laptop = await _laptopService.SortGetAsync();
                laptop = laptop.OrderBy(x => x.Price).ToList();





            }
            else if (bttn == "En Yüksek Fiyat")
            {
                laptop = await _laptopService.SortRevGetAsync();
                laptop = laptop.OrderByDescending(x => x.Price).ToList();

            }
            else if (bttn == "En Yüksek Puan")
            {
                laptop = await _laptopService.SortScoreGetAsync();

            }


            if (laptop == null)
            {
                return NotFound();
            }

            if (laptop == null)
            {
                return NotFound();
            }

            var laptopss = new Laptops
            {
                partialLaptopslist = laptop,
                filtrerlaptopslist = await _laptopService.GetAsync()
            };
            return View(laptopss);

        }
        [AllowAnonymous]
        // GET: Laptops/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _laptopService.GetAsync() == null)
            {
                return NotFound();
            }

            var laptop = await _laptopService.GetAsync(id);
              
            if (laptop == null)
            {
                return NotFound();
            }

            return View(laptop);
        }

        // GET: Laptops/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Laptops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Brand,ModelName,OperatingSystem,OperatingType,OperatingGeneration,Ram,DiskSize,ScreenSize,Score,Price,Photo,WebName,Weblink,Svg")] Laptop laptop)
        {
            if (ModelState.IsValid)
            {
               await _laptopService.CreateAsync(laptop);

                return RedirectToAction(nameof(AdminIndex));
            }
            return View(laptop);
        }

        // GET: Laptops/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _laptopService.GetAsync() == null)
            {
                return NotFound();
            }

            var laptop = await _laptopService.GetAsync(id);
            if (laptop == null)
            {
                return NotFound();
            }
            return View(laptop);
        }

        // POST: Laptops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Brand,ModelName,OperatingSystem,OperatingType,OperatingGeneration,Ram,DiskSize,ScreenSize,Score,Price,Photo,WebName,Weblink,Svg")] Laptop laptop)
        {
            if (id != laptop.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                  await _laptopService.UpdateAsync(id, laptop);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaptopExists(laptop.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                      
                        throw;
                    }
                }
                return RedirectToAction(nameof(AdminIndex));
            }
            return View(laptop);
        }

        // GET: Laptops/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _laptopService.GetAsync() == null)
            {
                return NotFound();
            }

             await _laptopService.RemoveAsync(id);
            return RedirectToAction(nameof(AdminIndex));

        }

        // POST: Laptops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_laptopService.GetAsync() == null)
            {
                return Problem("Entity set 'Yazlab1_1Context.Laptop'  is null.");
            }
            var laptop = await _laptopService.GetAsync(id);
            if (laptop != null)
            {
             await   _laptopService.RemoveAsync(id);
            }

        
            return RedirectToAction(nameof(AdminIndex));
        }

        private bool LaptopExists(string id)
        {
           var laptop= _laptopService.GetAsync(id);
           if (laptop==null)
            {

                return false ;
            }
            return true;
        }

       

        public async Task<List<Laptop>> Search()

        {
            var rwww = "ma";


           
            var filter1 = Builders<Laptop>.Filter.Where(x => x.Brand.Contains(rwww));


            var asss = await _laptopService.GetWhereAsync(filter1);

            //var filter2 = Builders<Laptop>.Filter.Where(x => x.Weblink.Contains(searchKey));


            //viewModel.LaptopList2 = await _laptopService.GetWhereAsync(filter1);

            //var filter3 = Builders<Laptop>.Filter.Where(x => x.WebName.Contains(searchKey));




            return asss;
        }

    }
}
