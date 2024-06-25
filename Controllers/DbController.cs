using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections;
//using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers;

[Authorize]
public class DbController : Controller{
    MySqlConnection connection { get; set; }
    private readonly IConfiguration _config;

    public DbController(IConfiguration config){
        _config = config;
    }

    public void Connect () {
        connection = new MySqlConnection(_config["ConnectionStrings:DatabaseConnection"]);
        connection.Open();
    }

    public void Disconnect () {
        connection.Close();
    }
}

