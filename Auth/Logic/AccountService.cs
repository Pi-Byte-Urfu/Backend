using Backend.Auth.Dal.Interfaces;
using Backend.Auth.Dal.Models;
using Backend.Auth.Dto;

namespace Backend.Auth.Logic;

public class AccountService
{
    private IAccountRepo _accountRepo;
    private IHttpContextAccessor _httpContextAccessor;

    public AccountService(IAccountRepo accountRepo, IHttpContextAccessor httpContextAccessor)
    {
        _accountRepo = accountRepo;
        _httpContextAccessor = httpContextAccessor;
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
        var context = _httpContextAccessor.HttpContext;
        var protocolString = context.Request.IsHttps ? "https" : "http";
        return new AccountGetDto()
        {
            Id = account.Id,
            Name = account.Name,
            Surname = account.Surname,
            PhotoUrl = $"{protocolString}://{context.Request.Host}/api/v1/accounts/{account.Id}/photo",
        };
    }

    public async Task SavePhotoOnServerAsync(int accountId, AccountUploadPhotoDto accountUploadPhotoDto)
    {
        var file = accountUploadPhotoDto.Photo;
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        CreateDirectoryIfNotExists(baseDirectory, "static_files/photos");
        var photosDirectory = $"{baseDirectory}/static_files/photos";
        var guid = GetRandomGuid();

        var filePath = GeneratePath(photosDirectory, guid);

        using var fileStream = new FileStream(path: filePath, mode: FileMode.CreateNew);
        await file.CopyToAsync(fileStream);

        await _accountRepo.UpdatePhotoPathAsync(accountId, filePath);
    }

    public async Task<byte[]> GetPhotoFromServerAsync(int accountId)
    {
        var account = await _accountRepo.GetEntityByIdAsync(accountId);
        var pathToFile = account.PhotoUrl;

        using var fileStream = new FileStream(path: pathToFile, mode: FileMode.Open);
        var bytesAmount = fileStream.Length;
        var photoBytes = new byte[bytesAmount];
        await fileStream.ReadAsync(photoBytes);

        return photoBytes;
    }

    public async Task<AccountGetDto> GetAccountByUserIdAsync(int userId)
    {
        var account = await _accountRepo.GetAccountByUserIdAsync(userId);
        return MapEntityToGetDto(account);
    } 

    private string GeneratePath(string directory, string name)
    {
        return Path.Combine(directory, name);
    }

    private string GetRandomGuid()
    {
        return Guid.NewGuid().ToString("N");
    }

    private void CreateDirectoryIfNotExists(string path, string directoryName)
    {
        foreach (var directory in Directory.GetDirectories(path))
            if (directory == directoryName)
                return;

        Directory.CreateDirectory($"{path}/{directoryName}");
    }
}
