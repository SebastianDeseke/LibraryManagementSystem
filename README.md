# Library Management System
Welcome to my fun little project in *C#*

## Description

This project is a System with which to manage the ongoing task normally found in a __Library__. It can be scalled down for a single person use (yes some of us still read books). It is intended to be modular and have *API compatibility*. I am trying to make it accessable for frontend via API, but this field is a little unfamiliar to me. 

Every book is ordened in a _shelf_ that has a disctinct **SharingAttribute** (You can choose, but genre makes sense). Every Shelf has a _Section_ that it belongs to. Every Section has a overarching **SharedTheme**. The shelves have an ID as identifier, while the section have a char value a-z as identifier. When combined it makes for a definitiv identifier of a books location. 

*Note: I understand that limiting it to char a-z does limit us to only 26 uid's, but for most use cases this is enough. If you need more, you can modify the SectionID to suit your situation.*

## Installation

/

## Usage

Reflection is used to make a generic method that will adapt its Instace depending on the object that is parsed. Eaxmple: 
```csharp
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
```

## Licences

[GitHub](https://github.com/SebastianDeseke/LibraryManagementSystem)
Distributed under the __YoMamma__ License.

### What ChatGpt said :) 

By following these guidelines and structuring your ReadMe file properly, you can ensure that users and contributors will have a clear and concise understanding of your project.
