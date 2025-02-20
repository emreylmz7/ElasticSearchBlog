using Elastic.Clients.Elasticsearch;
using ElasticSearchBlog.WebAPI.Extensions;
using ElasticSearchBlog.WebAPI.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSingleton<ElasticsearchClient>(provider =>
{
    var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
        .DefaultIndex("blogposts");
    return new ElasticsearchClient(settings);
});

builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.ConfigureMinimalApi();

app.Run();