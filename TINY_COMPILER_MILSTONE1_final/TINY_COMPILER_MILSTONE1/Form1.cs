using JASONParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TINY_COMPILER_MILSTONE1
{


   
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            richTextBox2.Text = "";
            TINY_Compiler.tiny_Scanner.Tokens.Clear();
            Errors.Error_List.Clear();
            TINY_Compiler.TokenStream.Clear();
            string Code = richTextBox1.Text;
            TINY_Compiler.Start_Compiling(Code);
            PrintTokens();
            PrintErrors();
            Form2 f = new Form2();
            f.Show();
        }
        void PrintTokens()
        {
            
            for (int i = 0; i < TINY_Compiler.tiny_Scanner.Tokens.Count; i++)
            {
               dataGridView1.Rows.Add(TINY_Compiler.tiny_Scanner.Tokens.ElementAt(i).lex, TINY_Compiler.tiny_Scanner.Tokens.ElementAt(i).token_type);
              

            }
        }

        void PrintErrors()
        {
            for(int i=0; i<Errors.Error_List.Count; i++)
            {
                richTextBox2.Text += Errors.Error_List[i];
                richTextBox2.Text += "\r\n";
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
             richTextBox2.Text = "";
             richTextBox1.Text = "";
            TINY_Compiler.TokenStream.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Monotype Corsiva", 14, FontStyle.Italic);
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.ReadOnly = true;
            richTextBox2.ReadOnly = true;
            richTextBox2.BackColor = Color.White;
            richTextBox1.BackColor = Color.White;
            richTextBox1.Font = new Font("Monotype Corsiva", 18, FontStyle.Italic);
            richTextBox2.Font = new Font("Monotype Corsiva", 16, FontStyle.Italic);
            dataGridView1.DefaultCellStyle.Font = new Font("Monotype Corsiva", 16, FontStyle.Italic);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
          

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
