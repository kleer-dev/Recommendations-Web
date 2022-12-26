using Algolia.Search.Clients;
using Algolia.Search.Models.Search;
using Recommendations.Application.Common.Interfaces;
using Recommendations.Domain;

namespace Recommendations.Application.Common.Algolia;

public class AlgoliaService : IAlgoliaService
{
    private readonly ISearchClient _searchClient;
    private readonly ISearchIndex _searchIndex;

    public AlgoliaService(string applicationId, string adminKey, string indexName)
    {
        _searchClient = new SearchClient(applicationId, adminKey);
        _searchIndex = _searchClient.InitIndex(indexName);
    }

    public async Task AddOrUpdateRecord<T>(T entity)
        where T : class
    {
        ((dynamic)entity).ObjectID = ((dynamic)entity).Id.ToString();
        await _searchIndex.SaveObjectAsync(entity);
    }

    public async Task DeleteRecord<T>(T entity)
        where T : class
    {
        var dynamicEntity = (dynamic)entity;
        ((dynamic)entity).ObjectID = ((dynamic)entity).Id.ToString();
        await _searchIndex.DeleteObjectAsync(dynamicEntity.ObjectID);
    }

    public async Task<List<Review>> Search(string query)
    {
        var searchQuery = new Query { SearchQuery = query };
        var searchResult = await _searchIndex.SearchAsync<Review>(searchQuery);
        var reviews = searchResult.Hits;

        return reviews;
    }
}