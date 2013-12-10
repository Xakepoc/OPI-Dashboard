using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Web.Models
{
    public class ProjectListingModel
    {
        public string SelectedRegion { get; set; }
        public string SelectedCountry { get; set; }
        public string SelectedImpactArea { get; set; }
        public string SelectedCoreCompetency { get; set; }
        public string InitFilterString { get; set; }

        public List<ProjectModel> ListOfProjects { get; set; }
        public List<string> ListOfCountries { get; set; }
        public List<string> ListOfRegions { get; set; }
        public List<string> ListOfImpactAreas { get; set; }
        public List<string> ListOfCoreCompetency { get; set; }

        public void Init(string region, string country, string impactArea, string coreCompetency)
        {
            var projects = new ListOfProjectsModel();
            var list = projects.Projects;

            SelectedRegion = region;
            SelectedCountry = country;
            SelectedImpactArea = impactArea;
            SelectedCoreCompetency = coreCompetency;
            var str = new StringBuilder();
            if (!string.IsNullOrEmpty(SelectedRegion))
            {
                str.Append("['" + SelectedRegion.Replace(' ', '_') + "',");
            }
            else
            {
                str.Append("['all',");
            }
            if (!string.IsNullOrEmpty(SelectedCountry))
            {
                str.Append("'" + SelectedCountry.Replace(' ', '_') + "',");
            }
            else
            {
                str.Append("'all',");
            }
            if (!string.IsNullOrEmpty(SelectedImpactArea))
            {
                str.Append("'" + SelectedImpactArea.Replace(' ', '_') + "',");
            }
            else
            {
                str.Append("'all',");
            }
            if (!string.IsNullOrEmpty(SelectedCoreCompetency))
            {
                str.Append("'" + SelectedCoreCompetency.Replace(' ', '_') + "']");
            }
            else
            {
                str.Append("'all']");
            }

            InitFilterString = str.ToString();

            ListOfRegions = list.GroupBy(arg => arg.Region).Select(g => g.Key).ToList();
            ListOfCountries = list.GroupBy(arg => arg.Country).Select(g => g.Key).ToList();
            ListOfImpactAreas = list.GroupBy(arg => arg.ImpactArea).Select(g => g.Key).ToList();
            ListOfCoreCompetency = list.GroupBy(arg => arg.CoreCompetancy).Select(g => g.Key).ToList();
            ListOfProjects = list.OrderBy(arg => arg.Country).ToList();

        }
    }
}