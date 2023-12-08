﻿using Backend.DataTransferObjects;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services;

public class PasswordHelperService : IPasswordHelperService
{
    private IEncryptionService _encryptionService;
    private IPasswordRepo _passwordRepo;

    public PasswordHelperService(IEncryptionService encryptionService, IPasswordRepo passwordRepo)
    {
        _encryptionService = encryptionService;
        _passwordRepo = passwordRepo;
    }

    public int GetPasswordHash(string password) => password.GetHashCode();

    public async Task<int> AddHashedPasswordToDatabaseAsync(string password)
    {
        var hashedPassword = GetPasswordHash(password);
        var cryptedPassword = _encryptionService.EncryptString(password);

        var passwordModel = new PasswordModel() { HashedPassword = hashedPassword, CryptedPassword = cryptedPassword };
        var id = await _passwordRepo.CreateEntityAsync(passwordModel);

        return id;
    }

    public async Task<bool> IsPasswordCorrectAsync(UserModel user, UserLoginDto userLoginInfo)
    {
        var passwordFromDatabaseObject = await _passwordRepo.GetPasswordByHash(GetPasswordHash(userLoginInfo.Password));
        if (passwordFromDatabaseObject is null)
            throw new BadHttpRequestException("Can't get this password from db - (no idea why btw)", statusCode: 422);

        return userLoginInfo.Password == _encryptionService.DecryptString(passwordFromDatabaseObject.CryptedPassword);
    }
}
