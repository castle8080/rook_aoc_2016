namespace rook_aoc_2016.Services;

using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public class ProblemInputService {

    private static Regex INPUT_FILE_REGEX = new Regex(@"input_(\d+).*\.txt$");

    private IWebHostEnvironment _wbEnvironment;

    public ProblemInputService(IWebHostEnvironment wbEnvironment) {
        _wbEnvironment = wbEnvironment;
    }

    public IList<ProblemInput> GetInputs(int day) {
        return AllInputs().Where(i => i.Day == day).OrderBy(i => Tuple.Create(i.Name.Length, i.Name)).ToList();
    }   

    public IEnumerable<ProblemInput> AllInputs() {
        var contents = _wbEnvironment.WebRootFileProvider.GetDirectoryContents("input");

        // Collect entries.
        foreach (var finfo in contents) {
            var m = INPUT_FILE_REGEX.Match(finfo.Name);
            if (m.Success) {
                var day = Int32.Parse(m.Groups[1].ToString());
                var pInput = new ProblemInput(finfo, day);
                yield return pInput;
            }
        }
    }

}