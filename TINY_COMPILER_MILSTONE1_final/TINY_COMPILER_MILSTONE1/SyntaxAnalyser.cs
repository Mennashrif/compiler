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
        public Node ReadStatement()
        {
            Node node = new Node("ReadStatement");
            Node read = match(TINY_Token_Class.read);
            Node identifier = Identifier();
            Node semicolon = match(TINY_Token_Class.Semicolon);
            if (read != null && identifier != null && semicolon != null)
            {
                node.children.Add(read);
                node.children.Add(identifier);
                node.children.Add(semicolon);
                return node;
            }
            return null;
        }
        public Node WriteStatement()
        {
            Node node = new Node("WriteStatement");
            Node write = match(TINY_Token_Class.write);
            Node expression = Expression();
            Node endl = match(TINY_Token_Class.endl);
            Node semicolon = match(TINY_Token_Class.Semicolon);
            if (write != null && expression != null && endl != null && semicolon != null)
            {
                node.children.Add(write);
                node.children.Add(expression);
                node.children.Add(endl);
                node.children.Add(semicolon);
                return node;
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
        public Node DataType()
        {
            return null;
        }

        // tasneem  
        public Node FunctionName()
        {
            Node node = new Node("Function Name");
            Node identifier = Identifier();

            if (identifier != null)
            {
                node.children.Add(identifier);
                return node;
            }
            return null;
        }

        // EX : int x 
        public Node Parameter()
        {
            Node node = new Node(" Parameter ");
            Node dataType = DataType();
           
            if (dataType != null)
            {
                Node identifier = Identifier();
                if (identifier != null)
                {
                    node.children.Add(dataType);
                    node.children.Add(identifier);
                    return node;
                }
               
            }
            return null;
        }
        //EX : int sum(int a, int b) 
        public Node FunctionDeclaration()
        {
            Node node = new Node(" Function Declaration ");
            Node dataType = DataType();
            if (dataType != null)
            {
                Node FunName = FunctionName();
                if (FunName !=null)
                {
                    Node leftbracket = match(TINY_Token_Class.LParanthesis);
                    if (leftbracket != null)
                    {
                        Node attrib = FunctionAttribute();
                         Node rightbracket = match(TINY_Token_Class.RParanthesis);
                            if (rightbracket!=null)
                            {
                                node.children.Add(dataType);
                                node.children.Add(FunName);
                                node.children.Add(leftbracket);
                                if(attrib != null)   node.children.Add(attrib); 
                                node.children.Add(rightbracket);
                                return node;

                            }
                        

                    }
                }
            }
                return null;
        }
        // EX : int a, int b,int c 
        public Node FunctionAttribute()
        {
            Node node = new Node(" Function Attribute ");
            Node parameter = Parameter();
            if (parameter != null)
            {
                node.children.Add(parameter);
                Node more_parameter = Attributess();
                if (more_parameter != null)     node.children.Add(more_parameter);
                return node;
            }
            return null;
        }

        public Node Attributess()
        {
            Node node = new Node(" Function Attributes ");
            Node comma = match(TINY_Token_Class.Comma);
            if (comma != null)
            {
                Node parameter = Parameter();
                if (parameter != null)
                {
                    node.children.Add(comma);
                    node.children.Add(parameter);
                    Node attributess = Attributess();
                    if (attributess != null) node.children.Add(attributess);
                    return node;
                }
            }
            return null;
        }

        public Node Function_Body()
        {
            Node node = new Node(" Function Boby ");
            if (match(TINY_Token_Class.Lcarlypracket) != null)
            {
                if (Statments() != null)
                {
                    if (Return_statement() != null)
                    {
                        if (match(TINY_Token_Class.Rcarlypracket) != null)
                        {
                            node.children.Add(match(TINY_Token_Class.Lcarlypracket));
                            node.children.Add(Statments());
                            node.children.Add(Return_statement());
                            node.children.Add(match(TINY_Token_Class.Rcarlypracket));
                            return node;
                        }
                    }
                }
            }
            return null;
        }
        public Node Statments()
        {
            return null;
        }

        // int sum () { body }
        public Node Function_Statement()
        {
            Node node = new Node(" Function Statment ");
            Node funDec = FunctionDeclaration();
            if (funDec != null)
            {
                
                Node funBody = Function_Body();
                if (funBody != null)
                {
                    node.children.Add(funDec);
                    node.children.Add(funBody);
                    return node;
                }
            }
            return null;
        }

        public Node Main_Function()
        {
            Node node = new Node(" Main Function ");
            Node data = DataType();
            if (data != null)
            {
                Node main = match(TINY_Token_Class.main);
                if (main != null)
                {
                    Node left = match(TINY_Token_Class.LParanthesis);
                    Node right = match(TINY_Token_Class.RParanthesis);
                    if (left != null && right !=null)
                    {
                        Node body = Function_Body();
                        if (body != null)
                        {
                          node.children.Add(data);
                          node.children.Add(main);
                          node.children.Add(left);
                          node.children.Add(right);
                          node.children.Add(body);
                          return node;

                        }
                    }
                   
                }
            }
            return null;
        }

        public Node Program()
        {
            Node node = new Node(" Program ");
            Node functionss = Functionss();
            if (functionss != null)
            {
                Node main = Main_Function();
                if (main != null)
                {
                    node.children.Add(functionss);
                    node.children.Add(main);
                    return node;
                }
            }
            return null;
        }

        public Node Functionss()
        {
            Node node = new Node("Functionss ");
            Node function = Function_Statement();
            if (function != null)
            {
                Node functionss = Functionss();
                if (functionss != null)
                {
                    node.children.Add(function);
                    node.children.Add(functionss);
                    return node;
                }
            }
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
