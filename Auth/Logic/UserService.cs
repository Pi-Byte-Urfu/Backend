using Backend.Auth.Dal.Interfaces;
using Backend.Auth.Dal.Models;
using Backend.Auth.Dto;
using Backend.Auth.Enums;
using Backend.Auth.Logic.Interfaces;
using Backend.Base.Services;

namespace Backend.Auth.Logic;

public class UserService
{
    private IUserRepo _userRepo;
    private IPasswordHelperService _passwordHelper;
    private IStudentRepo _studentRepo;
    private ITeacherRepo _teacherRepo;
    private IAccountRepo _accountRepo;
    private IPasswordRepo _passwordRepo;

    public UserService(
        IUserRepo userRepo,
        IPasswordHelperService passwordHelper,
        IStudentRepo studentRepo,
        ITeacherRepo teacherRepo,
        IAccountRepo accountRepo,
        IPasswordRepo passwordRepo)
    {
        _passwordHelper = passwordHelper;
        _userRepo = userRepo;
        _studentRepo = studentRepo;
        _teacherRepo = teacherRepo;
        _accountRepo = accountRepo;
        _passwordRepo = passwordRepo;
    }

    public async Task<UserAuthResponseDto> Login(UserLoginDto userLoginInfo)
    {
        var user = await _userRepo.GetUserByEmailAsync(userLoginInfo.Email);
        if (user is null)
            throw new BadHttpRequestException("Неправильный email", 422);

        var isCorrectLoginInfo = await _passwordHelper.IsPasswordCorrectAsync(user, userLoginInfo);
        if (isCorrectLoginInfo)
            return new UserAuthResponseDto() { Id = user.Id, UserType = user.UserType };

        throw new BadHttpRequestException("Неверный пароль", 400);
    }

    public async Task<UserAuthResponseDto> Register(UserRegistrationDto user)
    {
        var userWithThisEmail = await _userRepo.GetUserByEmailAsync(user.Email);
        if (userWithThisEmail is not null)
            throw new BadHttpRequestException("Пользователь с таким email уже существует", 400);

        var hashedPassword = _passwordHelper.GetPasswordHash(user.Password);
        var newUser = new UserModel() { Email = user.Email, HashedPassword = hashedPassword, UserType = user.UserType };

        var id = await _userRepo.CreateEntityAsync(newUser);

        await AddToNeeededUserTypeEntityAsync(newUser);
        await _passwordHelper.AddHashedPasswordToDatabaseAsync(user.Password);

        await CreateEmptyAccountAsync(newUser);

        return new UserAuthResponseDto() { Id = id, UserType = newUser.UserType };
    }

    public async Task CreateEmptyAccountAsync(UserModel user)
    {
        var emptyAccount = new AccountModel() {
            Email = user.Email,
            PhotoUrl = StaticFilesManager.StandardPhotoPath,

            Name = string.Empty,
            Surname = string.Empty,
            Patronymic = string.Empty,

            UserId = user.Id 
        };

        await _accountRepo.CreateEntityAsync(emptyAccount);
    }

    public async Task ChangePassword(int userId, string oldPassword, string newPassword)
    {
        var user = await _userRepo.GetEntityByIdAsync(userId);
        var loginDto = new UserLoginDto() { Email = user.Email, Password = oldPassword };
        if (!(await _passwordHelper.IsPasswordCorrectAsync(user, loginDto)))
            throw new BadHttpRequestException(statusCode: 400, message: "Неправильный старый пароль");
        
        var oldHashedPassword = _passwordHelper.GetPasswordHash(oldPassword);
        var oldPasswordModel = await _passwordRepo.GetPasswordByHash(oldHashedPassword);
        await _passwordRepo.DeleteEntityByIdAsync(oldPasswordModel.Id);

        await _passwordHelper.AddHashedPasswordToDatabaseAsync(newPassword);
    }

    public async Task AddToNeeededUserTypeEntityAsync(UserModel user)
    {
        if (user.UserType is UserType.Student)
            await AddStudentEntityToDatabaseAsync(user.Id);
        else if (user.UserType is UserType.Teacher)
            await AddTeacherEntityToDatabaseAsync(user.Id);
    }

    private async Task AddStudentEntityToDatabaseAsync(int userId)
    {
        var newStudent = new StudentModel() { UserId = userId };
        await _studentRepo.CreateEntityAsync(newStudent);
    }

    private async Task AddTeacherEntityToDatabaseAsync(int userId)
    {
        var newTeacher = new TeacherModel() { UserId = userId };
        await _teacherRepo.CreateEntityAsync(newTeacher);
    }
}