using Domains.DTOs;
using Domains.DTOs.User;
using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Interfaces
{
    public interface IUser
    {
        Task Add(AddUserRequest request);
        Task Update(UpdateUserRequest request);
        Task<MainResponse> Delete(int id);
        Task<User> GetById(int id);
        Task<List<GetAllResponse>> GetAll();
        Task<LoginResponse> Login(LoginRequest request);
    }
} 