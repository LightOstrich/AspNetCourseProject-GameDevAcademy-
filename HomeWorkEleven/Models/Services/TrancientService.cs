namespace HomeWorkEleven.Models.Services;

public class TrancientService
{
    private string word { get; set; } = words[_random.Next(words.Length)];
    private static Random _random = new();

    private static string[] words =
        { "apple", "banana", "car", "dog", "elephant", "fruit", "grape", "house", "ice cream", "jacket" };

    public void WriteWord()
    {
        Console.Write("-" + word);
    }
}