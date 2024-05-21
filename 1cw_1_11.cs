using System;
using System.Collections.Generic;
using System.Linq;

public class Profession
{
    public string Field { get; set; }
    public Guid Uid { get; private set; }
    public int Salary { get; set; }
    public string Description { get; private set; }

    public Profession(string field, int salary, string description)
    {
        if (description.Length < 20 || description.Length > 200)
        {
            throw new ArgumentException("Описание должно быть не менее 20 и не более 200 символов.");
        }

        Field = field;
        Uid = Guid.NewGuid();
        Salary = salary;
        Description = description;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"ID: {Uid}");
        Console.WriteLine($"Сфера: {Field}");
        Console.WriteLine($"Зарплата: {Salary}");
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

public class Program
{
    public static void Main()
    {
        List<Profession> professions = new List<Profession>
        {
            new Profession("Медицина", 150000, "Описание1.................."),
            new Profession("Маркетинг", 85000, "Описание2................."),
            new Profession("IT", 120000, "Описание3.................."),
            new Profession("Образование", 60000, "Описание4................"),
            new Profession("Финансы", 130000, "Описание5...............")
        };

        Profession highestPaidProfession = professions.OrderByDescending(p => p.Salary).First();

        highestPaidProfession.UpdateDescription("Это описание самой прибыльной профессии из данного списка.");

        var sortedProfessions = professions.OrderByDescending(p => p.Salary).ToList();

        foreach (var profession in sortedProfessions)
        {
            profession.DisplayInfo();
            Console.WriteLine(new string('-', 60));
        }
    }
}