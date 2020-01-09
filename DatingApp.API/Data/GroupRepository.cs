using System;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DataContext _context;
        public GroupRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<GroupUserDto> AddUserIntoGroup(GroupUserDto groupUser)
        {
            var checkBool = CheckUserInGroup(groupUser.GroupName, groupUser.UserName);
            if(checkBool == true) 
            {
                return null;
            }
            GroupUser obj = new GroupUser();
            obj.GroupName = groupUser.GroupName;
            obj.UserName = groupUser.UserName;
            await _context.GroupUsers.AddAsync(obj);
            await _context.SaveChangesAsync();
            return groupUser;
        }

        private bool CheckUserInGroup(string groupName, string userName)
        {
            var groupUser = _context.GroupUsers.FirstOrDefault(x => x.GroupName == groupName && x.UserName == userName);
            if(groupUser != null) 
            {
                return true;
            }
            return false;
        }
    }
}