using Backend.Dal.Auth.Models;
using Backend.DataTransferObjects.Base;

namespace Backend.DataTransferObjects.Auth;

public class AccountGetDto : Dto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhotoUrl { get; set; }
}

public class AccountCreateDto : Dto
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}

public class AccountUpdateDto : UpdateDto<AccountModel>
{
    public string Name { get; set; }
    public string Surname { get; set; }

    public override AccountModel UpdateEntity(AccountModel entityToUpdate)
    {
        if (Name is not null)
            entityToUpdate.Name = Name;
        if (Surname is not null)
            entityToUpdate.Surname = Surname;

        return entityToUpdate;
    }
}