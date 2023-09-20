using System.Text.Json;
using System.Text.Json.Serialization;
using HomeWorkEleven.Models;

namespace HomeWorkEleven.JSONSettings.Converter;

public class ProductTypeJsonConverter : JsonConverter<ProductTypeModel>
{
    public override ProductTypeModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ProductTypeModel value, JsonSerializerOptions options)
    {
        var translation = TranslateProductType(value);
        writer.WriteStringValue(translation);
    }

    private string TranslateProductType(ProductTypeModel productTypeModel)
    {
        switch (productTypeModel)
        {
            case ProductTypeModel.Accessories:
                return "Аксесуары";
            case ProductTypeModel.Appliances:
                return "Приборы";
            case ProductTypeModel.ChildrenToys:
                return "Детские игрушки";
            case ProductTypeModel.Bag:
                return "Сумка";
            case ProductTypeModel.Bicycle:
                return "Велосипед";
            case ProductTypeModel.Book:
                return "Книга";
            case ProductTypeModel.Computer:
                return "Компьютер";
            case ProductTypeModel.Cosmetics:
                return "Косметика";
            case ProductTypeModel.Dishes:
                return "Блюда";
            case ProductTypeModel.Game:
                return "Игра";
            case ProductTypeModel.Magazine:
                return "Журнал";
            case ProductTypeModel.Movie:
                return "Фильм";
            case ProductTypeModel.Music:
                return "Музыка";
            case ProductTypeModel.Phone:
                return "Телефон";
            case ProductTypeModel.Radio:
                return "Радио";
            case ProductTypeModel.Shoes:
                return "Обувь";
            case ProductTypeModel.SportsEquipment:
                return "Спортивное снаряжение";
            case ProductTypeModel.Tools:
                return "Инструменты";
            case ProductTypeModel.Watch:
                return "Часы";
            case ProductTypeModel.TvSeries:
                return "Телесериалы";
            default:
                return "Продукт";
        }
    }
}