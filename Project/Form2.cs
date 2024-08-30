using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Form2 : Form
    {
        internal enum Modes
        {
            ADD, 
            MODIFY,
            FINALGRADE
        }
        internal static Form2 current;

        private Modes mode = Modes.ADD;

        private string[] assignInitial;
        public Form2()
        {
            current = this;
            InitializeComponent();
        }
        internal void Start(Modes m, DataGridViewSelectedRowCollection c)
        {
            mode = m;
            Text = "" + mode;

            comboBox1.DisplayMember = "StId";
            comboBox1.ValueMember = "StId";
            comboBox1.DataSource = Data.Students.GetStudents();
            comboBox1.DropDownStyle= ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;


            comboBox2.DisplayMember = "CId";
            comboBox2.ValueMember = "CId";
            comboBox2.DataSource = Data.Courses.GetCourses();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.SelectedIndex = 0;

            comboBox3.DisplayMember = "ProgId";
            comboBox3.ValueMember = "ProgId";
            comboBox3.DataSource = Data.Programs.GetPrograms();
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.SelectedIndex = 0;

            textBox1.ReadOnly = true;
            textBox2.ReadOnly= true;
            textBox4.ReadOnly = true;
            textBox3.Enabled = false;

            if(((mode == Modes.MODIFY) || (mode == Modes.FINALGRADE)) && (c!=null))
            {
                comboBox1.SelectedValue = c[0].Cells["StId"].Value;
                comboBox2.SelectedValue = c[0].Cells["CId"].Value;
                comboBox3.SelectedValue = c[0].Cells["ProgId"].Value;
                textBox3.Text = ""+c[0].Cells["FinalGrade"].Value;
                assignInitial = new string[] { (string)c[0].Cells["StId"].Value, (string)c[0].Cells["CId"].Value, (string)c[0].Cells["progId"].Value };
            }
            if (mode == Modes.MODIFY)
            {
                textBox3.Enabled = false;
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
            }
            if (mode == Modes.FINALGRADE) 
            {
                textBox3.Enabled = true;
                textBox3.ReadOnly = false;
                comboBox1.Enabled= false;
                comboBox2.Enabled= false;
                comboBox3.Enabled= false;
            
            }
            ShowDialog();
        }


        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                var a = from r in Data.Students.GetStudents().AsEnumerable()
                        where r.Field<string>("StId") == (string)comboBox1.SelectedValue
                        select new { Name = r.Field<string>("StName") };
                textBox1.Text = a.Single().Name;
            }
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                var a = from r in Data.Courses.GetCourses().AsEnumerable()
                        where r.Field<string>("CId") == (string)comboBox2.SelectedValue
                        select new { Name = r.Field<string>("CName") };
                textBox2.Text = a.Single().Name;
            }
        }
        //program ID
        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            if(comboBox3.SelectedItem != null)
            {
                var a = from r in Data.Programs.GetPrograms().AsEnumerable()
                        where r.Field<string>("ProgId") == (string)comboBox3.SelectedValue
                        select new { Name = r.Field<string>("ProgName") };
                textBox4.Text = a.Single().Name;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int r = -1;
            if (mode == Modes.ADD)
            {
                
                r = Data.Enrollments.InsertData(new string[] { (string)comboBox1.SelectedValue, (string)comboBox2.SelectedValue });
            }
            if (mode == Modes.MODIFY)
            {
                List<string[]> lId = new List<string[]>();
                lId.Add(assignInitial);

                r = Data.Enrollments.InsertData(new string[] { (string)comboBox1.SelectedValue, (string)comboBox2.SelectedValue });

                if (r == 0)
                {
                    r = Data.Enrollments.DeleteData(lId);
                }
            }
            if (mode == Modes.FINALGRADE)
            {
                r = Business.Enrollments.UpdateFinalGrade(assignInitial,textBox3.Text);
            }

            if (r == 0) { Close(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

      
    }
}
