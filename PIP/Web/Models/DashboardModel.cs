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
                    CoreProjects = projects.CoreProjects,
                    CoreProjectsPercent = (projects.CoreProjects / projects.TotalPortfolio * 100m),
                    StrategicProjects = projects.StrategicProjects,
                    StrategicProjectsPercent = (projects.StrategicProjects / projects.TotalPortfolio * 100m),
                    TotalPortfolio = projects.TotalPortfolio,
                    NumberOfProjects = list.Count(),
                    TotalChevron = projects.TotalChevron,
                    TotalChevronPercent = (projects.TotalChevron / projects.TotalPortfolio * 100m),
                    TotalCocaCola = projects.TotalCocaCola,
                    TotalCocaColaPercent = (projects.TotalCocaCola / projects.TotalPortfolio * 100m),
                    TotalGates = projects.TotalGates,
                    TotalGatesPercent = (projects.TotalGates / projects.TotalPortfolio * 100m),
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
        public decimal TotalPortfolio { get; set; }
        public decimal CoreProjects { get; set; }
        public decimal CoreProjectsPercent { get; set; }
        public decimal TotalGates { get; set; }
        public decimal TotalGatesPercent { get; set; }

        public int NumberOfProjects { get; set; }
        public decimal StrategicProjects { get; set; }
        public decimal StrategicProjectsPercent { get; set; }
        public decimal TotalChevron { get; set; }
        public decimal TotalChevronPercent { get; set; }
        public decimal TotalCocaCola { get; set; }
        public decimal TotalCocaColaPercent { get; set; }
    }

    public class ChartModel
    {
        public List<CountryModel> CountryList { get; set; }
        public decimal MaxAward { get; set; }
    }

}


