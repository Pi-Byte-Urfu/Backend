using Backend.Controllers.Base;
using Backend.DataTransferObjects;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services;
using Backend.Services.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using NuGet.Protocol;

namespace Backend.Controllers
{
    [Route("api/v1/accounts")]
    [ApiController]
    public class AccountController : BaseCrudController<AccountModel>
    {
        public AccountController(IAccountRepo repo) : base(repo)
        {
        }
    }
}
