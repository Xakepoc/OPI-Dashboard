using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class DashboardModel
    {
        public List<RegionModel> Regions { get; set; }
        public StatisticModel Statistic { get; set; }
        public ChartModel Chart { get; set; }

        public void Init()
        {
            var projects = new ListOfProjectsModel();
            var list = projects.Projects;
            Regions = (from r in list
                       group r by r.Region
                       into g
                       select new RegionModel
                           {
                               RegionName = g.Key.ToUpper(),
                               CountryList = (from i in g
                                              group i by new {i.Country}
                                              into g2
                                              select new CountryModel
                                                  {
                                                      CountryName = g2.Key.Country,
                                                      ProjectCount = g2.Count(),
                                                  }).ToList()
                           }).OrderByDescending(arg=>arg.CountryList.Count()).ToList();

            Chart = new ChartModel
                {
                    CountryList = (from r in list
                                   group r by r.Country
                                   into g
                                   select new CountryModel
                                       {
                                           CountryName = g.Key,
                                           TotalAward = g.Sum(arg => arg.TotalAward),
                                       }).ToList(),
                    
                    
                };
            Chart.MaxAward = Chart.CountryList.Max(arg => arg.TotalAward);
            Statistic = new StatisticModel
                {
                    CoreProjects = projects.CoreProjects/1000000,
                    TotalWoPGMF = projects.TotalWoPgmf/1000000,
                    NumberOfProjects = list.Count(),
                    LargestAward = list.Max(arg => arg.TotalAward)/1000000,
                    PercentOfPortfolio = projects.PortfolioPersent*100,
                    TotalDirectExpenses = list.Sum(arg => arg.TotalDirectExpenses)/1000000
                };
        }
    }

    public class RegionModel
    {
        public string RegionName { get; set; }
        public List<CountryModel> CountryList { get; set; }
    }

    public class CountryModel
    {
        public string CountryName { get; set; }
        public int ProjectCount { get; set; }
        public decimal TotalAward { get; set; }
    }

    public class StatisticModel
    {
        public decimal LargestAward { get; set; }
        public int NumberOfProjects { get; set; }
        public decimal TotalDirectExpenses { get; set; }
        public decimal CoreProjects { get; set; }
        public decimal PercentOfPortfolio { get; set; }
        public decimal TotalWoPGMF { get; set; }
    }

    public class ChartModel
    {
        public List<CountryModel> CountryList { get; set; }
        public decimal MaxAward { get; set; }
    }

}


