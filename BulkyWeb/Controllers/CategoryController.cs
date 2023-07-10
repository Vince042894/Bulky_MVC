using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) //ApplicationDbContext from program.cs
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList(); //Create list to category index
            return View(objCategoryList);
        }
        public IActionResult Create()       //Create Category @ category index
        {
            return View(); //default @model new Category
        }
        [HttpPost]
		public IActionResult Create(Category obj)  //Post or create Category
		{
           if(obj.Name == obj.DisplayOrder.ToString())       //this is a Server side validation
         {
             ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name."); // error message
          }
			//if (obj.Name.ToLower() == "test")
			//{
			//	ModelState.AddModelError("", "Test is a invalid value"); // error message
			//}

			if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);						//add new category to database
                _db.SaveChanges();                              //got to database and save new categories
				TempData["success"] = "Category created successfully";
				return RedirectToAction("Index");
			}
            return View();
		}

		public IActionResult Edit(int? id)							//Edit Category
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category? categoryFromDb = _db.Categories.Find(id);							//Multiple way of editing
			//Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
			//Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();



			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb); 
		}
		[HttpPost]
		public IActionResult Edit(Category obj)  //Edit Category
		{
			
			//if (obj.Name.ToLower() == "test")
			//{
			//	ModelState.AddModelError("", "Test is a invalid value"); // error message
			//}

			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj); //Update cause of the obj
				_db.SaveChanges();      //got to database and save new categories
				TempData["success"] = "Category updated successfully";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Delete(int? id)       //Edit Category
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category? categoryFromDb = _db.Categories.Find(id);                         //Multiple way of editing
			//Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
			//Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();



			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}
		[HttpPost, ActionName("Delete")]     //same name Delete in public
		public IActionResult DeletePOST(int? id)  //Delete Category
		{

			Category? obj = _db.Categories.Find(id);
			if (obj == null) 
			{
				return NotFound();	
			}
			_db.Categories.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index");			
		}

	}
}
