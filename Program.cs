using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class Program {

    static void Main(string[] args)
    {
        //SeedTasks();
        //PrintDatabase();
        PrintIncompleteTasksAndTodos();
    }

    static BloggingContext db = new BloggingContext();

    static void SeedTasks()
    {
        // Note: This sample requires the database to be created before running.
        Console.WriteLine($"Database path: {db.DbPath}.");

        // Create
        Console.WriteLine("Inserting Task Produce software");
        Task ProduceSoftware = new Task { Name = "Produce software" };
        ProduceSoftware.ToDos.Add(new ToDo { Name = "Write code" });
        ProduceSoftware.ToDos.Add(new ToDo { Name = "Compile source" });
        ProduceSoftware.ToDos.Add(new ToDo { Name = "Test program" });
        db.Add(ProduceSoftware);

        Console.WriteLine("Inserting Task Brew coffee");
        Task BrewCoffee = new Task { Name = "Brew coffee" };
        BrewCoffee.ToDos.Add(new ToDo { Name = "Pour water" });
        BrewCoffee.ToDos.Add(new ToDo { Name = "Pour coffee" });
        BrewCoffee.ToDos.Add(new ToDo { Name = "Turn on" });
        db.Add(BrewCoffee);


        db.SaveChanges();
    }

    static void PrintDatabase()
    {
        var tasks = db.Tasks
            .Include(a => a.ToDos)
            .OrderBy(a => a.TaskId)
        ;

        foreach (var task in tasks) { 
           Console.WriteLine(task.Name);
            foreach (var toDo in task.ToDos) {
                Console.WriteLine("  * " + toDo.Name);
            }
            Console.WriteLine("");
        }

    }

    static void PrintIncompleteTasksAndTodos()
    {
        var tasks = db.Tasks
            .Include(a => a.ToDos)
            .OrderBy(a => a.TaskId)
            .Where (a => a.ToDos.Any(s => s.IsComplete == false))
        ;

        foreach (var task in tasks)
        {
            Console.WriteLine(task.Name);
            foreach (var toDo in task.ToDos)
            {
                Console.WriteLine("  * " + toDo.Name);
            }
            Console.WriteLine("");
        }

    }

}

/*

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

*/