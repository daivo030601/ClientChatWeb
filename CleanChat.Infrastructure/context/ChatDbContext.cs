using CleanChat.Domain;
using CleanChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Infrastructure.context
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasOne(c => c.Client)
                .WithMany(m => m.Messages)
                .HasForeignKey(c => c.ClientId);

            modelBuilder.Entity<Message>()
                .HasOne(c => c.Topic)
                .WithMany(m => m.Messages)
                .HasForeignKey(c => c.TopicId);

            modelBuilder.Entity<ClientTopic>()
                .HasKey(c => new { c.ClientId, c.TopicId });

            modelBuilder.Entity<Client>().HasData( new List<Client>() 
            {
                new Client() {ClientId = 1, Name = "chau", Password = "123"},
                new Client() {ClientId = 2, Name = "dai", Password = "123"},
                new Client() {ClientId = 3, Name = "tuananh", Password = "123"}
            });

            modelBuilder.Entity<Topic>().HasData(new List<Topic>()
            {
                new Topic() {TopicId = 1, TopicName = "A"},
                new Topic() {TopicId = 2, TopicName = "B"},
                new Topic() {TopicId = 3, TopicName = "C"},
            });

            modelBuilder.Entity<Message>().Property(m => m.SentDate).HasDefaultValueSql("getdate()");

        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ClientTopic> ClientTopics { get; set; }
    }
}
