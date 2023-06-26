namespace Application.Common.Models;

public enum ErrorCodes
{
    Undefined = 0,
    ValidationFailed = 400,
    Forbidden = 403,
    NotFound = 404,
    Unhandled = 500,
}
