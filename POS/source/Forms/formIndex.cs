using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace POS
{
    public partial class formIndex : Form
    {
        private List<string> lsProID = new List<string>();
        private List<string> lsProName = new List<string>();
        private List<int> lsProAmount = new List<int>();
        private List<string> lsProImage = new List<string>();
        private List<double> lsProCost = new List<double>();
        private List<double> lsProSell = new List<double>();
        private List<string> lsProStatust = new List<string>();
        private List<string> lsCategoryID = new List<string>();
        private List<string> lsBrandID = new List<string>();
        private PictureBox[] arrImage;
        private Label[] arrProName;
        private Label[] arrProBrand;
        private Label[] arrProPrice;
        private Label[] arrProAmount;
        private Label[] arrProCate;
        private int s = 0, e = 5;
        public formIndex()
        {
            InitializeComponent();
        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            var Login = new formLogin();
            Login.Show();
            this.Hide();
        }

        private void formIndex_Load(object sender, EventArgs e)
        {
            init();
            getEmpData();
            fetchProductData();
            mapProductData();
        }
        private void getEmpData()
        {
            lblID.Text = EmployeeData.empID;
            lblname.Text = EmployeeData.empFname;
        }
        private void fetchProductData()
        {
            try
            {
                DBConfig.conn.Open();
                //Field
                string sql;
                var adapter = new SqlDataAdapter();
                var productTB = new DataTable();

                sql = "SELECT * FROM Products";
                adapter.SelectCommand = new SqlCommand(sql, DBConfig.conn);
                adapter.Fill(productTB);

                try
                {
                    DataRow[] dr = productTB.Select();

                    foreach(var itm in dr)
                    {
                        lsProID.Add(itm["product_id"].ToString());
                        lsProName.Add(itm["product_name"].ToString());
                        lsProAmount.Add(Convert.ToInt32(itm["amount"].ToString()));
                        lsProImage.Add(itm["image_url"].ToString());
                        lsProCost.Add(Convert.ToDouble(itm["cost_price"].ToString()));
                        lsProSell.Add(Convert.ToDouble(itm["selling_price"].ToString()));
                        lsProStatust.Add(itm["product_status"].ToString());
                        lsCategoryID.Add(itm["category_id"].ToString());
                        lsBrandID.Add(itm["brand_id"].ToString());
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            DBConfig.conn.Close();
        }
        private void init()
        {
            arrImage = new PictureBox[5]
            {
                ProIM1, ProIM2, ProIM3, ProIM4, ProIM5
            };
            arrProName = new Label[5]
            {
                ProN1,ProN2,ProN3,ProN4,ProN5
            };
            arrProBrand = new Label[5]
            {
                ProB1,ProB2,ProB3,ProB4,ProB5
            };
            arrProPrice = new Label[5]
            {
                ProPr1,ProPr2,ProPr3,ProPr4,ProPr5
            };
            arrProAmount = new Label[5]
            {
               Num1,Num2,Num3,Num4,Num5
            };
            arrProCate = new Label[5]
            {
                Cate1,Cate2,Cate3,Cate4,Cate5
            };
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void mapProductData()
        {
            for(int i = s; i < e; i++)
            {
                arrImage[i].ImageLocation = lsProImage[i];
                arrProName[i].Text = lsProName[i];
                arrProPrice[i].Text = lsProSell[i].ToString("#,#.00");

            }
        }
    }
}
