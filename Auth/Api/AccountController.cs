﻿using Backend.Auth.Dto;
using Backend.Auth.Logic;
using Backend.Base.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Backend.Auth.Api;

[Route("api/v1/accounts")]
[ApiController]
public class AccountController
{
    private AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    //[HttpGet]
    //[Route(template: "{id}")]
    //public async Task<AccountGetDto> GetAccountById([FromRoute] int id)
    //{
    //    var account = await _accountService.GetAccountByIdAsync(id);
    //    return account;
    //}

    [HttpGet]
    [Route(template: "user")]
    public async Task<AccountGetDto> GetAccountByUserId([FromHeader] UserAuthInfo authInfo)
    {
        var account = await _accountService.GetAccountByUserIdAsync(authInfo.Id);
        return account;
    }

    [HttpGet]
    [Route(template: "user/{userId}")]
    public async Task<AccountGetDto> GetAccountByUserIdRoute([FromRoute] int userId)
    {
        var account = await _accountService.GetAccountByUserIdAsync(userId);
        return account;
    }

    //[HttpDelete]
    //[Route(template: "{id}")]
    //public async Task DeleteAccountById([FromRoute] int id)
    //{
    //    await _accountService.DeleteAccountByIdAsync(id);
    //}

    [HttpPatch]
    [Route(template: "user")]
    public async Task UpdateAccountByUserIdAsync([FromHeader] UserAuthInfo authInfo, [FromBody] AccountUpdateDto accountUpdateDto)
    {
        var account = await GetAccountByUserIdAsync(authInfo.Id);
        await _accountService.UpdateAccountByUserIdAsync(account.Id, accountUpdateDto);
    }

    [HttpPost]
    [Route(template: "{id}/photo")]
    [Consumes("multipart/form-data")]
    public async Task<IResult> UploadPhoto([FromRoute] int id, [FromForm] AccountUploadPhotoDto accountUploadPhotoDto)
    {
        await _accountService.SavePhotoOnServerAsync(id, accountUploadPhotoDto);
        return Results.Ok();
    }

    [HttpGet]
    [Route(template: "{id}/photo")]
    public async Task<IResult> GetPhoto([FromRoute] int id)
    {
        var photoBytes = await _accountService.GetPhotoFromServerAsync(id);
        return Results.File(fileContents: photoBytes, contentType: "image/png");
    }

    private async Task<AccountGetDto> GetAccountByUserIdAsync([FromRoute] int id)
    {
        return await _accountService.GetAccountByIdAsync(id);
    }
}
