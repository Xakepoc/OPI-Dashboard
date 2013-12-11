using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Excel;

namespace Web.Models
{
    public class ListOfProjectsModel
    {
        public decimal CoreProjects { get; set; }
        public decimal PortfolioPersent { get; set; }
        public decimal TotalWoPgmf { get; set; }
        public List<ProjectModel> Projects
        {
            get
            {
                var path = HttpContext.Current.Server.MapPath(@"~/App_Data/data.xlsx");
                var fs = File.OpenRead(path);
                DataTable tmp;
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                excelReader.IsFirstRowAsColumnNames = true;
                excelReader.Read();
                var list = new List<ProjectModel>();
                while (excelReader.Read())
                {
                    var project = new ProjectModel();
                   
                    project.Zcode = excelReader.GetString(0);
                    if (excelReader.GetString(8) != null && excelReader.GetString(8).TrimEnd() == "core projects")
                        CoreProjects = excelReader.GetDecimal(9);
                    if (excelReader.GetString(8) != null && excelReader.GetString(8).TrimEnd() == "% of portfolio")
                    {
                        var d = double.Parse(excelReader.GetString(9).Replace(',', '.'),
                                                                      System.Globalization.CultureInfo.InvariantCulture);
                        PortfolioPersent = (Decimal)d;
                    }
                        
                    if (excelReader.GetString(8) != null && excelReader.GetString(8).TrimEnd() == "total without PGMF")
                        TotalWoPgmf = excelReader.GetDecimal(9);
                    if(project.Zcode == null)
                        continue;
                    project.ProjectName = excelReader.GetString(1);
                    project.Donor = excelReader.GetString(2);
                    project.DonorDescription = excelReader.GetString(3);
                    project.DonorUrl = excelReader.GetString(4);
                    project.Region = excelReader.GetString(5);
                    project.Country = excelReader.GetString(6);
                    project.StartDate = excelReader.GetDateTime(7);
                    project.EndDate = excelReader.GetDateTime(8);
                    project.TotalAward = excelReader.GetInt32(9);
                    var value = excelReader.GetString(10);
                    if (value != null)
                    {
                        project.TotalDirectExpenses = excelReader.GetInt32(10);
                    }
                    else
                    {
                        project.TotalDirectExpenses = 0;
                    }

                    value = excelReader.GetString(11);
                    if (value != null)
                    {
                        project.TotalSubaward = excelReader.GetInt32(11);
                    }
                    else
                    {
                        project.TotalSubaward = 0;
                    }

                    value = excelReader.GetString(12);
                    if (value != null)
                    {
                        project.TotalFeesOhContingency = excelReader.GetInt32(12);
                    }
                    else
                    {
                        project.TotalFeesOhContingency = 0;
                    }
                    value = excelReader.GetString(13);
                    if (value != null)
                    {
                        var d = double.Parse(value.Replace(',', '.'),
                                                                       System.Globalization.CultureInfo.InvariantCulture);
                        project.CostRecoveryPercentage = (Decimal)d * 100;
                    }
                    else
                    {
                        project.CostRecoveryPercentage = 0;
                    }

                    value = excelReader.GetString(14);
                    if (value != null)
                    {
                        var d = double.Parse(value.Replace(',', '.'),
                                                                       System.Globalization.CultureInfo.InvariantCulture);
                        project.NegotiatedRateDonor = (Decimal)d;
                    }
                    else
                    {
                        project.NegotiatedRateDonor = 0;
                    }

                    project.FixedPrice = excelReader.GetString(15).ToLower() == "yes";
                    project.ImpactArea = excelReader.GetString(16);
                    project.CoreCompetancy = excelReader.GetString(17);
                    project.Description = excelReader.GetString(18);
                    project.ContactsDcOffice = excelReader.GetString(19);
                    project.ContactsCountryOffice = excelReader.GetString(20);
                    project.ProjectStatus = excelReader.GetString(21);
                    list.Add(project);
                }
                excelReader.Close();

                return list;
            }
        } 
    }
}