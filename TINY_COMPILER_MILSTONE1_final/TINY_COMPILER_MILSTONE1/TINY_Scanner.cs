using JASONParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TINY_COMPILER_MILSTONE1
{

    public enum TINY_Token_Class
{
  
    Dot, Semicolon, Comma, LParanthesis, RParanthesis, AssignmentlOp, LessThanOp, AndOp, OROp, ISEqualOp,
    GreaterThanOp, NotEqualOp, PlusOp, MinusOp, MultiplyOp, DivideOp, Lsquarepracket, Rsquarepracket, Lcarlypracket, Rcarlypracket,


    Int, Float,String,read,write,repeat,until,If,Elseif,Else,then,Return,endl,main,

    Identifier, Constant, Comment, Number, Stringstat,end

}



    public class TINY_Token
    {
        public string lex;
        public TINY_Token_Class token_type;
    }

    public class TINY_Scanner
    {
        public List<TINY_Token> Tokens = new List<TINY_Token>();
        Dictionary<string, TINY_Token_Class> ReservedWords = new Dictionary<string, TINY_Token_Class>();
        Dictionary<string, TINY_Token_Class> Operators = new Dictionary<string, TINY_Token_Class>();

        public TINY_Scanner()
        {


            int x = 3;
            Console.WriteLine(x);
            ReservedWords.Add("if", TINY_Token_Class.If);
            ReservedWords.Add("else", TINY_Token_Class.Else);
            ReservedWords.Add("elseif", TINY_Token_Class.Elseif);
            ReservedWords.Add("endl", TINY_Token_Class.endl);
            ReservedWords.Add("until", TINY_Token_Class.until);
            ReservedWords.Add("return", TINY_Token_Class.Return);
            ReservedWords.Add("int", TINY_Token_Class.Int);
            ReservedWords.Add("string", TINY_Token_Class.String);
            ReservedWords.Add("float", TINY_Token_Class.Float);
            ReservedWords.Add("repeat", TINY_Token_Class.repeat);
            ReservedWords.Add("read", TINY_Token_Class.read);
            ReservedWords.Add("then", TINY_Token_Class.then);
            ReservedWords.Add("write", TINY_Token_Class.write);
            ReservedWords.Add("main", TINY_Token_Class.main);
            ReservedWords.Add("end", TINY_Token_Class.end);

            Operators.Add(".", TINY_Token_Class.Dot);
            Operators.Add(";", TINY_Token_Class.Semicolon);
            Operators.Add(",", TINY_Token_Class.Comma);
            Operators.Add("(", TINY_Token_Class.LParanthesis);
            Operators.Add(")", TINY_Token_Class.RParanthesis);
            Operators.Add(":=", TINY_Token_Class.AssignmentlOp);
            Operators.Add("=", TINY_Token_Class.ISEqualOp);
            Operators.Add("<", TINY_Token_Class.LessThanOp);
            Operators.Add(">", TINY_Token_Class.GreaterThanOp);
            Operators.Add("&&", TINY_Token_Class.AndOp);
            Operators.Add("||", TINY_Token_Class.OROp);
            Operators.Add("<>", TINY_Token_Class.NotEqualOp);
            Operators.Add("+", TINY_Token_Class.PlusOp);
            Operators.Add("-", TINY_Token_Class.MinusOp);
            Operators.Add("*", TINY_Token_Class.MultiplyOp);
            Operators.Add("/", TINY_Token_Class.DivideOp);
            Operators.Add("[", TINY_Token_Class.Lsquarepracket);
            Operators.Add("]", TINY_Token_Class.Rsquarepracket);
            Operators.Add("{", TINY_Token_Class.Lcarlypracket);
            Operators.Add("}", TINY_Token_Class.Rcarlypracket);
        }
  
        public void StartScanning(string SourceCode) {

            bool error = false;
            string CurrentLexeme="";
            string equal = "";
            string notequal="";
            string and = "";
            string or = "";
            char temp='_';
            
            bool operat = false;
            
           
            
            for (int i = 0; i < SourceCode.Length; i++)
            {
                int j = i+1;
                
                char CurrentChar = SourceCode[i];
                 CurrentLexeme = CurrentChar.ToString();

         
               

                if (CurrentChar == ' ' || CurrentChar == '\r' || CurrentChar == '\n')
                    continue;

                if ((CurrentChar >= 'A' && CurrentChar <= 'z')&&!(i>0&& (SourceCode[i-1] >= '0' && SourceCode[i-1] <= '9'))) 
                {
                    for (; j < SourceCode.Length; j++)
                    {
                        if ((SourceCode[j] >= 'A' && SourceCode[j] <= 'z') || (SourceCode[j] >= '0' && SourceCode[j] <= '9'))
                        {
                            CurrentLexeme += SourceCode[j];
                        }
                        else
                        {
                            break;

                        }
                    }
                    i = j-1;
                    Find_Match_Token(CurrentLexeme);
                }

                else if (CurrentChar=='"')
                {
                    bool is_string = false;
                    for (; j < SourceCode.Length; j++)
                    {
                        CurrentLexeme += SourceCode[j];

                        if (SourceCode[j] == '"')
                        {
                            is_string = true;
                            break;

                        }
                        
                        
                       /* if ((SourceCode[j] >= 'A' && SourceCode[j] <= 'z') || (SourceCode[j] >= '0' && SourceCode[j] <= '9')||SourceCode[j]=='"')
                        {
                            CurrentLexeme += SourceCode[j];
                        }*/
                       
                    }
                    if (is_string == true)
                    {
                        i = j;
                    }
                    else
                    {
                        i = j - 1;
                    }
                    Find_Match_Token(CurrentLexeme);
                }

                else if (CurrentChar == '/'&&SourceCode[i+1]=='*')
                {
                    bool is_comment = false;
                    for (; j < SourceCode.Length; j++)
                    {
                        CurrentLexeme += SourceCode[j];

                        if (SourceCode[j] == '/')
                        {
                            is_comment = true;
                            break;
                        }
                       // if ((SourceCode[j] >= 'A' && SourceCode[j] <= 'z') || (SourceCode[j] >= '0' && SourceCode[j] <= '9') || SourceCode[j] == '*' || SourceCode[j] == '/')
                        //{
                           
                       // }
                       // else
                       // {
                           // break;

                       // }
                    }
                    if (is_comment == true)
                    {
                        i = j;
                    }
                    else
                    {
                        i = j - 1;
                    }
                   
                    Find_Match_Token(CurrentLexeme);
                }

                else if ((CurrentChar >= '0' && CurrentChar <= '9') || (CurrentChar == '.' && i!=SourceCode.Length-1&&(SourceCode[i + 1]!=' '))
                    ||((CurrentChar == '+' || CurrentChar == '-')
                        && i ==0) || ((CurrentChar == '+' || CurrentChar == '-') && temp != '_'))
                {
                    temp = '_';
                    operat = false;
                    for (; j < SourceCode.Length; j++)
                    {
                        if (!((SourceCode[j] >= '0' && SourceCode[j] <= '9') || SourceCode[j] == '.' || (SourceCode[j] >= 'A' && SourceCode[j] <= 'z')))
                        {
                           
                            break;
                        }
                        else
                        {

                            if ((SourceCode[j] >= 'A' && SourceCode[j] <= 'z') && !(SourceCode[j - 1] >= '0' && SourceCode[j - 1] <= '9'))
                            {      //&&temp!='_'   if not number in grammer phase
                                break;
                            }
                            CurrentLexeme += SourceCode[j];
                      

                        }
                      
                    }
                    i = j - 1;
                    Find_Match_Token(CurrentLexeme);

                }
                else if (CurrentChar == ':')
                {
                    equal += CurrentLexeme;
                    if (i == SourceCode.Length - 1 || SourceCode[i + 1] != '=')
                    {
                        Find_Match_Token(equal);
                        equal = "";
                    }

                }
                else if (CurrentChar == '=' &&i>0&& SourceCode[i - 1] == ':')
                {
                    equal += CurrentChar;
                    Find_Match_Token(equal);
                    equal="";
                }
                  


                   else if (CurrentChar == '<')
                {
                    notequal += CurrentLexeme;
                    if (i == SourceCode.Length - 1 || SourceCode[i + 1] != '>')
                    {
                        Find_Match_Token(notequal);
                        notequal = "";
                    }

                }
                else if (CurrentChar == '>' &&i>0&& SourceCode[i - 1] == '<')
                {
                    notequal += CurrentChar;
                    Find_Match_Token(notequal);
                    notequal="";
                }
                else if(CurrentChar=='&'){
                        and += CurrentLexeme;
                    if (i == SourceCode.Length - 1 || SourceCode[i + 1] != '&')
                    {
                        Find_Match_Token(and);
                        and = "";
                    }

                }
                else if (CurrentChar == '&' && i > 0 && SourceCode[i - 1] == '&')
                {
                    and += CurrentChar;
                    Find_Match_Token(and);
                    and = "";
                }


                else if (CurrentChar == '|')
                {
                    or += CurrentLexeme;
                    if (i == SourceCode.Length - 1 || SourceCode[i + 1] != '|')
                    {
                        Find_Match_Token(or);
                        or = "";
                    }

                }
                else if (CurrentChar == '|' && i > 0 && SourceCode[i - 1] == '|')
                {
                    or += CurrentChar;
                    Find_Match_Token(or);
                    or = "";
                }


          
                
              
               
                else
                {

                    Find_Match_Token(CurrentLexeme);
                }



                if (SourceCode[i] == '+' || SourceCode[i] == '-' || SourceCode[i] == '=')
                {
                    operat = true;
                    temp = CurrentChar;
                }
            }


        }
        int size = 0;
        //test every tokens to get the match one
        public  void Find_Match_Token(string Lex)
        {
            TINY_Token_Class TC;

            
           List<string>rev=new List<string>();
           List<string> operatorsl = new List<string>();
            int counter = 0;
            string lex_con = "";
            size = Lex.Length;
      

                 if (!isreversed_keyword(Lex))
                {
                    for (int i = 0; i < ReservedWords.Count; i++)
                    {
                        if (Lex== ReservedWords.ElementAt(i).Key)
                        {

                            TINY_Token Tok = new TINY_Token();
                            Tok.lex = Lex;
                            Tok.token_type = ReservedWords.ElementAt(i).Value;
                            Tokens.Add(Tok);
                            break;
                        }

                    }

                }



                
                    else if (!isoperator(Lex))
                    {
                        

                            for (int i = 0; i < Operators.Count; i++)
                            {

                                if (Lex == Operators.ElementAt(i).Key)
                                {

                                    TINY_Token Tok = new TINY_Token();
                                    Tok.lex = Lex;
                                    Tok.token_type = Operators.ElementAt(i).Value;
                                    Tokens.Add(Tok);
                                    break;
                                }

                            }

                
                    }

                 else if (isIdentifier(Lex))
                {
                    TINY_Token Tok = new TINY_Token();
                    Tok.lex = Lex;
                    Tok.token_type = TINY_Token_Class.Identifier;
                    Tokens.Add(Tok);

                }


                else if (Lex == ";")
                {
                    TINY_Token Tok = new TINY_Token();
                    Tok.lex = Lex;
                    Tok.token_type = TINY_Token_Class.Semicolon;
                    Tokens.Add(Tok);

                }

                else if (isstring(Lex))
                {
                    TINY_Token Tok = new TINY_Token();
                    Tok.lex = Lex;
                    Tok.token_type = TINY_Token_Class.Stringstat;
                    Tokens.Add(Tok);

                }


                else if (iscomment(Lex))
                {
                    TINY_Token Tok = new TINY_Token();
                    Tok.lex = Lex;
                    Tok.token_type = TINY_Token_Class.Comment;
                    Tokens.Add(Tok);

                }

                else if (isnumber(Lex))
                {
                    TINY_Token Tok = new TINY_Token();
                    Tok.lex = Lex;
                    Tok.token_type = TINY_Token_Class.Number;
                    Tokens.Add(Tok);

                }


                else
                {
                    TINY_Token Tok = new TINY_Token();
                    Tok.lex = Lex;
                    Errors.Error_List.Add(Tok.lex);
                }


            
        }
        // check if operator
        bool isoperator(string lex)
        {
            bool isnotValid = true;
            
                for (int i = 0; i < Operators.Count; i++)
                {
                    if (lex== Operators.ElementAt(i).Key)
                    {
                        isnotValid = false;
                        break;
                    }

                }
            

            return isnotValid;
        }

        // check if  identifier
        bool isIdentifier(string lex)
        {
            bool isValid = true;
            if (lex[0] >= 'A' && lex[0] <= 'z')
            {
                for (int i = 1; i <lex.Length; i++)
                {
                    if (lex[i] >= 'A' && lex[i] <= 'z' || lex[i] >= '0' && lex[i] <= '9')
                    {
                        isValid = true;

                    }
                    else
                        isValid = false;
                    break;
                }

            }
            else
            {
                isValid = false;


            }


            return isValid;
        }
        // check if reversed word
        bool isreversed_keyword(string lex)
        {
            bool isnotValid = true;

            for (int i = 0; i < ReservedWords.Count; i++)
            {
                if (lex == ReservedWords.ElementAt(i).Key)
                {
                    isnotValid = false;
                    break;
                }

            }

            return isnotValid;

        }

        // check if string
        bool isstring(string lex)
        {

            bool isValid = false;
           

            if (lex[0] == '"' && lex[lex.Length - 1] == '"')
            {
                if (lex.Length==2)
                {
                    return true;
                }
                else
                {
                    return true;
                }
               /* for (int i = 1; i <=lex.Length - 2; i++)
                {
                    if ((lex[i] >= 'A' && lex[i] <= 'z' )|| (lex[i] >= '0' && lex[i] <= '9'))
                    {
                        isValid = true;
                    }
                    else
                    {
                        isValid = false;
                        break;

                    }

                }*/
            }
            
           


            return isValid;
        }

        //check if comment
        bool iscomment(string lex)
        {
            bool isValid = false;
            if (lex[0] == '/' && lex[1] == '*' && lex[lex.Length - 2] == '*' && lex[lex.Length - 1] == '/')
            {
                if (lex.Length == 4)
                {
                    return true;
                }
                else
                {
                    return true;
                }
              /*  for (int i = 2; i <= lex.Length - 3; i++)
                {
                    if (lex[i] >= 'A' && lex[i] <= 'z' || lex[i] >= '0' && lex[i] <= '9')
                    {
                        isValid = true;
                    }
                    else
                    {
                        isValid = false;
                        break;
                    }


                }*/
            }
            return isValid;

        }
        //check if number
        bool isnumber(string lex)
        {
            bool isValid = false;
            int c = 0;
            if ((lex[0] == '-' || lex[0] == '+' || (lex[0] >= '0' && lex[0] <= '9')) && (lex[lex.Length - 1] >= '0' && lex[lex.Length - 1] <= '9'))
            {
                if ((lex[0] == '-' || lex[0] == '+') && lex[1] == '.')
                {
                    return false;
                }
                isValid = true;
                for (int i = 1; i < lex.Length; i++)
                {

                    if (lex[i] >= '0' && lex[i] <= '9' || lex[i] == '.')
                    {
                        if (lex[i] == '.')
                        {

                            c++;
                        }


                    }

                    else
                    {
                        isValid = false;
                        break;
                    }


                }



            }


            else
            {
                isValid = false;
            }

            if (c > 1)
            {
                isValid = false;
            }
            return isValid;

        }


      
    }

}

/*
          bool error = false;
          string CurrentLexeme = "";
          for (int i = 0; i < SourceCode.Length; i++)
          {
              int j = i + 1;
              char CurrentChar = SourceCode[i];


              if (CurrentChar == ' ' || CurrentChar == '\r' || CurrentChar == '\n')
              {

                  CurrentLexeme = "";
                  continue;
              }

              else
              {


                  CurrentLexeme += CurrentChar.ToString();

                for (; j < SourceCode.Length; j++)
                  {
                      if (SourceCode[j] == ' ' || SourceCode[j] == '\r' || SourceCode[j] == '\n')
                      {
                          break; 
                      }


                      CurrentLexeme += SourceCode[j];


                  }


                  FindTokenClass(CurrentLexeme);
                  i = j - 1;





              }
            
               

          }
         // FindTokenClass(CurrentLexeme);

          TINY_Compiler.TokenStream = Tokens;*/
