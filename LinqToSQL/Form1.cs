using LinqToSQL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqToSQL
{
    public partial class Form1 : Form
    {
        NorthwindEntities nwdb;
        public Form1()
        {
            nwdb = new NorthwindEntities();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region Linq To Entity
            dataGridView1.DataSource = nwdb.Products.Select(x => new
            {
                x.ProductID,
                x.ProductName,
                x.UnitPrice,
                x.UnitsInStock
            }).ToList();
            #endregion

            #region Linq To SQL
            var products = from p in nwdb.Products
                           select new
                           {
                               p.ProductID,
                               p.ProductName,
                               p.UnitPrice,
                               p.UnitsInStock
                           };
            dataGridView1.DataSource = products.ToList();
            #endregion

            #region Linq To Entity
            dataGridView1.DataSource = nwdb.Employees
                .Where(x => x.Country == "UK" && SqlFunctions.DateDiff("yyyy", x.BirthDate, DateTime.Now) > 55)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    Age = SqlFunctions.DateDiff("yyyy", x.BirthDate, DateTime.Now),
                    x.City
                })
                .ToList();
            #endregion

            #region Linq To SQL
            var result = from emp in nwdb.Employees
                         where emp.Country == "UK" && SqlFunctions.DateDiff("yyyy", emp.BirthDate, DateTime.Now) > 55
                         select new
                         {
                             emp.FirstName,
                             emp.LastName,
                             Age = SqlFunctions.DateDiff("yyyy", emp.BirthDate, DateTime.Now),
                             emp.City
                         };
            dataGridView1.DataSource = result.ToList();
            #endregion

            #region Linq To Entity
            dataGridView1.DataSource = nwdb.Suppliers
                .Join(nwdb.Products, s => s.SupplierID, p => p.SupplierID, (s, p) => new { s, p })
                .GroupBy(x => x.p.ProductName)
                .Select(x => new
                {
                    x.Key,
                    Count = x.Sum(z => z.p.UnitsOnOrder)
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            #endregion

            #region Linq To Entity
            dataGridView1.DataSource = nwdb.Categories
                .Join(nwdb.Products, c => c.CategoryID, p => p.CategoryID, (c, p) => new { c, p })
                .Join(nwdb.Order_Details, ord => ord.p.ProductID, od => od.ProductID, (ord, od) => new { ord, od })
                .GroupBy(x => x.ord.p.ProductName)
                .Select(x => new
                {
                    Urun = x.Key,
                    Toplam = x.Sum(z => z.od.Quantity * z.od.UnitPrice * (int)Math.Floor(1 - z.od.Discount))
                })
                .OrderByDescending(x => x.Toplam)
                .ToList();

            #endregion

            var context = new NorthwindEntities();
            {
                dataGridView1.DataSource = context.Ten_Most_Expensive_Products().ToList();
            }
            var context1 = new NorthwindEntities();
            {
                dataGridView1.DataSource = context1.SalesByCategory("SeaFood", "2010").ToList();
            }

        }
    }
}
