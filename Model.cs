using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class BloggingContext : DbContext
{
    public DbSet<Task> Tasks { get; set; }
    public DbSet<ToDo> ToDos { get; set; }

    public string DbPath { get; }

    public BloggingContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blogging.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class Task
{
    public int TaskId { get; set; }
    public string? Name { get; set; }
    public List<ToDo> ToDos { get; set; } = new();

}

public class ToDo
{
    public int ToDoId { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; } = false;

    public int TaskId { get; set; }
    public Task? Task { get; set; }

}