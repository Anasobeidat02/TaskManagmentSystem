using Domains.DTOs.TaskItem;
using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Interfaces;

public interface ITaskItem
{
    Task Add(AddItemRequet requet , int userId);
    Task Update(UpdateItemRequest requet);
    Task Delete(int id);
    Task<TaskItem> GetById(int id);
    Task<List<GetAllRespone>> GetAll();
}
