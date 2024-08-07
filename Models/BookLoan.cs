using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Models;

public class BookLoan
{
    //A book loan is a record of a book being borrowed by a member
    public int Id { get; set; }
    private Member member { get; set; }
    private Book LoanedBook { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public bool Returned { get; set; }
    public bool Overdue { get; set; }

    public BookLoan(Member member, Book LoanedBook, DateTime LoanDate, DateTime ReturnDate, bool Returned, bool Overdue)
    {
        //Edit over with later methods
        this.member = member;
        this.LoanedBook = LoanedBook;
        this.LoanDate = LoanDate;
        this.ReturnDate = ReturnDate;
        this.Returned = Returned;
        this.Overdue = Overdue;
    }

    public void ReturnedBook()
    {
        this.Returned = true;
    }

    public void CheckOverdue()
    {
        if (DateTime.Now > this.ReturnDate)
        {
            this.Overdue = true;
        }
    }

    public void CheckStatus()
    {
        if (this.Returned)
        {
            Console.WriteLine("Book Returned");
        }
        else if (this.Overdue)
        {
            Console.WriteLine("Book Overdue");
        }
        else
        {
            Console.WriteLine("Book on Loan");
        }
    }
}