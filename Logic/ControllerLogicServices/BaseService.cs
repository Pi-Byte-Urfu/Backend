//using Backend.Dal.Models;
//using Backend.DataTransferObjects;
//using Backend.Repositories;
//using Backend.Repositories.Interfaces;

//namespace Backend.Logic.ControllerLogicServices;

//public abstract class BaseService<TRepo, TModel>
//    where TRepo : BaseRepo<TModel>
//    where TModel : BaseModel
//{
//    private TRepo _baseRepo;

//    public BaseService(TRepo repo)
//    {
//        _baseRepo = repo;
//    }

//    public virtual async Task<GetDto<TModel>> GetEntityById(int id)
//    {
//        var entity = await _baseRepo.GetEntityByIdAsync(id);
//        GetDto<>
//    }

//    public virtual async Task<List<GroupModel>> GetAllEntities()
//    {
//        throw new NotImplementedException();
//    }

//    public abstract Task<int> CreateEntity(Dto accountCreatingDto);

//    public virtual async Task DeleteAccountById(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public virtual async Task<GroupModel> UpdateEntity(int id, UpdateDto<TModel> accountUpdateDto)
//    {
//        throw new NotImplementedException();
//    }
//}
