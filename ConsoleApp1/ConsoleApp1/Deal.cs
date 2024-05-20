using System.Collections.Immutable;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;

namespace ConsoleApp1;

public class Deal
{
    public int Sum {get;set;}
    public string? Id {get;set;}
    public DateTime Date {get;set;}

    public static IEnumerable<Deal> GetDealsFromJson(string pathName)
    {
        IEnumerable<Deal> deals;
        using FileStream fs = new FileStream(pathName, FileMode.OpenOrCreate);
        deals = JsonSerializer.Deserialize<IEnumerable<Deal>>(fs);
        return deals;
    }
        
    public static IList<string> GetNumbersOfDeals(IEnumerable<Deal> deals)
    {
        IList<string> output = new List<string>();
        var temp = deals.Where(p => p.Sum > 100).OrderBy(p => p.Date).ToArray();
        if (temp.Length >= 5)
        {
            for (int i = 0; i < 5; i++)
            {
                output.Add(temp[i].Sum.ToString() + ' ' + temp[i].Id + ' ' + temp[i].Date.ToString());
                Console.WriteLine(temp[i].Sum.ToString() + ' ' + temp[i].Id + ' '+temp[i].Date.ToString() );
            }
        }
        else
        {
            foreach (var VARIABLE in temp)
            {
                output.Add(VARIABLE.Sum.ToString() + VARIABLE.Id + VARIABLE.Date.ToString());
                Console.WriteLine(VARIABLE.Sum + VARIABLE.Id + VARIABLE.Date);
            }
        }
        

        return output;
    }
    public record SumByMonth(DateTime Month, int Sum);
    
    public static IList<SumByMonth> GetSumsByMonth(IEnumerable<Deal> deals)
    {
        IList<SumByMonth> output = new List<SumByMonth>();
        var temp = deals.GroupBy(p => new DateTime(Convert.ToInt32(p.Date.Year),
            Convert.ToInt32(p.Date.Month),
            1));;
        foreach (var VARIABLE in temp)
        {
            int sum = 0;
            
            foreach (var deal in VARIABLE)
            {
                sum += deal.Sum;
            }
            
            output.Add(new SumByMonth(VARIABLE.Key, sum));
        }

        return output;
    }

    

}