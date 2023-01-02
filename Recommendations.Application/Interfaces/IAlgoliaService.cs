using Recommendations.Domain;

namespace Recommendations.Application.Interfaces;

public interface IAlgoliaService
{
    public Task AddOrUpdateRecord<T>(T entity) where T : class;
    public Task DeleteRecord<T>(T entity) where T : class;
    public Task<List<Guid>> Search(string query);
}