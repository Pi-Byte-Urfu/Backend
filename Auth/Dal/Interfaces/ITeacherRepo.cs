using Backend.Auth.Dal.Models;
using Backend.Base.Dal.Interfaces;

namespace Backend.Auth.Dal.Interfaces;

public interface ITeacherRepo : IBaseRepo<TeacherModel>
{
    public Task<TeacherModel?> GetTeacherByUserId(int userId);
}
