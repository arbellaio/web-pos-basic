using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RecompildPOS.Models.Accounts;
using RecompildPOS.Models.Businesses;
using RecompildPOS.Models.EndOfDayReports;
using RecompildPOS.Models.Expense;
using RecompildPOS.Models.Finances;
using RecompildPOS.Models.InventoryStocks;
using RecompildPOS.Models.OrderProcesses;
using RecompildPOS.Models.Orders;
using RecompildPOS.Models.Products;
using RecompildPOS.Models.Sync;
using RecompildPOS.Models.Transactions;
using RecompildPOS.Models.Users;

namespace RecompildPOS.Services.DataContext
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {
        }
    // "ConnectionString": "Filename=./Db/p.db/3;"

        public DbSet<AccountSync> Accounts { get; set; }
        public DbSet<BusinessSync> Businesses { get; set; }
        public DbSet<BusinessExpenseSync> BusinessExpenses { get; set; }
        public DbSet<BusinessFinanceSync> BusinessFinances { get; set; }
        public DbSet<UserSync> Users { get; set; }
        public DbSet<UserSyncLog> UserSyncLogs { get; set; }
        public DbSet<EndOfDayReportSync> EndOfDayReports { get; set; }
        public DbSet<AccountTransactionSync> AccountTransactions { get; set; }
        public DbSet<InventoryStockSync> InventoryStocks { get; set; }
        public DbSet<OrderProcessSync> OrderProcesses { get; set; }
        public DbSet<OrderProcessDetailSync> OrderProcessDetails { get; set; }
        public DbSet<OrderDetailSync> OrderDetails { get; set; }
        public DbSet<OrderSync> Orders { get; set; }
        public DbSet<ProductSync> Products { get; set; }
    }
}
