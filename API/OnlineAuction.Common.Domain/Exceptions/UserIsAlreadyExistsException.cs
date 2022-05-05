using System;

namespace OnlineAuction.Common.Domain.Exceptions
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
