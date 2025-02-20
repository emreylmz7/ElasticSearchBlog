using Bogus;
using Elastic.Clients.Elasticsearch;
using ElasticSearchBlog.WebAPI.Models;
using ElasticSearchBlog.WebAPI.Repositories;

namespace ElasticSearchBlog.WebAPI.Extensions;

public static class WebApplicationExtensions
{
    public static void ConfigureMinimalApi(this WebApplication app)
    {
        // Create a new blog post
        app.MapPost("/posts", async (CreateBlogPostDto request, IBlogPostRepository repository, CancellationToken cancellationToken) =>
        {
            var post = new BlogPost
            {
                Title = request.Title,
                Content = request.Content,
                Author = request.Author,
                Category = request.Category
            };

            await repository.CreateAsync(post, cancellationToken);
            return Results.Created($"/posts/{post.Id}", post);
        });

        // Get all blog posts
        app.MapGet("/posts", async (IBlogPostRepository repository, CancellationToken cancellationToken) =>
        {
            var posts = await repository.GetAllAsync(cancellationToken);
            return Results.Ok(posts);
        });

        // Get a single blog post by ID
        app.MapGet("/posts/{id}", async (string id, IBlogPostRepository repository, CancellationToken cancellationToken) =>
        {
            var post = await repository.GetByIdAsync(id, cancellationToken);
            if (post == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(post);
        });

        // Update a blog post by ID
        app.MapPut("/posts/{id}", async (string id, UpdateBlogPostDto request, IBlogPostRepository repository, CancellationToken cancellationToken) =>
        {
            var existingPost = await repository.GetByIdAsync(id, cancellationToken);
            if (existingPost == null)
            {
                return Results.NotFound();
            }

            existingPost.Title = request.Title;
            existingPost.Content = request.Content;
            existingPost.Author = request.Author;
            existingPost.Category = request.Category;

            await repository.UpdateAsync(id, existingPost, cancellationToken);
            return Results.NoContent();
        });

        // Delete a blog post by ID
        app.MapDelete("/posts/{id}", async (string id, IBlogPostRepository repository, CancellationToken cancellationToken) =>
        {
            var existingPost = await repository.GetByIdAsync(id, cancellationToken);
            if (existingPost == null)
            {
                return Results.NotFound();
            }

            await repository.DeleteAsync(id, cancellationToken);
            return Results.NoContent();
        });

        // Seed data with fake blog posts
        app.MapPost("/seedposts", async (IBlogPostRepository repository, CancellationToken cancellationToken) =>
        {
            await repository.SeedDataAsync(cancellationToken);
            return Results.Ok("Data seeding completed.");
        });

        // Search for blog posts by title or content
        app.MapGet("/api/blogs/search", async (string query, IBlogPostRepository repository, CancellationToken cancellationToken) =>
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Results.BadRequest("Query parameter is required.");
            }

            var posts = await repository.SearchAsync(query, cancellationToken);
            return Results.Ok(posts);
        });
    }
}