using Business.Entities;
using Business.Interface;
using Microsoft.AspNetCore.Http;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ERP.Helpers
{
    public interface ILoggerManager
    {
        void LogDebug(string message);
        void LogError(string message);
        void LogInformation(string message);
        void LogWarning(string message);
        void LogDebug(Exception e, string message);
        void LogError(Exception e, string message);
        void LogInformation(Exception e, string message);
        void LogWarning(Exception e, string message);
    }

    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        private static HttpContext Current => new HttpContextAccessor().HttpContext;
        private static ISuperAdminService superAdmin => (ISuperAdminService)Current.RequestServices.GetService(typeof(ISuperAdminService));


        public void LogDebug(string message)
        {
            logger.Debug(message);
        }
        public void LogError(string message)
        {
            logger.Error(message);
        }

        public void LogInformation(string message)
        {
            logger.Info(message);
        }

        public void LogWarning(string message)
        {
            logger.Warn(message);
        }
        public void LogDebug(Exception e, string message)
        {
            logger.Debug(e, message);
        }

        public void LogError(Exception e, string message)
        {
            //logger.Error(e, message);
            //SendErrorMail(e);
            LogError(e);
        }

        public void LogInformation(Exception e, string message)
        {
            logger.Info(e, message);
        }

        public void LogWarning(Exception e, string message)
        {
            logger.Warn(e, message);
        }
        private void LogError(Exception _exception)
        {
            try
            {
                ErrorMetadata model = new ErrorMetadata();
                model.ErrorMessage = _exception.Message;
                model.ErrorDescription = _exception.StackTrace.toStringWithDash();
                superAdmin.InsertError(model);
            }
            catch
            {
            }
        }
        public void SendErrorMail(Exception e)
        {
            try
            {
                var body = File.ReadAllText(Directory.GetCurrentDirectory() + "\\wwwroot\\templates\\error.html");
                var replacement = new Dictionary<string, string>
                {
                    { "#UserNameAndUserId#", "API" },
                    { "#Message#", e.Message },
                    { "#Logger#", e?.TargetSite?.DeclaringType?.FullName },
                    { "#CallSite#", e?.TargetSite?.Name },
                    { "#Level#", "Error Logger" },
                    { "#LineAndColumn#", Convert.ToString(new StackTrace(e, true)?.GetFrame(0)?.GetFileLineNumber() )},
                    { "#StackTrace#", e.StackTrace }
                };
                body = replacement.Aggregate(body, (result, s) => result.Replace(s.Key, s.Value));
                try
                {
                    //RandDPOSSmtpClient client = SettingsController.SmtpClient;
                    //if (client != null)
                    //{
                    //    IMailSender mailer = new SmtpMailSender(client);
                    //    IRandDPOSMail mail = new RandDPOSMail()
                    //    .Body(body)
                    //    .From(client.From, client.DisplayName)
                    //    .Subject("Error in Application -- " + DateTime.UtcNow.ToString("dd/MM/yyyy HH.mm tt" + " (TZ.UTC)"))
                    //    .To(Startup.StaticConfig.GetSection("EmailConfig").GetValue<string>("Kinfo.RandDPOS.Email.ErrorEmails"))
                    //    .BodyAsHtml();
                    //    bool sendSucess = mailer.New(mail).Send();
                    //}
                }
                catch (Exception ex)
                {
                    LogError("Error: [Sending email for Logger] " + ex != null ? ex.Message : ex.Message);
                }
            }
            catch (Exception ex)
            {
                LogError("Error: [Sending email for Logger] " + ex.Message);
            }
        }
    }
}
