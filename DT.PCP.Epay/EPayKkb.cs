using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Hosting;
using DT.PCP.Logging;

namespace DT.PCP.Epay
{
    // *************************************************************************
    //Модуль обеспечения работы функций ЭЦП сервиса https://epay.kkb.kz в среде .NET Framework 2.0 и выше
    //Разработано в ТОО "Таулинк" специально для сервиса http://www.oplata.kz
    // *************************************************************************

    public static class EPayKkb
    {
        //Эти параметры необходимо настроить под магазин
        //Полный путь к pfx-файлу с ключами магазина (файл дает банк)
        static string KKBpfxFile = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, ConfigurationManager.AppSettings["CertFolder"], "cert.pfx");
        //Пароль к pfx-файлу (дает банк)
        static string KKBpfxPass = ConfigurationManager.AppSettings["PfxPassword"];
        //Полный путь к файлу с публичным ключом банка (файл дает банк)
        static string KKBCaFile = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, ConfigurationManager.AppSettings["CertFolder"], "kkbca.cer");
        //Номер сертификата  (дает банк)
        static string Cert_Id = ConfigurationManager.AppSettings["CertID"];
        //Имя магазина (дает банк)
        static string ShopName = ConfigurationManager.AppSettings["ShopName"];
        //номер терминала магазина, он же MerchantID  (дает банк)
        private static string merchant_id = ConfigurationManager.AppSettings["MerchantID"];

        //Эти параметры как правило не требуют изменения
        //Код валюты 398 - тенге
        const string currency = "398";

        static string KKBRequestStr = "<merchant cert_id=\"" + Cert_Id + "\" name=\"" + ShopName + "\"><order order_id=\"%ORDER%\" amount=\"%AMOUNT%\" currency=\"" + currency + "\"><department merchant_id=\"" + merchant_id + "\" amount=\"%AMOUNT%\"/></order></merchant>";
        private static string KKBApproveStr = "<merchant id=\"" + merchant_id + "\"><command type=\"complete\"/>" + "<payment reference=\"%REFERENCE%\" approval_code=\"%APPROVAL_CODE%\" orderid=\"%ORDER%\" amount=\"%AMOUNT%\" currency_code=\"%CURRENCY%\"/></merchant>";

        private static Byte[] ConvertStringToByteArray(String s)
        {
            return (new ASCIIEncoding()).GetBytes(s);
        }

        public static string BuildApproveOrder(string order, string amount, string approvalCode, string reference)
        {
            try
            {
                string StrForSign = KKBApproveStr.Replace("%ORDER%", order).Replace("%REFERENCE%", reference).Replace("%APPROVAL_CODE%", approvalCode).Replace("%CURRENCY%", currency).Replace("%AMOUNT%", amount);
                X509Certificate2 KKbCert = new X509Certificate2(KKBpfxFile, KKBpfxPass, X509KeyStorageFlags.MachineKeySet);
                RSACryptoServiceProvider rsaCSP = (RSACryptoServiceProvider)KKbCert.PrivateKey;
                byte[] SignData = rsaCSP.SignData(ConvertStringToByteArray(StrForSign), "SHA1");
                Array.Reverse(SignData);
                string ResultStr = "<document>" + StrForSign + "<merchant_sign type=\"RSA\" cert_id=\"" + Cert_Id + "\">" + HttpUtility.UrlEncode(Convert.ToBase64String(SignData, Base64FormattingOptions.None)) + "</merchant_sign></document>";
                return ResultStr;
            }
            catch (Exception e)
            {
            }
            return string.Empty;
        }

        //Функция Build64 генерирует запрос который отправляется на https://epay.kkb.kz/jsp/process/logon.jsp
        //В качестве входящих параметров ожидает idOrder (номер заказа в магазине) и Amount (сумма к оплате)
        //Возвращает строку в Base64
        public static string Build64(string idOrder, decimal Amount)
        {
            Logger _logger = new Logger();
            
            try
            {
               
                string StrForSign = KKBRequestStr.Replace("%ORDER%", idOrder).Replace("%AMOUNT%", string.Format("{0:0.00}", Amount).Replace(",", "."));
                X509Certificate2 KKbCert = new X509Certificate2(KKBpfxFile, KKBpfxPass, X509KeyStorageFlags.MachineKeySet);
                RSACryptoServiceProvider rsaCSP = (RSACryptoServiceProvider)KKbCert.PrivateKey;
                byte[] SignData = rsaCSP.SignData(ConvertStringToByteArray(StrForSign), "SHA1");
                Array.Reverse(SignData);
                string ResultStr = "<document>" + StrForSign + "<merchant_sign type=\"RSA\">" + Convert.ToBase64String(SignData, Base64FormattingOptions.None) + "</merchant_sign></document>";

                return Convert.ToBase64String(ConvertStringToByteArray(ResultStr), Base64FormattingOptions.None);
            }
            catch (Exception e)
            {
                
                _logger.Debug(e.Message);
                _logger.Debug(e.StackTrace);
                return string.Empty;
            }
           
        }

        //Функция  Verify проверяет корректность подписи, полученной от банка
        //В качестве входящих параметров ожидает StrForVerify (строка, которую получили от банка) и Sign (ЭЦП к данной строке)
        public static bool Verify(string StrForVerify, string Sign)
        {
            X509Certificate2 KKbCert = new X509Certificate2(KKBCaFile);
            RSACryptoServiceProvider rsaCSP = (RSACryptoServiceProvider)KKbCert.PublicKey.Key;
            byte[] bStrForVerify = ConvertStringToByteArray(StrForVerify);
            byte[] bSign = Convert.FromBase64String(Sign);
            Array.Reverse(bSign);
            bool Result = false;
            try
            {
                Result = rsaCSP.VerifyData(bStrForVerify, "SHA1", bSign);
            }
            catch (Exception ex)
            {
                Result = false;

            }
            return Result;
        }

        //Функция Build64 подписывает произвольную строку
        //В качестве входящих параметров ожидает StrForSign (подписываемая строка)
        //Возвращает ЭЦП кодированный в Base64
        public static string Sign64(string StrForSign)
        {
            X509Certificate2 KKbCert = new X509Certificate2(KKBpfxFile, KKBpfxPass);
            RSACryptoServiceProvider rsaCSP = (RSACryptoServiceProvider)KKbCert.PrivateKey;
            byte[] SignData = rsaCSP.SignData(ConvertStringToByteArray(StrForSign), "SHA1");
            Array.Reverse(SignData);
            return Convert.ToBase64String(SignData, Base64FormattingOptions.None);
        }



    }
}
