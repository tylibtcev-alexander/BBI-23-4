using System;
using System.IO;
using System.Linq;

abstract class Problem
{
    public abstract void Execute(string data);

    public override string ToString()
    {
        return this.GetType().Name;
    }
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

    public override string ToString()
    {
        return "Задача 1: Частота строчных букв в тексте.";
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

    public override string ToString()
    {
        return "Задача 3: Разбиение текста на сегменты по 50 символов.";
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

    public override string ToString()
    {
        return "Задача 5: Частота первых букв слов.";
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

    public override string ToString()
    {
        return "Задача 7: Поиск слов, содержащих подстроку 'сла'.";
    }
}

class Problem11 : Problem
{
    public override void Execute(string data)
    {
        char[] delimiters = new char[] { ',', ';', ' ', '\n' };
        string[] names = data.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < names.Length - 1; i++)
        {
            for (int j = i + 1; j < names.Length; j++)
            {
                string first = names[i].Trim();
                string second = names[j].Trim();

                int comparisonResult = Char.ToLower(first[0]) - Char.ToLower(second[0]);
                if (comparisonResult == 0)
                {
                    comparisonResult = CompareStrings(first, second);
                }

                if (comparisonResult > 0)
                {
                    string temp = names[i];
                    names[i] = names[j];
                    names[j] = temp;
                }
            }
        }

        foreach (var name in names)
        {
            Console.WriteLine(name.Trim());
        }
    }

    private int CompareStrings(string first, string second)
    {
        int minLength = Math.Min(first.Length, second.Length);
        for (int i = 0; i < minLength; i++)
        {
            if (first[i] != second[i])
            {
                return first[i] - second[i];
            }
        }
        return first.Length - second.Length;
    }

    public override string ToString()
    {
        return "Задача 11: Сортировка имен.";
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

    public override string ToString()
    {
        return "Задача 14: Сумма чисел в тексте.";
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
            Console.WriteLine(problems[i].ToString());
            problems[i].Execute(files[i]);
            Console.WriteLine();
        }
    }
}
