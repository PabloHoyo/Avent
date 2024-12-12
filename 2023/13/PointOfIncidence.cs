namespace Avent;

internal class PointOfIncidence : Puzzle
{
    public PointOfIncidence() : base("./13/input.txt") { }

    public override string Part1()
    {
        int result = 0;
        var currentMatrix = new List<string>();
        lines = lines.Append(string.Empty).ToArray();
        foreach (var line in lines)
        {
            if (line != string.Empty)
            {
                currentMatrix.Add(line);
            }
            else
            {
                var rowsReflection = GetReflectionRowIndex(currentMatrix);
                if (rowsReflection > 0) 
                { 
                    result += rowsReflection * 100;
                }
                else
                { 
                    currentMatrix = RotateMatrix(currentMatrix);
                    var colsReflection = GetReflectionRowIndex(currentMatrix);
                    result += colsReflection;
                }
                currentMatrix = new();
            }
        }
        return result.ToString();
    }

    public override string Part2()
    {
        return "";
    }

    private static List<string> RotateMatrix(List<string> matrix)
    {
        var rotated = Enumerable.Repeat("", matrix[0].Count()).ToList();
        for (var x = 0; x < matrix[0].Count(); x++)
        {
            for (var y = 0; y < matrix.Count(); y++)
            {
                rotated[x] += matrix[y][x];
            }
        }
        return rotated;
    }

    private int GetReflectionRowIndex(List<string> matrix)
    {
        var consecutiveDuplicates = new List<(int index1, int index2)>();
        var lastRow = "";
        for (int i = 0; i < matrix.Count; i++)
        {
            if (matrix[i] == lastRow) 
            {
                consecutiveDuplicates.Add((i - 1, i));
            }
            lastRow = matrix[i];
        }
        foreach (var item in consecutiveDuplicates)
        {
            var (index1, index2) = item;
            var isReflection = true;
            do
            {
                isReflection &= matrix[index1] == matrix[index2];
                index1--;
                index2++;
            }
            while (isReflection && index1 >= 0 && index2 < matrix.Count);

            if (isReflection)
            {
                return item.index2;
            }
        }
        return 0;
    }
}
