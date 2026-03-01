using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.BLL.Interfaces;

namespace InventoryManagementSystem.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly IDictionaryService _dictionaryService;

        public DictionaryController(IDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _dictionaryService.GetCategoriesAsync();

            return Ok(categories);
        }
    }
}
