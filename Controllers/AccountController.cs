using Backend.DataTransferObjects;
using Backend.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/v1/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDatabase _database;

        public AccountController(AppDatabase database)
        {
            _database = database;
        }

        [HttpGet]
        public async Task<IResult> GetAccounts()
        {
            var accountsList = await _database.Accounts.ToListAsync();
            return Results.Ok(accountsList); // TODO: Add user to response ???
        }

        [HttpPost]
        public async Task<IResult> PostAccount(AccountCreatingDto accountCreatingDto)
        {
            var newAccount = new AccountModel()
            {
                UserId = accountCreatingDto.UserId,
                UserType = accountCreatingDto.UserType,
                Name = accountCreatingDto.Name,
                Surname = accountCreatingDto.Surname,
            };

            //if (HttpContext.Request.Form.Files.Count > 0)
            //    AddPhotoToAccount(newAccount, HttpContext.Request.Form);

            var addingToAccountTransaction = await _database.Accounts.AddAsync(newAccount);
            var gettingDatabaseValues = await addingToAccountTransaction.GetDatabaseValuesAsync();
            await _database.SaveChangesAsync(); // There are error

            var id = (int)gettingDatabaseValues["Id"];

            if (accountCreatingDto.UserType is Enums.UserType.Student)
                AddStudentEntityToDatabase(id);
            else if (accountCreatingDto.UserType is Enums.UserType.Teacher)
                AddTeacherEntityToDatabase(id);

            

            return Results.Ok(id);
        }

        //[NonAction]
        //private async void AddPhotoToAccount(AccountModel account, IFormCollection form)
        //{
        //    var file = form.Files[0];
        //    string fileName = Path.GetFileName(file.FileName);
        //    string path = "../static_files/"; // Make this normally here (in future)

        //    if (!Directory.Exists(path))
        //        Directory.CreateDirectory(path);

        //    string fullPath = Path.Combine(path, fileName);

        //    using var fileStream = new FileStream(path: fullPath, mode: FileMode.CreateNew, FileAccess.Write);
        //    await file.CopyToAsync(fileStream);

        //    account.PhotoUrl = fullPath;
        //}

        [NonAction]
        private async void AddStudentEntityToDatabase(int accountId)
        {
            var newStudent = new StudentModel() { AccountId = accountId };
            await _database.Students.AddAsync(newStudent);
            await _database.SaveChangesAsync();
        }

        [NonAction]
        private async void AddTeacherEntityToDatabase(int accountId)
        {
            var newTeacher = new TeacherModel() { AccountId = accountId };
            await _database.Teachers.AddAsync(newTeacher);
            await _database.SaveChangesAsync();
        }
    }
}
