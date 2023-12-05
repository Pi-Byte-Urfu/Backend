using Backend.Models;
using Backend.Repositories.Interfaces;

namespace Backend.Repositories;

public class GroupRepo : BaseRepo<GroupModel>, IGroupRepo
{
    public GroupRepo(AppDatabase database) : base(database)
    {

    }
}