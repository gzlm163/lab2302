using System;
using System.Collections.Generic;
public abstract class Animal
{
    public string Name;
    public int Age;
    public string Habitat;
    public string DietType;
    public string Color;
    public double Weight;

    protected Animal(string name, int age, string habitat, string dietType, string color, double weight)
    {
        Name = name;
        Age = age;
        Habitat = habitat;
        DietType = dietType;
        Color = color;
        Weight = weight;
    }

    public virtual string GetInfo()
    {
        return $"Name: {Name}, Age: {Age}, Habitat: {Habitat}, Diet: {DietType}, Color: {Color}, Weight: {Weight} kg";
    }

    public abstract string TypeName { get; }
}
public class Mammal : Animal
{
    public bool HasFur { get; }

    public override string TypeName
    {
        get { return "Mammal"; }
    }

    public Mammal(string name, int age, string habitat, string dietType, string color, double weight, bool hasFur)
        : base(name, age, habitat, dietType, color, weight)
    {
        HasFur = hasFur;
    }

    public override string GetInfo()
    {
        string furText = HasFur ? "yes" : "no";
        return $"Type: {TypeName}, {base.GetInfo()}, Has Fur: {furText}";
    }
}
public class Bird : Animal
{
    public double WingSpan { get; }

    public override string TypeName
    {
        get { return "Bird"; }
    }

    public Bird(string name, int age, string habitat, string dietType, string color, double weight, double wingSpan)
        : base(name, age, habitat, dietType, color, weight)
    {
        WingSpan = wingSpan;
    }

    public override string GetInfo()
    {
        return $"Type: {TypeName}, {base.GetInfo()}, Wingspan: {WingSpan} m";
    }
}
public class Fish : Animal
{
    public string WaterType { get; }

    public override string TypeName
    {
        get { return "Fish"; }
    }

    public Fish(string name, int age, string habitat, string dietType, string color, double weight, string waterType)
        : base(name, age, habitat, dietType, color, weight)
    {
        WaterType = waterType;
    }

    public override string GetInfo()
    {
        return $" Type: {TypeName}, {base.GetInfo()}, Water Type: {WaterType}";
    }
}
public class Reptile : Animal
{
    public bool IsVenomous { get; }

    public override string TypeName
    {
        get { return "Reptile"; }
    }

    public Reptile(string name, int age, string habitat, string dietType, string color, double weight, bool isVenomous)
        : base(name, age, habitat, dietType, color, weight)
    {
        IsVenomous = isVenomous;
    }

    public override string GetInfo()
    {
        string venomText = IsVenomous ? "yes" : "no";
        return $" Type: {TypeName}, {base.GetInfo()}, Venomous: {venomText}";
    }
}
public class Amphibian : Animal
{
    public string SkinMoisture { get; }

    public override string TypeName
    {
        get { return "Amphibian"; }
    }

    public Amphibian(string name, int age, string habitat, string dietType, string color, double weight, string skinMoisture)
        : base(name, age, habitat, dietType, color, weight)
    {
        SkinMoisture = skinMoisture;
    }

    public override string GetInfo()
    {
        return $" Type: {TypeName},{base.GetInfo()}, Skin Moisture: {SkinMoisture}";
    }
}
public sealed class AnimalManager
{
    private static AnimalManager _instance;
    private readonly List<Animal> animals;

    private AnimalManager()
    {
        animals = new List<Animal>();
    }

