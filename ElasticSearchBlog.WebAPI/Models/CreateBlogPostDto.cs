namespace ElasticSearchBlog.WebAPI.Models;

record CreateBlogPostDto(
    string Title,
    string Content,
    string Author,
    string Category
    );