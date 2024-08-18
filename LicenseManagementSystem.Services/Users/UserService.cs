using AutoMapper;
using LicenseManagementSystem.DA;
using LicenseManagementSystem.Models.Models;
using Microsoft.EntityFrameworkCore;


namespace LicenseManagementSystem.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DataBaseContext _context;


        public UserService(IMapper mapper, DataBaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return _mapper.Map<List<UserModel>>(await _context.Users.ToListAsync());
        }
        public async Task<UserModel> GetUserByIdService(long id)
        {
            return _mapper.Map<UserModel>(await _context.Users.FirstOrDefaultAsync(x => x.Id == id));
        }
    }
}
