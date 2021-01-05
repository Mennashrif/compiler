using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TINY_COMPILER_MILSTONE1
{
    public static class TINY_Compiler
    {
        public static TINY_Scanner tiny_Scanner = new TINY_Scanner();

        public static List<string> Lexemes = new List<string>();
        public static List<TINY_Token> TokenStream = new List<TINY_Token>();


        public static void Start_Compiling(string SourceCode)
        {

            tiny_Scanner.StartScanning(SourceCode);

        }


        static void SplitLexemes(string SourceCode)
        {
            string[] Lexemes_arr = SourceCode.Split(' ');
            for (int i = 0; i < Lexemes_arr.Length; i++)
            {
                if (Lexemes_arr[i].Contains("\r\n"))
                {
                    Lexemes_arr[i] = Lexemes_arr[i].Replace("\r\n", string.Empty);
                }
                Lexemes.Add(Lexemes_arr[i]);
            }

        }


    }
}
