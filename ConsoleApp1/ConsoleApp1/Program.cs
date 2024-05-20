using Lab1;
using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1;
class Program
{
    static void Main(string[] args)
    {
        //CreateTanks();
        Tank[] tanks = GetTanks().Result;
        Unit[] units = GetUnits().Result;
        Factory[] factories = GetFactories().Result;
        Console.WriteLine($"Количество резервуаров: {tanks.Length}, установок: {units.Length}");

        var foundUnit = FindUnit(units, tanks, "Резервуар 2");
        var factory = FindFactory(factories, foundUnit);

        Console.WriteLine($"Резервуар 2 принадлежит установке {foundUnit.Name} и заводу {factory.Name}");

        var totalVolume = GetTotalVolume(tanks);
        Console.WriteLine($"Общий объем резервуаров: {totalVolume}");
        Output(tanks, units, factory);
        var deals = Deal.GetDealsFromJson("../../../JSON_sample_1.json");

        var list = Deal.GetNumbersOfDeals(deals);
        foreach (var VARIABLE in list)
        {
            Console.WriteLine(VARIABLE);
        }

        using (FileStream fs = new FileStream("../../../GetNumbersOfDeals.json", FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize<IList<string>>(fs, list);
        }

        var list2 = Deal.GetSumsByMonth(deals);

        foreach (var VARIABLE in list2)
        {
            Console.WriteLine(VARIABLE.Sum.ToString() + ' ' + VARIABLE.Month.ToString("yyyy/MM"));
        }

        using (FileStream fs = new FileStream("../../../GetSumsByMonth.json", FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize<IList<Deal.SumByMonth>>(fs, list2);
        }
    }

    public async static Task<Tank[]> GetTanks()
    {
        List<Tank> tanks;
        using (FileStream fs = new FileStream("../../../tanks.json", FileMode.OpenOrCreate))
        {
            var options = new JsonSerializerOptions
            {
           
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            tanks = await JsonSerializer.DeserializeAsync<List<Tank>>(fs, options);
            
        }
        return tanks.ToArray();
    }

    public async static Task<Unit[]> GetUnits()
    {
        List<Unit> units;
        using (FileStream fs = new FileStream("../../../units.json", FileMode.OpenOrCreate))
        {
            var options = new JsonSerializerOptions
            {

                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            units = await JsonSerializer.DeserializeAsync<List<Unit>>(fs, options);

        }
        return units.ToArray();
    }

    public async static Task<Factory[]> GetFactories()
    {

        List<Factory> factories;
        using (FileStream fs = new FileStream("../../../factories.json", FileMode.OpenOrCreate))
        {
            var options = new JsonSerializerOptions
            {

                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            factories = await JsonSerializer.DeserializeAsync<List<Factory>>(fs, options);



        }
        return factories.ToArray();
    }

    public static Unit? FindUnit(Unit[] units, Tank[] tanks, string unitName)
    {
        var findedTank = from tank in tanks
                         where tank.Name == unitName
                         orderby tank.Name
                         select tank;
       
        if (findedTank.Count() == 0) Console.WriteLine("Такого резервуара не существует");
        else
        {
            var tankUnitID = findedTank.ToArray()[0].UnitId;
            var findedUnit = from unit in units
                             where tankUnitID == unit.Id
                             select unit;
            return findedUnit.ToArray()[0];
        }
        return null;
    }

    //public static Unit? FindUnit(Unit[] units, Tank[] tanks, string unitName)
    //{
    //    var findedTank = tanks.Where(tank => tank.Name == unitName).OrderBy(tank => tank.Name);
    //    var tankUnitID = findedTank.ToArray()[0].UnitId;
    //    if (findedTank == null) Console.WriteLine("Такого резервуара не существует");
    //    else
    //    {
    //        var findedUnit = units.Where(unit => tankUnitID == unit.Id);
    //        return findedUnit.ToArray()[0];
    //    }
    //    return null;
    //}



    public static Factory FindFactory(Factory[] factories, Unit unit)
    {
        var findedFactory = from factory in factories
                            where factory.Id == unit.FactoryId
                            select factory;
        if (findedFactory != null) return findedFactory.ToArray()[0];
        return null;
    }

    //public static Factory FindFactory(Factory[] factories, Unit unit)
    //{
    //    var findedFactory = factories.Where(factory => factory.Id == unit.FactoryId);
    //    if (findedFactory != null) return findedFactory.ToArray()[0];
    //    return null;
    //}

    // реализуйте этот метод, чтобы он возвращал суммарный объем резервуаров в массиве
    public static int GetTotalVolume(Tank[] units)
    {
        var selectedVolume = from tank in units
                             select tank.MaxVolume;

        int SummaryVolume = selectedVolume.Sum();

        return SummaryVolume;
    }
    //public static int GetTotalVolume(Tank[] units)
    //{
    //    var selectedVolume = units.Select(tank => tank.MaxVolume);

    //    int SummaryVolume = selectedVolume.Sum();

    //    return SummaryVolume;
    //}
    public async static void Output(params object[] input)
    {
        List<object> outputs = new List<object>();
        outputs.AddRange(input);

        using (FileStream fs = new FileStream("output.json", FileMode.OpenOrCreate))
        {
            var options = new JsonSerializerOptions
            {

                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            await JsonSerializer.SerializeAsync<List<object>>(fs, outputs, options);



        }
    }
}
