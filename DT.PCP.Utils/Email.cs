using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace DT.PCP.Utils
{
    public class Email : IDisposable
    {
        private SmtpClient _client;
        private bool? _useSsl;

        public MailMessage Message { get; set; }

        private Email()
        {
            Message = new MailMessage();
            _client = new SmtpClient();
            _client.UseDefaultCredentials = false;
        }

        /// <summary>
        /// Устанавливает номер порта smtp хоста
        /// </summary>
        /// <param name="port">Номер порта</param>
        /// <returns>Экземпляр класса Email</returns>
        public Email Port(int port)
        {
            _client.Port = port;
            return this;
        }

        /// <summary>
        /// Устанавливает адрес smtp 
        /// </summary>
        /// <param name="smtpHost">Адрес smtp хоста</param>
        /// <returns>Экземпляр класса Email</returns>
        public Email SmtpHost(string smtpHost)
        {
            _client.Host = smtpHost;
            return this;
        }

        /// <summary>
        /// Устанавливает логин/пароль
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns>Экземпляр класса Email</returns>
        public Email Credentials(string login, string password)
        {
            _client.Credentials = new NetworkCredential(login, password);
            return this;
        }

        /// <summary>
        /// Создает новый экземпляр Email 
        /// </summary>
        /// <param name="emailAddress">Email отправителя</param>
        /// <param name="name">Имя отправителя</param>
        /// <returns>Экземпляр класса Email</returns>
        public static Email From(string emailAddress, string name = "")
        {
            var email = new Email
            {
                Message = { From = new MailAddress(emailAddress, name) }
            };
            return email;
        }

        /// <summary>
        /// Добавляет получателя к письму, имена и email разделяются ;
        /// </summary>
        /// <param name="emailAddress">Email адрес получателя</param>
        /// <param name="name">Имя получателя</param>
        /// <returns></returns>
        public Email To(string emailAddress, string name)
        {
            if (emailAddress.Contains(";"))
            {
                var nameSplit = name.Split(';');
                var addressSplit = emailAddress.Split(';');
                for (int i = 0; i < addressSplit.Length; i++)
                {
                    var currentName = string.Empty;
                    if ((nameSplit.Length - 1) >= i)
                    {
                        currentName = nameSplit[i];
                    }
                    Message.To.Add(new MailAddress(addressSplit[i], currentName));
                }
            }
            else
            {
                Message.To.Add(new MailAddress(emailAddress, name));
            }
            return this;
        }

        /// <summary>
        /// Добавляет получателя к письму
        /// </summary>
        /// <param name="emailAddress">Email адрес получателя (для множетсва получателей используется разделитель ;)</param>
        /// <returns></returns>
        public Email To(string emailAddress)
        {
            if (emailAddress.Contains(";"))
            {
                foreach (string address in emailAddress.Split(';'))
                {
                    Message.To.Add(new MailAddress(address));
                }
            }
            else
            {
                Message.To.Add(new MailAddress(emailAddress));
            }

            return this;
        }

        /// <summary>
        /// Добавляет всех получателей в списке в писмьо
        /// </summary>
        /// <param name="mailAddresses">Список получателей</param>
        /// <returns>Экземпляр класса Email</returns>
        public Email To(IList<MailAddress> mailAddresses)
        {
            foreach (var address in mailAddresses)
            {
                Message.To.Add(address);
            }
            return this;
        }

        /// <summary>
        /// Добавляет СС к письму
        /// </summary>
        /// <param name="emailAddress">Email адрес СС</param>
        /// <param name="name">Имя СС</param>
        /// <returns>Экземпляр класса Email</returns>
        public Email CC(string emailAddress, string name = "")
        {
            Message.CC.Add(new MailAddress(emailAddress, name));
            return this;
        }

        /// <summary>
        /// Добавляет список СС к письму
        /// </summary>
        /// <param name="mailAddresses">Список получателей СС</param>
        /// <returns>Экземпляр класса Email</returns>
        public Email CC(IList<MailAddress> mailAddresses)
        {
            foreach (var address in mailAddresses)
            {
                Message.CC.Add(address);
            }
            return this;
        }

        /// <summary>
        /// Добавляет BCC к письму
        /// </summary>
        /// <param name="emailAddress">Email адрес BCC</param>
        /// <param name="name">Имя BCC</param>
        /// <returns>Экземпляр класса Email</returns>
        public Email BCC(string emailAddress, string name = "")
        {
            Message.Bcc.Add(new MailAddress(emailAddress, name));
            return this;
        }

        /// <summary>
        /// Добавляет список BCC к письму
        /// </summary>
        /// <param name="mailAddresses">Список получателей BCC</param>
        /// <returns>Экземпляр класса Email</returns>
        public Email BCC(IList<MailAddress> mailAddresses)
        {
            foreach (var address in mailAddresses)
            {
                Message.Bcc.Add(address);
            }
            return this;
        }

        /// <summary>
        /// Устанавливает поле "Копия"
        /// </summary>
        /// <param name="address">Email адрес</param>
        /// <returns>Экземпляр класса Email</returns>
        public Email ReplyTo(string address)
        {
            Message.ReplyToList.Add(new MailAddress(address));

            return this;
        }

        /// <summary>
        /// Устанавливает поле "Копия"
        /// </summary>
        /// <param name="address">Email адрес</param>
        /// <param name="name">Имя</param>
        /// <returns>Экземпляр класса Email</returns>
        public Email ReplyTo(string address, string name)
        {
            Message.ReplyToList.Add(new MailAddress(address, name));

            return this;
        }

        /// <summary>
        /// Устанавливает тему сообщения
        /// </summary>
        /// <param name="subject">Тема сообщения</param>
        /// <returns>Экземпляр класса Email</returns>
        public Email Subject(string subject)
        {
            Message.Subject = subject;
            return this;
        }

        /// <summary>
        /// Добавляет содержимое к писмьу
        /// </summary>
        /// <param name="body">Содержимое письма</param>
        /// <param name="isHtml">True если содержимое является html, false для простого текста (Опционально)</param>
        public Email Body(string body, bool isHtml = true)
        {
            Message.Body = body;
            Message.IsBodyHtml = isHtml;
            return this;
        }

        /// <summary>
        /// Помечает письмо высоким приоритетом
        /// </summary>
        public Email HighPriority()
        {
            Message.Priority = MailPriority.High;
            return this;
        }

        /// <summary>
        /// Помечает письмо низким приоритетом
        /// </summary>
        public Email LowPriority()
        {
            Message.Priority = MailPriority.Low;
            return this;
        }

        /// <summary>
        /// Добавляет вложения к письму
        /// </summary>
        /// <param name="attachment">Вложения для добавления</param>
        /// <returns>Экземпляр класса Email</returns>
        public Email Attach(Attachment attachment)
        {
            if (!Message.Attachments.Contains(attachment))
                Message.Attachments.Add(attachment);

            return this;
        }

        /// <summary>
        /// Добавляет несколько вложений к письму
        /// </summary>
        /// <param name="attachments">Список вложений для добавления</param>
        /// <returns>Экземпляр класса Email</returns>
        public Email Attach(IList<Attachment> attachments)
        {
            foreach (var attachment in attachments.Where(attachment => !Message.Attachments.Contains(attachment)))
            {
                Message.Attachments.Add(attachment);
            }
            return this;
        }

        public Email UseSSL()
        {
            _useSsl = true;
            return this;
        }

        /// <summary>
        /// Отправка почты асинхронно
        /// </summary>
        /// <returns>Экземпляр класса Email</returns>
        public Email Send()
        {
            if (_useSsl.HasValue)
                _client.EnableSsl = _useSsl.Value;

            _client.Send(Message);
            return this;
        }

        /// <summary>
        /// Отправка почты асинхронно с callback функцией
        /// </summary>
        /// <param name="callback">Метод который вызовится после завершения</param>
        /// <param name="token">Токен пользователя для передачи в callback функцию</param>
        /// <returns>Экземпляр класса Email</returns>
        public Email SendAsync(SendCompletedEventHandler callback, object token = null)
        {
            if (_useSsl.HasValue)
                _client.EnableSsl = _useSsl.Value;

            _client.SendCompleted += callback;
            _client.SendAsync(Message, token);

            return this;
        }

        /// <summary>
        /// Отменяет асинхронную отправку почты
        /// </summary>
        /// <returns>Экземпляр класса Email</returns>
        public Email Cancel()
        {
            _client.SendAsyncCancel();
            return this;
        }

        /// <summary>
        /// Освобождает все ресурсы
        /// </summary>
        public void Dispose()
        {
            if (_client != null)
                _client.Dispose();

            if (Message != null)
                Message.Dispose();
        }
    }
}
