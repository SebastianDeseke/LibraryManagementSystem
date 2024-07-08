using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace LibraryManagementSystem.Models;

public class Book
{
    public enum Language
    {
        en, de, fr, nl, afr, zul, xho
    }
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public string Genre { get; set; }

    public Book(string Title, string Author, string ISBN, string Genre)
    {
        this.Title = Title;
        this.Author = Author;
        this.ISBN = CheckISBN(ISBN) ? ISBN : throw new ArgumentException("[!] Invalid ISBN");
        this.Genre = Genre;
    }

    public Book() { }

    public bool CheckISBN(string ISBN)
    {
        //Regex pain :(
        Regex regex = new Regex(@"^(?:ISBN(?:-1[03])?:?●)?(?=[0-9X]{10}$|(?=(?:[0-9]+[-●]){3})↵
                                [-●0-9X]{13}$|97[89][0-9]{10}$|(?=(?:[0-9]+[-●]){4})[-●0-9]{17}$)↵
                                (?:97[89][-●]?)?[0-9]{1,5}[-●]?[0-9]+[-●]?[0-9]+[-●]?[0-9X]$");
        if (regex.IsMatch(ISBN))
        {
            //Remove non ISBN characters, then split into array
            string cleandISBN = Regex.Replace(ISBN, "[- ]|^ISBN(?:-1[03])?:?", "");
            var chars = cleandISBN.ToCharArray();
            //Get last character for checksum
            char last = chars[^1];
            int sum = 0;
            char check;

            if (chars.Length == 9)
            {
                //Compute iSBN 10 check digit
                Array.Reverse(chars);
                for (int i = 0; i < chars.Length; i++)
                {
                    sum += (i + 2) * int.Parse(chars[i].ToString());
                }
                int checkDigit = 11 - (sum % 11);
                check = checkDigit == 10 ? 'X' : checkDigit == 11 ? '0' : checkDigit.ToString()[0];
            }
            else
            {
                //Compute iSBN 13 check digit
                for (int i = 0; i < chars.Length; i++)
                {
                    sum += (i % 2 * 2 + 1) * int.Parse(chars[i].ToString());
                }
                int checkDigit = 10 - (sum % 10);
                check = checkDigit == 10 ? '0' : checkDigit.ToString()[0];
            }

            if (check == last)
            {
                Console.WriteLine("[#] Valid ISBN");
                return true;
            }
            else
            {
                Console.WriteLine("[!] Invalid ISBN check digit");
                return false;
            }
        }
        else
        {
            Console.WriteLine("[!] Invalid ISBN input");
            return false;
        }
    }

    public string GetLanguageString(Language lang)
    {
        switch (lang)
        {
            case Language.en:
                return "English";
            case Language.de:
                return "German";
            case Language.fr:
                return "French";
            case Language.nl:
                return "Dutch";
            case Language.afr:
                return "Afrikaans";
            case Language.zul:
                return "Zulu";
            case Language.xho:
                return "Xhosa";
            default:
                return "unkown language";
        }
    }
}