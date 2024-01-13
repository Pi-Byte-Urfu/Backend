using Backend.Base.Dal;
using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Courses.Dal;

public class GroupRepo(
    AppDatabase database,
    IStudentGroupsRepo studentGroupsRepo,
    IGroupCoursesRepo groupCoursesRepo
    ) : BaseRepo<GroupModel>(database), IGroupRepo
{
    protected AppDatabase _database = database;

    public override async Task<int> DeleteEntityByIdAsync(int id)
    {
        var allStudentGroups = (await studentGroupsRepo.GetAllEntitiesAsync()).Where(x => x.GroupId == id).ToList();
        var allGroupCourses = (await groupCoursesRepo.GetAllEntitiesAsync()).Where(x => x.GroupId == id).ToList();

        foreach (var a in allGroupCourses)
            await groupCoursesRepo.DeleteEntityByIdAsync(a.Id);

        foreach (var b in allStudentGroups)
            await studentGroupsRepo.DeleteEntityByIdAsync(b.Id);

        var id2 = await base.DeleteEntityByIdAsync(id);
        await _database.SaveChangesAsync();

        return id2;
    }

    public async Task<List<GroupModel>> GetAllTeacherGroupsAsync(int teacherId)
    {
        return await table.Where(group => group.TeacherId == teacherId).ToListAsync();
    }

    public async Task<GroupModel> GetGroupByGuidAsync(string groupGuid)
    {
        return await table.FirstOrDefaultAsync(group => group.AddGuid == groupGuid);
    }
}