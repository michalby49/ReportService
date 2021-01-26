using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Model.Domains
{
    public class ReportPosition
    {
        public int Id { get; set; }
        public int RaportId { get; set; }
        public string Title { get; set; }
        public string Destription { get; set; }
        public decimal Value { get; set; }
    }
}
