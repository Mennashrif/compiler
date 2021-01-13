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
        public List<string> Errors = new List<string>();
        public static Node root;

        public Node Parse(List<TINY_Token> Tokens)
        {
            TokenStream = Tokens;
            root = Program();

            return root;
        }
        public Node ReservedKeywords() //done 
        {
            Node node = new Node("ReservedKeyWords");
            if (ISmatch(TINY_Token_Class.Int, tokenIndex))
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
            else if (ISmatch(TINY_Token_Class.read, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.read));
                return node;
            }
            else if (ISmatch(TINY_Token_Class.write, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.write));
                return node;
            }
            else if (ISmatch(TINY_Token_Class.repeat, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.repeat));
                return node;
            }
            else if (ISmatch(TINY_Token_Class.until, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.until));
                return node;
            }
            else if (ISmatch(TINY_Token_Class.If, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.If));
                return node;
            }
            else if (ISmatch(TINY_Token_Class.Elseif, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.Elseif));
                return node;
            }
            else if (ISmatch(TINY_Token_Class.Else, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.Else));
                return node;
            }
            else if (ISmatch(TINY_Token_Class.then, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.then));
                return node;
            }
            else if (ISmatch(TINY_Token_Class.main, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.main));
                return node;
            }
            else if (ISmatch(TINY_Token_Class.Return, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.Return));
                return node;
            }
            else if (ISmatch(TINY_Token_Class.endl, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.endl));
                return node;
            }
            else
            {

            }
            return null;
        }
        public Node Condition()
        {
            Node node = new Node("Condition");
            Node identifier = match(TINY_Token_Class.Identifier);
            Node condition_operator = Condition_operator();
            Node term = Term();
            if (identifier != null && condition_operator.children.Count() != 0 && term.children.Count() != 0)
            {
                node.children.Add(identifier);
                node.children.Add(condition_operator);
                node.children.Add(term);
            }
            else
                Errors.Add(" expected boolean in condition !");
            return node;
        }
        public Node Term()
        {
            Node node = new Node("Term");
            Node function_call = Function_Call();

            if (ISmatch(TINY_Token_Class.Number, tokenIndex))
                node.children.Add(match(TINY_Token_Class.Number));

            else if (function_call != null)
                node.children.Add(function_call);

            else if (ISmatch(TINY_Token_Class.Identifier, tokenIndex))
                node.children.Add(match(TINY_Token_Class.Identifier));

            return node;
        }
        public Node Condition_operator()
        {
            Node node = new Node("Condition_operator");

            if (TokenStream[tokenIndex].token_type == TINY_Token_Class.GreaterThanOp)
                node.children.Add(match(TINY_Token_Class.GreaterThanOp));

            else if (TokenStream[tokenIndex].token_type == TINY_Token_Class.LessThanOp)
                node.children.Add(match(TINY_Token_Class.LessThanOp));

            else if (TokenStream[tokenIndex].token_type == TINY_Token_Class.ISEqualOp)
                node.children.Add(match(TINY_Token_Class.ISEqualOp));

            else if (TokenStream[tokenIndex].token_type == TINY_Token_Class.NotEqualOp)
                node.children.Add(match(TINY_Token_Class.NotEqualOp));

            return node;
        }
        public Node Return_statement() // not tested ,finish equation first 
        {
            Node node = new Node("Return_statement");

            if (ISmatch(TINY_Token_Class.Return, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.Return));

                Node expression = Expression();
                if (expression != null)
                {
                    node.children.Add(expression);
                    if (ISmatch(TINY_Token_Class.Semicolon, tokenIndex))
                    {
                        node.children.Add(match(TINY_Token_Class.Semicolon));

                    }
                    else
                    {
                        Errors.Add("Missing Semicolon !!");
                    }
                }
            }
            return node;
        }
        public Node ReadStatement()
        {
            Node node = new Node("ReadStatement");
            Node read = match(TINY_Token_Class.read);
            if (read != null)
            {
                node.children.Add(read);

                Node identifier = match(TINY_Token_Class.Identifier);
                if (identifier != null)
                {
                    node.children.Add(identifier);
                    Node semicolon = match(TINY_Token_Class.Semicolon);
                    if (semicolon != null)
                    {
                        node.children.Add(semicolon);

                    }
                    else
                    {
                        Errors.Add("Missing Semicolon !!");
                    }

                }
                else
                {
                    Errors.Add("Found read without identifier !!");
                }
            }
            return node;
        }
        public Node WriteStatement()// not tested ,finish equation first 
        {
            Node node = new Node("WriteStatement");
            Node write = match(TINY_Token_Class.write);
            if (write != null)
            {
                node.children.Add(write);
                Node expression = Expression();
                if (expression.children[0].children.Count != 0)
                    node.children.Add(expression);
                Node endl = match(TINY_Token_Class.endl);
                if (endl != null)
                    node.children.Add(endl);
                Node semicolon = match(TINY_Token_Class.Semicolon);
                if (semicolon != null)
                {
                    node.children.Add(semicolon);
                }
                if (semicolon == null)
                {
                    Errors.Add("Missing semicolon !! ");
                }

                if (expression.children[0].children.Count == 0 && endl == null)
                    Errors.Add("Found write without expression or endl !! ");
            }
            return node;
        }
        public Node Datatype()
        {
            Node node = new Node("Datatype");
            if (ISmatch(TINY_Token_Class.Int, tokenIndex))
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
            int start = 0;
            int end = 0;
            if (ISmatch(TINY_Token_Class.Identifier, tokenIndex))
            {
                Node id = match(TINY_Token_Class.Identifier);

                node.children.Add(id);
                Node assignmentlOp = match(TINY_Token_Class.AssignmentlOp);
                Node equal = match(TINY_Token_Class.ISEqualOp);
                if (assignmentlOp != null || equal != null)
                {
                    node.children.Add(assignmentlOp);
                    start = tokenIndex;
                    Node expression = Expression();
                    if (expression.children.Count != 0)
                    {
                        node.children.Add(expression);
                        end = tokenIndex;
                        Node semicolon = match(TINY_Token_Class.Semicolon);
                        if (semicolon != null)
                        {
                            node.children.Add(semicolon);

                        }
                        else if (!ISmatch(TINY_Token_Class.Comma, tokenIndex))
                            Errors.Add("Missing semicolon");

                    }
                    else
                        Errors.Add("Invalid expression");
                }
                if (equal != null)
                {
                    string m = " ";
                    for (int i = start; i < end; i++)
                    {
                        m += TokenStream[i].lex;
                    }
                    Errors.Add(":= instead of = in " + id.Name + "=" + m);
                }
            }

            return node;
        }
        public Node Expression()
        {
            Node node = new Node("Expression");

            //Node equation = Equation();
            if (ISmatch(TINY_Token_Class.Stringstat, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.Stringstat));

            }
            Node term = Term();
            if (term.children.Count != 0)
            {
                node.children.Add(term);

            }

            //else if (equation != null)
            //{
            //    node.children.Add(equation);
            //    return node;
            //}    

            return node;
        }
        public Node Arithmatic_Operation()
        {
            Node node = new Node("Arithmatic_Operatio");
            if (ISmatch(TINY_Token_Class.MinusOp, tokenIndex))
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
                        else if (ISmatch(TINY_Token_Class.Identifier, tokenIndex) && !ISmatch(TINY_Token_Class.AssignmentlOp, tokenIndex + 1))
                        {
                            node.children.Add(match(TINY_Token_Class.Identifier));
                            // error semicolon
                            Errors.Add("Missing semicolon here !!");
                            return node;
                        }
                        else if (!ISmatch(TINY_Token_Class.Identifier, tokenIndex) && (ISmatch(TINY_Token_Class.Semicolon, tokenIndex + 1) || ISmatch(TINY_Token_Class.AssignmentlOp, tokenIndex)))
                        {
                            Errors.Add("Expected identifier  found " + DataType.children[0].Name);
                            return node;
                        }
                        else
                        {
                            // assighmentstate;
                            break;
                        }
                    }

                    Node Assignmentstate = Assignment_Statement();
                    if (Assignmentstate.children.Count != 0 && ISmatch(TINY_Token_Class.Comma, tokenIndex))
                    {
                        node.children.Add(Assignmentstate);
                        node.children.Add(match(TINY_Token_Class.Comma));
                    }
                    else if (Assignmentstate.children.Count != 0)
                    {
                        node.children.Add(Assignmentstate);
                        node.children.Add(match(TINY_Token_Class.Semicolon));
                        return node;
                    }
                    else
                        break;
                    //else if(Assignmentstate != null)
                    //{
                    //    node.children.Add(Assignmentstate);

                    //    return node;
                    //}
                }
            }
            return null;
        }
        public Node Function_Call()
        {
            Node node = new Node("Function_Call");

            if (ISmatch(TINY_Token_Class.Identifier, tokenIndex) && ISmatch(TINY_Token_Class.LParanthesis, tokenIndex + 1))
            {
                node.children.Add(match(TINY_Token_Class.Identifier));
                node.children.Add(match(TINY_Token_Class.LParanthesis));
            }
            else
                return null;
            if (ISmatch(TINY_Token_Class.RParanthesis, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.RParanthesis));
                if (ISmatch(TINY_Token_Class.Semicolon, tokenIndex))
                {
                    node.children.Add(match(TINY_Token_Class.Semicolon));
                    return node;
                }
                else
                {
                    Errors.Add("Missing semicolon");
                    return node;
                }
            }

            else if (ISmatch(TINY_Token_Class.Identifier, tokenIndex) && ISmatch(TINY_Token_Class.RParanthesis, tokenIndex + 1))
            {
                node.children.Add(match(TINY_Token_Class.Identifier));
                node.children.Add(match(TINY_Token_Class.RParanthesis));
                if (ISmatch(TINY_Token_Class.Semicolon, tokenIndex))
                {
                    node.children.Add(match(TINY_Token_Class.Semicolon));
                    return node;
                }
                else
                {
                    Errors.Add("Missing semicolon");
                    return node;
                }
            }
            else
            {
                parameter(node);
                if (ISmatch(TINY_Token_Class.Semicolon, tokenIndex))
                {
                    node.children.Add(match(TINY_Token_Class.Semicolon));
                    return node;
                }
                else
                {
                    Errors.Add("Missing semicolon");
                    return node;
                }


            }

        }

        private void parameter(Node node)
        {

            if (ISmatch(TINY_Token_Class.RParanthesis, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.RParanthesis));
                return;
            }
            else if (ISmatch(TINY_Token_Class.Semicolon, tokenIndex))
            {
                Errors.Add("missing )");
                return;
            }
            else
            {

                node.children.Add(match(TINY_Token_Class.Comma));
                node.children.Add(Expression());
                parameter(node);

            }

        }

        public Node match(TINY_Token_Class ExpectedToken)
        {
            TINY_Token CurrentToken = new TINY_Token();
            if (tokenIndex < TokenStream.Count)
            {
                CurrentToken = TokenStream[tokenIndex];
            }
            if (CurrentToken.token_type == ExpectedToken)
            {
                Node node = new Node(CurrentToken.lex);
                tokenIndex++;
                return node;
            }
            return null;
        }

        public bool ISmatch(TINY_Token_Class ExpectedToken, int tk_index)
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

        //had la amany
        public Node Boolean_Operator()
        {
            Node node = new Node("Boolean_Operator");
            if (ISmatch(TINY_Token_Class.AndOp, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.AndOp));
                return node;
            }
            else if (ISmatch(TINY_Token_Class.OROp, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.OROp));
                return node;
            }

            return null;
        }
        //if a+b>5
        //if a==1  then g:=4 
        public Node Condition_Statement()
        {
            Node node = new Node("condition_statement");
            Node condition = Condition();


            if (condition != null)
            {
                node.children.Add(condition);
                Node boolean_Operator = Boolean_Operator();
                if (boolean_Operator != null)
                {
                    node.children.Add(boolean_Operator);
                    Node condition_Statement = Condition_Statement();
                    node.children.Add(condition_Statement);
                }
                return node;

            }
            return null;
        }
        public Node Statements()
        {
            Node node = new Node("Statements");
            Node statement = Statement();
            if (statement != null)
            {
                node.children.Add(statement);
                Node statements = Statements();
                node.children.Add(statements);
            }
            return node;
        }
        public Node Statement()
        {
            Node node = new Node("Statement");
            Node writeStatement = WriteStatement();
            if (writeStatement.children.Count != 0)
            {
                node.children.Add(writeStatement);
                return node;
            }
            Node readStatement = ReadStatement();
            if (readStatement.children.Count != 0)
            {
                node.children.Add(readStatement);
                return node;
            }
            Node comment_Statement = match(TINY_Token_Class.Comment);
            if (comment_Statement != null)
            {
                node.children.Add(comment_Statement);
                return node;
            }
            Node repeat_statement = Repeat_statement();
            if (repeat_statement != null)
            {
                node.children.Add(repeat_statement);
                return node;
            }
            Node declarationStatement = DeclarationStatement();
            if (declarationStatement != null)
            {
                node.children.Add(declarationStatement);
                return node;
            }
            Node If_statement = if_statement();
            if (If_statement != null)
            {
                node.children.Add(If_statement);
                return node;
            }
            Node return_statement = Return_statement();
            if (return_statement.children.Count != 0)
            {
                node.children.Add(return_statement);
                return node;
            }
            Node function_Call = Function_Call();
            if (function_Call != null)
            {
                node.children.Add(function_Call);
                return node;
            }
            Node assignment_Statement = Assignment_Statement();
            if (assignment_Statement.children.Count != 0)
            {
                node.children.Add(assignment_Statement);
                return node;
            }
            return null;
        }
        public Node if_statement()
        {
            Node node = new Node("if_statement");

            if (ISmatch(TINY_Token_Class.If, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.If));
                Node condition_Statement = Condition_Statement();
                if (condition_Statement != null)
                {
                    node.children.Add(condition_Statement);
                    if (ISmatch(TINY_Token_Class.then, tokenIndex))
                    {
                        node.children.Add(match(TINY_Token_Class.then));
                        Node statements = Statements();
                        if (statements != null)
                            node.children.Add(statements);
                        Node elseClaose = Else_Claose();
                        if (elseClaose != null)
                        {
                            node.children.Add(elseClaose);
                            return node;
                        }
                    }
                }
            }

            return null;
        }

        public Node ELse_if_statement()
        {
            Node node = new Node("Else_if_statement");

            if (ISmatch(TINY_Token_Class.Elseif, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.Elseif));
                Node condition_Statement = Condition_Statement();
                if (condition_Statement != null)
                {
                    node.children.Add(condition_Statement);
                    if (match(TINY_Token_Class.then) != null)
                    {
                        node.children.Add(match(TINY_Token_Class.then));
                        Node statements = Statements();
                        if (statements != null)
                            node.children.Add(statements);
                        Node else_Claose = Else_Claose();
                        if (else_Claose != null)
                        {
                            node.children.Add(else_Claose);
                            return node;
                        }
                    }
                }
            }

            return null;
        }
        public Node Else_statement()
        {
            Node node = new Node("else_statement");

            if (ISmatch(TINY_Token_Class.Else, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.Else));
                Node statements = Statements();
                if (statements != null)
                {
                    node.children.Add(statements);
                    if (ISmatch(TINY_Token_Class.endl, tokenIndex))
                    {
                        node.children.Add(match(TINY_Token_Class.endl));
                        return node;
                    }
                }
            }

            return null;
        }
        public Node Else_Claose()
        {
            Node node = new Node("Else_Claose");
            Node eLse_if_statement = ELse_if_statement();
            if (eLse_if_statement != null)
            {
                node.children.Add(eLse_if_statement);
                return node;
            }
            Node else_statement = Else_statement();
            if (else_statement != null)
            {
                node.children.Add(else_statement);
                return node;
            }
            if (ISmatch(TINY_Token_Class.endl, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.endl));
                return node;
            }
            return null;
        }
        public Node Repeat_statement()
        {
            Node node = new Node("Repeat_statement");
            if (ISmatch(TINY_Token_Class.repeat, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.repeat));
                Node statements = Statements();
                if (statements != null)
                {
                    node.children.Add(statements);
                    if (ISmatch(TINY_Token_Class.until, tokenIndex))
                    {
                        node.children.Add(match(TINY_Token_Class.until));
                        Node condition_Statement = Condition_Statement();
                        if (condition_Statement != null)
                        {
                            node.children.Add(condition_Statement);
                            return node;
                        }
                    }
                }
            }
            return null;
        }
        //da bta3 tasneem 
        public Node FunctionName()
        {
            Node node = new Node("Function Name");
            Node identifier = match(TINY_Token_Class.Identifier);

            if (identifier != null && ISmatch(TINY_Token_Class.main, tokenIndex) == false)
            {
                node.children.Add(identifier);
                return node;
            }
            return null;
        }


        public Node Parameter()
        {
            Node node = new Node(" Parameter ");
            Node dataType = Datatype();

            if (dataType != null)
            {
                node.children.Add(dataType);
                Node identifier = match(TINY_Token_Class.Identifier);
                if (identifier != null)
                {
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
            Node dataType = Datatype();
            if (dataType != null)
            {
                node.children.Add(dataType);
                Node FunName = FunctionName();
                if (FunName != null)
                {
                    node.children.Add(FunName);
                    if (ISmatch(TINY_Token_Class.LParanthesis, tokenIndex))
                    {
                        node.children.Add(match(TINY_Token_Class.LParanthesis));
                        Node attrib = FunctionAttribute();
                        if (attrib != null)
                        {
                            node.children.Add(attrib);
                        }
                        if (ISmatch(TINY_Token_Class.RParanthesis, tokenIndex))
                        {
                            node.children.Add(match(TINY_Token_Class.RParanthesis));
                        }
                        return node;
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
                Node attributess = Attributess();
                if (attributess != null)
                {
                    node.children.Add(attributess);

                }
                return node;
            }
            return null;
        }

        public Node Attributess()
        {
            Node node = new Node(" Function Attributes ");
            if (ISmatch(TINY_Token_Class.Comma, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.Comma));
                Node parameter = Parameter();

                if (parameter != null)
                {

                    node.children.Add(parameter);
                    Node attributess = Attributess();
                    node.children.Add(attributess);
                    return node;
                }

            }
            return null;
        }


        public Node Function_Body()
        {
            Node node = new Node(" Function Boby ");

            if (ISmatch(TINY_Token_Class.Lcarlypracket, tokenIndex))
            {
                node.children.Add(match(TINY_Token_Class.Lcarlypracket));
                Node statment = Statements();
                if (statment != null)
                    node.children.Add(statment);
                //var name = statment.children[statment.children.Count - 1].children.ElementAt(0).Name;
                //MessageBox.Show(name);
                //if (name == "Return_statement")
                //{
                    if (ISmatch(TINY_Token_Class.Rcarlypracket, tokenIndex))
                    {
                        node.children.Add(match(TINY_Token_Class.Rcarlypracket));
                        return node;
                    }
                //}
            }
            return null;
        }

        // int sum () { body }
        public Node Function_Statement()
        {
            Node node = new Node(" Function Statment ");
            Node funDec = FunctionDeclaration();
            if (funDec != null)
            {
                node.children.Add(funDec);
                Node funBody = Function_Body();
                if (funBody != null)
                {
                    node.children.Add(funBody);
                    return node;
                }
            }
            return null;
        }

        public Node Main_Function()
        {
            Node nodee = new Node(" Main Function ");
            Node data = Datatype();
            if (data != null)
            {
                nodee.children.Add(data);
                if (ISmatch(TINY_Token_Class.main, tokenIndex))
                {
                    nodee.children.Add(match(TINY_Token_Class.main));
                    if (ISmatch(TINY_Token_Class.LParanthesis, tokenIndex) && ISmatch(TINY_Token_Class.RParanthesis, tokenIndex + 1))
                    {

                        nodee.children.Add(match(TINY_Token_Class.LParanthesis));
                        nodee.children.Add(match(TINY_Token_Class.RParanthesis));

                        Node body = Function_Body();
                        if (body != null)
                        {
                            nodee.children.Add(body);
                            return nodee;

                        }
                        return nodee;
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
                node.children.Add(functionss);
                tokenIndex -= 1;
            }

            Node mainn = Main_Function();
            if (mainn != null)
            {
                node.children.Add(mainn);
                return node;
            }
            return null;
        }

        public Node Functionss()
        {
            Node node = new Node("Functionss ");
            Node function = Function_Statement();
            if (function != null)
            {
                node.children.Add(function);
                Node functionss = Functionss();
                node.children.Add(functionss);
            }
            return node;
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