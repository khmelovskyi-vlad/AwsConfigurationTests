namespace ConfigurationTests.Model;

public class ConfigurationTestsResponseModel
{
    public ConfigurationTestsResponseModel(string result)
    {
        Result = result;
    }

    public string Result { get; set; }
}