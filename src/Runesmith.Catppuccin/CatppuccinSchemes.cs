using System.Composition;
using Runesmith.Sdk.Appearance;

namespace Runesmith.Catppuccin;

/// <summary>The syntax color schemes of the four Catppuccin flavors, as the TextMate themes embedded in the assembly.</summary>
[Export(typeof(IColorSchemeContributor))]
public sealed class CatppuccinSchemes : IColorSchemeContributor
{
    public IEnumerable<ColorScheme> Schemes =>
        [.. Flavor.All.Select(flavor => new ColorScheme(flavor.Id, flavor.DisplayName, Read(flavor.Key)) { IsDark = flavor.IsDark })];

    /// <summary>Gets a scheme's JSON as the assembly carries it.</summary>
    internal static byte[] Read(string key)
    {
        using var stream = typeof(CatppuccinSchemes).Assembly.GetManifestResourceStream($"Schemes.{key}.json")
            ?? throw new InvalidOperationException($"The scheme {key} is not in the assembly.");
        var bytes = new byte[stream.Length];
        stream.ReadExactly(bytes);
        return bytes;
    }
}
