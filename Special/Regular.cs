// Regular -- Parse tree node strategy for printing regular lists

using System;

namespace Tree
{
    public class Regular : Special
    {
        public Regular() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printRegular(t, n, p);
        }

        // to evaluate expressions of the regular form '(f a1 ... an)',
        // i.e. function calls that don't fit under another Special
        // subclass, recursively evaluate f, a1, ... an, calling the
        // results f', a1', ... an', then call apply with f' as the
        // first argument, and (a1', ... an') as the second argument.
        public override Node eval(Node t, Environment env)
        {
            // get f' from t to use for apply call later.
            Node fPrime = t.getCar().eval(env);
            
            // create node for holding list (a1' ... an')
            Node aPrime = Nil.getInstance();

            // get cdr of regular (a1 ... an)
            t = t.getCdr();

            // recursively evaluate a1 ... a(n-1)
            while (t.getCdr() != Nil.getInstance())
            {
                aPrime = new Cons(aPrime, t.getCar().eval(env));
                t = t.getCdr();
            }

            // evaluate an, add to list, and return a'
            aPrime = new Cons(aPrime, t.getCar().eval(env));
            return fPrime.apply(aPrime);
        }
    }
}


