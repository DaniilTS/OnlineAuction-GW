namespace OnlineAuction.Common.Domain
{
    public static class ExceptionConstants
    {
        public static readonly string UserIsAlreadyExists = "User with such email or phone is already exists";
        public static readonly string PasswordOrEmailIsNotRight = "Password or email is not correct";
        public static readonly string UserCreationProcessFailed = "Creation process of user has been failed";
        public static readonly string RefreshTokenIsNotValid = "Refresh token is not valid";
    }
}
