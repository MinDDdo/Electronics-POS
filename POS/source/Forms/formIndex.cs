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
        private Panel[] panelList;
        private List<string> lsProID = new List<string>();
        private List<string> lsProName = new List<string>();
        private List<int> lsProAmount = new List<int>();
        private List<string> lsProImage = new List<string>();
        private List<double> lsProCost = new List<double>();
        private List<double> lsProSell = new List<double>();
        private List<string> lsProStatust = new List<string>();
        private List<string> lsCategoryID = new List<string>();
        private List<string> lsBrandID = new List<string>();
        private List<string> lsBrandName = new List<string>();
        private List<string> lsCategoryName = new List<string>();
        private PictureBox[] arrImage;
        private Label[] arrProName;
        private Label[] arrProBrand;
        private Label[] arrProPrice;
        private Label[] arrProAmount;
        private Label[] arrProCate;
        private int start = 0, end = 5, st = 0;
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
            fetchBrandData();
            fetchCategoryData();
            mapProductData();
        }
        private void getEmpData()
        {
            lblID.Text = EmployeeData.empID;
            lblname.Text = EmployeeData.empFname;
        }
        private void fetchBrandData()
        {
            try
            {
                DBConfig.conn.Open();
                //Field
                string sql;
                var adapter = new SqlDataAdapter();
                var brandTB = new DataTable();

                sql = "SELECT * FROM Brands";
                adapter.SelectCommand = new SqlCommand(sql, DBConfig.conn);
                adapter.Fill(brandTB);

                foreach(var itm in lsBrandID)
                {
                    sql = $"brand_id='{itm}'";
                    try
                    {
                        DataRow[] dr = brandTB.Select(sql);

                        foreach(var el in dr)
                        {
                            lsBrandName.Add(el["brand_name"].ToString());
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            DBConfig.conn.Close();
        }
        private void fetchCategoryData()
        {
            try
            {
                DBConfig.conn.Open();
                //Field
                string sql;
                var adapter = new SqlDataAdapter();
                var categoryTB = new DataTable();

                sql = "SELECT * FROM Categorys";
                adapter.SelectCommand = new SqlCommand(sql, DBConfig.conn);
                adapter.Fill(categoryTB);

                foreach (var itm in lsCategoryID)
                {
                    sql = $"category_id='{itm}'";
                    try
                    {
                        DataRow[] dr = categoryTB.Select(sql);

                        foreach (var el in dr)
                        {
                            lsCategoryName.Add(el["category_name"].ToString());
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
            catch (Exception ex)
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
            panelList = new Panel[5]
            {
                panellist1,panellist2,panellist3,panellist4,panellist5
            };
        }

        private void mapProductData()
        {
            st = start;
            if(st < lsProImage.Count)
            {
                for (int i = 0; i < 5; i++)
                {
                    panelList[i].Visible = false;
                    if (st < lsProImage.Count)
                    {
                        panelList[i].Visible = true;
                        arrImage[i].ImageLocation = lsProImage[st];
                        arrProName[i].Text = lsProName[st];
                        arrProPrice[i].Text = lsProSell[st].ToString("#,#.00");
                        arrProBrand[i].Text = lsBrandName[st];
                        arrProCate[i].Text = lsCategoryName[st];
                    }
                    st++;
                }
            }
        }

        private void Del1_Click(object sender, EventArgs e)
        {
            
        }

        private void Del2_Click(object sender, EventArgs e)
        {

        }

        private void Del3_Click(object sender, EventArgs e)
        {

        }

        private void Del4_Click(object sender, EventArgs e)
        {

        }

        private void Del5_Click(object sender, EventArgs e)
        {

        }
        private void delClick(Label lbl)
        {

        }
        private int convertIndex(Label lbl)
        {
            int idx = Convert.ToInt32(lbl.Tag.ToString());
            return idx;
        }

        private void back_Click(object sender, EventArgs e)
        {
            if(start != 0)
            {
                page.Text = (Convert.ToInt32(page.Text) - 1) + "";
                start -= 5;
                end -= 5;
                mapProductData();
            }
        }

        private void next_Click(object sender, EventArgs e)
        {
            if(end <= lsProImage.Count)
            {
                page.Text = (Convert.ToInt32(page.Text) + 1) + "";
                start += 5;
                end += 5;
                mapProductData();
            }
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

                    foreach (var itm in dr)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            DBConfig.conn.Close();
        }
    }
}
