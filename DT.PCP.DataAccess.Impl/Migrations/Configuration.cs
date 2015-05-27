using DT.PCP.Domain;

namespace DT.PCP.DataAccess.Impl.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PcpContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PcpContext context)
        {

             context.Users.AddOrUpdate(u => new {u.CarNumber, u.CarPassportNumber},
                                      new User {CarNumber = "z1", CarPassportNumber = "p1"},
                                      new User {CarNumber = "z2", CarPassportNumber = "p2"});


        }
    }
}
