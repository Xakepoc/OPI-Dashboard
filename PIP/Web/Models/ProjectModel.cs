using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ProjectModel
    {
        public string Zcode { get; set; }
        public string ProjectName { get; set; }
        public string Donor { get; set; }
        public string DonorDescription { get; set; }
        public string DonorUrl { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalAward { get; set; }
        public decimal TotalDirectExpenses { get; set; }
        public decimal TotalSubaward { get; set; }
        public decimal TotalFeesOhContingency { get; set; }
        public decimal CostRecoveryPercentage { get; set; }
        public decimal NegotiatedRateDonor { get; set; }
        public bool FixedPrice { get; set; }
        public string ImpactArea { get; set; }
	    public string CoreCompetancy { get; set; }
        public string Description { get; set; }
        public string ContactsDcOffice { get; set; }
        public string ContactsCountryOffice { get; set; }
        public string ProjectStatus { get; set; }

        public string ContactsDcEmail
        {
            get
            {
                if (string.IsNullOrEmpty(ContactsDcOffice))
                    return string.Empty;

                var str = ContactsDcOffice.Split(':');
                if (str.Length < 1)
                    return string.Empty;
                return str[1];
            }
        }
        public string ContactsDcName
        {
            get
            {
                if (string.IsNullOrEmpty(ContactsDcOffice))
                    return string.Empty;

                var str = ContactsDcOffice.Split(':');
                if (str.Length < 1)
                    return string.Empty;
                return str[0];
            }
        }
        public string ContactsCountryEmail
        {
            get
            {
                if (string.IsNullOrEmpty(ContactsCountryOffice))
                    return string.Empty;

                var str = ContactsCountryOffice.Split(':');
                if (str.Length < 1)
                    return string.Empty;
                return str[1];
            }
        }
        public string ContactsCountryName
        {
            get
            {
                if (string.IsNullOrEmpty(ContactsCountryOffice))
                    return string.Empty;

                var str = ContactsCountryOffice.Split(':');
                if (str.Length < 1)
                    return string.Empty;
                return str[0];
            }
        }

        public void Init(string zCode)
        {
            var projects = new ListOfProjectsModel();
            var list = projects.Projects;
            var p = list.FirstOrDefault(arg => arg.Zcode == zCode);
            if (p != null)
            {
                Zcode = p.Zcode;
                ProjectName = p.ProjectName;
                Donor = p.Donor;
                DonorDescription = p.DonorDescription;
                DonorUrl = p.DonorUrl;
                Region = p.Region;
                Country = p.Country;
                StartDate = p.StartDate;
                EndDate = p.EndDate;
                TotalAward = p.TotalAward;
                TotalDirectExpenses = p.TotalDirectExpenses;
                TotalSubaward = p.TotalSubaward;
                TotalFeesOhContingency = p.TotalFeesOhContingency;
                CostRecoveryPercentage = p.CostRecoveryPercentage;
                NegotiatedRateDonor = p.NegotiatedRateDonor;
                FixedPrice = p.FixedPrice;
                ImpactArea = p.ImpactArea;
                CoreCompetancy = p.CoreCompetancy;
                Description = p.Description;
                ContactsDcOffice = p.ContactsDcOffice;
                ContactsCountryOffice = p.ContactsCountryOffice;
                ProjectStatus = p.ProjectStatus;
            }

        }
    }
}