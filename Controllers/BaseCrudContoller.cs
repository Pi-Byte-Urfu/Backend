using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Repositories.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Backend.Controllers.Base;

public abstract class BaseCrudController<T> : ControllerBase
    where T : BaseModel
{
    private readonly IBaseRepo<T> _repo;

    public BaseCrudController(IBaseRepo<T> repo)
    {
        _repo = repo;
    }

    [HttpPost]
    public virtual async Task<IActionResult> Create([FromBody] T entity)
    {
        var id = await _repo.CreateEntityAsync(entity);
        return Ok(id);
    }

    [HttpGet]
    public virtual async Task<IActionResult> GetAll()
    {
        var entities = await _repo.GetAllEntitiesAsync();
        return Ok(entities);
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetById([FromRoute] int id)
    {
        var entity = await _repo.GetEntityByIdAsync(id);
        if (entity == null)
            return NotFound();

        return Ok(entity);
    }

    [HttpPatch("{id}")]
    public virtual async Task<IActionResult> Update([FromRoute] int id, [FromBody] JsonPatchDocument jsonPatchObject)
    {
        await _repo.UpdateEntityAsync(id, jsonPatchObject);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _repo.DeleteEntityByIdAsync(id);
        return NoContent();
    }
}