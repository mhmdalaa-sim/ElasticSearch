# ElasticSearch API

A modern .NET 8 REST API for managing and searching documents using Elasticsearch. This project demonstrates clean code principles, design patterns, and best practices.

## 🚀 Quick Start

### Prerequisites
- Docker & Docker Compose
- .NET 8 SDK (for local development)

### Run the Application

```powershell
# Start Elasticsearch and Kibana
docker-compose up -d

# Restore dependencies and run
dotnet restore
dotnet run

# Open Swagger UI
# Navigate to: https://localhost:7000/swagger/index.html
```

## 📚 Documentation

For comprehensive documentation including architecture, design patterns, API endpoints, and troubleshooting, see:

**[📖 READ COMPREHENSIVE DOCUMENTATION](./README_COMPREHENSIVE.md)**

## ✨ Features

- ✅ Full-text search across multiple fields
- ✅ Document indexing with automatic ID generation
- ✅ Comprehensive error handling with meaningful responses
- ✅ Input validation and sanitization
- ✅ Structured logging
- ✅ Swagger/OpenAPI documentation
- ✅ Clean architecture with design patterns
- ✅ Dependency injection for testability
- ✅ Docker support for easy deployment

## 🏗️ Technology Stack

| Component | Version |
|-----------|---------|
| .NET | 8.0 |
| Elasticsearch | 8.13.0 |
| Kibana | 8.13.0 |
| NEST (Client) | 7.17.5 |
| Swagger | 6.4.0 |

## 📋 API Endpoints

### Index a Document
```
POST /api/documents
Content-Type: application/json

{
  "title": "Document Title",
  "content": "Document content here...",
  "author": "Author Name"
}
```

### Search Documents
```
GET /api/documents/search?q=search_term
```

## 🏛️ Architecture

```
Controllers (HTTP layer)
    ↓
Services (Business logic)
    ↓
Elasticsearch Client (Data access)
    ↓
Elasticsearch
```

## 🎯 Design Patterns Used

- **Dependency Injection** - Loose coupling and testability
- **Repository/Service Pattern** - Business logic abstraction
- **Data Transfer Objects (DTO)** - Request/response separation
- **Interface Segregation** - Focused abstractions
- **Structured Logging** - Debugging and monitoring

## 🛠️ Development

```powershell
# Development mode with auto-reload
dotnet watch run

# Build
dotnet build

# Clean build
dotnet clean
dotnet build
```

## 🐳 Docker Support

### Start Services
```powershell
docker-compose up -d
```

### View Logs
```powershell
docker logs -f <container_name>
```

### Stop Services
```powershell
docker-compose down
```

## 🔧 Troubleshooting

### Elasticsearch Connection Failed
```powershell
# Verify Elasticsearch is running
docker ps | grep elasticsearch

# Check health
Invoke-WebRequest http://localhost:9200 -UseBasicParsing
```

### Port Already in Use
```powershell
# Find and kill process
netstat -ano | findstr :9200
taskkill /PID <PID> /F
```

See [Comprehensive Documentation](./README_COMPREHENSIVE.md#troubleshooting) for more troubleshooting guidance.

## 📖 Full Documentation

For detailed documentation including:
- Complete setup instructions
- Architecture diagrams
- All API endpoints with examples
- Configuration guide
- Development workflow
- Deployment guidelines
- Contributing guidelines

Please refer to: **[README_COMPREHENSIVE.md](./README_COMPREHENSIVE.md)**

## 📝 Project Structure

```
├── Controllers/          # API controllers
├── Services/            # Business logic
├── Models/              # Domain models and DTOs
├── Properties/          # Launch settings
├── Program.cs           # Startup and DI configuration
├── docker-compose.yml   # Docker orchestration
└── README.md            # This file
```

## 🔐 Security Notes

- Currently unauthenticated (add JWT/OAuth for production)
- Elasticsearch security disabled in dev (enable for production)
- Always use HTTPS in production
- Implement rate limiting for public APIs
- Validate all user inputs server-side

## 📦 Dependencies

All dependencies are managed via NuGet and defined in `ElasticSearch.csproj`:

```xml
<ItemGroup>
    <PackageReference Include="NEST" Version="7.17.5" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.0" />
</ItemGroup>
```

## 🚀 Next Steps

1. **Read** the comprehensive documentation
2. **Run** `docker-compose up -d` to start services
3. **Execute** `dotnet run` to start the API
4. **Test** via Swagger UI at `https://localhost:7000/swagger/index.html`
5. **Explore** the code to understand the architecture

## 📞 Support

For issues or questions:
1. Check the comprehensive documentation's troubleshooting section
2. Verify Elasticsearch is running: `http://localhost:9200`
3. Check application logs in console or Kibana
4. Create an issue on GitHub

## 📄 License

This project is licensed under the MIT License.

---

**For complete documentation, see [README_COMPREHENSIVE.md](./README_COMPREHENSIVE.md)**
