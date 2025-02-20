# ElasticSearch Blog API

A simple **Minimal .NET 9 Web API** for managing blog posts with CRUD operations, providing fast and relevant search functionality powered by **ElasticSearch**.

## Features
- **CRUD Operations**: Create, Read, Update, Delete blog posts.
- **Full-Text Search**: Search blog posts by title and content using **ElasticSearch**.
- **ElasticSearch Integration**: Fast and relevant search results with scoring and highlighting.
- **Simple and Lightweight**: Built with minimal API for efficiency and ease of use.

## Technologies Used
- **.NET 9**: Backend framework for building the API.
- **ElasticSearch**: Search engine for indexing and querying blog data.
- **Docker** (optional): To run **ElasticSearch** locally.

## Getting Started

### Prerequisites

- **.NET 9 SDK**: You can download it from [here](https://dotnet.microsoft.com/download/dotnet).
- **ElasticSearch**: If you don’t have it installed, you can run it with Docker. See below for instructions.

### Run ElasticSearch with Docker

To run **ElasticSearch** in Docker, execute the following command:

```bash
docker run -d --name elasticsearch \
  -p 9200:9200 \
  -e "discovery.type=single-node" \
  -e "ELASTIC_USERNAME=elastic" \
  -e "ELASTIC_PASSWORD=changeme" \
  docker.elastic.co/elasticsearch/elasticsearch:8.10.2
```

### Running the API

1. Clone the repository:
   ```bash
   git clone https://github.com/emreylmz7/ElasticSearchBlog.git
   cd ElasticSearchBlog
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Run the application:
   ```bash
   dotnet run
   ```

The API will be available at `http://localhost:5296`.

### Endpoints

- **POST /api/blogs**: Create a new blog post.
- **GET /api/blogs**: Retrieve all blog posts.
- **GET /api/blogs/{id}**: Retrieve a specific blog post by ID.
- **PUT /api/blogs/{id}**: Update an existing blog post.
- **DELETE /api/blogs/{id}**: Delete a blog post.
- **GET /api/blogs/search**: Search for blog posts by title or content (using query parameter).

### Example Search Request

To search for a blog post with the keyword "ElasticSearch":

```bash
GET /api/blogs/search?q=ElasticSearch
```

## Contributing

Feel free to fork the repository, submit pull requests, or open issues if you encounter any problems or have suggestions.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

Bu **README.md** dosyası, projenizin kurulumunu, özelliklerini ve kullanımıyla ilgili temel bilgileri içeriyor. GitHub'da projenizi tanıtmak için uygun olacaktır!
