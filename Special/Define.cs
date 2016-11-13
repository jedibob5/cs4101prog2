// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree
{
    public class Define : Special
    {
        public Define() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printDefine(t, n, p);
        }

        public override Node eval(Node t, Environment env)
        {
            // if cdr is null, define call has no arguments (error state)
            if (t.getCdr() == Nil.getInstance())
            {
                Console.Error.WriteLine("Special form 'Define' evaluated with no arguments");
                return Nil.getInstance();
            }

            // to evaluate an expression of the form '(eval x e)',
            // evaluate e, store its value as e1, then look up x in the current scope.
            // if a binding for x exists, set the value to e1
            // otherwise, add (x e1) as the first element of the first association
            // lists into env.

            t = t.getCdr();

            // determine if the form is '(eval x e)'
            if (t.getCar().isSymbol())
            {
                // evaluate e, then store into e1
                Node e1 = t.getCdr().eval(env);
                env.define(t.getCar(), e1);
            }

            // if the expression is of the form '(define (x p1 ... pn) b1 ... bm)',
            // construct the lambda expression
            // (lambda(p1...pn) b1...bm)
            // then proceed as for the definition
            // (define x(lambda(p1...pn) b1...bm))

            if (t.getCar().isPair())
            {
                Node arg1 = t.getCar().getCar();
                Cons arg2 = new Cons(new Ident("lambda"), new Cons(t.getCdr().getCar(), t.getCdr().getCar().getCdr()));
                env.define(arg1, arg2);
            }
            return null;
        }
    }
}


