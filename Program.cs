using System;
using System.Linq;

using var db = new BloggingContext();

// Note: This sample requires the database to be created before running.
Console.WriteLine($"Database path: {db.DbPath}.");

// Create
Console.WriteLine("Inserting a new Task");
db.Add(new Task { Name = "My Task" });
db.SaveChanges();

// Read
Console.WriteLine("Querying for a Task");
var task = db.Tasks
    .OrderBy(b => b.TaskId)
    .First();

// Update
Console.WriteLine("Updating the Task and adding a ToDo");
task.Name = "My Task Hallo World";
task.ToDos.Add(
    new ToDo { Name = "Hello World"});
db.SaveChanges();

// ECHO
Console.Write("Print Task Title: ");
Console.WriteLine(task.ToString());

// Delete
Console.WriteLine("Delete the Task");
db.Remove(task);
db.SaveChanges();