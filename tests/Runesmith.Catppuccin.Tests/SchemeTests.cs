using System.Text.Json;

namespace Runesmith.Catppuccin.Tests;

public sealed class SchemeTests
{
    public static TheoryData<string> Flavors => [.. Flavor.All.Select(f => f.Key)];

    [Fact]
    public void GivesEachSchemeAsJson()
    {
        var schemes = new CatppuccinSchemes().Schemes.ToList();

        Assert.Equal(new CatppuccinThemes().Themes.Select(t => t.ColorScheme), schemes.Select(s => s.Id));
        Assert.Equal([false, true, true, true], schemes.Select(s => s.IsDark));
        Assert.All(schemes, s =>
        {
            Assert.Empty(s.FilePath);
            Assert.False(s.Json.IsEmpty, $"{s.Id} has no JSON.");
        });
    }

    [Theory]
    [MemberData(nameof(Flavors))]
    public void GivesTheEmbeddedThemeUnchanged(string key)
    {
        var flavor = Flavor.All.Single(f => f.Key == key);
        var scheme = new CatppuccinSchemes().Schemes.Single(s => s.Id == flavor.Id);

        Assert.Equal(CatppuccinSchemes.Read(key), scheme.Json.ToArray());
    }

    [Theory]
    [MemberData(nameof(Flavors))]
    public void ColorsTheTokensAsCatppuccinDoes(string key)
    {
        var flavor = Flavor.All.Single(f => f.Key == key);
        var scheme = new CatppuccinSchemes().Schemes.Single(s => s.Id == flavor.Id);
        using var json = JsonDocument.Parse(scheme.Json);
        var root = json.RootElement;
        var p = flavor.Palette;

        Assert.Equal(flavor.DisplayName, root.GetProperty("name").GetString());
        Assert.Equal(flavor.IsDark ? "dark" : "light", root.GetProperty("type").GetString());
        var rules = root.GetProperty("tokenColors").EnumerateArray().ToList();
        Assert.Equal(p.Text, rules[0].GetProperty("settings").GetProperty("foreground").GetString());
        Assert.Equal(p.Mauve, Foreground(rules, "keyword"));
        Assert.Equal(p.Mauve, Foreground(rules, "storage.modifier"));
        Assert.Equal(p.Green, Foreground(rules, "string"));
        Assert.Equal(p.Pink, Foreground(rules, "constant.character.escape"));
        Assert.Equal(p.Overlay2, Foreground(rules, "comment"));
        Assert.Equal(p.Peach, Foreground(rules, "constant.numeric"));
        Assert.Equal(p.Yellow, Foreground(rules, "entity.name.type"));
        Assert.Equal(p.Blue, Foreground(rules, "entity.name.function"));
        Assert.Equal(p.Maroon, Foreground(rules, "variable.parameter"));
        Assert.Equal(p.Sky, Foreground(rules, "keyword.operator"));
        Assert.Equal(p.Teal, Foreground(rules, "variable.other.enummember"));
        Assert.Equal(p.Red, Foreground(rules, "variable.language"));
        Assert.All(rules.Where(r => r.GetProperty("settings").TryGetProperty("foreground", out _)),
            r => Assert.Matches("^#[0-9A-F]{6}$", r.GetProperty("settings").GetProperty("foreground").GetString()));
    }

    private static string? Foreground(List<JsonElement> rules, string scope)
    {
        var rule = Assert.Single(rules, r => r.TryGetProperty("scope", out var scopes) && scopes.EnumerateArray().Any(s => s.GetString() == scope));
        return rule.GetProperty("settings").GetProperty("foreground").GetString();
    }
}
