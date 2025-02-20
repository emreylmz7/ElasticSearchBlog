namespace ElasticSearchBlog.WebAPI.Models;
record UpdateBlogPostDto(
    Guid Id,
    string Title,
    string Content,
    string Author,
    string Category
    );
