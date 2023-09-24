namespace HomeWorkEleven.Models.Services;

public  class SingletonService
{
    private string WordOne = "Плов";
    private string WordTwo = "Хачапури";
    private string WordThree = "Салат";

    public void WriteWords()
    {
        Console.Write($"{WordOne} {WordTwo} {WordThree}");
    }
}