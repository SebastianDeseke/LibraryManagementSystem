using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace LibraryManagementSystem.Models;

public class Book
{
    public Guid Uid { get; set; }
    public string? Title { get; set; }
    public string Author { get; set; }
    public string? ISBN { get; set; }
    public string Language { get; set; }
    public string? Genre { get; set; }

    public Book(string? Title, string Author, string? ISBN, string Language, string? Genre)
    {
        Uid = makeUid();
        this.Title = Title;
        this.Author = Author;
        this.ISBN = ISBN;
        this.Language = Language;
        this.Genre = Genre;
    }

    public Guid makeUid()
    {
        return Guid.NewGuid();
    }

    public bool checkISBN(string ISBN)
    {
        //Regex pain :(
        Regex regex = new Regex(@"^(?:ISBN(?:-1[03])?:?●)?(?=[0-9X]{10}$|(?=(?:[0-9]+[-●]){3})↵
                                [-●0-9X]{13}$|97[89][0-9]{10}$|(?=(?:[0-9]+[-●]){4})[-●0-9]{17}$)↵
                                (?:97[89][-●]?)?[0-9]{1,5}[-●]?[0-9]+[-●]?[0-9]+[-●]?[0-9X]$");
        if (regex.IsMatch(ISBN))
        {
            //Remove non ISBN characters, then split into array
            var chars = ISBN.Replace("/[- ]|^ISBN(?:-1[03])?:?/g", "").Split("");
            //Get last character for checksum
            var last = chars[^1];
            int sum = 0;
            
            return true;
        }
        else
        {
            return false;
        }
    }
}