using Backend.Auth.Dal.Models;
using Backend.Base.Dto;

namespace Backend.Auth.Dto;

public class AccountGetDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhotoUrl { get; set; }
}

public class AccountCreateDto : BaseDto
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}

public class AccountUpdateDto : BaseUpdateDto<AccountModel>
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

public class AccountUploadPhotoDto : BaseDto
{
    public IFormFile Photo { get; set;  }
}