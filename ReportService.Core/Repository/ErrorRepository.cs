using ReportService.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Core.Repository
{
    public class ErrorRepository
    {
        public List<Error> GetLastErrors(int IntervalInminutes)
        {
            return new List<Error>
            {
                new Error {Message = "Błąd tetowy 1", Date = DateTime.Now},
                new Error {Message = "Błąd tetowy 2", Date = DateTime.Now}
            };
        }
    }
}
