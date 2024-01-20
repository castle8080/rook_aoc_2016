using Microsoft.Extensions.FileProviders;

namespace rook_aoc_2016.Services;

public class ProblemInput {
    public string Name { get => FileInfo.Name; }

    public IFileInfo FileInfo { get; }

    public int Day { get; }

    public ProblemInput(IFileInfo fileInfo, int day) {
        FileInfo = fileInfo;
        Day = day;
    }
}