using System.IO;

namespace DT.PCP.NotificationTemplate
{
    public static class Template
    {
        public static string ViolationEmailNotification
        {
            get { return File.ReadAllText("Templates\\NotificatioEmail.cshtml"); }
        }

        public static string NotificationCheckCode
        {
            get
            {
                var basePath = System.AppDomain.CurrentDomain.RelativeSearchPath ??
                           System.AppDomain.CurrentDomain.BaseDirectory;
                var path = Path.Combine(basePath, "Templates",
                                        "NotificationCheckCode.cshtml");
               
                return File.ReadAllText(path);
            }
        }

        public static string NotificationSubscribed
        {
            get
            {
                var basePath = System.AppDomain.CurrentDomain.RelativeSearchPath ??
                           System.AppDomain.CurrentDomain.BaseDirectory;
                var path = Path.Combine(basePath, "Templates",
                                        "NotificationSubscribed.cshtml");

                return File.ReadAllText(path);
            }
        }

        public static string NotificationUnsubscribe
        {

            get
            {
                var basePath = System.AppDomain.CurrentDomain.RelativeSearchPath ??
                          System.AppDomain.CurrentDomain.BaseDirectory;
                var path = Path.Combine(basePath, "Templates",
                                        "NotificationUnSubscribed.cshtml");
                return File.ReadAllText(path);
            }
        }
    }
}
