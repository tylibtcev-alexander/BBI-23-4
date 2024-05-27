using System;
using System.IO;
using System.Linq;

abstract class Problem
{
    public abstract void Execute(string data);
}

class Problem1 : Problem
{
    public override void Execute(string data)
    {
        int[] characterCount = new int[32];
        foreach (char letter in data)
        {
            if (char.IsLetter(letter) && char.IsLower(letter))
            {
                int position = letter - 'а';
                characterCount[position]++;
            }
        }
        for (int i = 0; i < characterCount.Length; i++)
        {
            double proportion = (double)characterCount[i] / data.Length;
            char character = (char)('а' + i);
            Console.WriteLine($"{character}: {proportion:P2}");
        }
    }
}

class Problem3 : Problem
{
    public override void Execute(string data)
    {
        int startIndex = 0;
        while (startIndex < data.Length)
        {
            int segmentLength = Math.Min(50, data.Length - startIndex);
            string segment = data.Substring(startIndex, segmentLength);
            int spacePosition = segment.LastIndexOf(' ');
            if (spacePosition != -1)
            {
                segment = segment.Substring(0, spacePosition);
                startIndex += spacePosition + 1;
            }
            else
            {
                startIndex += segmentLength;
            }
            Console.WriteLine(segment);
        }
    }
}

class Problem5 : Problem
{
    public override void Execute(string data)
    {
        string[] tokens = data.Split(new char[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        var frequencyGroup = tokens
            .Select(token => token.ToLower()[0])
            .GroupBy(initial => initial)
            .OrderByDescending(group => group.Count());
        foreach (var group in frequencyGroup)
        {
            Console.WriteLine($"{group.Key}: {group.Count()}");
        }
    }
}

class Problem7 : Problem
{
    public override void Execute(string data)
    {
        string pattern = "сла";
        string[] tokens = data.Split(new char[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        var matchingTokens = tokens.Where(token => token.Contains(pattern));
        foreach (var token in matchingTokens)
        {
            Console.WriteLine(token);
        }
    }
}

class Problem11 : Problem
{
    public override void Execute(string data)
    {
        char[] delimiters = new char[] { ',', ';', ' ', '\n' };
        string[] names = data.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

        Array.Sort(names, (first, second) =>
        {
            int comparisonResult = Char.ToLower(first.Trim()[0]) - Char.ToLower(second.Trim()[0]);
            if (comparisonResult == 0)
            {
                comparisonResult = string.Compare(first.Trim(), second.Trim());
            }
            return comparisonResult;
        });

        foreach (var name in names)
        {
            Console.WriteLine(name.Trim());
        }
    }
}

class Problem14 : Problem
{
    public override void Execute(string data)
    {
        string[] items = data.Split(' ');
        int totalSum = 0;
        foreach (var item in items)
        {
            if (int.TryParse(item, out int number))
            {
                totalSum += number;
            }
        }
        Console.WriteLine($"Сумма чисел: {totalSum}");
    }
}

class MainProgram
{
    static void Main(string[] args)
    {
        string[] files = {
            File.ReadAllText(@"C:\\Users\\New\\source\\repos\\8 лаба\\8 лаба\\input1.txt"),
            File.ReadAllText(@"C:\\Users\\New\\source\\repos\\8 лаба\\8 лаба\\input3.txt"),
            File.ReadAllText(@"C:\\Users\\New\\source\\repos\\8 лаба\\8 лаба\\input5.txt"),
            File.ReadAllText(@"C:\\Users\\New\\source\\repos\\8 лаба\\8 лаба\\input7.txt"),
            File.ReadAllText(@"C:\\Users\\New\\source\\repos\\8 лаба\\8 лаба\\input11.txt"),
            File.ReadAllText(@"C:\\Users\\New\\source\\repos\\8 лаба\\8 лаба\\input14.txt")
        };
        Problem[] problems = {
            new Problem1(),
            new Problem3(),
            new Problem5(),
            new Problem7(),
            new Problem11(),
            new Problem14()
        };
        for (int i = 0; i < problems.Length; i++)
        {
            Console.WriteLine($"Задача {i + 1}:");
            problems[i].Execute(files[i]);
            Console.WriteLine();
        }
    }
}
