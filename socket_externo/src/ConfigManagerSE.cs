using System;
using System.Collections.Generic;
using System.IO;

class ConfigManagerSE
{
    public static Dictionary<string, string> LeerConfiguracion(string filePath)
    {
        var config = new Dictionary<string, string>();
        string currentSection = null;

        foreach (var line in File.ReadAllLines(filePath))
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    currentSection = line.Trim('[', ']');
                }
                else if (line.Contains('=') && currentSection != null)
                {
                    var keyValue = line.Split('=');
                    string key = $"{currentSection}.{keyValue[0].Trim()}";
                    config[key] = keyValue[1].Trim();
                }
            }
        }
        return config;
    }
}
