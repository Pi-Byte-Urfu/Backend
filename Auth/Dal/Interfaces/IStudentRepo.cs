using Backend.Auth.Dal.Models;
using Backend.Base.Dal.Interfaces;

namespace Backend.Auth.Dal.Interfaces;

public interface IStudentRepo : IBaseRepo<StudentModel>
{
    public Task<StudentModel> GetStudentByUserId(int userId);
}
