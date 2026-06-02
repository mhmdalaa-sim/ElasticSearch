using ElasticSearch.Models;
using ElasticSearch.Models.Dtos;
using ElasticSearch.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.Controllers
{
    /// <summary>
    /// API controller for document management operations.
    /// Provides endpoints for indexing and searching documents in Elasticsearch.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _service;
        private readonly ILogger<DocumentsController> _logger;

        public DocumentsController(IDocumentService service, ILogger<DocumentsController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Creates and indexes a new document.
        /// </summary>
        /// <param name="request">The document creation request.</param>
        /// <returns>Success response with document ID.</returns>
        /// <response code="200">Document successfully indexed.</response>
        /// <response code="400">Invalid request data.</response>
        /// <response code="500">Internal server error during indexing.</response>
        [HttpPost]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] CreateDocumentRequest request)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("Add endpoint called with null request.");
                    return BadRequest(new ErrorResponse 
                    { 
                        Message = "Request body cannot be empty.",
                        Code = "INVALID_REQUEST"
                    });
                }

                if (string.IsNullOrWhiteSpace(request.Title) || 
                    string.IsNullOrWhiteSpace(request.Content) || 
                    string.IsNullOrWhiteSpace(request.Author))
                {
                    _logger.LogWarning("Add endpoint called with incomplete data.");
                    return BadRequest(new ErrorResponse 
                    { 
                        Message = "Title, Content, and Author are required fields.",
                        Code = "MISSING_FIELDS"
                    });
                }

                var doc = new Document
                {
                    Title = request.Title,
                    Content = request.Content,
                    Author = request.Author
                };

                await _service.IndexAsync(doc);

                return Ok(new { id = doc.Id, message = "Document successfully indexed." });
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Argument validation failed in Add endpoint.");
                return BadRequest(new ErrorResponse 
                { 
                    Message = ex.Message,
                    Code = "VALIDATION_ERROR"
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Elasticsearch operation failed in Add endpoint.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse 
                { 
                    Message = "Failed to index document. Please try again later.",
                    Code = "INDEXING_ERROR"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in Add endpoint.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse 
                { 
                    Message = "An unexpected error occurred.",
                    Code = "INTERNAL_ERROR"
                });
            }
        }

        /// <summary>
        /// Searches for documents using a query string.
        /// </summary>
        /// <param name="q">The search query string.</param>
        /// <returns>Collection of matching documents.</returns>
        /// <response code="200">Search successful.</response>
        /// <response code="400">Invalid query parameter.</response>
        /// <response code="500">Internal server error during search.</response>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<DocumentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string q)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(q))
                {
                    _logger.LogWarning("Search endpoint called with empty query.");
                    return BadRequest(new ErrorResponse 
                    { 
                        Message = "Search query cannot be empty.",
                        Code = "EMPTY_QUERY"
                    });
                }

                var results = await _service.SearchAsync(q);

                var response = results.Select(doc => new DocumentResponse
                {
                    Id = doc.Id,
                    Title = doc.Title,
                    Content = doc.Content,
                    Author = doc.Author,
                    CreatedAt = doc.CreatedAt
                }).ToList();

                _logger.LogInformation("Search completed. Found {Count} documents.", response.Count);

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Argument validation failed in Search endpoint.");
                return BadRequest(new ErrorResponse 
                { 
                    Message = ex.Message,
                    Code = "VALIDATION_ERROR"
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Elasticsearch operation failed in Search endpoint.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse 
                { 
                    Message = "Failed to search documents. Please try again later.",
                    Code = "SEARCH_ERROR"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in Search endpoint.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse 
                { 
                    Message = "An unexpected error occurred.",
                    Code = "INTERNAL_ERROR"
                });
            }
        }
    }
}
