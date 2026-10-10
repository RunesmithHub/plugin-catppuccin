using System.Reflection;
using Avalonia.Media;
using HammerUI.Theming;
using Runesmith.Sdk.Appearance;

namespace Runesmith.Catppuccin.Tests;

public sealed class ThemeTests
{
    private const double Unreadable = 3;
    private const double Readable = 4.5;

    public static TheoryData<string> Flavors => [.. Flavor.All.Select(f => f.Key)];

    [Fact]
    public void ListsTheFourFlavorsWithTheirSchemes()
    {
        var themes = new CatppuccinThemes().Themes.ToList();

        Assert.Equal(["Catppuccin Latte", "Catppuccin Frappé", "Catppuccin Macchiato", "Catppuccin Mocha"], themes.Select(t => t.Name));
        Assert.Equal(["runesmith.catppuccin.latte", "runesmith.catppuccin.frappe", "runesmith.catppuccin.macchiato", "runesmith.catppuccin.mocha"], themes.Select(t => t.Id));
        Assert.Equal([false, true, true, true], themes.Select(t => t.IsDark));
        Assert.All(themes, t => Assert.Equal(t.Id, t.ColorScheme));
    }

    [Theory]
    [MemberData(nameof(Flavors))]
    public void SetsEveryColorToAHexColor(string key)
    {
        var colors = Theme(key).Colors;

        foreach (var property in typeof(ThemeColors).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var value = (string?)property.GetValue(colors);
            Assert.True(value is not null, $"{property.Name} is not set.");
            Assert.Matches("^#([0-9A-F]{6}|[0-9A-F]{8})$", value);
            Assert.True(Color.TryParse(value, out _), $"{property.Name} {value} is not a color.");
        }
    }

    [Theory]
    [MemberData(nameof(Flavors))]
    public void IsAsDarkAsItSays(string key)
    {
        var theme = Theme(key);

        Assert.Equal(theme.IsDark, ColorMath.Luminance(Parse(theme.Colors.Background)) < 0.5);
    }

    [Theory]
    [MemberData(nameof(Flavors))]
    public void TextReadsWellOnEverySurface(string key)
    {
        var colors = Theme(key).Colors;
        var text = Parse(colors.TextPrimary);

        foreach (var surface in new[] { colors.Background, colors.Surface, colors.SurfaceRaised, colors.SurfaceSunken })
            Assert.True(Contrast(text, Parse(surface)) >= Readable, $"The text has too little contrast with {surface}.");
        Assert.True(Contrast(Parse(colors.TextSecondary), Parse(colors.Surface)) >= Unreadable, "The secondary text is too faint.");
        Assert.True(Contrast(Parse(colors.AccentForeground), Parse(colors.Accent)) >= Unreadable, "The text on the accent is too faint.");
    }

    private static ColorTheme Theme(string key) => new CatppuccinThemes().Themes.Single(t => t.Id == $"runesmith.catppuccin.{key}");

    private static Color Parse(string? value) => Color.Parse(value!);

    private static double Contrast(Color foreground, Color background)
    {
        var opaque = Color.FromRgb(background.R, background.G, background.B);
        var blended = ColorMath.Mix(opaque, Color.FromRgb(foreground.R, foreground.G, foreground.B), foreground.A / 255.0);
        var a = ColorMath.Luminance(blended);
        var b = ColorMath.Luminance(opaque);
        return (Math.Max(a, b) + 0.05) / (Math.Min(a, b) + 0.05);
    }
}
