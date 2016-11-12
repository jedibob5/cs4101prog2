// Let -- Parse tree node strategy for printing the special form let

using System;

namespace Tree
{
    public class Let : Special
    {
	public Let() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printLet(t, n, p);
        }

        // to evaluate an expression of the form '(let ((x1 e1) ... (xn en)) b1 ... bm)',
        // recursively evaluate e1 ... en, and call the results e1' ... en'
        // construct an association list ((x1 e1') ... (xn en'))
        // create a new environment, env1, by 'cons-ing' the assoc. list in front of 'env'
        // recursively evaluate b1 ... bm in the new environment env1, and return the
        // result of evaluating bm.
        public override Node eval(Node t, Environment env)
        {
            // error state
            if(t.getCdr() == Nil.getInstance())
            {
                Console.Error.WriteLine("Special form 'Begin' evaluated with no arguments");
                return Nil.getInstance();
            }

            // get cdr of t to eval
            Node temp = t.getCdr();

            // construct a new environment for the Let expression
            Environment e = new Environment(env);

            while(temp.getCar() != Nil.getInstance())
            {
                e.define(temp.getCar().getCar(), temp.getCar().getCdr().getCar().eval(e));
                temp = temp.getCdr();
            }

            return t.getCdr().getCdr().getCar().eval(e);
        }
    }
}


