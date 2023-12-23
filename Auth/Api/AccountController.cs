using Backend.Auth.Dto;
using Backend.Auth.Logic;
using Backend.Auth.Dal.Models;
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

    [HttpGet]
    [Route(template: "{id}")]
    public async Task<AccountGetDto> GetAccountById([FromRoute] int id)
    {
        var account = await _accountService.GetAccountByIdAsync(id);
        return account;
    }

    [HttpGet]
    public async Task<List<AccountGetDto>> GetAllAccounts()
    {
        var accounts = await _accountService.GetAllAccountsAsync();
        return accounts;
    }

    [HttpDelete]
    [Route(template: "{id}")]
    public async Task DeleteAccountById([FromRoute] int id)
    {
        await _accountService.DeleteAccountByIdAsync(id);
    }

    [HttpPatch]
    [Route(template: "user/{id}")]
    public async Task UpdateAccountByUserIdAsync([FromRoute] int id, [FromBody] AccountUpdateDto accountUpdateDto)
    {
        await _accountService.UpdateAccountByUserIdAsync(id, accountUpdateDto);
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

    [HttpGet]
    [Route(template: "user/{id}")]
    public async Task<AccountGetDto> GetAccountByUserIdAsync([FromRoute] int id)
    {
        return await _accountService.GetAccountByIdAsync(id);
    }
}
