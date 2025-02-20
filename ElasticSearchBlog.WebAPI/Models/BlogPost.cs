namespace ElasticSearchBlog.WebAPI.Models;

public class BlogPost
{
    public BlogPost()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string Author { get; set; } = default!;
    public string Category { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}
