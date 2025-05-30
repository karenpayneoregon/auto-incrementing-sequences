﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using IncrementSequenceDemos.Models;
using static ConfigurationLibrary.Classes.ConfigurationHelper;
namespace IncrementSequenceDemos.Data
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerSequence> CustomerSequence { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Example1> Example1 { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerSequence>(entity =>
            {
                entity.Property(e => e.Sequence)
                    .HasComputedColumnSql("([SequencePreFix]+[CurrentSequenceValue])", false);

                entity.Property(e => e.SequencePreFix)
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.HasOne(d => d.CustomerIdentifierNavigation)
                    .WithMany(p => p.CustomerSequence)
                    .HasForeignKey(d => d.CustomerIdentifier)
                    .HasConstraintName("FK_CustomerSequence_Customers");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerIdentifier);

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.ContactName).HasMaxLength(30);

                entity.Property(e => e.ContactTitle).HasMaxLength(30);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.PostalCode).HasMaxLength(10);
            });

            modelBuilder.Entity<Example1>(entity =>
            {
                entity.Property(e => e.InvoiceNumber).IsRequired();
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.HasOne(d => d.CustomerIdentifierNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerIdentifier)
                    .HasConstraintName("FK_Orders_Customers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}