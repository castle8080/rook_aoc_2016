using System.Text;
using Microsoft.Extensions.FileProviders;

namespace rook_aoc_2016.Services;

public static class ProblemInputExt {

    public static TextReader CreateReader(this ProblemInput input) {
        return new StreamReader(input.FileInfo.CreateReadStream(), encoding: Encoding.UTF8);
    }

    public static async Task<IList<string>> ReadLinesAsync(this ProblemInput input) {
        var lines = new List<string>();
        using (var reader = input.CreateReader()) {
            while (true) {
                var line = await reader.ReadLineAsync();
                if (line == null) {
                    return lines;
                }
                else {
                    lines.Add(line);
                }
            }
        }
    }
}