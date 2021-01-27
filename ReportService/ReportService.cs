using EmailSender;
using ReportService.Core.Repository;
using ReportService.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ReportService
{
    public partial class ReportService : ServiceBase
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private const int SendHour = 8;
        private const int IntervalInMinutes = 60;
        private Timer _timer = new Timer( IntervalInMinutes * 60000);
        private ErrorRepository _errorRepository = new ErrorRepository();
        private ReportRepository _reportRepository = new ReportRepository();
        private Email _email;
        private GenerateHtlmEmail _htlmEmail;
        private string _emailReciver = "wiktoria.freus1@gmail.com";
        public ReportService()
        {
            InitializeComponent();
            _email = new Email(new EmailParams
            {
                HostSmtp = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                SenderName = "Michał Beśka",
                SenderEmail = "reportservicetest@gmail.com",
                SenderEmailPassword = "btowczbeplzobilj"
            });
        }

        protected override void OnStart(string[] args)
        {
            _timer.Elapsed += DoWork;
            _timer.Start();
            Logger.Info("Service start");
        }

        private async void DoWork(object sender, ElapsedEventArgs e)
        {         
            try
            {
                await SendErrors();
                await SendReport();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private async Task SendErrors()
        {
            var errors = _errorRepository.GetLastErrors(IntervalInMinutes);

            if (errors == null || !errors.Any())
                return;

            await _email.Send("Błędy w aplikacji", _htlmEmail.GenerateErrors(errors, IntervalInMinutes), _emailReciver);  
            Logger.Info("Error sent");
        }
        private async Task SendReport()
        {
            var actualHour = DateTime.Now.Hour;

            if (actualHour < SendHour)
                return;

            var report = _reportRepository.GetLastNotSentReport();

            if (report == null)
                return;

            await _email.Send("Raport dobowy", _htlmEmail.GenerateReport(report), _emailReciver);

            _reportRepository.RaportSent(report);

            Logger.Info("Report sent");
        }
        protected override void OnStop()
        {
            Logger.Info("Service stop");
        }
    }
}
