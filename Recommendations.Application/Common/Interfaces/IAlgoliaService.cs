using Recommendations.Domain;

namespace Recommendations.Application.Common.Interfaces;

public interface IAlgoliaService
{
    public Task AddOrUpdateRecord<T>(T entity) where T : class;
    public Task DeleteRecord<T>(T entity) where T : class;
    public Task<List<Review>> Search(string query);
}