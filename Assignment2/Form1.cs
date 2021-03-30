using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Assignment2
{
    public partial class Form1 : Form
    {
        List<Panel> listPanel = new List<Panel>();

        int pIndex;

        //3-23-21 SH NEW 9L: create a class for
        class Sale
        {
            public static int counter { get; set; }
            public int id { get; set; }
            public List<string> product { get; set; }
            public decimal total { get; set; }
            public string paid { get; set; }
            public int rate { get; set; }
            public string pers { get; set; }
        }

        //3-23-21 SH NEW 1L: create a list
        private List<Sale> sales = new List<Sale>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listPanel.Add(panel1);
            listPanel.Add(panel2);
            listPanel.Add(panel3);
            listPanel.Add(panel4);
            listPanel[0].BringToFront();

            //3-29-21 SH NEW 1L: load id
            if (sales.Count == 0)
            {
                if (!File.Exists("THISID.txt"))
                    CreateIdFile();

                FileInfo info = new FileInfo("THISID");
                FileStream stream = info.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
                StreamReader reader = new StreamReader(stream);
                Sale.counter = int.Parse(reader.ReadToEnd());
                reader.Close();
            }
        }

        private void Form1_FromClosing(Object sender, FormClosingEventArgs e)
        {
            CreateIdFile();
        }

        private void CreateIdFile()
        {
            FileInfo info = new FileInfo("THISID");
            FileStream stream = info.Open(FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(Sale.counter.ToString());
            writer.Close();
        }

        private void aSale_Click(object sender, EventArgs e)
        {
            listPanel[1].BringToFront();

            idIn.Text = (Sale.counter + 1).ToString();
        }

        private bool ImportSale()
        {
            string[] rec = null;
            string[] content = null;

            OpenFileDialog opener = new OpenFileDialog();

            opener.InitialDirectory = "../../Assignment2";
            opener.Filter = "Text|*.txt";
            opener.Title = "Import Sale";
            opener.RestoreDirectory = true;
            opener.ShowDialog();

            if (opener.FileName != "")
            {
                content = File.ReadAllLines(opener.FileName);

                foreach (string line in content)
                {
                    Sale s = new Sale();

                    rec = line.Split(new char[] { '\t', '\n' });

                    s.id = int.Parse(rec[0]);
                    string[] products = rec[1].Split(new char[] { ',' });
                    s.product = new List<string>(products);
                    s.total = int.Parse(rec[2]);
                    s.paid = rec[3];
                    s.rate = int.Parse(rec[4]);
                    s.pers = rec[5];

                    sales.Add(s);
                }

                Sale.counter = sales.Last().id;

                return true;
            }

            return false;
        }

        private void mSale_Click(object sender, EventArgs e)
        {
            listPanel[2].BringToFront();
        }

        private void report_Click(object sender, EventArgs e)
        {
            listPanel[3].BringToFront();
        }

        private void paidIn_TextChanged(object sender, EventArgs e) { }

        private void productIn_TextChanged(object sender, EventArgs e) { }

        private void totalIn_TextChanged(object sender, EventArgs e) { }

        private void idIn_TextChanged(object sender, EventArgs e) { }

        private void rateIn_TextChanged(object sender, EventArgs e) { }

        private void perIn_TextChanged(object sender, EventArgs e) { }

        private void save_Click(object sender, EventArgs e)
        {
            int num1;
            if (string.IsNullOrEmpty(idIn.Text.Trim()) || int.TryParse(idIn.Text.Trim(), out num1) == false || int.Parse(idIn.Text) > 10)
            {
                idIn.Text = "Please fill in field with a number(1-10)";
                return;
            }
            if (string.IsNullOrEmpty(productIn.Text.Trim()))
            {
                productIn.Text = "Please fill in field";
                return;
            }
            decimal num2 = 0;
            if (string.IsNullOrEmpty(totalIn.Text.Trim()) || decimal.TryParse(totalIn.Text.Trim(), out num2) == false)
            {
                totalIn.Text = "Please fill in field with a number";
                return;
            }
            if (string.IsNullOrEmpty(paidIn.Text.Trim()))
            {
               paidIn.Text = "Please fill in field";
                return;
            }
            if (string.IsNullOrEmpty(rateIn.Text.Trim()) || int.TryParse(rateIn.Text.Trim(), out num1) == false)
            {
                rateIn.Text = "Please fill in field with a number";
                return;
            }
            if (string.IsNullOrEmpty(perIn.Text.Trim()))
            {
                perIn.Text = "Please fill in field";
                return;
            }
            else
            {
                string record = idIn.Text + "\t" + productIn.Text + "\t" + totalIn.Text + "\t" +
                            paidIn.Text + "\t" + rateIn.Text + "\t" + perIn.Text + "\n";


                SaveFileDialog saver = new SaveFileDialog();
                saver.InitialDirectory = "../../Assignment2";
                saver.Filter = "Text|*.txt";
                saver.Title = "Export Sale";
                saver.ShowDialog();

                if (saver.FileName != "")
                {
                    File.AppendAllText(saver.FileName, record);

                    idIn.Text = (Sale.counter + 1).ToString();
                    productIn.Clear();
                    totalIn.Clear();
                    paidIn.Clear();
                    rateIn.Clear();
                    perIn.Clear();

                    // Increment the next workout count
                    Sale.counter = int.Parse(idIn.Text);
                }
            }
        }

        private void menu1_Click(object sender, EventArgs e)
        {
            listPanel[0].BringToFront();
        }

        private void idMod_TextChanged(object sender, EventArgs e){}

        private void productMod_TextChanged(object sender, EventArgs e){}

        private void totalMod_TextChanged(object sender, EventArgs e){}

        private void paidMod_TextChanged(object sender, EventArgs e){}

        private void rateMod_TextChanged(object sender, EventArgs e){}

        private void perMod_TextChanged(object sender, EventArgs e){}


        private void back_Click(object sender, EventArgs e)
        {
            listPanel[1].BringToFront();
        }

        private void next_Click(object sender, EventArgs e)
        {
            listPanel[3].BringToFront();
        }

        private void menu2_Click(object sender, EventArgs e)
        {
            listPanel[0].BringToFront();
        }

        private void rate1_TextChanged(object sender, EventArgs e){}

        private void rate2_TextChanged(object sender, EventArgs e){}

        private void output_TextChanged(object sender, EventArgs e){}

        private void menu3_Click(object sender, EventArgs e)
        {
            listPanel[0].BringToFront();
        }

        private void search_Click(object sender, EventArgs e)
        {
            int num1 = 0;
            if(rate1.Text != "" || int.TryParse(rate1.Text.Trim(), out num1) == true || rate2.Text != "" || int.TryParse(rate2.Text.Trim(), out num1) == true)
            {
                output.Clear();

                int first = int.Parse(rate1.Text);
                int last = int.Parse(rate2.Text);

                var fL = from s in sales
                         where s.id >= first && s.id <= first
                         select s;

                if(fL != null)
                {
                    foreach(Sale s in fL)
                    {
                        output.AppendText("ID: " + s.id + Environment.NewLine +
                                          "Products: ");
                        foreach (string p in s.product)
                        {
                            output.AppendText(p);

                            if (p != s.product.Last())
                                output.AppendText(",");
                            else
                                output.AppendText("\r\n\r\n");
                        }

                        output.AppendText("Total: " + s.total + Environment.NewLine +
                                          "Paid With: " + s.paid + Environment.NewLine +
                                          "Rating: " + s.rate + Environment.NewLine +
                                          "Personality: " + s.pers);
                    }
                }
            }

            rate1.Clear();
            rate2.Clear();
        }

        private void mod_Click(object sender, EventArgs e)
        {

        }
    }
}
