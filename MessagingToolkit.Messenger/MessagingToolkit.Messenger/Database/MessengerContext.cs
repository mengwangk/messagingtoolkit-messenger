using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MessagingToolkit.Messenger.Model;

namespace MessagingToolkit.Messenger.Database
{
    public class MessengerContext: DbContext
    {
        /// <summary>
        /// Database context
        /// </summary>
        public MessengerContext() : base("MessengerContext")
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<IncomingMessage> IncomingMesages { get; set; }

        public DbSet<Gateway> Gateways { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
