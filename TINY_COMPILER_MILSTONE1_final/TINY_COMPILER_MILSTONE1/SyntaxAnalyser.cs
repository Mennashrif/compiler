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
                 root = DeclarationStatement();
           
            return root;
        }
        public Node Condition()
        {
            Node node = new Node("Condition");
            Node identifier = match(TINY_Token_Class.Identifier);
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
            Node node = new Node("Term");
            Node function_call = Function_Call();
            if (ISmatch(TINY_Token_Class.Number,tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.Number));
                return node;
            }
            else if (function_call != null)
            {
                node.children.Add(function_call);
                return node;
            }
            else if(ISmatch(TINY_Token_Class.Identifier,tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.Identifier));
                return node;
            }
           
          
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
        public Node Return_statement() // not tested ,finish equation first
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
            Node identifier = match(TINY_Token_Class.Identifier);
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
        public Node WriteStatement()// not tested ,finish equation first 
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
        public Node Datatype()
        {
            Node node = new Node("Datatype");
            if (ISmatch(TINY_Token_Class.Int,tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.Int));
                return node;
            }
            else if (ISmatch(TINY_Token_Class.Float, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.Float));
                return node;
            }
            else if (ISmatch(TINY_Token_Class.String, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.String));
                return node;
            }
     
            return null;
        }
        public Node Assignment_Statement()
        {
            Node node = new Node("Assignment_Statement");
            Node id= match(TINY_Token_Class.Identifier);
            Node equal = match(TINY_Token_Class.ISEqualOp);
            Node expression = Expression();
            if(id != null && equal != null && expression != null)
            {
                node.children.Add(id);
                node.children.Add(equal);
                node.children.Add(expression);
                return node;
            }
            return null;
        }
        public Node Expression()
        {
            Node node = new Node("Expression");
            Node String = match(TINY_Token_Class.String);
            Node term = Term();
            Node equation = Equation();
            if (String != null)
            {
                node.children.Add(String);
                return node;
            }
            else if (term != null)
            {
                node.children.Add(term);
                return node;
            }
            else if (equation != null)
            {
                node.children.Add(equation);
                return node;
            }    
            
            return null;
        }
        public Node Arithmatic_Operation()
        {
            Node node = new Node("Arithmatic_Operatio");
            if (ISmatch(TINY_Token_Class.MinusOp,tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.MinusOp));
                return (node);
            }
            else if (ISmatch(TINY_Token_Class.PlusOp, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.PlusOp));
                return (node);
            }
            else if (ISmatch(TINY_Token_Class.MultiplyOp, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.MultiplyOp));
                return (node);
            }
            else if (ISmatch(TINY_Token_Class.DivideOp, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.DivideOp));
                return (node);
            }
            return null;
        }

        public Node Equation()
        {
            Node node = new Node("Equation");
              Left(node);
              Mid(node);
            if (node == null)
                return null;
            Right(node);
            return node;
        }
        public void Left(Node node)
        {
            if (ISmatch(TINY_Token_Class.LParanthesis, tokenIndex) || (Term() != null && tokenIndex == TokenStream.Count - 1))
            {
                return;
            }
            else
            {
                node.children.Add(Term());
                node.children.Add(Arithmatic_Operation());
            }
            
        }
        public void Mid(Node node)
        {
            return;
        }
        public void Right(Node node)
        {
            return;
        }

        public Node DeclarationStatement()      //semicolon error
        {
            
            Node node = new Node("DeclarationStatement");
            Node DataType = Datatype();

            if (DataType != null)
            {
                node.children.Add(DataType);
                while (true)
                {
                    while (true)
                    {
                        if (ISmatch(TINY_Token_Class.Identifier, tokenIndex) && ISmatch(TINY_Token_Class.Comma, tokenIndex + 1))
                        {
                            node.children.Add(match(TINY_Token_Class.Identifier));
                            node.children.Add(match(TINY_Token_Class.Comma));
                        }
                        else if (ISmatch(TINY_Token_Class.Identifier, tokenIndex) && ISmatch(TINY_Token_Class.Semicolon, tokenIndex + 1))
                        {
                            node.children.Add(match(TINY_Token_Class.Identifier));
                            node.children.Add(match(TINY_Token_Class.Semicolon));
                            return node;
                        }
                        else if (ISmatch(TINY_Token_Class.Identifier, tokenIndex)&& !ISmatch(TINY_Token_Class.ISEqualOp, tokenIndex+1))
                        {
                            node.children.Add(match(TINY_Token_Class.Identifier));
                            // error semicolon
                            return node;
                        }

                        else
                            break;
                    }
                    Node Assignmentstate = Assignment_Statement();
                    if (Assignmentstate != null && ISmatch(TINY_Token_Class.Comma, tokenIndex)){
                        node.children.Add(Assignmentstate);
                        node.children.Add(match(TINY_Token_Class.Comma));
                        }
                    else if(Assignmentstate != null && ISmatch(TINY_Token_Class.Semicolon, tokenIndex))
                    {
                        node.children.Add(Assignmentstate);
                        node.children.Add(match(TINY_Token_Class.Semicolon));
                        return node;
                    }
                    else if(Assignmentstate != null)
                    {
                        node.children.Add(Assignmentstate);
                        //error semicolon
                        return node;
                    }
                    
    
                } 
            }
            return null;
        }
        
        public Node Function_Call()
        {
            Node node = new Node("Function_Call");
            
            if (ISmatch(TINY_Token_Class.Identifier,tokenIndex)&&ISmatch( TINY_Token_Class.LParanthesis,tokenIndex+1))
            {
                node.children.Add(match(TINY_Token_Class.Identifier));
                node.children.Add(match(TINY_Token_Class.LParanthesis));
            }
            else
                return null;
            if (ISmatch(TINY_Token_Class.RParanthesis,tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.RParanthesis));
                return node;
            }
            else if (ISmatch(TINY_Token_Class.Identifier, tokenIndex) && ISmatch(TINY_Token_Class.RParanthesis, tokenIndex+1))
            {
                node.children.Add(match(TINY_Token_Class.Identifier));
                node.children.Add(match(TINY_Token_Class.RParanthesis));
                    return node;
           }
            else
            {
                parameter(node);
                 return node;
                
            }
        }

        private void parameter(Node node)
        {

            if (ISmatch(TINY_Token_Class.RParanthesis,tokenIndex) )
            {
                node.children.Add(match(TINY_Token_Class.RParanthesis));
                return;
            }
            else
            {
                node.children.Add(match(TINY_Token_Class.Comma));
                node.children.Add(match(TINY_Token_Class.Identifier));
                parameter(node);

            }
            
        }

        public Node match(TINY_Token_Class ExpectedToken)
        {
            TINY_Token CurrentToken = TokenStream[tokenIndex];
            if (CurrentToken.token_type == ExpectedToken)
            {
                Node node = new Node(CurrentToken.lex);
                tokenIndex++;
                return node;
            }
            return null;
        }

        public bool ISmatch(TINY_Token_Class ExpectedToken,int tk_index)
        {
            TINY_Token CurrentToken = new TINY_Token();
            if (tk_index < TokenStream.Count)
            {
                 CurrentToken = TokenStream[tk_index];
                
            }
            if (CurrentToken.token_type == ExpectedToken)
            {
                return true;
            }
            return false;
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
