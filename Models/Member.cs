using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace LibraryManagementSystem.Models;

public class Member {
    //A member is a person who can borrow booksfrom the library
    //Maybe add more information about he books they take/like
    public Guid Id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }

    public Member (string firstName, string lastName, string email, string phoneNumber, DateTime DateOfBirth) {
        this.Id = MakeID();
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.DateOfBirth = DateOfBirth;
    }

    public Guid MakeID (){
        return Guid.NewGuid();
    }

    public void CheckEmail (string email) {
        EmailAddressAttribute emailCheck = new();
        if (emailCheck.IsValid(email)) {
            Console.WriteLine("Email is valid");
        } else {
            Console.WriteLine("Email is not valid");
        }
    }
}