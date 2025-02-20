using Bogus;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticSearchBlog.WebAPI.Models;

namespace ElasticSearchBlog.WebAPI.Repositories;

public class BlogPostRepository : IBlogPostRepository
{
    private readonly ElasticsearchClient _client;

    public BlogPostRepository(ElasticsearchClient client)
    {
        _client = client;
    }

    public async Task<CreateResponse> CreateAsync(BlogPost post, CancellationToken cancellationToken)
    {
        var createRequest = new CreateRequest<BlogPost>(post.Id.ToString())
        {
            Document = post
        };
        return await _client.CreateAsync(createRequest, cancellationToken);
    }

    public async Task<IEnumerable<BlogPost>> GetAllAsync(CancellationToken cancellationToken)
    {
        var searchRequest = new SearchRequest("blogposts")
        {
            Size = 100,
            Sort = new List<SortOptions>
            {
                SortOptions.Field(new Field("title.keyword"), new FieldSort { Order = SortOrder.Asc })
            }
        };
        var response = await _client.SearchAsync<BlogPost>(searchRequest, cancellationToken);
        return response.Documents;
    }

    public async Task<BlogPost?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var getRequest = new GetRequest("blogposts", id);
        var response = await _client.GetAsync<BlogPost>(getRequest, cancellationToken);
        return response.Source;
    }

    public async Task<UpdateResponse<BlogPost>> UpdateAsync(string id, BlogPost post, CancellationToken cancellationToken)
    {
        var updateRequest = new UpdateRequest<BlogPost, BlogPost>("blogposts", id)
        {
            Doc = post
        };
        return await _client.UpdateAsync(updateRequest, cancellationToken);
    }

    public async Task<DeleteResponse> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var deleteRequest = new DeleteRequest("blogposts", id);
        return await _client.DeleteAsync(deleteRequest, cancellationToken);
    }

    public async Task SeedDataAsync(CancellationToken cancellationToken)
    {
        for (int i = 0; i < 100; i++)
        {
            var faker = new Faker();
            var post = new BlogPost
            {
                Title = faker.Lorem.Sentence(),
                Content = faker.Lorem.Paragraphs(),
                Author = faker.Name.FullName(),
                Category = faker.Lorem.Word()
            };

            var createRequest = new CreateRequest<BlogPost>(post.Id.ToString())
            {
                Document = post
            };

            await _client.CreateAsync(createRequest, cancellationToken);
        }
    }

    public async Task<IEnumerable<BlogPost>> SearchAsync(string query, CancellationToken cancellationToken)
    {
        var searchRequest = new SearchRequest("blogposts")
        {
            Query = Query.MultiMatch(new MultiMatchQuery
            {
                Query = query,
                Fields = new[] { "title", "content" } // Search in both title and content
            }),
            Size = 100 // Limit the number of results
        };

        var response = await _client.SearchAsync<BlogPost>(searchRequest, cancellationToken);
        return response.Documents;
    }


}