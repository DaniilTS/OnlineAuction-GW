using OnlineAuction.DBAL.Repositories;
using System;
using System.Threading.Tasks;

namespace OnlineAuction.API.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task SetBlockedState(Guid userId, bool state)
        {
            var user = await _userRepository.GetObject(userId);
            user.IsBlocked = state;
            await _userRepository.UpdateObject(user);
        }

        public async Task SetDeletedState(Guid userId, bool state)
        {
            var user = await _userRepository.GetObject(userId);
            user.IsDeleted = state;
            await _userRepository.UpdateObject(user);
        }
    }
}