    public static AnimalManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AnimalManager();
            }
            return _instance;
        }
    }

    public void AddAnimal(Animal animal)
    {
        animals.Add(animal);
    }

    public void ShowAllAnimals()
    {
        if (animals.Count == 0)
        {
            Console.WriteLine("No animals found.");
            return;
        }

        for (int animalIndex = 0; animalIndex < animals.Count; ++animalIndex)
        {
            Console.WriteLine($"[{animalIndex}] {animals[animalIndex].GetInfo()}");
        }
    }

    public void ShowAnimalByName(string name)
    {
        for (int animalIndex = 0; animalIndex < animals.Count; ++animalIndex)
        {
            if (animals[animalIndex].Name == name)
            {
                Console.WriteLine(animals[animalIndex].GetInfo());
                return;
            }
        }
        Console.WriteLine("Animal with that name not found.");
    }

    public void RunMenu()
    {
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1 - Add animal");
            Console.WriteLine("2 - Show all animals");
            Console.WriteLine("3 - Show animal by name");
            Console.WriteLine("0 - Exit");
            Console.Write("Select option: ");

            string input = Console.ReadLine();

            if (input == "0") break;

            switch (input)
            {
                case "1":
                    AddAnimalViaMenu();
                    break;

                case "2":
                    ShowAllAnimals();
                    break;

                case "3":
                    Console.Write("Enter name: ");
                    string name = Console.ReadLine();
                    ShowAnimalByName(name);
                    break;

                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }
    }

    private void AddAnimalViaMenu()
    {
        Console.WriteLine("Select animal type:");
        Console.WriteLine("1 - Mammal");
        Console.WriteLine("2 - Bird");
        Console.WriteLine("3 - Fish");
        Console.WriteLine("4 - Reptile");
        Console.WriteLine("5 - Amphibian");
        Console.Write("Type: ");

        string type = Console.ReadLine();

        string name = ReadString("Name: ");
        int age = ReadInt("Age: ");
        string habitat = ReadString("Habitat: ");
        string dietType = ReadString("Diet type: ");
        string color = ReadString("Color: ");
        double weight = ReadDouble("Weight (kg): ");

        Animal animal;

        switch (type)
        {
            case "1":
                bool hasFur = ReadBool("Has fur (yes/no): ");
                animal = new Mammal(name, age, habitat, dietType, color, weight, hasFur);
                break;

            case "2":
                double wingSpan = ReadDouble("Wingspan (m): ");
                animal = new Bird(name, age, habitat, dietType, color, weight, wingSpan);
                break;

            case "3":
                string waterType = ReadString("Water type (fresh/salt): ");
                animal = new Fish(name, age, habitat, dietType, color, weight, waterType);
                break;

            case "4":
                bool isVenomous = ReadBool("Venomous (yes/no): ");
                animal = new Reptile(name, age, habitat, dietType, color, weight, isVenomous);
                break;

            case "5":
                string skinMoisture = ReadString("Skin moisture: ");
                animal = new Amphibian(name, age, habitat, dietType, color, weight, skinMoisture);
                break;

            default:
                Console.WriteLine("Unknown animal type.");
                return;
        }

        AddAnimal(animal);
        Console.WriteLine("Animal added.");
    }

    private string ReadString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();

            Console.WriteLine("Input cannot be empty.");
        }
    }

    private int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value) && value >= 0)
                return value;

            Console.WriteLine("Please enter a valid non-negative integer.");
        }
    }

    private double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out double value) && value >= 0)
                return value;

            Console.WriteLine("Please enter a valid non-negative number.");
        }
    }

    private bool ReadBool(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);

            string input = Console.ReadLine();
            if (input != null)
            {
                input = input.Trim().ToLower();
            }

            if (input == "yes") return true;
            if (input == "no") return false;
            Console.WriteLine("Please enter 'yes' or 'no'.");
        }
    }
}
class Program
{
    static void Main()
    {
        var manager = AnimalManager.Instance;

        var lion = new Mammal("Barsik", 5, "forest", "predator", "golden", 19.5, true);
        var eagle = new Bird("Eagle", 3, "mountains", "predator", "brown", 6.3, 2.0);
        var salmon = new Fish("Salmon", 2, "river", "omnivore", "silver", 4.5, "fresh");

        manager.AddAnimal(lion);
        manager.AddAnimal(eagle);
        manager.AddAnimal(salmon);

        manager.RunMenu();
    }
}