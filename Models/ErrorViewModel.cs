using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Models;

public class ErrorViewModel
{
    public string RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public string? Message { get; set; }

    public ErrorViewModel(string RequestId, string Message)
    {
        this.RequestId = RequestId;
        this.Message = Message;
    }
}