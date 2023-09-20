using System.Text.Json;
using System.Text.RegularExpressions;

namespace HomeWorkEleven.JSONConverter.Policies;

//TODO:Реализовать snake case Json политику(ProductType = product_Type)
//https://stackoverflow.com/questions/63055621

public class SnakeCasePolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        name = Regex.Replace(name, "(.)([A-Z][a-z]+)", "$1_$2");
        name = Regex.Replace(name, "([a-z0-9])([A-Z])", "$1_$2");
        return name.ToLower();
    }
}