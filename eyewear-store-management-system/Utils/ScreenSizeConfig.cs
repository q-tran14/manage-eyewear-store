using System;
using System.Configuration;
using System.Drawing;

public static class ScreenSizeConfig
{
    public static Size Min => GetScreenSize("ScreenMinSize");
    public static Size Medium => GetScreenSize("ScreenMediumSize");
    public static Size Max => GetScreenSize("ScreenMaxSize");

    private static Size GetScreenSize(string key)
    {
        string value = ConfigurationManager.AppSettings[key];
        if (!string.IsNullOrEmpty(value))
        {
            string[] parts = value.Split(',');
            if (parts.Length == 2 && int.TryParse(parts[0], out int width) && int.TryParse(parts[1], out int height))
            {
                return new Size(width, height);
            }
        }
        return new Size(1280, 720); // Giá trị mặc định nếu không đọc được
    }
}
