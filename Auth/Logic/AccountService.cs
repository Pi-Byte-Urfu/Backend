using Backend.Auth.Dal.Interfaces;
using Backend.Auth.Dal.Models;
using Backend.Auth.Dto;

namespace Backend.Auth.Logic;

public class AccountService
{
    private IAccountRepo _accountRepo;

    public AccountService(IAccountRepo accountRepo)
    {
        _accountRepo = accountRepo;
    }

    public async Task<AccountGetDto> GetAccountByIdAsync(int id)
    {
        var account = await _accountRepo.GetEntityByIdAsync(id);
        return MapEntityToGetDto(account);
    }

    public async Task<List<AccountGetDto>> GetAllAccountsAsync()
    {
        var accounts = await _accountRepo.GetAllEntitiesAsync();
        return accounts.Select(account => MapEntityToGetDto(account)).ToList();
    }

    public async Task<int> CreateAccountAsync(AccountCreateDto accountCreatingDto)
    {
        var newAccount = new AccountModel()
        {
            Name = accountCreatingDto.Name,
            Surname = accountCreatingDto.Surname,
            UserId = accountCreatingDto.UserId,
            PhotoUrl = "StandardAvatarUrl"
        };

        var id = await _accountRepo.CreateEntityAsync(newAccount);
        return id;
    }

    public async Task DeleteAccountByIdAsync(int id)
    {
        await _accountRepo.DeleteEntityByIdAsync(id);
    }

    public async Task UpdateAccountAsync(int id, AccountUpdateDto accountUpdateDto)
    {
        await _accountRepo.UpdateEntityAsync(id, accountUpdateDto);
    }

    private AccountGetDto MapEntityToGetDto(AccountModel account)
    {
        return new AccountGetDto()
        {
            Name = account.Name,
            Surname = account.Surname,
            PhotoUrl = account.PhotoUrl,
        };
    }
}
