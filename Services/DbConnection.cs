using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data;

namespace LibraryManagementSystem.Services;

public class DbConnection
{
    MySqlConnection connection { get; set; }
    private readonly IConfiguration _config;
    private readonly ILogger<DbConnection> _logger;

    public DbConnection(IConfiguration config, ILogger<DbConnection> logger)
    {
        _config = config;
        _logger = logger;
    }

    public void Connect()
    {
        connection = new MySqlConnection(_config["ConnectionStrings:DatabaseConnection"]);
        connection.Open();
    }

    public void Disconnect()
    {
        connection.Close();
    }

    //Get all Methods
    public List<Member> GetAllMembers()
    {
        Connect();
        List<Member> members = new();
        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT * FROM members";
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            members.Add(reader.GetFieldValue<Member>(0));
            Console.WriteLine(reader.GetString(0));
        }
        Disconnect();
        return members;
    }

    public List<BookLoan> GetAllLoans()
    {
        Connect();
        List<BookLoan> loans = new();
        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT * FROM loans";
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            loans.Add(reader.GetFieldValue<BookLoan>(0));
            Console.WriteLine(reader.GetString(0));
        }
        Disconnect();
        return loans;
    }

    public List<Book> GetAllBooks()
    {
        Connect();
        List<Book> books = new();
        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT * FROM books";
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            books.Add(reader.GetFieldValue<Book>(0));
            Console.WriteLine(reader.GetString(0));
        }
        Disconnect();
        return books;
    }

    //generic methods using reflection
    public List<T> GetAll<T>(string table)
    {
        Connect();
        List<T> resultSet = new();
        var objectProperties = typeof(T).GetProperties();
        var cmd = connection.CreateCommand();
        cmd.CommandText = $"SELECT * FROM {table}";
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var obj = Activator.CreateInstance<T>();
            foreach (var prop in objectProperties)
            {
                prop.SetValue(obj, reader[prop.Name]);
            }
            resultSet.Add(obj);
        }
        Disconnect();
        return resultSet;
    }

    public T GetById<T>(string table, int id)
    {
        Connect();
        var objectProperties = typeof(T).GetProperties();
        var cmd = connection.CreateCommand();
        cmd.CommandText = $"SELECT * FROM {table} WHERE id = @id";
        cmd.Parameters.AddWithValue("@id", id);
        var reader = cmd.ExecuteReader();
        T obj = default;
        if (reader.Read())
        {
            obj = Activator.CreateInstance<T>();
            foreach (var prop in objectProperties)
            {
                prop.SetValue(obj, reader[prop.Name]);
            }
        }
        Disconnect();
        return obj;
    }

    public void Update<T>(string table, T obj, int id)
    {
        Connect();
        var cmd = connection.CreateCommand();
        var objectProperties = typeof(T).GetProperties();
        cmd.Parameters.AddWithValue("@id", id);

        string columnValues = string.Join(",", objectProperties.Select(p => $"{p.Name} = @val{p.Name}"));
        foreach (var prop in objectProperties)
        {
            cmd.Parameters.AddWithValue($"@val{prop.Name}", prop.GetValue(obj));
        }
        cmd.CommandText = $"UPDATE {table} SET {columnValues} WHERE id = @id";
        Console.WriteLine(cmd.CommandText);
        cmd.ExecuteNonQuery();
        Disconnect();
    }

    public void Create<T>(string table, T obj)
    {
        Connect();
        var cmd = connection.CreateCommand();
        var objectProperties = typeof(T).GetProperties();
        string columns = string.Join(",", objectProperties.Select(p => p.Name));
        string values = string.Join(",", objectProperties.Select(p => $"@val{p.Name}"));
        foreach (var prop in objectProperties)
        {
            cmd.Parameters.AddWithValue($"@val{prop.Name}", prop.GetValue(obj));
        }
        cmd.CommandText = $"INSERT INTO {table} ({columns}) VALUES ({values})";
        Console.WriteLine(cmd.CommandText);
        cmd.ExecuteNonQuery();
        Disconnect();
    }

    // single methods

    public void AddBookInShelf(int bookid, int shelfid)
    {
        Connect();
        string sqlQuery = $@"UPDATE books SET shelf_id = {shelfid}
                            WHERE Id = {bookid}";

        using (MySqlCommand command = new(sqlQuery, connection))
        {
            command.ExecuteNonQuery();
        }

        Disconnect();
    }

    // check methods
    public bool CheckIfExist(int id, string table)
    {
        Connect();
        var cmd = connection.CreateCommand();
        cmd.CommandText = $"SELECT * FROM {table} WHERE id = @id";
        cmd.Parameters.AddWithValue("@id", id);
        var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            Disconnect();
            return true;
        }
        else
        {
            Disconnect();
            return false;
        }
    }
}

