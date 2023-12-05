using Backend.Models;

using Microsoft.AspNetCore.JsonPatch;

namespace Backend.Repositories.Interfaces;

public interface IBaseRepo<T>
    where T : BaseModel
{
    public Task<List<T>> GetAllEntitiesAsync();
    public Task<T> GetEntityByIdAsync(int id);
    public Task<int> CreateEntityAsync(T dto);
    public void UpdateEntityAsync(int id, JsonPatchDocument jsonPatchObject);
    public void DeleteEntityByIdAsync(int id);
}