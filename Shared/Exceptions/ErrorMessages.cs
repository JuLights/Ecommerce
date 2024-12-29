namespace Shared.Exceptions;

/// <summary>
///     Contains various error messages as enum values
/// </summary>
public enum ErrorMessages
{
    #region User Errors
    UserNotFound,
    UserNotLoggedIn,
    UserNotEnoughPermissions,
    AuthNotPermitted,
    AuthTokenInvalid,
    AuthRefreshTokenInvalid,
    #endregion
    
    #region Password Errors

    InvalidPassword,

    #endregion
}