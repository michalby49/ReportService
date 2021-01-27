using ReportService.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Core.Repository
{
    public class ReportRepository
    {
        public Report GetLastNotSentReport()
        {
            // pobieranie z bd

            return new Report
            {
                Id = 1,
                Title = "R/1/2021",
                Date = new DateTime(2020, 1, 1, 12, 0, 0),
                Positions = new List<ReportPosition>
                {
                    new ReportPosition
                    {
                        Id = 1,
                        RaportId = 1,
                        Title = "Position One",
                        Destription = "Description One",
                        Value = 43.01M

                    },
                    new ReportPosition
                    {
                        Id = 3,
                        RaportId = 1,
                        Title = "Position Three",
                        Destription = "Description Three",
                        Value = 123M

                    }
                }
            };
        }

        public void RaportSent(Report report)
        {
            report.IsSend = true;
            // zapis w bd
        }
    }
}
