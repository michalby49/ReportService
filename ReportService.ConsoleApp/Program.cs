using EmailSender;
using ReportService.Core;
using ReportService.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var emailReciver = "";
            var htlmEmail = new GenerateHtlmEmail();

            var email = new Email(new EmailParams
            {
                HostSmtp = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                SenderName = "",
                SenderEmail = "",
                SenderEmailPassword = ""
            });
            
            var reports = new Report
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

            var errors = new List<Error>
            {
                new Error {Message = "Błąd tetowy 1", Date = DateTime.Now},
                new Error {Message = "Błąd tetowy 2", Date = DateTime.Now}
            };

            Console.WriteLine("wysyłanie błędów");
            email.Send("Błędy w aplikacji", htlmEmail.GenerateErrors(errors, 10), emailReciver).Wait();

            Console.WriteLine("Wysyłanie raportu");
            email.Send("Raport dobowy", htlmEmail.GenerateReport(reports), emailReciver).Wait();

            Console.WriteLine("wysłano");
        }
    }
}
