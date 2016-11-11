// Cond -- Parse tree node strategy for printing the special form cond

using System;

namespace Tree
{
    public class Cond : Special
    {
	public Cond() { }

        public override void print(Node t, int n, bool p)
        { 
            Printer.printCond(t, n, p);
        }

        // to evaluate expressions of the form '(cond (b1 ...) ... (bn ...))',
        // evaluate b1 through bn until a condition not equal to #f is found.
        // if this case is of the form '(bi)', return bi.
        // if this case is of the form '(bi e1 ... en)', evaluate e1-en and return
        // the result of evaluating en
        public override Node eval(Node t, Environment env)
        {
            // if cond expression has no cdr, invalid syntax (error state)
            if(t.getCdr() == Nil.getInstance())
            {
                Console.Error.WriteLine("Special form 'cond' evaluated with no arguments");
                return Nil.getInstance();
            }

            // get cdr of expression
            t = t.getCdr();

            // while cond statement has a cdr and its car (bi) evaluates to #f, keep moving down the tree
            while(t.getCdr() != Nil.getInstance() && t.getCar().getCar().eval(env) == BoolLit.getInstance(false))
            {
                t = t.getCdr();
            }

            // once a condition is found that is not #f or end of statement,
            // get car of t to evaluate
            t = t.getCar();

            // if cdr of t is null, statement is of form '(bi)', so return bi
            if (t.getCdr().eval(env) == Nil.getInstance())
                return (t.getCar());

            // otherwise, statement is of form (bi e1 ... en)
            // evaluate e1 through en, then return the result of evaluating en
            // first evaluate e1 through e(n-1)
            while(t.getCdr() != Nil.getInstance())
            {
                t.getCdr().getCar().eval(env);
                t = t.getCdr();
            }

            // finally evaluate and return en
            return t.getCar().eval(env);
        }
    }
}


