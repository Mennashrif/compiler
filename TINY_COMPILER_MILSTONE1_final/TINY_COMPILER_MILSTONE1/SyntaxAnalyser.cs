using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TINY_COMPILER_MILSTONE1;

namespace JASONParser
{
    public class Node
    {
        public List<Node> children = new List<Node>();
        public string Name;
        public Node(string Name)
        {
            this.Name = Name;
        }
    }
   public class SyntaxAnalyser
    {

        int tokenIndex = 0;
        static List<TINY_Token> TokenStream;
        public static Node root;

        public  Node Parse(List<TINY_Token> Tokens)
        {
            TokenStream = Tokens;
                 root = Program();
           
            return root;
        }
        public Node Condition()
        {
            Node node = new Node("Condition");
            Node identifier = Identifier();
            Node condition_operator = Condition_operator();
            Node term = Term();
            if (identifier != null && condition_operator != null && term != null)
            {
                node.children.Add(identifier);
                node.children.Add(condition_operator);
                node.children.Add(term);
                return node;
            }
             return null;
        }
        public Node Term()
        {
            return null;
        }
        public Node Condition_operator()
        {
            Node node = new Node("Condition_operator");
            if (TokenStream[tokenIndex].token_type == TINY_Token_Class.GreaterThanOp)
            {
                node.children.Add(match(TINY_Token_Class.GreaterThanOp));
                return node;
            }
            else if (TokenStream[tokenIndex].token_type == TINY_Token_Class.LessThanOp)
            {
                node.children.Add(match(TINY_Token_Class.LessThanOp));
                return node;
            }
            else if (TokenStream[tokenIndex].token_type == TINY_Token_Class.ISEqualOp)
            {
                node.children.Add(match(TINY_Token_Class.ISEqualOp));
                return node;
            }
            else if (TokenStream[tokenIndex].token_type == TINY_Token_Class.NotEqualOp)
            {
                node.children.Add(match(TINY_Token_Class.NotEqualOp));
                return node;
            }

            return null;
        }
        public Node Return_statement()
        {
            Node node = new Node("Return_statement");
            Node Return = match(TINY_Token_Class.Return);
            Node expression = Expression();
            Node semicolon = match(TINY_Token_Class.Semicolon);
            if(Return!=null && expression!=null && semicolon != null)
            {
                node.children.Add(Return);
                node.children.Add(expression);
                node.children.Add(semicolon);
            }
            return null;
        }
        public Node Expression()
        {
            return null;
        }
        public Node Identifier()
        {
            return null;
        }
        public Node Program()
        {
            return null;
        }
        public Node DeclarationStatement()
        {
            return null;
        }

        public Node Function_Call()
        {
            return null;
        }

        public Node match(TINY_Token_Class ExpectedToken)
        {



            return null;
        }


        //use this function to print the parse tree in TreeView Toolbox
        public static TreeNode PrintParseTree(Node root)
        {
            TreeNode tree = new TreeNode("Parse Tree");
            TreeNode treeRoot = PrintTree(root);
            if (treeRoot != null)
                tree.Nodes.Add(treeRoot);
            return tree;
        }
        static TreeNode PrintTree(Node root)
        {
            if (root == null || root.Name == null)
                return null;
            TreeNode tree = new TreeNode(root.Name);
            if (root.children.Count == 0)
                return tree;
            foreach (Node child in root.children)
            {
                if (child == null)
                    continue;
                tree.Nodes.Add(PrintTree(child));
            }
            return tree;
        }
    }
}
