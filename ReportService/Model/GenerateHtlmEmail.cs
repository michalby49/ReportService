using ReportService.Model.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Model
{
    class GenerateHtlmEmail
    {
        public string GenerateErrors (List<Error> errors, int interval)
        {
            if (errors == null)
                throw new ArgumentNullException(nameof(errors));
            
            if (!errors.Any())
                return string.Empty;

            var htlm = $"Błędby z ostatnich {interval} minut.<br /><br />";
            htlm +=
                @"
                    <table border=1 cellpadding=5 cellspacing=1>
                        <tr>
                            <td allain=center bgcolor=lightgrey>Wiadomość</td>
                            <td allain=center bgcolor=lightgrey>Data</td>
                        </tr>
                 ";
            foreach(var error in errors)
            {
                htlm +=
                    $@"<tr>
                           <td>{error.Message}</td> 
                           <td>{error.Date.ToString("dd-MM-yyyy HH:mm")}</td> 
                        </tr>
                    "; 
            }
            htlm += @"</table> <br /><br /> <i>Automatyczna wiadomość wysłana z aplikacji Report Service.</i>";
            return htlm;
        }

        public string GenerateReport(Report report)
        {
            if (report == null)
                throw new ArgumentNullException(nameof(report));

            var htlm = $"Raport {report.Title} z dnia {report.Date.ToString("dd-MM-yyyy")}./<br /><br />";

            if(report.Positions.Any() && report.Positions != null)
            {
            htlm +=
                @"
                    <table border=1 cellpadding=5 cellspacing=1>
                        <tr>
                            <td allain=center bgcolor=lightgrey>Tytuł</td>
                            <td allain=center bgcolor=lightgrey>Opis</td>
                            <td allain=center bgcolor=lightgrey>Wartość</td>

                        </tr>
                 ";
                foreach (var position in report.Positions)
                {
                    htlm +=
                        $@"<tr>
                           <td>{position.Title}</td> 
                           <td>{position.Destription}</td> 
                           <td>{position.Value.ToString("0.00")} zł</td> 
                        </tr>
                    ";
                }
                htlm += @"</table>:";
            }  
            else
                htlm += "--brak danych do wyświetlenia--";
                htlm += @"<br /><br /> <i>Automatyczna wiadomość wysłana z aplikacji Report Service.</i>";
            return htlm;
           
        }
    }
}
