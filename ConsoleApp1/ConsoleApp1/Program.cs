using System;

class Program
{
    static void Main(string[] args)
    {
        var factories = GetFactories();
        var units = GetUnits();
        var tanks = GetTanks();

        Console.WriteLine($"Количество заводов: {factories.Length}, установок: {units.Length}, резервуаров: {tanks.Length}");

        foreach (var tank in tanks)
        {
            Console.WriteLine($"Резервуар \"{tank.Name}\" с объемом {tank.Volume}/{tank.MaxVolume}, принадлежит установке \"{units[tank.UnitId - 1].Name}\" и заводу \"{factories[units[tank.UnitId - 1].FactoryId - 1].Name}\"");
        }

        var totalVolume = GetTotalVolume(tanks);
        Console.WriteLine($"Общий объем всех резервуаров: {totalVolume}");

       
        // Добавляем функциональность поиска по наименованию
        Console.WriteLine("Введите название объекта для поиска:");
        string searchName = Console.ReadLine();

        SearchAndDisplayObject(factories, "завод", searchName);
        SearchAndDisplayObject(units, "установка", searchName);
        SearchAndDisplayObject(tanks, "резервуар", searchName);
    }

    public static void SearchAndDisplayObject<T>(T[] objects, string objectType, string searchName) where T : class
    {
        foreach (var obj in objects)
        {
            dynamic dynamicObj = obj; // Используем dynamic для доступа к свойствам
            if (dynamicObj.Name.Contains(searchName))
            {
                Console.WriteLine($"Найден {objectType}: {dynamicObj.Name}");
            }
        }
    }

   
    public static Factory[] GetFactories()
    {
        return new Factory[]
        {
            new Factory { Id = 1, Name = "НПЗ№1", Description = "Первый нефтеперерабатывающий завод" },
            new Factory { Id = 2, Name = "НПЗ№2", Description = "Второй нефтеперерабатывающий завод" }
        };
    }

    public static Unit[] GetUnits()
    {
        return new Unit[]
        {
            new Unit { Id = 1, Name = "ГФУ-2", Description = "Газофракционирующая установка", FactoryId = 1 },
            new Unit { Id = 2, Name = "АВТ-6", Description = "Атмосферно-вакуумная трубчатка", FactoryId = 1 },
            new Unit { Id = 3, Name = "АВТ-10", Description = "Атмосферно-вакуумная трубчатка", FactoryId = 2 }
        };
    }

    public static Tank[] GetTanks()
    {
        return new Tank[]
        {
            new Tank { Id = 1, Name = "Резервуар 1", Description = "Надземный-вертикальный", Volume = 1500, MaxVolume = 2000, UnitId = 1 },
            new Tank { Id = 2, Name = "Резервуар 2", Description = "Надземный-горизонтальный", Volume = 2500, MaxVolume = 3000, UnitId = 1 },
            new Tank { Id = 3, Name = "Дополнительный резервуар 24", Description = "Надземный-горизонтальный", Volume = 3000, MaxVolume = 3000, UnitId = 2 },
            new Tank { Id = 4, Name = "Резервуар 35", Description = "Надземный-вертикальный", Volume = 3000, MaxVolume = 3000, UnitId = 2 },
            new Tank { Id = 5, Name = "Резервуар 47", Description = "Подземный-двустенный", Volume = 4000, MaxVolume = 5000, UnitId = 2 },
            new Tank { Id = 6, Name = "Резервуар 256", Description = "Подводный", Volume = 500, MaxVolume = 500, UnitId = 3 }
        };
    }

    public static int GetTotalVolume(Tank[] tanks)
    {
        int totalVolume = 0;
        foreach (var tank in tanks)
        {
            totalVolume += tank.Volume;
        }
        return totalVolume;
    }
}

public class Factory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class Unit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int FactoryId { get; set; }
}

public class Tank
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Volume { get; set; }
    public int MaxVolume { get; set; }
    public int UnitId { get; set; }
}


