using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using DT.PCP.CommonDomain;
using DT.PCP.DataAccess;
using DT.PCP.Domain;
using DT.PCP.Utils;
using Mandrill;
using UniSender;

namespace DT.PCP.BussinesServices.Impl
{
    public class EmailService:IEmailService
    {
        private readonly IRepository _repository;

        public EmailService(IRepository repository)
        {
            _repository = repository;
        }

        #region Implementation of IEmailService

        public void SendEmail(Order order)
        {
            var html =
                "<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta name=\"viewport\" content=\"width=device-width\"/><title>Vaucher</title></head><body><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" background=\"http://gifky.com/image.axd?picture=RocketMail/SharedImages/Light-Background/bg.jpg\" align=\"center\" style=\"border-collapse: collapse;text-align: left;background-repeat: repeat;background-color: #e6e6e6;\" id=\"pageContainer\"><tbody><tr><td style=\"padding-top: 30px;padding-bottom: 30px;\"><table width=\"600\" cellspacing=\"0\" cellpadding=\"0\" bgcolor=\"#ffffff\" align=\"center\" style=\"border-collapse: collapse;text-align: left;font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;font-weight: 300;font-size: 12px;line-height: 15pt;color: #777777;\"><tbody><tr><td width=\"270\" valign=\"middle\" style=\"font-size: 19px;padding-left: 30px;font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;color: #000000;border-top: 1px solid rgb(221, 221, 221);text-align: center;line-height: 29pt;\"><h1 style=\"font-weight: 300\">\u041f\u043e\u0437\u0434\u0440\u0430\u0432\u043b\u044f\u0435\u043c, \u0412\u044b \u0443\u0441\u043f\u0435\u0448\u043d\u043e \u043e\u043f\u043b\u0430\u0442\u0438\u043b\u0438 \u043d\u0430\u0440\u0443\u0448\u0435\u043d\u0438\u0435</h1></td></tr><tr><td width=\"270\" valign=\"middle\" style=\"padding-left: 30px;color: rgb(0, 0, 0);text-align: center;line-height: 29pt;font-family: Segoe UI,Helvetica,sans-serif;font-size: 17px;\"><a href=\"http://pcp.azurewebsites.net\" style=\"text-decoration: none;cursor: pointer;color: #518fce;\">pcp.azurewebsites.net</a></td></tr><tr><td width=\"270\" valign=\"middle\" style=\"padding-left: 30px;color: #9099A3;text-align: center;line-height: 17pt;font-family: Segoe UI,Helvetica,sans-serif;font-size: 17px;\"><p> \u0411\u043b\u0430\u0433\u043e\u0434\u0430\u0440\u0438\u043c \u0412\u0430\u0441 \u0437\u0430 \u043f\u0440\u043e\u044f\u0432\u043b\u0435\u043d\u043d\u043e\u0435 \u0434\u043e\u0432\u0435\u0440\u0438\u0435 <br/> \u043a \u043d\u0430\u0448\u0435\u043c\u0443 \u0441\u0430\u0439\u0442\u0443 \u043e\u043f\u043b\u0430\u0442\u044b \u0448\u0442\u0440\u0430\u0444\u043e\u0432 </p></td></tr></tbody></table><table width=\"600\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\" style=\"border-collapse: collapse;text-align: left;font-family: Arial, Helvetica, sans-serif;font-size: 12px;line-height: 15pt;font-weight: 300;color: #777777;\"><tbody><tr><td width=\"270\" valign=\"middle\" bgcolor=\"#d0d0d0\" style=\"font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;font-size: 21px;line-height: 16pt;color: #000000;font-weight: 300;padding-left: 30px;\"><p>\u0414\u0430\u0442\u0430 \u043e\u043f\u043b\u0430\u0442\u044b: </p></td><td width=\"270\" valign=\"middle\" bgcolor=\"#d0d0d0\" style=\"font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;font-size: 21px;line-height: 16pt;color: #000000;font-weight: 300;text-align: center\"><p id=\"pay-data\">12.20.2012</p></td></tr><tr><td height=\"40\" colspan=\"2\" valign=\"middle\" bgcolor=\"#ffffff\"></td></tr><tr><td width=\"270\" valign=\"middle\" bgcolor=\"#d0d0d0\" style=\"font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;font-size: 21px;line-height: 16pt;color: #000000;font-weight: 300;padding-left: 30px;\"><p>\u041d\u043e\u043c\u0435\u0440 \u043f\u0440\u0435\u0434\u043f\u0438\u0441\u0430\u043d\u0438\u044f: </p></td><td width=\"270\" valign=\"middle\" bgcolor=\"#d0d0d0\" style=\"font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;font-size: 21px;line-height: 16pt;color: #000000;font-weight: 300;text-align: center\"><p id=\"order-number\">01.0121212121</p></td></tr></tbody></table><table width=\"600\" cellspacing=\"0\" cellpadding=\"0\" bgcolor=\"#ffffff\" align=\"center\" style=\"border-collapse: collapse;text-align: left;font-weight: 300\"><tbody><tr><td width=\"270\" valign=\"middle\" style=\"padding-left: 30px;font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;color: #000000;border-top: 1px solid rgb(221, 221, 221);font-size: 17px;line-height: 0pt;\"><p>\u0424\u0418\u041e \u043d\u0430\u0440\u0443\u0448\u0438\u0442\u0435\u043b\u044f</p></td></tr><tr><td width=\"270\" valign=\"middle\" style=\"padding-left: 30px;font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;color: #000000;border-bottom: 1px solid rgb(221, 221, 221);font-size: 17px;\"><p id=\"fio\" style=\"text-decoration: underline\">\u0418\u0412\u0410\u041d\u041e\u0412 \u0418\u0412\u0410\u041d \u0418\u0412\u0410\u041d\u041e\u0412\u0418\u0427</p></td></tr><tr><td width=\"270\" valign=\"middle\" style=\"padding-left: 30px;font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;color: #000000;border-top: 1px solid rgb(221, 221, 221);font-size: 17px;line-height: 0pt;\"><p>e - mail</p></td></tr><tr><td width=\"270\" valign=\"middle\" style=\"padding-left: 30px;font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;color: #000000;border-bottom: 1px solid rgb(221, 221, 221);font-size: 17px;\"><p id=\"email\" style=\"text-decoration: underline\">test@mail.com</p></td></tr><tr><td width=\"270\" valign=\"middle\" style=\"padding-left: 30px;font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;color: #000000;border-top: 1px solid rgb(221, 221, 221);font-size: 17px;line-height: 0pt;\"><p>\u041d\u043e\u043c\u0435\u0440 \u0442\u0435\u043b\u0435\u0444\u043e\u043d\u0430</p></td></tr><tr><td width=\"270\" valign=\"middle\" style=\"padding-left: 30px;font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;color: #000000;border-bottom: 1px solid rgb(221, 221, 221);font-size: 17px;\"><p id=\"phone\" style=\"text-decoration: underline\">+77017223456</p></td></tr><tr><td width=\"270\" valign=\"middle\" style=\"padding-left: 30px;font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;color: #000000;border-top: 1px solid rgb(221, 221, 221);font-size: 17px;line-height: 0pt;\"><p>\u041c\u0430\u0440\u043a\u0430 \u0422\u0421</p></td></tr><tr><td width=\"270\" valign=\"middle\" style=\"padding-left: 30px;font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;color: #000000;border-bottom: 1px solid rgb(221, 221, 221);font-size: 17px;\"><p id=\"mark\" style=\"text-decoration: underline\">TOYOTA CAMRY</p></td></tr><tr><td width=\"270\" valign=\"middle\" style=\"padding-left: 30px;font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;color: #000000;border-top: 1px solid rgb(221, 221, 221);font-size: 17px;line-height: 0pt;\"><p>\u0426\u0432\u0435\u0442</p></td></tr><tr><td width=\"270\" valign=\"middle\" style=\"padding-left: 30px;font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;color: #000000;border-bottom: 1px solid rgb(221, 221, 221);font-size: 17px;\"><p id=\"color\" style=\"text-decoration: underline\">\u0411\u0415\u041b\u042b\u0419</p></td></tr><tr><td width=\"270\" valign=\"middle\" style=\"padding-left: 30px;font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;color: #000000;border-top: 1px solid rgb(221, 221, 221);font-size: 17px;line-height: 0pt;\"><p>\u0410\u0434\u0440\u0435\u0441 \u0440\u0435\u0433\u0438\u0441\u0442\u0440\u0430\u0446\u0438\u0438</p></td></tr><tr><td width=\"270\" valign=\"middle\" style=\"padding-left: 30px;font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;color: #000000;border-bottom: 1px solid rgb(221, 221, 221);font-size: 17px;\"><p id=\"register\" style=\"text-decoration: underline\">\u0410\u041b\u041c\u0410\u0422\u042b, \u0433. \u0413.\u0410\u041b\u041c\u0410\u0422\u042b, \u0440\u0430\u0439\u043e\u043d \u0411\u041e\u0421\u0422\u0410\u041d\u0414\u042b\u041a\u0421\u041a\u0418\u0419, \u0443\u043b\u0438\u0446\u0430/\u043c\u043a\u0440\u043d. \u041a\u0410\u0417\u0410\u0425\u0424\u0418\u041b\u042c\u041c, \u0434\u043e\u043c 8, \u043a\u0432. 105 </p></td></tr><tr><td width=\"270\" valign=\"middle\" style=\"padding-left: 30px;font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;color: #000000;border-top: 1px solid rgb(221, 221, 221);font-size: 17px;line-height: 0pt;\"><p>\u0413\u0420\u041d\u0417</p></td></tr><tr><td width=\"270\" valign=\"middle\" style=\"padding-left: 30px;font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;color: #000000;border-bottom: 1px solid rgb(221, 221, 221);font-size: 17px;\"><p id=\"grnz\" style=\"text-decoration: underline\">A100AAA</p></td></tr><tr><td width=\"270\" valign=\"middle\" style=\"padding-left: 30px;font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;color: #000000;border-top: 1px solid rgb(221, 221, 221);font-size: 17px;line-height: 0pt;\"><p>\u0421\u0420\u0422\u0421</p></td></tr><tr><td width=\"270\" valign=\"middle\" style=\"padding-left: 30px;font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;color: #000000;border-bottom: 1px solid rgb(221, 221, 221);font-size: 17px;\"><p id=\"srts\" style=\"text-decoration: underline\">AA001123211 </p></td></tr></tbody></table><table width=\"600\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\" style=\"border-collapse: collapse;text-align: left;font-family: Arial, Helvetica, sans-serif;font-weight: normal;font-size: 12px;line-height: 15pt;font-weight: 300;color: #777777;\"><tbody><tr><td width=\"600\" valign=\"middle\" bgcolor=\"#ffffff\" style=\"font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;font-size: 16px;line-height: 16pt;color: #9099A3;font-weight: 300;text-align: center\"><p style=\"margin-top: 0;margin-bottom: 16px !important;padding: 0;\"> \u0414\u0430\u043d\u043d\u043e\u0435 \u0441\u043e\u043e\u0431\u0449\u0435\u043d\u0438\u0435 \u0441\u0444\u043e\u0440\u043c\u0438\u0440\u043e\u0432\u0430\u043d\u043d\u043e \u0430\u0432\u0442\u043e\u043c\u0430\u0442\u0438\u0447\u0435\u0441\u043a\u0438 \u0438 \u043d\u0435 \u0442\u0440\u0435\u0431\u0443\u0435\u0442 \u043e\u0442\u0432\u0435\u0442\u0430. </p></td></tr></tbody></table><table width=\"600\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\" style=\"border-collapse: collapse;text-align: left;font-family: Arial, Helvetica, sans-serif;font-size: 12px;line-height: 15pt;font-weight: 300;color: #777777;\"><tbody><tr><td width=\"300\" valign=\"middle\" bgcolor=\"#d0d0d0\" style=\"font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;font-size: 16px;line-height: 16pt;color: #9099A3;font-weight: 300;padding: 10px;\"><img src=\"http://pcp.azurewebsites.net/images/logo-footer.png\" style=\"height: 72px;\"/></td><td width=\"300\" valign=\"middle\" bgcolor=\"#d0d0d0\" style=\"font-family: \'Segoe UI\', \'Helvetica Neue\', Helvetica, Arial, sans-serif;font-size: 16px;line-height: 16pt;color: #000000;font-weight: 300;text-align: center\"><p style=\"margin-top: 0;margin-bottom: 16px !important;padding-top: 10px;text-align: right;padding-right: 10px;\"> \u0420\u0435\u0441\u043f\u0443\u0431\u043b\u0438\u043a\u0430 \u041a\u0430\u0437\u0430\u0445\u0441\u0442\u0430\u043d, 010000.<br/> \u0433\u043e\u0440\u043e\u0434 \u0410\u0441\u0442\u0430\u043d\u0430<br/> \u0443\u043b\u0438\u0446\u0430 \u0418\u043c\u0430\u043d\u043e\u0432\u0430 19, \u043e\u0444. 806 </p></td></tr></tbody></table></td></tr></tbody></table></body></html>";
            
            foreach (var orderDetail in order.Details)
            {
                var body =
                string.Format(
                    "<p>Вы успешно оплатили нарушение.</p><p><strong>Дата оплаты:</strong> {0}</p><p><strong>Номер(а) предписания:</strong> {1}</p>",
                    order.PaymentDate, string.Join(",", order.Details.Select(d => d.OrderNumber)));
                
                Email.From("robot@naoplatu.kz")
                        .SmtpHost("82.200.165.5")
                        .Port(25)
                        .Credentials("registration@kazbilet.kz", "K@zb1let89")
                        .To(order.User.Email)
                        .Subject("Оплата штрафа")
                        .Body(body)
                        .Send();
            }
            
        }

        public void SendEmail(string email, string subject,  string htmlBody )
        {
            var api = new MandrillApi("0iyjosISNOboWeEy4QngJA");

            var result = api.SendMessage(new EmailMessage
            {
                to =
                    new List<EmailAddress> { new EmailAddress { email = email, name = "" } },
                from_email = "robot@naoplatu.kz",
                subject = subject,
                html = htmlBody,
            });
        }

        public void SendEmail(PayedViolation payedViolation)
        {
            var body =
                 string.Format(
                     "<p>Вы успешно оплатили нарушение.</p><p><strong>Дата оплаты:</strong> {0}</p><p><strong>Номер(а) предписания:</strong> {1}</p>",
                    DateTime.Now, payedViolation.OrderNumber);

            Email.From("registration@kazbilet.kz")
                    .SmtpHost("82.200.165.5")
                    .Port(25)
                    .Credentials("robot@naoplatu.kz", "K@zb1let89")
                    .To(payedViolation.Email)
                    .Subject("Оплата штрафа")
                    .Body(body)
                    .Send();
        }
        
        #endregion
    }
}
