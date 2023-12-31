﻿using Backend.Base.Dal.Interfaces;
using Backend.CoursePages.Dal.Models;

namespace Backend.CoursePages.Dal.Interfaces;

public interface ITaskPageRepo : IBaseRepo<TaskPageModel>
{
    public Task UpdateContent(int pageId, string content);
    public Task UpdateScore(int pageId, int maxScore);
    public Task DeleteByPageId(int pageId);
}