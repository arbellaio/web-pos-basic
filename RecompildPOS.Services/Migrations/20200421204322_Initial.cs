using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RecompildPOS.Services.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    BusinessId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    OwnerName = table.Column<string>(nullable: true),
                    LicenseNumber = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BusinessOwnerUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.BusinessId);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    AccountCode = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Balance = table.Column<decimal>(nullable: false),
                    CreditLimit = table.Column<decimal>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BusinessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountTransactions",
                columns: table => new
                {
                    AccountTransactionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    TypeName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    AccountId = table.Column<int>(nullable: false),
                    OrderAmount = table.Column<decimal>(nullable: false),
                    OrderCost = table.Column<decimal>(nullable: false),
                    PaidAmount = table.Column<decimal>(nullable: false),
                    TotalDiscount = table.Column<decimal>(nullable: false),
                    TotalTax = table.Column<decimal>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrderToken = table.Column<string>(nullable: true),
                    OrderId = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    InvoiceNo = table.Column<string>(nullable: true),
                    OrderProcessId = table.Column<int>(nullable: false),
                    AccountPaymentModeId = table.Column<int>(nullable: true),
                    ClosingAccountBalance = table.Column<decimal>(nullable: false),
                    OpeningAccountBalance = table.Column<decimal>(nullable: false),
                    BusinessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTransactions", x => x.AccountTransactionId);
                    table.ForeignKey(
                        name: "FK_AccountTransactions_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessExpenses",
                columns: table => new
                {
                    BusinessExpenseId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExpenseName = table.Column<string>(nullable: true),
                    ExpenseAmount = table.Column<decimal>(nullable: false),
                    ExpenseCycle = table.Column<int>(nullable: false),
                    BusinessId = table.Column<int>(nullable: false),
                    BusinessName = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessExpenses", x => x.BusinessExpenseId);
                    table.ForeignKey(
                        name: "FK_BusinessExpenses_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessFinances",
                columns: table => new
                {
                    BusinessFinanceId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MonthlyEarning = table.Column<decimal>(nullable: false),
                    BusinessId = table.Column<int>(nullable: false),
                    BusinessName = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessFinances", x => x.BusinessFinanceId);
                    table.ForeignKey(
                        name: "FK_BusinessFinances_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EndOfDayReports",
                columns: table => new
                {
                    EndOfDayReportId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderToken = table.Column<string>(nullable: true),
                    TotalSale = table.Column<decimal>(nullable: false),
                    TotalPaidCash = table.Column<decimal>(nullable: false),
                    TotalDiscount = table.Column<decimal>(nullable: false),
                    TotalCashSubmitted = table.Column<decimal>(nullable: false),
                    SubmittedDate = table.Column<DateTime>(nullable: false),
                    TotalNetSale = table.Column<decimal>(nullable: false),
                    TotalNetTax = table.Column<decimal>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    TransactionLogId = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BusinessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndOfDayReports", x => x.EndOfDayReportId);
                    table.ForeignKey(
                        name: "FK_EndOfDayReports_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryStocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    InStock = table.Column<decimal>(nullable: false),
                    Available = table.Column<decimal>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BusinessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryStocks_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProcesses",
                columns: table => new
                {
                    OrderProcessId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    OrderToken = table.Column<string>(nullable: true),
                    OrderId = table.Column<int>(nullable: true),
                    AccountId = table.Column<int>(nullable: true),
                    OrderNotes = table.Column<string>(nullable: true),
                    OrderProcessTotal = table.Column<decimal>(nullable: false),
                    OrderProcessStatusId = table.Column<int>(nullable: true),
                    TransactionTypeId = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BusinessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProcesses", x => x.OrderProcessId);
                    table.ForeignKey(
                        name: "FK_OrderProcesses_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    OrderNumber = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    AccountId = table.Column<int>(nullable: true),
                    StatusId = table.Column<int>(nullable: true),
                    InvoiceNo = table.Column<string>(nullable: true),
                    OrderCost = table.Column<decimal>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BusinessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SkuCode = table.Column<string>(nullable: true),
                    BarCode1 = table.Column<string>(nullable: true),
                    BarCode2 = table.Column<string>(nullable: true),
                    QrCode = table.Column<string>(nullable: true),
                    SellPrice = table.Column<decimal>(nullable: false),
                    BuyPrice = table.Column<decimal>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    Tax = table.Column<decimal>(nullable: false),
                    ProductNotes = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BusinessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BusinessId = table.Column<int>(nullable: false),
                    UserRole = table.Column<int>(nullable: false),
                    SerialNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSyncLogs",
                columns: table => new
                {
                    UserSyncId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    SerialNo = table.Column<string>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    TableName = table.Column<string>(nullable: true),
                    ApiEndPoint = table.Column<string>(nullable: true),
                    BusinessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSyncLogs", x => x.UserSyncId);
                    table.ForeignKey(
                        name: "FK_UserSyncLogs_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProcessDetails",
                columns: table => new
                {
                    OrderProcessDetailId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    OrderProcessId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    QuantityProcessed = table.Column<decimal>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: true),
                    AccountId = table.Column<int>(nullable: false),
                    TransactionTypeId = table.Column<int>(nullable: false),
                    BusinessId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProcessDetails", x => x.OrderProcessDetailId);
                    table.ForeignKey(
                        name: "FK_OrderProcessDetails_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProcessDetails_OrderProcesses_OrderProcessId",
                        column: x => x.OrderProcessId,
                        principalTable: "OrderProcesses",
                        principalColumn: "OrderProcessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    Quantity = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    TaxAmount = table.Column<decimal>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrderDetailStatusId = table.Column<int>(nullable: true),
                    BusinessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BusinessId",
                table: "Accounts",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_BusinessId",
                table: "AccountTransactions",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessExpenses_BusinessId",
                table: "BusinessExpenses",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessFinances_BusinessId",
                table: "BusinessFinances",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_EndOfDayReports_BusinessId",
                table: "EndOfDayReports",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryStocks_BusinessId",
                table: "InventoryStocks",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_BusinessId",
                table: "OrderDetails",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProcessDetails_BusinessId",
                table: "OrderProcessDetails",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProcessDetails_OrderProcessId",
                table: "OrderProcessDetails",
                column: "OrderProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProcesses_BusinessId",
                table: "OrderProcesses",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BusinessId",
                table: "Orders",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BusinessId",
                table: "Products",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BusinessId",
                table: "Users",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSyncLogs_BusinessId",
                table: "UserSyncLogs",
                column: "BusinessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountTransactions");

            migrationBuilder.DropTable(
                name: "BusinessExpenses");

            migrationBuilder.DropTable(
                name: "BusinessFinances");

            migrationBuilder.DropTable(
                name: "EndOfDayReports");

            migrationBuilder.DropTable(
                name: "InventoryStocks");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "OrderProcessDetails");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserSyncLogs");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "OrderProcesses");

            migrationBuilder.DropTable(
                name: "Businesses");
        }
    }
}
