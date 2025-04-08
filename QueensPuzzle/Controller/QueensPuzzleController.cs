
using QueensPuzzle.Models;
using System.Text;

namespace QueensPuzzle.Controller;

public class QueensPuzzleController(int numberQueens)
{
    private int NumberQueens { get; set; } = numberQueens;

    public async IAsyncEnumerable<int[]> GetSolutionsAsync()
    {
        var board = new Board(NumberQueens, []);
        var solutions = board.GetSubSolutionsAsync();
        await foreach (var solution in solutions)
            yield return solution;
    }

    public string GetSolutionString(int[]solution, int numberSolution)
    {
        Console.WriteLine($"printing solution {numberSolution}...");
        if (solution.Length == 0)
            return "error: Solution is empty";
        if (solution.Length != NumberQueens)
            return "error: Solution has wrong number of queens.";

        var returnStringBuilder = new StringBuilder($"Printing solution ({solution[0]}");
        foreach (var position in solution.Skip(1))
        {
            returnStringBuilder.Append($", {position}");
        }
        returnStringBuilder.Append(")\n");
        var board = new Board(NumberQueens, solution);
        returnStringBuilder.Append(board.GetStringRepresentation());

        return returnStringBuilder.ToString();
    }
}
