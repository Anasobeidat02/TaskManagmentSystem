using Domains.DTOs;
using Domains.DTOs.User;
using Domains.Entities;
using Domains.Interfaces;
using Infrastractures;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class UserService : IUser
{
    private readonly TMSDbContext _dbContext;
    private readonly TokenService _tokenService;

    public UserService(TMSDbContext context, TokenService tokenService)
    {
        _dbContext = context;
        _tokenService = tokenService;
    }

    public async Task Add(AddUserRequest request)
    {
        try
        {
            var isExists = await IsUserExists(request.EmailAddress);
            if (!isExists)
            {
                var user = request.Adapt<User>();
                user.PasswordHash = Hash.HashText(request.Password);
                user.CreatedAt = DateTimeOffset.Now;

                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User with same email address already exists.");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<MainResponse> Delete(int id)
    {
        try
        {
            var user = await GetById(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                return new MainResponse()
                {
                    IsSuccess = true,
                    Message = "User deleted sucessfully",
                };
            }
            else
            {
                return new MainResponse()
                {
                    IsSuccess = false,
                    Message = "User not found.",
                };
            }
        }
        catch (Exception ex)
        {
            return new MainResponse()
            {
                IsSuccess = false,
                Message = ex.Message,
            };
        }
    }

    public Task<List<GetAllResponse>> GetAll()
    {
        var users = _dbContext.Users
             .AsNoTracking()
             .ProjectToType<GetAllResponse>()
             .ToListAsync();

        return users;
    }

    public async Task<User> GetById(int id)
    {
        try
        {
            var user = await _dbContext.Users.Where(q => q.Id == id)
                .FirstOrDefaultAsync();

            return user;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            throw new ArgumentNullException("Username and password are required");

        var userInfo = await _dbContext.Users.Where(q => q.EmailAddress == request.UserName)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (userInfo == null)
            throw new Exception("Invalid username or password");


        var isPasswordValid = Hash.VerifyHash(request.Password, userInfo.PasswordHash);

        if (!isPasswordValid)
            throw new Exception("Invalid username or password");

        var response = new LoginResponse()
        {
            FullName = userInfo.FullName,
            EmailAddress = userInfo.EmailAddress,
            Token = _tokenService.CreateToken(userInfo)
        }; 

        return response;
    }

    public async Task Update(UpdateUserRequest request)
    {
        try
        {
            if (request.Id == 0)
                throw new ArgumentNullException("User id is required");

            var existingUser = await _dbContext.Users.Where(q => q.Id == request.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (existingUser == null)
                throw new Exception("User not found.");

            var user = request.Adapt<User>();
            user.UpdatedAt = DateTimeOffset.Now;
            user.PasswordHash = existingUser.PasswordHash;


            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }



    #region Private Methods

    private async Task<bool> IsUserExists(string email)
    {
        var exists = await _dbContext.Users.Where(q => q.EmailAddress == email).AnyAsync();
        return exists;
    }

    #endregion
}
