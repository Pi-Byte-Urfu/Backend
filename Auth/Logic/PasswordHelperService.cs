﻿using Backend.Auth.Dal.Interfaces;
using Backend.Auth.Dal.Models;
using Backend.Auth.Dto;
using Backend.Auth.Logic.Interfaces;

namespace Backend.Auth.Logic;

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
        var passwordHash = GetPasswordHash(userLoginInfo.Password);
        var passwordFromDatabaseObject = await _passwordRepo.GetPasswordByHash(passwordHash);
        if (passwordFromDatabaseObject is null)
            return false;

        var decryptedPassword = _encryptionService.DecryptString(passwordFromDatabaseObject.CryptedPassword);
        return userLoginInfo.Password == decryptedPassword;
    }
}
