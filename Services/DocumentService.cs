using ElasticSearch.Models;
using Nest;

namespace ElasticSearch.Services
{
    /// <summary>
    /// Service for managing document operations with Elasticsearch.
    /// Handles indexing, searching, and document retrieval.
    /// </summary>
    public class DocumentService : IDocumentService
    {
        private readonly IElasticClient _client;
        private readonly ILogger<DocumentService> _logger;

        public DocumentService(IElasticClient client, ILogger<DocumentService> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Indexes a document in Elasticsearch.
        /// </summary>
        /// <param name="doc">The document to index.</param>
        /// <exception cref="ArgumentNullException">Thrown when doc is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when indexing fails.</exception>
        public async Task IndexAsync(Document doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException(nameof(doc), "Document cannot be null.");
            }

            try
            {
                _logger.LogInformation("Attempting to index document with ID: {DocumentId}", doc.Id);

                var response = await _client.IndexDocumentAsync(doc);

                if (!response.IsValid)
                {
                    _logger.LogError("Failed to index document: {DebugInformation}", response.DebugInformation);
                    throw new InvalidOperationException($"Failed to index document: {response.ServerError?.Error?.Reason}");
                }

                _logger.LogInformation("Successfully indexed document with ID: {DocumentId}", doc.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while indexing document with ID: {DocumentId}", doc.Id);
                throw;
            }
        }

        /// <summary>
        /// Searches for documents using a multi-field query.
        /// </summary>
        /// <param name="query">The search query string.</param>
        /// <returns>Collection of matching documents.</returns>
        /// <exception cref="ArgumentNullException">Thrown when query is null or empty.</exception>
        public async Task<IEnumerable<Document>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("Query cannot be null or empty.", nameof(query));
            }

            try
            {
                _logger.LogInformation("Searching for documents with query: {Query}", query);

                var result = await _client.SearchAsync<Document>(s => s
                    .Query(q => q
                        .MultiMatch(m => m
                            .Query(query)
                            .Fields(f => f
                                .Field(p => p.Title)
                                .Field(p => p.Content)
                                .Field(p => p.Author)
                            )
                        )
                    )
                    .Highlight(h => h
                        .Fields(
                            hf => hf.Field(f => f.Content),
                            hf => hf.Field(f => f.Title)
                        )
                    )
                );

                if (!result.IsValid)
                {
                    _logger.LogError("Search failed: {DebugInformation}", result.DebugInformation);
                    throw new InvalidOperationException($"Search operation failed: {result.ServerError?.Error?.Reason}");
                }

                _logger.LogInformation("Search completed. Found {DocumentCount} documents", result.Documents.Count);
                return result.Documents;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while searching for documents with query: {Query}", query);
                throw;
            }
        }
    }
}
