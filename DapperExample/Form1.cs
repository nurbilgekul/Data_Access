using Dapper;
using DapperExample.Entities.Concrete;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DapperExample
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnectionString = new SqlConnection(@"Data Source=DESKTOP-L4SUHVS; initial catalog=StudentDb; Integrated Security=True;");
        public Form1()
        {
            InitializeComponent();
        }
        int Id = 0;
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlConnectionString.State == ConnectionState.Closed)
                {
                    sqlConnectionString.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", Id);
                    param.Add("@Name", txtName.Text.Trim());
                    param.Add("@Marks", txtMarks.Text.Trim());

                    //StoredProcedure: StudentAdd
                    //create proc StudentAddorEdit
                    //@Id int,
                    //@Name nchar(100),
                    //@Marks int
                    //as
                    //begin
                    //if (@Id = 0)
                    //    begin
                    //    insert into Student
                    //    (
                    //    Name,
                    //    Marks
                    //    )
                    //values
                    //(@Name,
                    //@Marks
                    //)
                    //end


                    sqlConnectionString.Execute("StudentAdd", param, commandType: CommandType.StoredProcedure);
                    //Clear();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnectionString.Close();
            }

        }
        void GetAllStudent()
        {
            //StoredProcedure: StudentViewAllorSearch
            //create proc StudentViewAllorSearch
            //@SearchText nchar(100)
            //as begin
            //select*
            //from Student
            //where @SearchText = '' or
            //Name like '%' + @SearchText + '%'
            //end

            DynamicParameters param = new DynamicParameters();
            param.Add("@SearchText", txtSearch.Text.Trim());
            List<Student> students = sqlConnectionString.Query<Student>("StudentViewAllorSearch", param, commandType: CommandType.StoredProcedure).ToList<Student>();
            dataGridView1.DataSource = students;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                GetAllStudent();
                //Clear();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            btnDelete.Enabled = false;


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetAllStudent();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }



        void Clear()
        {
            txtName.Text = txtMarks.Text = "";
            Id = 0;
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //StoredProcedure: StudentDeleteById
                //create proc StudentDeleteById
                //@Id int
                //as begin
                //Delete from Student
                //Where Id = @Id
                //end

                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", Id);
                sqlConnectionString.Execute("StudentDeleteById", param, commandType: CommandType.StoredProcedure);
                Clear();
                GetAllStudent();
                MessageBox.Show("Deleted");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
          
            btnDelete.Enabled = true;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.Index!=-1)
                {
                    Id =Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txtMarks.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    btnDelete.Enabled = true;
                    btnUpdate.Enabled = true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //StoredProcedure: UpdateStudentInfo
            //create proc UpdateStudentInfo
            //@Id int,
            //@Name nchar(100),
            //@Marks int
            //as begin
            //update Student
            //set
            //Name=@Name,
            //Marks=@Marks
            //Where Id=@Id
            //end

            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", Id);
            param.Add("@Name", txtName.Text.Trim());
            param.Add("@Marks", txtMarks.Text.Trim());
            sqlConnectionString.Execute("UpdateStudentInfo", param, commandType: CommandType.StoredProcedure);
            Clear();
            GetAllStudent();
        }
    }
}
