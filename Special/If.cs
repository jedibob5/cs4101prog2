// If -- Parse tree node strategy for printing the special form if

using System;

namespace Tree
{
    public class If : Special
    {
	public If() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printIf(t, n, p);
        }

        // to evaluate expressions of the form '(if b t e)',
        // evaluate b, then if b is not the boolean constant #f,
        // evaluate t and return the result. otherwise, evaluate
        // e and return the result.
        public override Node eval(Node t, Environment env)
        {
            // if cdr is nil, return nil (error state)
            if (t.getCdr() == Nil.getInstance())
            {
                Console.Error.WriteLine("Special form 'If' evaluated with no arguments");
                return Nil.getInstance();
            }

            // get cdr of expression (conditional and execution statements)
            t = t.getCdr();

            // if cdr of t is nil, expression has a conditional but no executable statements (error state)
            if(t.getCdr() == Nil.getInstance())
            {
                Console.Error.WriteLine("Special form 'If' evaluated with insufficient arguments");
                return Nil.getInstance();
            }

            // evaluate car of b (conditional). if b is not #f, evaluate and return t.
            if (t.getCar().eval(env) != BoolLit.getInstance(false))
            {
                return (t.getCdr().getCar().eval(env));
            }
            else
            {
                // otherwise, b is #f. evaluate and return e.
                // if e is nil, statement is still valid. interpreter will parse and return nil.
                return (t.getCdr().getCdr().getCar().eval(env));
            }
        }
    }
}

