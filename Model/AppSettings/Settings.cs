using Microsoft.Extensions.Configuration;
using System;

public class Settings
{
    private readonly IConfiguration _configuration;

    public Settings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string DoSomething()
    {
        string DbContext = _configuration["appSettings:ApiKey"];
        return DbContext;
    }
}
