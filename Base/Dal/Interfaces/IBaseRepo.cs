using Backend.Base.Dal.Models;
using Backend.Base.Dto;
using Microsoft.AspNetCore.JsonPatch;

namespace Backend.Base.Dal.Interfaces;

public interface IBaseRepo<T>
    where T : BaseModel
{
    public Task<List<T>> GetAllEntitiesAsync();
    public Task<T> GetEntityByIdAsync(int id);
    public Task<int> CreateEntityAsync(T dto);
    public Task<int> UpdateEntityAsync(int id, BaseUpdateDto<T> updateDto);
    public Task<int> DeleteEntityByIdAsync(int id);
}