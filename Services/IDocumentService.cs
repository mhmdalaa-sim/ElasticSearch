using ElasticSearch.Models;

namespace ElasticSearch.Services
{
    /// <summary>
    /// Interface for document operations with Elasticsearch.
    /// Provides abstraction for document indexing and searching.
    /// </summary>
    public interface IDocumentService
    {
        /// <summary>
        /// Indexes a document in Elasticsearch.
        /// </summary>
        /// <param name="doc">The document to index.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task IndexAsync(Document doc);

        /// <summary>
        /// Searches for documents using a query string.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>Collection of matching documents.</returns>
        Task<IEnumerable<Document>> SearchAsync(string query);
    }
}
