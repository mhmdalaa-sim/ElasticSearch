using ElasticSearch.Models;
using ElasticSearch.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentService _service;

        public DocumentsController(DocumentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Document doc)
        {
            await _service.IndexAsync(doc);
            return Ok();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string q)
        {
            var results = await _service.SearchAsync(q);
            return Ok(results);
        }
    }
}
