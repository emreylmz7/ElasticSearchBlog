using Elastic.Clients.Elasticsearch;
using ElasticSearchBlog.WebAPI.Models;

namespace ElasticSearchBlog.WebAPI.Repositories;

public interface IBlogPostRepository
{
    Task<CreateResponse> CreateAsync(BlogPost post, CancellationToken cancellationToken);
    Task<IEnumerable<BlogPost>> GetAllAsync(CancellationToken cancellationToken);
    Task<BlogPost?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<UpdateResponse<BlogPost>> UpdateAsync(string id, BlogPost post, CancellationToken cancellationToken);
    Task<DeleteResponse> DeleteAsync(string id, CancellationToken cancellationToken);
    Task SeedDataAsync(CancellationToken cancellationToken);
    Task<IEnumerable<BlogPost>> SearchAsync(string query, CancellationToken cancellationToken);
}