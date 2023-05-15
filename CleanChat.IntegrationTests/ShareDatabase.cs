using Bogus;
using CleanChat.Domain.Entities;
using CleanChat.Infrastructure.context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.IntegrationTests
{
    public class ShareDatabase : IDisposable
    {
        private static readonly object _lock = new object();
        private static bool _databaseInitialized;
        public DbConnection Connection
        {
            get;
        }

        public ShareDatabase()
        {
            Connection = new SqlConnection("Server=CL-5CD849B649;Database=ChatAppDB;User Id=sa;Password=Daivo@0306;TrustServerCertificate=True;Trusted_Connection=True;");
            Seed();
            Connection.Open();
        }

        public ChatDbContext CreateContext(DbTransaction? transaction = null)
        {
            var context = new ChatDbContext(new DbContextOptionsBuilder<ChatDbContext>()
                                .UseSqlServer(Connection)
                                .Options);


            if (transaction != null)
            {
                context.Database.UseTransaction(transaction);
            }
            return context;
        }

        private void Seed()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                        SeedData(context);
                    }

                    _databaseInitialized = true;
                }
            }
        }

        private async void SeedData(ChatDbContext context)
        {
            var productIds = 3;
            var subedClientIds = 0;
            var TopicName = 'C';
            var fakeTopics = new Faker<Topic>()
                .RuleFor(o => o.TopicName, f => $"Name Topic {++TopicName}");

            var fakeUser = new Faker<Client>()
                .RuleFor(o => o.Name, f => $"Name of ClientId {++productIds}")
                .RuleFor(o => o.Password, f => $"123");

            var fakeSubcribe = new Faker<ClientTopic>()
                .RuleFor(o => o.ClientId, f => ++subedClientIds)
                .RuleFor(o => o.TopicId, f => 1);

            var clients = fakeUser.Generate(4);
            var topics = fakeTopics.Generate(5);
            var clientTopic = fakeSubcribe.Generate(4);

            context.Topics.AddRange(topics);
            context.Clients.AddRange(clients);

            context.SaveChanges();
            context.ClientTopics.AddRange(clientTopic);

            context.SaveChanges();

        }

        public void Dispose() => Connection.Dispose();
    }

}
