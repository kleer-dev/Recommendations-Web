using Algolia.Search.Clients;
using Algolia.Search.Http;
using Algolia.Search.Models.Search;
using AutoMapper;
using Newtonsoft.Json.Linq;
using Recommendations.Application.Interfaces;
using Recommendations.Domain;

namespace Recommendations.Application.Common.Clouds.Algolia;

public class AlgoliaService : IAlgoliaService
{
    private readonly ISearchClient _searchClient;
    private readonly ISearchIndex _searchIndex;

    private readonly string[] _requestedFields = { "objectID" };

    public AlgoliaService(string applicationId, string adminKey,
        string indexName)
    {
        _searchClient = new SearchClient(applicationId, adminKey);
        _searchIndex = _searchClient.InitIndex(indexName);
    }

    public async Task AddOrUpdateRecord<T>(T entity) where T : class =>
        await _searchIndex.SaveObjectAsync(entity);

    public async Task DeleteRecord<T>(T entity)
        where T : class
    {
        var dynamicEntity = (dynamic)entity;
        ((dynamic)entity).ObjectID = ((dynamic)entity).Id.ToString();
        await _searchIndex.DeleteObjectAsync(dynamicEntity.ObjectID);
    }

    public async Task<List<Guid>> Search(string query)
    {
        var searchQuery = new Query
        {
            SearchQuery = query,
            AttributesToRetrieve = _requestedFields,
            TypoTolerance = false
        };
        var searchResponse = await _searchIndex.SearchAsync<JObject>(searchQuery);

        return await ConvertResultIdsToGuid(searchResponse);
    }

    private Task<List<Guid>> ConvertResultIdsToGuid(SearchResponse<JObject> searchResponse)
    {
        var guids = searchResponse.Hits
            .Select(i => i.Value<string>(_requestedFields[0]))
            .Where(i => i != null)
            .Select(i => Guid.Parse(i!))
            .ToList();
        return Task.FromResult(guids);
    }
}