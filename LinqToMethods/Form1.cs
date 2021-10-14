using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinqToMethods.Models;

namespace LinqToMethods
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        NorthwindEntities1 nwdb = new NorthwindEntities1();

        //LINQMETHODS


        private void button1_Click(object sender, EventArgs e)
        {
            //ToList()
            dataGridView1.DataSource = nwdb.Categories.ToList();

            //Where()
            dataGridView1.DataSource = nwdb.Employees.Where(x => x.EmployeeID == 2);

            //Select()
            dataGridView1.DataSource = nwdb.Customers.Select(x => new
            {
                x.CustomerID,
                x.CompanyName,
                x.ContactName,
                x.Address
            }).ToList();

            //OrderBy()
            dataGridView1.DataSource = nwdb.Categories.Where(x => x.CategoryID >= 8)
                                                      .Select(x => new
                                                      {
                                                          x.CategoryID,
                                                          x.CategoryName,
                                                          x.Description
                                                      })
                                                      .OrderBy(x => x.CategoryName)
                                                      .ToList();
            //OrderByDescending()
            dataGridView1.DataSource = nwdb.Products.Where(x => x.ProductID <= 11)
                                                  .Select(x => new
                                                  {
                                                      x.ProductID,
                                                      x.ProductName,
                                                      x.UnitsInStock
                                                  })
                                                  .OrderByDescending(x => x.UnitsInStock)
                                                  .ToList();
            //First()

            Employee employee = nwdb.Employees.First(x => x.FirstName == "Robert");
            if (employee == null)

                MessageBox.Show("Aradığınız isimde bir çalışan bulunmamaktadır.");

            else
                MessageBox.Show($"Aradığınız Çalışan:{employee.FirstName}\n{employee.LastName}");

            try
            {
                Product product = nwdb.Products.First(x => x.ProductID == 8);
                MessageBox.Show($"Product Id:{product.ProductID}");
            }
            catch (Exception)
            {

                MessageBox.Show("Aradığınız Id numaralı ürün bulunmamaktadır.");
            }

            //FirstorDefault

            Product product1 = nwdb.Products.FirstOrDefault(x => x.UnitsInStock > 10);
            if (product1 == null)
                MessageBox.Show("Aradığınız ürün bulunmamaktadır.");
            else
                MessageBox.Show($"Aradığınız Ürün:{product1.ProductName}\n{product1.UnitsInStock}");

            //Find()
            Category category = nwdb.Categories.Find(11);

            if (category == null)

                MessageBox.Show("Aradığınız isimde bir çalışan bulunmamaktadır.");

            else
                MessageBox.Show($"Aradığınız Çalışan:{category.CategoryID}\n{category.CategoryName}");

            //Take()
            dataGridView1.DataSource = nwdb.Categories.Where(x => x.CategoryID >= 8)
                                                     .Select(x => new
                                                     {
                                                         x.CategoryID,
                                                         x.CategoryName,
                                                         x.Description,
                                                         x.Picture
                                                     })
                                                     .OrderBy(x => x.CategoryID)
                                                     .Take(5)
                                                     .ToList();
            //Skip()
            dataGridView1.DataSource = nwdb.Products.Where(x => x.CategoryID >= 8)
                                                  .Select(x => new
                                                  {
                                                      x.ProductID,
                                                      x.ProductName,
                                                      x.CategoryID,
                                                      x.Category
                                                  })
                                                  .OrderBy(x => x.ProductID)
                                                  .Skip(2)
                                                  .ToList();
            //Contains()
            dataGridView1.DataSource = nwdb.Categories.Where(x => x.CategoryName.Contains("s"))
                                                    .Select(x => new
                                                    {
                                                        x.CategoryID,
                                                        x.CategoryName,
                                                        x.Description
                                                    })
                                                    .OrderByDescending(x => x.CategoryName)
                                                    .ToList();

            //StartWith
            dataGridView1.DataSource = nwdb.Products.Where(x => x.ProductName.StartsWith("e")).ToList();


            //EndsWit()
            dataGridView1.DataSource = nwdb.Categories.Where(x => x.CategoryName.EndsWith("s")).ToList();

            //Any()
            bool result = nwdb.Employees.Any(x => x.FirstName == "Robert");
            MessageBox.Show(result.ToString());
            bool result1 = nwdb.Employees.Any(x => x.FirstName.StartsWith("r"));
            MessageBox.Show(result1.ToString());

            //Count()
            int isciSayisi = nwdb.Employees.Count();
            MessageBox.Show($"İşçi Sayısı:{isciSayisi}");

            //Sum ()
            decimal? toplamFiyat = nwdb.Products.Where(x => x.CategoryID == 8).Sum(x => x.UnitPrice);

            //Min()-Max()
            int? minUrunStogu = nwdb.Products.Min(x => x.UnitsInStock);
            int? maxUrunStogu = nwdb.Products.Max(x => x.UnitsInStock);
            MessageBox.Show($"En az stoklu urun:{minUrunStogu}\nEn çok stoklu urun:{maxUrunStogu}");


            //Distinct()
            List<string> suppliers = nwdb.Suppliers.Select(x => x.Country).Distinct().ToList();
            foreach (var item in suppliers)
            {
                listBox1.Items.Add(item);
            }

            //GroupBy()
            dataGridView1.DataSource = nwdb.Suppliers.GroupBy(x => x.Country)
                                                   .Select(x => x.Key)
                                                   .ToList();



            //Average()
            decimal? ortalamaFiyat = nwdb.Products.Average(x => x.UnitPrice);
            MessageBox.Show($"Ortalama Fiyat:{ortalamaFiyat}");

        }



    }
}
