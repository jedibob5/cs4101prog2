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

        public override Node eval(Node t, Environment env)
        {

            //set tmpNode to cdr of t
            Node tmpNode = t.getCdr();
            //go through items in tmpNode
            while(tmpNode.getCdr() != Nil.getInstance())
            {
                //define tmp variable for environment
                Node tmp1 = tmpNode.getCar().getCar();
                Node tmp2 = tmpNode.getCar().getCdr().getCar();
                //redefine environment based on tmp Nodes
                env.define(tmp1, tmp2);
                //progress tmpNode to next item
                tmpNode = tmpNode.getCdr();
            }
            //check status of node
            if (tmpNode.getCar().isSymbol())
            {
                return env.lookup(tmpNode.getCar());
            }
            else
            {
                return tmpNode.getCar().eval(env);
            }
        }
    }
}


