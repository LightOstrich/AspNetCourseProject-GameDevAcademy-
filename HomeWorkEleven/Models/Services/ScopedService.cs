namespace HomeWorkEleven.Models.Services;

public class ScopedService
{
    private string word { get; set; } = words[Random.Next(words.Length)];
    private static readonly Random Random = new();

    private static string[] words =
        { "apple", "banana", "car", "dog", "elephant", "fruit", "grape", "house", "ice cream", "jacket" };

    public void WriteWord()
    {
        Console.Write("-" + word);
    }
}