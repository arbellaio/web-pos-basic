using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecompildPOS.Web.Helpers
{
    public class TableNames
    {
        public const string Users = "Users";
        public const string Accounts = "Accounts";
        public const string Orders = "Orders";
        public const string OrderProcesses = "OrderProcesses";
        public const string Products = "Products";
        public const string Business = "Business";
        public const string BusinessFinances = "BusinessFinances";
        public const string BusinessExpenses = "BusinessExpenses";
        public const string AccountTransactions = "AccountTransactions";
        public const string EndOfDayReports = "EndOfDayReports";
        public const string InventoryStocks = "InventoryStocks";
    }


    public class MApiEndPoints
    {
        public const string Users = "/users";
        public const string PostUsers = "/postUsers";
        public const string Accounts = "/accounts";
        public const string PostAccounts = "/postAccounts";
        public const string Orders = "/orders";
        public const string PostOrders = "/postOrders";
        public const string OrderProcesses = "/orderProcesses";
        public const string PostOrderProcesses = "/postOrderProcesses";
        public const string Products = "/product";
        public const string PostProducts = "/postProducts";
        public const string Business = "/businesses";
        public const string PostBusiness = "/postBusinesses";
        public const string BusinessFinances = "/businessFinances";
        public const string PostBusinessFinances = "/postBusinessFinances";
        public const string BusinessExpenses = "/businessExpenses";
        public const string PostBusinessExpenses = "/postBusinessExpenses";
        public const string AccountTransactions = "/accountTransactions";
        public const string PostAccountTransactions = "/postAccountTransactions";
        public const string EndOfDayReports = "/endOfDayReports";
        public const string PostEndOfDayReports = "/postEndOfDayReports";
        public const string InventoryStocks = "/inventoryStocks";
        public const string PostInventoryStocks = "/postInventoryStocks";
        public const string RegisterBusiness = "/registerBusiness";
        public const string LoginUrl = "/loginBusiness";
        public const string VerifyApi = "/VerifyAcknowledgement";

    }


    public class ApiEndPoints
    {
        public const string Users = "/api/userapi/users";
        public const string PostUsers = "/api/userapi/postUsers";
        public const string Accounts = "/api/accountapi/accounts";
        public const string PostAccounts = "/api/accountapi/postAccounts";
        public const string Orders = "/api/orderapi/orders";
        public const string PostOrders = "/api/orderapi/postOrders";
        public const string OrderProcesses = "/api/orderprocessapi/orderProcesses";
        public const string PostOrderProcesses = "/api/orderprocessapi/postOrderProcesses";
        public const string Products = "/api/productapi/product";
        public const string PostProducts = "/api/productapi/postProducts";
        public const string Business = "/api/businessapi/businesses";
        public const string PostBusiness = "/api/businessapi/postBusinesses";
        public const string LoginUrl = "/api/businessapi/loginBusiness";
        public const string BusinessFinances = "/api/businessfinanceapi/businessFinances";
        public const string PostBusinessFinances = "/api/businessfinanceapi/postBusinessFinances";
        public const string BusinessExpenses = "/api/businessexpenseapi/businessExpenses";
        public const string PostBusinessExpenses = "/api/businessexpenseapi/postBusinessExpenses";
        public const string AccountTransactions = "/api/accounttransactionapi/accountTransactions";
        public const string PostAccountTransactions = "/api/accounttransactionapi/postAccountTransactions";
        public const string EndOfDayReports = "/api/endofdayreportapi/endOfDayReports";
        public const string PostEndOfDayReports = "/api/endofdayreportapi/postEndOfDayReports";
        public const string InventoryStocks = "/api/inventoryStocks";
        public const string PostInventoryStocks = "/api/postInventoryStocks";
        public const string RegisterBusiness = "/api/businessapi/registerBusiness";
        public const string VerifyApi = "/api/acknowledgementApi/verifyAcknowledgement";
        public const string ServerPing = "/api/acknowledgementApi/serverPing";

    }
}
