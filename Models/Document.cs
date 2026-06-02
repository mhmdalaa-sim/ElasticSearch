using System.ComponentModel.DataAnnotations;
using Nest;

namespace ElasticSearch.Models
{
    /// <summary>
    /// Represents a document stored in Elasticsearch.
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Gets or sets the unique identifier of the document.
        /// Auto-generated as a GUID if not provided.
        /// </summary>
        [Text(Name = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the title of the document.
        /// </summary>
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 500 characters.")]
        [Text(Analyzer = "standard")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the content of the document.
        /// </summary>
        [Required(ErrorMessage = "Content is required.")]
        [StringLength(50000, MinimumLength = 1, ErrorMessage = "Content must be between 1 and 50000 characters.")]
        [Text(Analyzer = "standard")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the author of the document.
        /// </summary>
        [Required(ErrorMessage = "Author is required.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Author must be between 1 and 200 characters.")]
        [Text(Analyzer = "standard")]
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the creation timestamp.
        /// Automatically set to UTC now if not provided.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
