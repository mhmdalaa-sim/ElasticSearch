namespace ElasticSearch.Models
{
    public class Document
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
