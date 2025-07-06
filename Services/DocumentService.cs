using ElasticSearch.Models;
using Nest;

namespace ElasticSearch.Services
{
    public class DocumentService
    {
        private readonly IElasticClient _client;

        public DocumentService(IElasticClient client)
        {
            _client = client;
        }

        public async Task IndexAsync(Document doc) =>
        await _client.IndexDocumentAsync(doc);

        public async Task<IEnumerable<Document>> SearchAsync(string query)
        {
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

            return result.Documents;
        }
    }
}
