# Catppuccin Themes for Runesmith

The official Catppuccin plugin for [Runesmith](https://github.com/RunesmithHub/Runesmith), `runesmith.catppuccin`: the four
[Catppuccin](https://catppuccin.com) flavors as color themes, each with a matching syntax color scheme.

| Theme | Look |
| --- | --- |
| Catppuccin Latte | Light |
| Catppuccin Frappé | Dark |
| Catppuccin Macchiato | Dark |
| Catppuccin Mocha | Dark |

Pick one in **Appearance**. Each theme brings its own syntax colors, which follow Catppuccin's style guide: keywords in Mauve, strings in
Green, types in Yellow, functions in Blue, numbers and constants in Peach, and comments in Overlay 2. The schemes can also be picked on their
own, with any theme.

## Capabilities

None. The plugin only adds themes and syntax colors, and carries its schemes inside its assembly, so it writes no files.

## Install

Install it from Runesmith's plugin manager, or from [hub.runesmith.dev](https://hub.runesmith.dev).

## Build and test

You need the .NET SDK that `global.json` names. Runesmith's packages come from the plugin hub's feed, which `nuget.config` sets up.

```bash
dotnet build plugin-catppuccin.slnx
dotnet test --solution plugin-catppuccin.slnx
dotnet build src/Runesmith.Catppuccin -t:InstallPlugin
```

`InstallPlugin` copies the built plugin into your Runesmith plugins folder; restart Runesmith to load it. [CONTRIBUTING.md](CONTRIBUTING.md)
has the checks a change must pass, and the [Runesmith documentation](https://docs.runesmith.dev/) covers writing plugins.

## Credits

The colors are [Catppuccin](https://github.com/catppuccin/catppuccin)'s, from its [palette](https://github.com/catppuccin/palette).
Thanks to the Catppuccin team and community. The palette is used under the MIT License, whose notice is also in [NOTICE](NOTICE):

```text
MIT License

Copyright (c) 2021 Catppuccin

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

## License

[Apache License 2.0](LICENSE).
