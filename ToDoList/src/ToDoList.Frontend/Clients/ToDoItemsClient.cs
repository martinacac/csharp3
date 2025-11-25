namespace ToDoList.Frontend.Clients;

using ToDoList.Domain.DTOs;
using ToDoList.Frontend.Models;

public class ToDoItemsClient : IToDoItemsClient
{
    private readonly HttpClient httpClient;

    public ToDoItemsClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<ToDoItemView>> ReadItems()
    {
        var toDoItemViews = new List<ToDoItemView>();
        var response = await httpClient.GetFromJsonAsync<List<ToDoItemGetResponseDto>>("api/ToDoItems");
        toDoItemViews = response.Select(dto => new ToDoItemView(dto.Id, dto.Name, dto.Description, dto.IsCompleted)).ToList();
        //toDoItemViews = response.Result.Select(dto => new ToDoItemView(dto.Id, dto.Name, dto.Description, dto.IsCompleted)).ToList();
        //if await nedávám Result

        return toDoItemViews;
    }
}
