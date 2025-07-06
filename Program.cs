
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
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IElasticClient>(sp =>
            {
                var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                    .DefaultIndex("documents");

                var client = new ElasticClient(settings);

                // Create index with analyzers (optional)
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
            builder.Services.AddScoped<DocumentService>(); // ✅ Add this line

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
