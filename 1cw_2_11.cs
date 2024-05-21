using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Profession
{
    public string Field { get; set; }
    public Guid Uid { get; private set; }
    public int BaseSalary { get; set; }
    public string Description { get; private set; }

    public Profession(string field, int baseSalary, string description)
    {
        if (description.Length < 20 || description.Length > 200)
        {
            throw new ArgumentException("Описание должно быть не менее 20 и не более 200 символов.");
        }

        Field = field;
        Uid = Guid.NewGuid();
        BaseSalary = baseSalary;
        Description = description;
    }

    public abstract int CalculateSalary();

    public void DisplayInfo()
    {
        Console.WriteLine($"ID: {Uid}");
        Console.WriteLine($"Сфера: {Field}");
        Console.WriteLine($"Зарплата: {CalculateSalary()}");
        Console.WriteLine($"Описание: {Description}");
    }

    public void UpdateDescription(string newDescription)
    {
        if (newDescription.Length < 20 || newDescription.Length > 200)
        {
            throw new ArgumentException("Описание должно быть не менее 20 и не более 200 символов.");
        }
        Description = newDescription;
    }
}

public class Fireman : Profession
{
    public int DangerAllowance { get; set; }

    public Fireman(string field, int baseSalary, string description, int dangerAllowance)
        : base(field, baseSalary, description)
    {
        DangerAllowance = dangerAllowance;
    }

    public override int CalculateSalary()
    {
        return BaseSalary + DangerAllowance;
    }
}

public class Engineer : Profession
{
    public string Category { get; set; }

    public Engineer(string field, int baseSalary, string description, string category)
        : base(field, baseSalary, description)
    {
        Category = category;
    }

    public override int CalculateSalary()
    {
        switch (Category)
        {
            case "Junior":
                return BaseSalary;
            case "Middle":
                return BaseSalary + 20000;
            case "Senior":
                return BaseSalary + 50000;
            default:
                return BaseSalary;
        }
    }
}

public class Scientist : Profession
{
    public string Degree { get; set; }

    public Scientist(string field, int baseSalary, string description, string degree)
        : base(field, baseSalary, description)
    {
        Degree = degree;
    }

    public override int CalculateSalary()
    {
        switch (Degree)
        {
            case "PhD":
                return BaseSalary + 30000;
            case "Doctor":
                return BaseSalary + 50000;
            default:
                return BaseSalary;
        }
    }
}

public class Program
{
    public static void Main()
    {
        var firemen = new List<Fireman>
        {
            new Fireman("Пожарник", 70000, "Самый обычный новичок пожарник.", 20000),
            new Fireman("Пожарник", 72000, "Пожарник с доп. возможностями.", 25000),
            new Fireman("Пожарник", 68000, "Пожарник в опасных зонах.", 18000),
            new Fireman("Пожарник", 75000, "Опытный пожарник.........", 22000),
            new Fireman("Пожарник", 73000, "Очень опытный пожарник.", 23000)
        };

        var engineers = new List<Engineer>
        {
            new Engineer("Инжинер", 80000, "Начинающий IT инжинер.", "Junior"),
            new Engineer("Инжинер", 85000, "Средний IT инжинер......", "Middle"),
            new Engineer("Инжинер", 90000, "Сеньор в IT разработке.", "Senior"),
            new Engineer("Инжинер", 87000, "Средний механик-инжинер.", "Middle"),
            new Engineer("Инжинер", 95000, "Тимлид в IT разработке.", "Senior")
        };


        var scientists = new List<Scientist>
        {
            new Scientist("Ученый", 95000, "Обычный ученый без допов.", "PhD"),
            new Scientist("Ученый", 100000, "Ученый с докторской степенью", "Doctor"),
            new Scientist("Ученый", 92000, "Ученый с мастерской степенью.", "Master"),
            new Scientist("Ученый", 97000, "Ученый-биолог в лаборатории.", "PhD"),
            new Scientist("Ученый", 93000, "Ученый-химик в лаборатории.", "Bachelor")
        };

        DisplaySortedProfessions(firemen.ConvertAll(p => (Profession)p), "Пожарники");
        DisplaySortedProfessions(engineers.ConvertAll(p => (Profession)p), "Инжинеры");
        DisplaySortedProfessions(scientists.ConvertAll(p => (Profession)p), "Ученые");

        var allProfessions = new List<Profession>();
        allProfessions.AddRange(firemen);
        allProfessions.AddRange(engineers);
        allProfessions.AddRange(scientists);

        Console.WriteLine("\nВсе профессии отсортированные по убыванию ЗП:");
        DisplaySortedProfessions(allProfessions);
    }

    public static void DisplaySortedProfessions(List<Profession> professions, string title = null)
    {
        if (title != null)
        {
            Console.WriteLine($"\n{title} Сортировка по ЗП:");
        }

        var sortedProfessions = professions.OrderByDescending(p => p.CalculateSalary()).ToList();

        foreach (var profession in sortedProfessions)
        {
            profession.DisplayInfo();
            Console.WriteLine(new string('-', 60));
        }
    }
}