using System.Configuration;
using System.Data.Entity;
using DT.PCP.Domain;

namespace DT.PCP.DataAccess.Impl
{
    /// <summary>
    /// Представляет контекст
    /// </summary>
    public class PcpContext : DbContext
    {
        #region Constructors


        public PcpContext()
            : base(ConfigurationManager.ConnectionStrings["Pcp"].ConnectionString)
        {
            Database.SetInitializer( new MigrateDatabaseToLatestVersion<PcpContext, Migrations.Configuration>());
            Database.Initialize(false);
        }

        #endregion

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OsmpPayment> OsmpPayments { get; set; }
        public DbSet<Commission> Commissions { get; set; }
        public DbSet<SmsNotification> SmsNotifications { get; set; }
        public DbSet<EmailNotification> EmailNotifications { get; set; }
      

    }
}
