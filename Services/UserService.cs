using Backend.DataTransferObjects;
using Backend.Models;
using Backend.Repositories;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services;

public class UserService : IUserService
{
    private IUserRepo _userRepo;
    private IPasswordHelperService _passwordHelper;
    private IStudentRepo _studentRepo;
    private ITeacherRepo _teacherRepo;

    public UserService(IUserRepo userRepo, IPasswordHelperService passwordHelper, IStudentRepo studentRepo, ITeacherRepo teacherRepo)
    {
        _passwordHelper = passwordHelper;
        _userRepo = userRepo;
        _studentRepo = studentRepo;
        _teacherRepo = teacherRepo;
    }

    public async Task<int> Login(UserLoginDto userLoginInfo)
    {
        var user = await _userRepo.GetUserByEmailAsync(userLoginInfo.Email);
        if (user is null)
            throw new BadHttpRequestException("There are no user with this email", 422);

        var isCorrectLoginInfo = await _passwordHelper.IsPasswordCorrectAsync(user, userLoginInfo);
        if (isCorrectLoginInfo)
            return user.Id;

        throw new BadHttpRequestException("Incorrect password", 400);
    }

    public async Task<int> Register(UserRegistrationDto user)
    {
        if (user.Email == "string")
            throw new BadHttpRequestException("This email is string - test error", 402); // TestThing

        var hashedPassword = _passwordHelper.GetPasswordHash(user.Password);
        var newUser = new UserModel() { Email = user.Email, HashedPassword = hashedPassword, UserType = user.UserType };

        var id = await _userRepo.CreateEntityAsync(newUser);

        await AddToNeeededUserTypeEntityAsync(newUser);
        await _passwordHelper.AddHashedPasswordToDatabaseAsync(user.Password);

        return id;
    }

    public async Task AddToNeeededUserTypeEntityAsync(UserModel user)
    {
        if (user.UserType is Enums.UserType.Student)
            await AddStudentEntityToDatabaseAsync(user.Id);
        else if (user.UserType is Enums.UserType.Teacher)
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