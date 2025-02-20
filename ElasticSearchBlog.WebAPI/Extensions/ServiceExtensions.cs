    using Elastic.Clients.Elasticsearch;

namespace ElasticSearchBlog.WebAPI.Extensions;

public static class ServiceExtensions
{
    public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
            .DefaultIndex("blogposts");

        var client = new ElasticsearchClient(settings);
        services.AddSingleton(client);
    }
}

