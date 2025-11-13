namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;

public static class ActionResultExtensions //použití při integračních testech
{
    public static T? GetValue<T>(this ActionResult<T> result) => result.Result is null
        ? result.Value
        : (T?)(result.Result as ObjectResult)?.Value;
}

