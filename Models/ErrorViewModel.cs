using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.Models;

public class ErrorViewModel
{
    public string RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}