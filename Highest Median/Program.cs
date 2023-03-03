using Highest_Median;
using System.Dynamic;
using System.Reflection;

using (var reader = new StreamReader(@"C:\V\country_vaccination_stats.csv"))
{
    int i = 0;
    List<DataType> listAll = new List<DataType>();
    while (!reader.EndOfStream)
    {

        DataType vaccineData = new DataType();
        var line = reader.ReadLine();
        var values = line.Split(',');
        if (i == 0)
        {
            i++;
            continue;
        }
        else
        {

            vaccineData.country = values[0];
            vaccineData.date = values[1];

            if (values[2] == "")
            {
                values[2] = "0";
                vaccineData.daily_vaccinations = Int32.Parse(values[2]);
            }
            else vaccineData.daily_vaccinations = Int32.Parse(values[2]);

            vaccineData.vaccines = values[3];
            listAll.Add(vaccineData);
        }

    }
    FindTopThreeMedian(listAll);
}

void FindTopThreeMedian(List<DataType> listAll)
{
    List<string> CountryList = new List<string>();
    
    List<Pair> FinalList = new List<Pair>();

    foreach (var item in listAll)
    {
        if(!CountryList.Contains(item.country))
        {
            CountryList.Add(item.country);  
        }
    }

    foreach(var item in CountryList)
    {
        Pair Object = new Pair();
        double median = 0;
        List<DataType> listing = new List<DataType>();
        
        foreach (var item2 in listAll) 
        {
              
            if (item == item2.country)
            {
                listing.Add(item2);
            }
        }
        Object.Country = item;
        int counted = listing.Count;
        int halfPart= counted / 2;
        var sort=listing.OrderBy(x => x.daily_vaccinations);
         
        if (counted % 2 == 0)
        {
            median=(Convert.ToDouble(((sort.ElementAt(halfPart).daily_vaccinations + sort.ElementAt(halfPart - 1).daily_vaccinations)) / 2));
            Object.Median = median;
        }
        else
        {
            median=Convert.ToDouble(sort.ElementAt((halfPart / 2) * 2).daily_vaccinations);
            Object.Median = median;
        }

        FinalList.Add(Object);
    }
    var item3 = (from a in FinalList
                 orderby a.Median descending
                 select a).Take(3);
    foreach(var item4 in item3)
    {
        Console.WriteLine(item4.Country + " " + item4.Median);
    }
    
}


