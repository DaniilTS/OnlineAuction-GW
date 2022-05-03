using System;

namespace OnlineAuction.API.Exceptions
{
    public class UserIsAlreadyExistsException: Exception
    {
        public UserIsAlreadyExistsException()
        {
        }

        public UserIsAlreadyExistsException(string message)
            : base(message)
        {
        }
    }
}
