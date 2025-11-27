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

    #region Product Errors
    ProductsDataNotFound,
    ProductDataNotFound,
    ProductNotCreated,
    ProductNotUpdated,
    ProductNotDeleted,
    
    #endregion
    
    #region Category Errors
    CategoryDataNotFound,
    CategoryNotCreated,
    CategoryNotUpdated,
    #endregion
    
    #region Static Data Errors
    ColorsDataNotFound,
    SubCategoriesDataNotFound,
    #endregion
}