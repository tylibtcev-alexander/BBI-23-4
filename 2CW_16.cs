using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
[JsonDerivedType(typeof(Task1), typeDiscriminator: "1")]
[JsonDerivedType(typeof(Task2), typeDiscriminator: "2")]
abstract class Task
{
    protected string text = " ";
    public string Text
    {
        get => text;
        protected set => text = value;
    }
    public Task(string text)
    {
        this.text = text;
    }
}
class Task1 : Task
{
    private int answer;
    public int Answer
    {
        get { return answer; }
        set { answer = value; }
    }
    [JsonConstructor]
    public Task1(string text) : base(text) { }
    public override string ToString()
    {
        int count = 0;
        string[] words = text.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            int k = 0;
            for (int j = 0; j < words[i].Length; j++)
            {
                if (Char.IsLetter(words[i][j]))
                {
                    k++;
                }
            }
            if (k == 1) count++;
        }
        answer = count;
        return count.ToString();
    }
}
class Task2 : Task
{
    private bool answer;
    public bool Answer
    {
        get { return answer; }
        set { answer = value; }
    }
    [JsonConstructor]
    public Task2(string text) : base(text)
    {
    }
    public override string ToString()
    {
        bool flag = true;
        char[] skobki = { '(', '[', '{', ')', ']', '}' };
        for (int j = 0; j < 3; j++)
        {
            int q = 0;
            char[] drugieskobki = new char[2];
            for (int i = 3; i < 6; i++)
            {
                if (skobki[i] != skobki[j + 3])
                {
                    drugieskobki[q] = skobki[i];
                    q++;
                }
            }
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == skobki[j])
                {
                    for (int k = i + 1; k < text.Length; k++)
                    {
                        if (text[k] == skobki[j + 3]) break;
                        if (text[k] == drugieskobki[0] || text[k] == drugieskobki[1])
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (flag == false) break;
            }
        }
        answer = flag;
        return flag.ToString();
    }
}
class JsonIO
{
    public static void Write<T>(T obj, string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize(fs, obj);
        }
    }
    public static T Read<T>(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            return JsonSerializer.Deserialize<T>(fs);
        }
    }
}
class Program
{
    static void Main()
    {
        string text = "Добрый день. Меня зовут Александр (Тылибцев). Я ученик в вузе МИСиС. На пару я пришел с хорошим настроением и отличной подготовкой к контрольной работ.";
        Task[] tasks =
        {
            new Task1(text),
            new Task2(text)
        };
        Console.WriteLine(tasks[0]);
        Console.WriteLine(tasks[1]);

        string path = @"C:\Users\m2302588\";
        string folderName = "Test";
        path = Path.Combine(path, folderName);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string fileName1 = "cw2_1.json";
        string fileName2 = "cw2_2.json";

        fileName1 = Path.Combine(path, fileName1);
        fileName2 = Path.Combine(path, fileName2);

        if (!File.Exists(fileName1))
        {
            JsonIO.Write<Task1>((Task1)tasks[0], fileName1);
        }
        else
        {
            var task1 = JsonIO.Read<Task1>(fileName1);
            Console.WriteLine(task1);
        }

        if (!File.Exists(fileName2))
        {
            JsonIO.Write<Task2>((Task2)tasks[1], fileName2);
        }
        else
        {
            var task2 = JsonIO.Read<Task2>(fileName2);
            Console.WriteLine(task2);
        }

    }
}