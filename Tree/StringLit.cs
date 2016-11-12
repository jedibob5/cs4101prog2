// StringLit -- Parse tree node class for representing string literals

using System;

namespace Tree
{
    public class StringLit : Node
    {
        private string stringVal;

        //needed for builtin
        public static bool quoted = true;

        public StringLit(string s)
        {
            stringVal = s;
        }

        public override void print(int n)
        {
            if (quoted)
            {
                Printer.printStringLit(n, stringVal);
            }
            //builtin display
            else
            {
                for(int i = 0; i < n; i = i + 1)
                {
                    Console.Write(" ");
                }
                Console.Write(stringVal);
                if(n >= 0)
                {
                    Console.WriteLine();
                }
            }
        }

        public override bool isString()
        {
            return true;
        }

        public override Node eval(Environment env)
        {
            return this;
        }
    }
}

