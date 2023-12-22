using Backend.Auth.Dal.Interfaces;
using Backend.Auth.Dal.Models;
using Backend.Auth.Dto;
using Backend.Auth.Enums;
using Backend.Auth.Logic.Interfaces;

namespace Backend.Auth.Logic;

public class UserService
{
    private IUserRepo _userRepo;
    private IPasswordHelperService _passwordHelper;
    private IStudentRepo _studentRepo;
    private ITeacherRepo _teacherRepo;
    private IAccountRepo _accountRepo;

    public UserService(
        IUserRepo userRepo,
        IPasswordHelperService passwordHelper,
        IStudentRepo studentRepo,
        ITeacherRepo teacherRepo,
        IAccountRepo accountRepo)
    {
        _passwordHelper = passwordHelper;
        _userRepo = userRepo;
        _studentRepo = studentRepo;
        _teacherRepo = teacherRepo;
        _accountRepo = accountRepo;
    }

    public async Task<UserAuthResponseDto> Login(UserLoginDto userLoginInfo)
    {
        var user = await _userRepo.GetUserByEmailAsync(userLoginInfo.Email);
        if (user is null)
            throw new BadHttpRequestException("There are no user with this email", 422);

        var isCorrectLoginInfo = await _passwordHelper.IsPasswordCorrectAsync(user, userLoginInfo);
        if (isCorrectLoginInfo)
            return new UserAuthResponseDto() { Id = user.Id, UserType = user.UserType };

        throw new BadHttpRequestException("Incorrect password", 400);
    }

    public async Task<UserAuthResponseDto> Register(UserRegistrationDto user)
    {
        if (user.Email == "string")
            throw new BadHttpRequestException("This email is string - test error", 402); // TestThing

        var hashedPassword = _passwordHelper.GetPasswordHash(user.Password);
        var newUser = new UserModel() { Email = user.Email, HashedPassword = hashedPassword, UserType = user.UserType };

        var id = await _userRepo.CreateEntityAsync(newUser);

        await AddToNeeededUserTypeEntityAsync(newUser);
        await _passwordHelper.AddHashedPasswordToDatabaseAsync(user.Password);

        await CreateEmptyAccountAsync(id);

        return new UserAuthResponseDto() { Id = id, UserType = newUser.UserType };
    }

    public async Task CreateEmptyAccountAsync(int userId)
    {
        var emptyAccount = new AccountModel() { 
            Name = string.Empty,
            PhotoUrl = $"{AppDomain.CurrentDomain.BaseDirectory}/static_files/photos/standard.jpg",
            Surname = string.Empty,
            UserId = userId };

        await _accountRepo.CreateEntityAsync(emptyAccount);
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