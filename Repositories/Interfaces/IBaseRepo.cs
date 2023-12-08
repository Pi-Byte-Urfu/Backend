using Backend.Models;

using Microsoft.AspNetCore.JsonPatch;

namespace Backend.Repositories.Interfaces;

public interface IBaseRepo<T>
    where T : BaseModel
{
    public Task<List<T>> GetAllEntitiesAsync();
    public Task<T> GetEntityByIdAsync(int id);
    public Task<int> CreateEntityAsync(T dto);
    public Task<int> UpdateEntityAsync(int id, JsonPatchDocument jsonPatchObject);
    public Task<int> DeleteEntityByIdAsync(int id);
}