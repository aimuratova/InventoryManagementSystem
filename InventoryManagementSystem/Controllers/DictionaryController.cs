using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.BLL.Interfaces;

namespace InventoryManagementSystem.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly IDictionaryService _dictionaryService;
        private readonly IGeneratorService _generatorService;

        public DictionaryController(IDictionaryService dictionaryService, IGeneratorService generatorService)
        {
            _dictionaryService = dictionaryService;
            _generatorService = generatorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _dictionaryService.GetCategoriesAsync();

            return Ok(categories);
        }

        [HttpGet]
        public async Task<IActionResult> GenerateCustomId(int typeId, string value)
        {            
            return Ok(await _generatorService.Generate(typeId, value));
        }
    }
}
