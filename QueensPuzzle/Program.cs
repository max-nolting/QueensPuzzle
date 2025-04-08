using QueensPuzzle.Controller;
using QueensPuzzle.Models;
using System.Threading.Tasks;

namespace QueensPuzzle;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Enter number of queens:");
        var numberQueens = -1;
        int.TryParse(Console.ReadLine(), out numberQueens);
        while (numberQueens < 1) {
            Console.WriteLine("invalid input. Please enter a positive integer.");
            int.TryParse(Console.ReadLine(), out numberQueens);
        }
        var controller = new QueensPuzzleController(numberQueens);
        var solutions = controller.GetSolutionsAsync();
        var fastForward = false;
        var numberSolutions = 0;
        await foreach (var solution in solutions)
        {
            numberSolutions++;
            if (fastForward)
                continue;
            var printText = controller.GetSolutionString(solution, numberSolutions);
            Console.WriteLine(printText);
            Console.WriteLine("Press Enter to continue, or type 'quit' and press enter to jump to the number of solutions");
            var entry = Console.ReadLine();
            if (entry == "quit")
            {
                Console.WriteLine("calculating...");
                fastForward = true;
            }
        }
        Console.WriteLine($"Done. Total number of solutions: {numberSolutions}.\nPress any key to exit.");

        Console.ReadKey();
    }
}
