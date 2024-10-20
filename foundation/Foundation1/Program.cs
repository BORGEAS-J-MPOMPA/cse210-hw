using System;
using System.Collections.Generic;

public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; } // Length in seconds
    private List<Comment> Comments = new List<Comment>();

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }

    public List<Comment> GetComments()
    {
        return Comments;
    }
}

public class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create videos
        Video video1 = new Video { Title = "C# Tutorial", Author = "John Doe", Length = 600 };
        Video video2 = new Video { Title = "Python Tutorial", Author = "Jane Smith", Length = 750 };
        Video video3 = new Video { Title = "JavaScript Basics", Author = "Mike Brown", Length = 500 };

        // Add comments to video1
        video1.AddComment(new Comment("Alice", "Great tutorial!"));
        video1.AddComment(new Comment("Bob", "Very helpful, thanks!"));
        video1.AddComment(new Comment("Charlie", "Could you explain more about classes?"));

        // Add comments to video2
        video2.AddComment(new Comment("David", "This was awesome!"));
        video2.AddComment(new Comment("Eve", "Loved the explanations."));
        
        // Add comments to video3
        video3.AddComment(new Comment("Frank", "Really clear and concise!"));
        video3.AddComment(new Comment("Grace", "Nice work!"));
        video3.AddComment(new Comment("Heidi", "Great examples."));

        // Create list of videos
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display video information and comments
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.Title}, Author: {video.Author}, Length: {video.Length} seconds");
            Console.WriteLine($"Number of comments: {video.GetCommentCount()}");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"Comment by {comment.Name}: {comment.Text}");
            }
            Console.WriteLine();
        }
    }
}
