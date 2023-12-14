namespace Backend.DataTransferObjects.Base;

public abstract class Dto
{

}

public abstract class UpdateDto<TModel> : Dto
{
    public abstract TModel UpdateEntity(TModel entityToUpdate);
}
