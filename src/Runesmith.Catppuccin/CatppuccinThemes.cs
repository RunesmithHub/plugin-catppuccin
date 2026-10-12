using System.Composition;
using Runesmith.Sdk.Appearance;

namespace Runesmith.Catppuccin;

/// <summary>The four Catppuccin flavors as color themes, each with its own syntax color scheme.</summary>
[Export(typeof(IColorThemeContributor))]
public sealed class CatppuccinThemes : IColorThemeContributor
{
    public IEnumerable<ColorTheme> Themes => Flavor.All.Select(Create);

    internal static ColorTheme Create(Flavor flavor)
    {
        var p = flavor.Palette;
        return new ColorTheme(flavor.Id, flavor.DisplayName, flavor.IsDark)
        {
            ColorScheme = flavor.Id,
            Colors = new ThemeColors
            {
                Accent = p.Mauve,

                // The terminal colors Catppuccin gives its ports: black and white are the surfaces, and bright colors repeat the normal ones.
                TerminalColors =
                [
                    flavor.IsDark ? p.Surface1 : p.Subtext1, p.Red, p.Green, p.Yellow, p.Blue, p.Pink, p.Teal, flavor.IsDark ? p.Subtext1 : p.Surface2,
                    flavor.IsDark ? p.Surface2 : p.Subtext0, p.Red, p.Green, p.Yellow, p.Blue, p.Pink, p.Teal, flavor.IsDark ? p.Subtext0 : p.Surface1,
                ],
                AccentForeground = flavor.IsDark ? p.Crust : p.Base,
                Background = p.Mantle,
                Surface = p.Base,
                SurfaceRaised = flavor.IsDark ? p.Surface0 : p.Base,
                SurfaceSunken = flavor.IsDark ? p.Crust : p.Mantle,
                SurfaceHover = WithAlpha(p.Overlay2, 0x1A),
                SurfacePressed = WithAlpha(p.Overlay2, 0x2E),
                BorderSubtle = p.Crust,
                BorderStrong = flavor.IsDark ? p.Surface1 : p.Surface0,
                TextPrimary = p.Text,
                TextSecondary = p.Subtext1,
                TextMuted = p.Overlay1,
                TextDisabled = p.Overlay0,
                Success = p.Green,
                Warning = p.Yellow,
                Danger = p.Red,
                Info = p.Teal,
                Scrim = WithAlpha(p.Crust, flavor.IsDark ? 0xA6 : 0x66),
            },
        };
    }

    private static string WithAlpha(string color, int alpha) => $"#{alpha:X2}{color[1..]}";
}
