using Infrastructure.Services;

namespace UnitTests;

public class JsonParserServiceTests
{
    public JsonParserService JsonParserService;

    public string JsonExample = @"{
        ""intProperty"" : 10,
        ""stringProperty"": ""value"",
        ""arrayIntProperty"": [ 1, 2, 3],
        ""arrayStringProperty"": [ ""array1"", ""array2"", ""array3""],
        ""arrayObjectProperty"": [ { 
            ""intProperty"" : 11, 
            ""stringProperty"": ""value in array object"",
            ""objectProperty"" : { ""intProperty"" : 12, ""stringProperty"": ""value in array object object"", ""arrayProperty"": [4] } } ],
        ""objectProperty"": { 
            ""intProperty"" : 13, 
            ""stringProperty"": ""value in object"",
            ""objectProperty"" : { ""intProperty"" : 14, ""stringProperty"": ""value in object object"" },
            ""arrayIntProperty"": [ 4, 5, 6],
            ""arrayStringProperty"": [ ""array11"", ""array22"", ""array33""],
            ""arrayObjectProperty"": [ { 
                ""intProperty"" : 15, 
                ""stringProperty"": ""value in object array object"",
                ""objectProperty"" : { ""intProperty"" : 16, ""stringProperty"": ""value in object array object object"" } } ]}
    }";

    public JsonParserServiceTests()
    {
        JsonParserService = new JsonParserService();
    }

    [Theory]
    [InlineData("intProperty", true)]
    [InlineData("arrayIntProperty.[0]", true)]
    [InlineData("array.[0].objectProperty.intProperty", true)]
    [InlineData("arrayObjectProperty.[0].objectProperty.arrayProperty.[0]", true)]
    [InlineData("ObjectProperty.arrayObjectProperty.[2342].object_Property.stringProperty", true)]
    [InlineData("ObjectProperty.[-2342].object_Property.stringProperty", false)]
    [InlineData("Object-Property.arrayObjectProperty.[2342].object_Property.stringProperty", false)]
    [InlineData("ObjectProperty.arrayObjectProperty.[as].object_Property", false)]
    [InlineData("ObjectProperty.arrayObject&Property.object_Property", false)]
    public void IsValidPath(string path, bool expected)
    {
        var result = JsonParserService.IsValidPath(path);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("intProperty", 10)]
    [InlineData("stringProperty", "value")]
    [InlineData("arrayIntProperty.[0]", 1)]
    [InlineData("arrayStringProperty.[0]", "array1")]
    [InlineData("arrayObjectProperty.[0].intProperty", 11)]
    [InlineData("arrayObjectProperty.[0].stringProperty", "value in array object")]
    [InlineData("arrayObjectProperty.[0].objectProperty.intProperty", 12)]
    [InlineData("arrayObjectProperty.[0].objectProperty.stringProperty", "value in array object object")]
    [InlineData("arrayObjectProperty.[0].objectProperty.arrayProperty.[0]", 4)]
    [InlineData("objectProperty.intProperty", 13)]
    [InlineData("objectProperty.stringProperty", "value in object")]
    [InlineData("objectProperty.objectProperty.intProperty", 14)]
    [InlineData("objectProperty.objectProperty.stringProperty", "value in object object")]
    [InlineData("objectProperty.arrayIntProperty.[2]", 6)]
    [InlineData("objectProperty.arrayStringProperty.[2]", "array33")]
    [InlineData("objectProperty.arrayObjectProperty.[0].intProperty", 15)]
    [InlineData("objectProperty.arrayObjectProperty.[0].stringProperty", "value in object array object")]
    [InlineData("objectProperty.arrayObjectProperty.[0].objectProperty.intProperty", 16)]
    [InlineData("objectProperty.arrayObjectProperty.[0].objectProperty.stringProperty", "value in object array object object")]
    public void GetValueByPath_Success(string path, object expected)
    {
        var result = JsonParserService.GetValueByPath<object>(JsonExample, path);

        Assert.NotStrictEqual(expected, result);
    }
}