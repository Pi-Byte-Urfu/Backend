using Backend.Auth.Dal.Interfaces;
using Backend.Auth.Dal.Models;
using Backend.Auth.Dto;
using Backend.Base.Services.Interfaces;

namespace Backend.Auth.Logic
{
    /// <summary>
    /// Represents a service for managing user accounts.
    /// </summary>
    public class AccountService
    {
        private IAccountRepo _accountRepo;
        private IHttpContextAccessor _httpContextAccessor;
        private IFileManager _fileManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="accountRepo">The account repository.</param>
        /// <param name="fileManager">The file manager.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public AccountService(IAccountRepo accountRepo, IFileManager fileManager, IHttpContextAccessor httpContextAccessor)
        {
            _accountRepo = accountRepo;
            _fileManager = fileManager;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Retrieves an account by its ID.
        /// </summary>
        /// <param name="id">The ID of the account.</param>
        /// <returns>The account DTO.</returns>
        public async Task<AccountGetDto> GetAccountByIdAsync(int id)
        {
            var account = await _accountRepo.GetEntityByIdAsync(id);
            return MapEntityToGetDto(account);
        }

        /// <summary>
        /// Retrieves all accounts.
        /// </summary>
        /// <returns>A list of account DTOs.</returns>
        public async Task<List<AccountGetDto>> GetAllAccountsAsync()
        {
            var accounts = await _accountRepo.GetAllEntitiesAsync();
            return accounts.Select(MapEntityToGetDto).ToList();
        }

        /// <summary>
        /// Deletes an account by its ID.
        /// </summary>
        /// <param name="id">The ID of the account to delete.</param>
        public async Task DeleteAccountByIdAsync(int id)
        {
            await _accountRepo.DeleteEntityByIdAsync(id);
        }

        /// <summary>
        /// Updates an account by its user ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="accountUpdateDto">The updated account DTO.</param>
        public async Task UpdateAccountByUserIdAsync(int userId, AccountUpdateDto accountUpdateDto)
        {
            await _accountRepo.UpdateAccountByUserIdAsync(userId, accountUpdateDto);
        }

        /// <summary>
        /// Saves a photo on the server for the specified account.
        /// </summary>
        /// <param name="accountId">The ID of the account.</param>
        /// <param name="accountUploadPhotoDto">The DTO containing the photo to save.</param>
        public async Task SavePhotoOnServerAsync(int accountId, AccountUploadPhotoDto accountUploadPhotoDto)
        {
            var file = accountUploadPhotoDto.Photo;
            var filePath = await _fileManager.UploadFileAsync(file: file, fileType: Base.Enums.FileType.Photo);

            await _accountRepo.UpdatePhotoPathAsync(accountId, filePath);
        }

        /// <summary>
        /// Retrieves the photo of the specified account from the server.
        /// </summary>
        /// <param name="accountId">The ID of the account.</param>
        /// <returns>The photo as a byte array.</returns>
        public async Task<byte[]> GetPhotoFromServerAsync(int accountId)
        {
            var account = await _accountRepo.GetEntityByIdAsync(accountId);
            var pathToFile = account.PhotoUrl;

            return await _fileManager.GetFileBytesAsync(path: pathToFile, fileType: Base.Enums.FileType.Photo);
        }

        /// <summary>
        /// Retrieves an account by its user ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The account DTO.</returns>
        public async Task<AccountGetDto> GetAccountByUserIdAsync(int userId)
        {
            var account = await _accountRepo.GetAccountByUserIdAsync(userId);
            return MapEntityToGetDto(account);
        }

        private AccountGetDto MapEntityToGetDto(AccountModel account)
        {
            var context = _httpContextAccessor.HttpContext;
            var protocolString = context.Request.IsHttps ? "https" : "http";
            return new AccountGetDto()
            {
                Id = account.Id,
                Email = account.Email,
                Name = account.Name,
                Surname = account.Surname,
                Patronymic = account.Patronymic,
                PhotoUrl = $"{protocolString}://{context.Request.Host}/api/v1/accounts/{account.Id}/photo",
            };
        }
    }
}