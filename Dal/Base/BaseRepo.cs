﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using Backend.Dal.Base.Models;
using Backend.Dal.Base.Interfaces;
using Backend.DataTransferObjects.Base;

namespace Backend.Dal.Base;

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

    public virtual async Task<int> CreateEntityAsync(T entity)
    {
        await table.AddAsync(entity);
        await _database.SaveChangesAsync();

        return entity.Id;
    }

    public virtual async Task<int> DeleteEntityByIdAsync(int id)
    {
        table.Remove(await GetEntityByIdAsync(id));
        await _database.SaveChangesAsync();
        return 0;
    }

    public virtual async Task<List<T>> GetAllEntitiesAsync()
    {
        return await table.ToListAsync();
    }

    public virtual async Task<T> GetEntityByIdAsync(int id)
    {
        return await table.FindAsync(id);
    }

    public virtual async Task<int> UpdateEntityAsync(int id, UpdateDto<T> updateDto)
    {
        var entity = await GetEntityByIdAsync(id);
        updateDto.UpdateEntity(entity);
        await _database.SaveChangesAsync();
        return id;
    }
}