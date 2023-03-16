﻿// See https://aka.ms/new-console-template for more information
using DABw6_Exercise27;

using (var db = new MyDBContext())
{
    // Note: This sample requires the database to be created before running.

    // Create
    Console.WriteLine("Inserting a new blog");
    db.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
    db.SaveChanges();

    // Read
    Console.WriteLine("Querying for a blog");
    var blog = db.Blogs
        .OrderBy(b => b.BlogId)
        .First();
    Console.WriteLine($"BlogId: {blog.BlogId} \turl:{blog.Url}");

    // Update
    Console.WriteLine("Updating the blog and adding a post");
    blog.Url = "https://devblogs.microsoft.com/dotnet";
    blog.Posts.Add(
        new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
    db.SaveChanges();

    //Delete
    Console.WriteLine("Delete the blog");
    db.Remove(blog);
    db.SaveChanges();
}
   