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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            SyntaxAnalyser SA = new SyntaxAnalyser();
            Node root = SA.Parse(TINY_Compiler.tiny_Scanner.Tokens);
            treeView1.Nodes.Add(SyntaxAnalyser.PrintParseTree(root));
            for (int i = 0; i < SA.Errors.Count; i++)
            {
                MessageBox.Show(SA.Errors[i]);
                richTextBox2.Text += (i+1)+"- " + SA.Errors[i] + "\n";
            }
            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
