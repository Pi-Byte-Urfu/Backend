using Backend.Auth.Dal.Interfaces;
using Backend.Auth.Dal.Models;
using Backend.Auth.Dto;
using Backend.Base.Services.Interfaces;

namespace Backend.Auth.Logic;

public class AccountService
{
    private IAccountRepo _accountRepo;
    private IHttpContextAccessor _httpContextAccessor;
    private IFileManager _fileManager;
    private IUserIdGetter _userIdGetter;

    public AccountService(IAccountRepo accountRepo, IFileManager fileManager, IHttpContextAccessor httpContextAccessor, IUserIdGetter userIdGetter)
    {
        _accountRepo = accountRepo;
        _fileManager = fileManager;
        _httpContextAccessor = httpContextAccessor;
        _userIdGetter = userIdGetter;
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

    public async Task UpdateAccountByUserIdAsync(int userId, AccountUpdateDto accountUpdateDto)
    {
        await _accountRepo.UpdateAccountByUserIdAsync(userId, accountUpdateDto);
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
            PhotoUrl = $"{protocolString}://{context.Request.Host}/api/v1/accounts/user/photo",
        };
    }

    public async Task SavePhotoOnServerAsync(int accountId, AccountUploadPhotoDto accountUploadPhotoDto)
    {
        var file = accountUploadPhotoDto.Photo;
        var filePath = await _fileManager.UploadFileAsync(file: file, fileType: Base.Enums.FileType.Photo);

        await _accountRepo.UpdatePhotoPathAsync(accountId, filePath);
    }

    public async Task<byte[]> GetPhotoFromServerAsync(int accountId)
    {
        var account = await _accountRepo.GetEntityByIdAsync(accountId);
        var pathToFile = account.PhotoUrl;

        return await _fileManager.GetFileBytesAsync(path: pathToFile, fileType: Base.Enums.FileType.Photo);
    }

    public async Task<AccountGetDto> GetAccountByUserIdAsync(int userId)
    {
        var account = await _accountRepo.GetAccountByUserIdAsync(userId);
        return MapEntityToGetDto(account);
    }
}