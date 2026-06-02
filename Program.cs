
using ElasticSearch.Models;
using ElasticSearch.Services;
using Nest;

namespace ElasticSearch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "ElasticSearch API",
                    Version = "v1",
                    Description = "API for managing and searching documents with Elasticsearch"
                });
            });

            // Register Elasticsearch client as singleton
            builder.Services.AddSingleton<IElasticClient>(sp =>
            {
                var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                    .DefaultIndex("documents");

                var client = new ElasticClient(settings);

                // Create index with analyzers if it doesn't exist
                if (!client.Indices.Exists("documents").Exists)
                {
                    client.Indices.Create("documents", c => c
                        .Map<Document>(m => m
                            .AutoMap()
                            .Properties(p => p
                                .Text(t => t
                                    .Name(n => n.Content)
                                    .Analyzer("standard"))
                            )
                        )
                    );
                }

                return client;
            });

            // Register DocumentService with interface abstraction
            builder.Services.AddScoped<IDocumentService, DocumentService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ElasticSearch API v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
