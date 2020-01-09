using System.Threading.Tasks;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IGroupRepository
    {
        Task<GroupUserDto> AddUserIntoGroup(GroupUserDto groupUser);
    }
}