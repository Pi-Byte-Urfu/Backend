using Microsoft.EntityFrameworkCore;
using Backend.Repositories.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Backend.Repositories;

public abstract class BaseRepo<T> : IBaseRepo<T>
    where T : BaseModel
{
    private readonly AppDatabase _database;
    protected DbSet<T> table;

    public BaseRepo(AppDatabase database)
    {
        _database = database;
        table = _database.Set<T>();
    }

    public async Task<int> CreateEntityAsync(T dto)
    {
        var addingTransaction = await table.AddAsync(dto);
        await _database.SaveChangesAsync();

        var databaseValues = await addingTransaction.GetDatabaseValuesAsync();
        var id = (int)databaseValues["Id"];
        return id;
    }

    public async void DeleteEntityByIdAsync(int id)
    {
        table.Remove(await GetEntityByIdAsync(id));
        await _database.SaveChangesAsync();
    }

    public async Task<List<T>> GetAllEntitiesAsync()
    {
        return await table.ToListAsync();
    }

    public async Task<T> GetEntityByIdAsync(int id)
    {
        return await table.FindAsync(id);
    }

    public async void UpdateEntityAsync(int id, JsonPatchDocument jsonPatchObject)
    {
        var entity = await GetEntityByIdAsync(id);
        jsonPatchObject.ApplyTo(entity);
        await _database.SaveChangesAsync();
    }
}