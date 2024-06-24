using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.Models;

public class Book {
    public Guid Uid { get; set; }
    public string? Title { get; set; }
    public string Author { get; set;}
    public string? ISBN { get; set; }
    public string Language { get; set; }
    public string? Genre { get; set; }

    public Book(string? Title, string Author, string? ISBN, string Language, string? Genre) {
        this.Title = Title;
        this.Author = Author;
        this.ISBN = ISBN;
        this.Language = Language;
        this.Genre = Genre;
    }
}