﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecompildPOS.Services.DataContext;

namespace RecompildPOS.Services.Migrations
{
    [DbContext(typeof(ApplicationDataContext))]
    [Migration("20200421204322_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("RecompildPOS.Models.Accounts.AccountSync", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AccountCode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Address")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("CreditLimit")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("AccountId");

                    b.HasIndex("BusinessId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("RecompildPOS.Models.Businesses.BusinessSync", b =>
                {
                    b.Property<int>("BusinessId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BusinessOwnerUserId")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LicenseNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("OwnerName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("BusinessId");

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("RecompildPOS.Models.EndOfDayReports.EndOfDayReportSync", b =>
                {
                    b.Property<int>("EndOfDayReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("OrderToken")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("SubmittedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("TotalCashSubmitted")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("TotalDiscount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("TotalNetSale")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("TotalNetTax")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("TotalPaidCash")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("TotalSale")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("TransactionLogId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("EndOfDayReportId");

                    b.HasIndex("BusinessId");

                    b.ToTable("EndOfDayReports");
                });

            modelBuilder.Entity("RecompildPOS.Models.Expense.BusinessExpenseSync", b =>
                {
                    b.Property<int>("BusinessExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<string>("BusinessName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("ExpenseAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("ExpenseCycle")
                        .HasColumnType("int");

                    b.Property<string>("ExpenseName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("BusinessExpenseId");

                    b.HasIndex("BusinessId");

                    b.ToTable("BusinessExpenses");
                });

            modelBuilder.Entity("RecompildPOS.Models.Finances.BusinessFinanceSync", b =>
                {
                    b.Property<int>("BusinessFinanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<string>("BusinessName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("MonthlyEarning")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("BusinessFinanceId");

                    b.HasIndex("BusinessId");

                    b.ToTable("BusinessFinances");
                });

            modelBuilder.Entity("RecompildPOS.Models.InventoryStocks.InventoryStockSync", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Available")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("InStock")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("InventoryStocks");
                });

            modelBuilder.Entity("RecompildPOS.Models.OrderProcesses.OrderProcessDetailSync", b =>
                {
                    b.Property<int>("OrderProcessDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("OrderProcessId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("QuantityProcessed")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.HasKey("OrderProcessDetailId");

                    b.HasIndex("BusinessId");

                    b.HasIndex("OrderProcessId");

                    b.ToTable("OrderProcessDetails");
                });

            modelBuilder.Entity("RecompildPOS.Models.OrderProcesses.OrderProcessSync", b =>
                {
                    b.Property<int>("OrderProcessId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Notes")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("OrderNotes")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("OrderProcessStatusId")
                        .HasColumnType("int");

                    b.Property<decimal>("OrderProcessTotal")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("OrderToken")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.HasKey("OrderProcessId");

                    b.HasIndex("BusinessId");

                    b.ToTable("OrderProcesses");
                });

            modelBuilder.Entity("RecompildPOS.Models.Orders.OrderDetailSync", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Notes")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("OrderDetailStatusId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("TaxAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("BusinessId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("RecompildPOS.Models.Orders.OrderSync", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("InvoiceNo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Notes")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal?>("OrderCost")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("OrderNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("BusinessId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("RecompildPOS.Models.Products.ProductSync", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BarCode1")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("BarCode2")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<decimal?>("BuyPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Category")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ProductNotes")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("QrCode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("SellPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("SkuCode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("Tax")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("ProductId");

                    b.HasIndex("BusinessId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("RecompildPOS.Models.Sync.UserSyncLog", b =>
                {
                    b.Property<int>("UserSyncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ApiEndPoint")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SerialNo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("TableName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserSyncId");

                    b.HasIndex("BusinessId");

                    b.ToTable("UserSyncLogs");
                });

            modelBuilder.Entity("RecompildPOS.Models.Transactions.AccountTransactionSync", b =>
                {
                    b.Property<int>("AccountTransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int?>("AccountPaymentModeId")
                        .HasColumnType("int");

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<decimal>("ClosingAccountBalance")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("InvoiceNo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Notes")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("OpeningAccountBalance")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("OrderAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("OrderCost")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("OrderProcessId")
                        .HasColumnType("int");

                    b.Property<string>("OrderToken")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("PaidAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("TotalDiscount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("TotalTax")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("TypeName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AccountTransactionId");

                    b.HasIndex("BusinessId");

                    b.ToTable("AccountTransactions");
                });

            modelBuilder.Entity("RecompildPOS.Models.Users.UserSync", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("SerialNo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId");

                    b.HasIndex("BusinessId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RecompildPOS.Models.Accounts.AccountSync", b =>
                {
                    b.HasOne("RecompildPOS.Models.Businesses.BusinessSync", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecompildPOS.Models.EndOfDayReports.EndOfDayReportSync", b =>
                {
                    b.HasOne("RecompildPOS.Models.Businesses.BusinessSync", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecompildPOS.Models.Expense.BusinessExpenseSync", b =>
                {
                    b.HasOne("RecompildPOS.Models.Businesses.BusinessSync", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecompildPOS.Models.Finances.BusinessFinanceSync", b =>
                {
                    b.HasOne("RecompildPOS.Models.Businesses.BusinessSync", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecompildPOS.Models.InventoryStocks.InventoryStockSync", b =>
                {
                    b.HasOne("RecompildPOS.Models.Businesses.BusinessSync", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecompildPOS.Models.OrderProcesses.OrderProcessDetailSync", b =>
                {
                    b.HasOne("RecompildPOS.Models.Businesses.BusinessSync", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecompildPOS.Models.OrderProcesses.OrderProcessSync", "OrderProcess")
                        .WithMany("OrderProcessDetails")
                        .HasForeignKey("OrderProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecompildPOS.Models.OrderProcesses.OrderProcessSync", b =>
                {
                    b.HasOne("RecompildPOS.Models.Businesses.BusinessSync", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecompildPOS.Models.Orders.OrderDetailSync", b =>
                {
                    b.HasOne("RecompildPOS.Models.Businesses.BusinessSync", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecompildPOS.Models.Orders.OrderSync", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecompildPOS.Models.Orders.OrderSync", b =>
                {
                    b.HasOne("RecompildPOS.Models.Businesses.BusinessSync", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecompildPOS.Models.Products.ProductSync", b =>
                {
                    b.HasOne("RecompildPOS.Models.Businesses.BusinessSync", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecompildPOS.Models.Sync.UserSyncLog", b =>
                {
                    b.HasOne("RecompildPOS.Models.Businesses.BusinessSync", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecompildPOS.Models.Transactions.AccountTransactionSync", b =>
                {
                    b.HasOne("RecompildPOS.Models.Businesses.BusinessSync", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecompildPOS.Models.Users.UserSync", b =>
                {
                    b.HasOne("RecompildPOS.Models.Businesses.BusinessSync", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
