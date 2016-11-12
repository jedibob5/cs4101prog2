// Set -- Parse tree node strategy for printing the special form set!

using System;

namespace Tree
{
    public class Set : Special
    {
	public Set() { }
	
        public override void print(Node t, int n, bool p)
        {
            Printer.printSet(t, n, p);
        }

        public override Node eval(Node t, Environment env)
        {
            //get tmp environment arguements
            Node tmp1 = t.getCdr().getCar();
            Node tmp2 = t.getCdr().getCdr().getCar();
            //redefine environment
            env.define(tmp1, tmp2);
            //return empty string object
            return new StringLit("");
        }
    }
}

