// Begin -- Parse tree node strategy for printing the special form begin

using System;

namespace Tree
{
    public class Begin : Special
    {
	public Begin() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printBegin(t, n, p);
        }

        // to evaluate expressions of the form '(begin e1 ... en)',
        // recursively evaluate e1 through en, then return the
        // result of evaluating en
        public override Node eval(Node t, Environment env)
        {
            // if cdr is nil, return nil (error state)
            if (t.getCdr() == Nil.getInstance())
            {
                Console.Error.WriteLine("Special form 'Begin' evaluated with no arguments");
                return Nil.getInstance();
            }


            // get cdr of begin expression (e1 ... en)
            t = t.getCdr();

            // recursively evaluate e1 ... e(n-1)
            while (t.getCdr() != Nil.getInstance())
            {
                t.getCar().eval(env);
                t = t.getCdr();
            }
            // evaluate en and return result
            return (t.getCar().eval(env));
        }
    }
}

