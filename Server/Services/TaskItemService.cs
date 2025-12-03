using Domains.DTOs.TaskItem;
using Domains.Entities;
using Domains.Interfaces;
using Infrastractures;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class TaskItemService : ITaskItem
{
    private readonly TMSDbContext _dbContext;

    public TaskItemService(TMSDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(AddItemRequet requet , int userId)
    {
        try
        {
            var taskItem = requet.Adapt<TaskItem>();
            taskItem.UserId = userId;
            taskItem.CreatedAt = DateTimeOffset.UtcNow;

            taskItem.TaskStatusId = (int)requet.Status;
            taskItem.TaskPriortyId = (int)requet.Priority;

            await _dbContext.TaskItems.AddAsync(taskItem);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task Delete(int id)
    {
        try
        {
            var taskItem = await GetById(id);

            if (taskItem != null)
            {
                _dbContext.TaskItems.Remove(taskItem);
                await _dbContext.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<GetAllRespone>> GetAll()
    {
        try
        {
            var taskItems = await _dbContext.TaskItems
            .Include(q => q.User)
            .Include(q => q.TaskStatus)
            .Include(q => q.TaskPriorty)
            .Select(q=>new GetAllRespone()
            {
                Id = q.Id,
                Title = q.Title,
                Description = q.Description,
                DueDate = q.DueDate,
                UserName = q.User.FullName,
                Status = q.TaskStatus.Title,
                Priority = q.TaskPriorty.Title
            })
            .AsNoTracking()
            .ToListAsync();

            return taskItems;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<TaskItem> GetById(int id)
    {
        var taskItem = await _dbContext.TaskItems.Where(q => q.Id == id)
                       .FirstOrDefaultAsync();

        return taskItem;
    }

    public async Task Update(UpdateItemRequest requet)
    {

            var taskItem = await _dbContext.TaskItems
            .FirstOrDefaultAsync(t => t.Id == requet.Id);

        if (taskItem == null)
            throw new KeyNotFoundException($"Task with id {requet.Id} not found");

        // هنا السر كله: نعمل Adapt على الكائن الأصلي (مو كائن جديد)
        requet.Adapt(taskItem);

        taskItem.UpdatedAt = DateTimeOffset.UtcNow;

        // ما نحتاج نكتب .Update() لأن الكائن متتبع أصلًا
        await _dbContext.SaveChangesAsync();
        
        // var taskItem = requet.Adapt<TaskItem>();
        // taskItem.UpdatedAt = DateTimeOffset.UtcNow;
       
        // _dbContext.TaskItems.Update(taskItem);
        // return _dbContext.SaveChangesAsync();
    }
}
