namespace Runesmith.Catppuccin;

/// <summary>A Catppuccin flavor: the color theme and the syntax color scheme of the same name.</summary>
/// <param name="Key">The flavor's short name, which also names its embedded scheme.</param>
internal sealed record Flavor(string Key, string Name, bool IsDark, Palette Palette)
{
    public static IReadOnlyList<Flavor> All { get; } =
    [
        new("latte", "Latte", IsDark: false, Palette.Latte),
        new("frappe", "Frappé", IsDark: true, Palette.Frappe),
        new("macchiato", "Macchiato", IsDark: true, Palette.Macchiato),
        new("mocha", "Mocha", IsDark: true, Palette.Mocha),
    ];

    /// <summary>Gets the id of both the theme and the scheme.</summary>
    public string Id => $"runesmith.catppuccin.{Key}";

    public string DisplayName => $"Catppuccin {Name}";
}
