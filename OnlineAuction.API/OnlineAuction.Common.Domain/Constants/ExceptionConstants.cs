﻿namespace OnlineAuction.Common.Domain
{
    public static class ExceptionConstants
    {
        public static readonly string UserIsAlreadyExists = "User with such email or phone is already exists";
        public static readonly string PasswordOrEmailIsNotRight = "Password or email is not correct";
        public static readonly string UserCreationProcessFailed = "Creation process of user has been failed";
        public static readonly string RefreshTokenIsNotValid = "Refresh token is not valid";
        public static readonly string UserIsBlocked = "Authentication Error.\nUser is blocked. Write to support to know additional info";
        public static readonly string UserCreationProccessFailed = "User creation proccess has been failed";
        public static readonly string LotCreationProccessFailed = "Lot creation proccess has been failed";
        public static readonly string PhotoUploadFailed = "Photo uploading has been failed";
    }
}
