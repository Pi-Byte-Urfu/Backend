using Backend.Dal.Models;
using Backend.DataTransferObjects;
using Backend.Logic.ControllerLogicServices;

using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

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
    [Route(template:"{id}")]
    public async Task<AccountGetDto> GetAccountById([FromRoute] int id)
    {
        var account = await _accountService.GetAccountById(id);
        return account;
    }

    [HttpGet]
    public async Task<List<AccountGetDto>> GetAllAccounts()
    {
        var accounts = await _accountService.GetAllAccounts();
        return accounts;
    }

    [HttpPost]
    public async Task<int> CreateAccount([FromBody] AccountCreateDto accountCreatingDto)
    {
        var id = await _accountService.CreateAccount(accountCreatingDto);
        return id;
    }

    [HttpDelete]
    [Route(template: "{id}")]
    public async Task DeleteAccountById([FromRoute] int id)
    {
        await _accountService.DeleteAccountById(id);
    }

    [HttpPatch]
    [Route(template: "{id}")]
    public async Task UpdateAccount([FromRoute] int id, [FromBody] AccountUpdateDto accountUpdateDto)
    {
        await _accountService.UpdateAccount(id, accountUpdateDto);
    }
}
