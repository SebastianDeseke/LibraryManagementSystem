using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data;
//using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers;

[Authorize]
public class DbController : Controller{
    MySqlConnection connection { get; set; }
    private readonly IConfiguration _config;
    private readonly ILogger<DbController> _logger;

    public DbController (IConfiguration config, ILogger<DbController> logger){
        _config = config;
        _logger = logger;
    }

    public void Connect () {
        connection = new MySqlConnection(_config["ConnectionStrings:DatabaseConnection"]);
        connection.Open();
    }

    public void Disconnect () {
        connection.Close();
    }

//Get all Methods
    public List<Member> GetAllMembers () {
        Connect();
        List<Member> members = new();
        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT * FROM members";
        var reader = cmd.ExecuteReader();
        while (reader.Read()) {
            members.Add(reader.GetFieldValue<Member>(0));
            Console.WriteLine(reader.GetString(0));
        }
        Disconnect();
        return members;
    }

    public List<BookLoan> GetAllLoans () {
        Connect();
        List<BookLoan> loans = new();
        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT * FROM loans";
        var reader = cmd.ExecuteReader();
        while (reader.Read()) {
            loans.Add(reader.GetFieldValue<BookLoan>(0));
            Console.WriteLine(reader.GetString(0));
        }
        Disconnect();
        return loans;
    }

    public List<Book> GetAllBooks () {
        Connect();
        List<Book> books = new();
        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT * FROM books";
        var reader = cmd.ExecuteReader();
        while (reader.Read()) {
            books.Add(reader.GetFieldValue<Book>(0));
            Console.WriteLine(reader.GetString(0));
        }
        Disconnect();
        return books;
    }

//Trying to make a generic method using reflection
    public List<T> GetAll<T> (string table) {
        Connect();
        List<T> resultSet = new();
        var objectProperties = typeof(T).GetProperties();
        var cmd = connection.CreateCommand();
        cmd.CommandText = $"SELECT * FROM {table}";
        var reader = cmd.ExecuteReader();
        while (reader.Read()) {
            var obj = Activator.CreateInstance<T>();
            foreach (var prop in objectProperties) {
                prop.SetValue(obj, reader[prop.Name]);
            }
            resultSet.Add(obj);
        }
        Disconnect();
        return resultSet;
    }

//Get single methods
    public Member GetMember (int id) {
        Connect();
        Member member;
        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT * FROM members WHERE id = @id";
        cmd.Parameters.AddWithValue("@id", id);
        var reader = cmd.ExecuteReader();
        while (reader.Read()) {
            member = reader.GetFieldValue<Member>(0);
            Console.WriteLine(reader.GetString(0));
        }
        Disconnect();
        return member;
    }

    public BookLoan GetLoan (int id) {
        Connect();
        BookLoan loan;
        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT * FROM loans WHERE id = @id";
        cmd.Parameters.AddWithValue("@id", id);
        var reader = cmd.ExecuteReader();
        while (reader.Read()) {
            loan = reader.GetFieldValue<BookLoan>(0);
            Console.WriteLine(reader.GetString(0));
        }
        Disconnect();
        return loan;
    }

    public Book GetBook (int id) {
        Connect();
        Book book;
        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT * FROM books WHERE id = @id";
        cmd.Parameters.AddWithValue("@id", id);
        var reader = cmd.ExecuteReader();
        while (reader.Read()) {
            book = reader.GetFieldValue<Book>(0);
            Console.WriteLine(reader.GetString(0));
        }
        Disconnect();
        return book;
    }

    public void UpdateModel (string UpdateTable, string UpdateColumn, string UpdateValue) {
        Connect();
        var cmd = connection.CreateCommand();
        cmd.CommandText = "UPDATE @UpdateTable SET @UpdateColumn = @UpdateValue";
        cmd.Parameters.AddWithValue("@UpdateTable", UpdateTable);
        cmd.Parameters.AddWithValue("@UpdateColumn", UpdateColumn);
        cmd.Parameters.AddWithValue("@UpdateValue", UpdateValue);
        cmd.ExecuteNonQuery();
        Disconnect();
    }
}

