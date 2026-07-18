namespace Almostengr.Common.Common.Shared;

public static class TemperatureExtensions
{
    public static float ToFahrenheit(this float celsius)
    {
        float fahrenheit = (celsius * 9 / 5) + 32;
        return fahrenheit;
    }

    public static float ToCelsius(this float fahrenheit)
    {
        float celsius = (32 - fahrenheit) * 5 / 9;
        return celsius;
    }
}
