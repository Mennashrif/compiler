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
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
