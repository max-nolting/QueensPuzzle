
using System.Text;

namespace QueensPuzzle.Models;

public class Board(int numberQueens, IEnumerable<int> placedQueens)
{
    private int NumberQueens = numberQueens;
    private List<int> PlacedQueens = [.. placedQueens];

    public async IAsyncEnumerable<int[]> GetSubSolutionsAsync()
    {
        int queenNumber = PlacedQueens.Count - 1;
        var diagonallyForbidden = PlacedQueens
            .Select(x =>
            {
                if (x == PlacedQueens.Last())
                    return -1;
                var rowNumber = PlacedQueens.FindIndex(y => y == x);
                var rowDiff = queenNumber - rowNumber;
                return x - rowDiff;
            })
            .Union(
                PlacedQueens.Select(x =>
                {
                    if (x == PlacedQueens.Last())
                        return -1;
                    var rowNumber = PlacedQueens.FindIndex(y => y == x);
                    var rowDiff = queenNumber - rowNumber;
                    return x + rowDiff;
                })
            );
        if (PlacedQueens.Count() > 0)
        {
            if (diagonallyForbidden.Contains(PlacedQueens.Last()))
            {
                yield break;
            }
        }
        if (NumberQueens == PlacedQueens.Count)
            yield return PlacedQueens.ToArray();

        var nextBoardConfigs = Enumerable.Range(0, NumberQueens)
            .Where(x => !PlacedQueens.Contains(x))
            .Select(x => new Board(numberQueens, [.. PlacedQueens, x]));
        var nextSolutions = nextBoardConfigs
            .Select(x => x.GetSubSolutionsAsync());

        foreach (var results in nextSolutions)
        {
            await foreach (var solution in results)
            {
                yield return solution;
            }
        }
    }

    public StringBuilder GetStringRepresentation()
    {
        var returnStringBuilder = new StringBuilder();
        for (int row = 0; row < NumberQueens; row++)
        {
            var queenPos = PlacedQueens[row];
            for (int col = 0; col < NumberQueens; col++)
            {
                var nextCell = new StringBuilder("| ");
                nextCell.Append(queenPos == col ? "Q" : " ");
                nextCell.Append(" ");
                returnStringBuilder.Append(nextCell);
            }
            returnStringBuilder.Append("|\n");
        }

        return returnStringBuilder;
    }
}
