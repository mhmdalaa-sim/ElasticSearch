namespace ElasticSearch.Models.Dtos
{
    /// <summary>
    /// Data Transfer Object for creating or updating a document.
    /// </summary>
    public class CreateDocumentRequest
    {
        /// <summary>
        /// Gets or sets the title of the document.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the content of the document.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the author of the document.
        /// </summary>
        public string Author { get; set; }
    }

    /// <summary>
    /// Data Transfer Object for search requests.
    /// </summary>
    public class SearchRequest
    {
        /// <summary>
        /// Gets or sets the search query.
        /// </summary>
        public string Query { get; set; }
    }

    /// <summary>
    /// Data Transfer Object for document responses.
    /// </summary>
    public class DocumentResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the document.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the document.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the content of the document.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the author of the document.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the creation timestamp.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Data Transfer Object for API error responses.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the error code or type.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the error occurred.
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
