using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ProjectsDao
    {
        public List<Project> GetProjects(Stream strm)
        {
            DataTable tmp;
            ExcelDataReader.ExcelDataReader spreadsheet;

            // try read excel
            try
            {
                spreadsheet = new ExcelDataReader.ExcelDataReader(strm);
            }
            catch (ExcelDataReader.Core.InvalidHeaderException ex)
            {
                
                return null;
            }
            catch (Exception ex)
            {
                
                return null;
            }
            strm.Close();

            // try get table
            if (spreadsheet.WorkbookData.Tables.Count > 0)
            {
                tmp = spreadsheet.WorkbookData.Tables[0].Copy();
            }
            else
            {
                //Log.Warn(String.Format("No sheets found inside excel file."));
                return null;
            }
            if (tmp.Rows.Count == 0)
            {
                //Log.Warn("Excel file must have more than 1 row.");
                return null;
            }

            // try create customized rows
            var recList = new List<ImportRecord>();
            if (tmp.Columns.Count < 27)
            {
                Log.Warn("Excel file must have at least 27 columns.");
                return null;
            }

            foreach (DataRow row in tmp.Rows)
            {
                if (row == null)
                    continue;

                ImportRecord rec;
                try
                {
                    rec = new ImportRecord { UniqueID = row[0].GetString() };
                    var archticsId = row[1].GetString();
                    rec.First = row[2].GetString();
                    rec.Last = row[3].GetString();
                    rec.Organization = row[4].GetString();
                    rec.StreetAddress = row[5].GetString();
                    rec.City = row[6].GetString();
                    rec.State = row[7].GetString();
                    rec.Zip = row[8].GetString();
                    rec.Country = row[9].GetString();
                    rec.PhoneNumber = row[10].GetString();
                    rec.Email = row[11].GetString();
                    rec.Sex = row[12].GetString();
                    rec.BirthDate = row[13].GetString();
                    rec.SSN = row[14].GetString();
                    rec.SSN = (rec.SSN != null) ? rec.SSN.Replace("-", "").Replace(" ", "") : null;
                    rec.PlaceOfBirth = row[15].GetString();
                    rec.PPN = row[16].GetString();
                    rec.BadgeDescription = row[17].GetString();
                    rec.BadgeType = row[18].GetString();
                    rec.AllSession = row[19].GetString();
                    rec.Arrive = row[20].GetString();
                    rec.Depart = row[21].GetString();
                    rec.Meal = row[22].GetString();
                    rec.MealCode = row[23].GetString();
                    rec.AccessCodes = row[24].GetString();
                    rec.GeneralRelease = row[25].GetString();
                    rec.PassportCountry = row[26].GetString();
                    // skeep not valid row
                    try
                    {
                        int uId = Convert.ToInt32(rec.UniqueID);
                        if (uId <= 0)
                            continue;

                        if (!string.IsNullOrEmpty(archticsId))
                        {
                            rec.ArchticsID = Convert.ToInt32(archticsId);
                            if (rec.ArchticsID <= 0)
                            {
                                continue;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Warn("Could not accept archtics import record, perhaps header or empty row.", ex);
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("Could not accept archtics import record ", ex);
                    continue;
                }
                recList.Add(rec);
            }

            return recList;
        }
    }
}
