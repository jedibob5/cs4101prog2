// BuiltIn -- the data structure for built-in functions

// Class BuiltIn is used for representing the value of built-in functions
// such as +.  Populate the initial environment with
// (name, new BuiltIn(name)) pairs.

// The object-oriented style for implementing built-in functions would be
// to include the C# methods for implementing a Scheme built-in in the
// BuiltIn object.  This could be done by writing one subclass of class
// BuiltIn for each built-in function and implementing the method apply
// appropriately.  This requires a large number of classes, though.
// Another alternative is to program BuiltIn.apply() in a functional
// style by writing a large if-then-else chain that tests the name of
// the function symbol.

using Parse;
using System;

namespace Tree
{
    public class BuiltIn : Node
    {
        private Node symbol;            // the Ident for the built-in function

        public BuiltIn(Node s)		{ symbol = s; }

        public Node getSymbol()		{ return symbol; }

        // TODO: The method isProcedure() should be defined in
        // class Node to return false.
        public override bool isProcedure()	{ return true; }

        public override void print(int n)
        {
            // there got to be a more efficient way to print n spaces
            for (int i = 0; i < n; i++)
                Console.Write(' ');
            Console.Write("#{Built-in Procedure ");
            if (symbol != null)
                symbol.print(-Math.Abs(n));
            Console.Write('}');
            if (n >= 0)
                Console.WriteLine();
        }

        public override Node apply(Node args)
        {
            //set initial values
            string argsName = symbol.getName();
            int argsAmount = 0;

            //initialize
            Node firstArgs = null;
            bool b1 = false;
            Node secondArgs = null;
            bool b2 = false;

            //setup initial Args
            while (!args.isNull())
            {
                argsAmount = argsAmount + 1;
                if(argsAmount == 1)
                {
                    firstArgs = args.getCar();
                    b1 = true;
                }
                if(argsAmount == 2)
                {
                    secondArgs = args.getCar();
                    b2 = true;
                }
                args = args.getCdr();
            }

            //start apply based on amount or arguements and type of arguement
            //switch statement possibly
            if (argsAmount == 0)
            {
                // read
                if (argsName.Equals("read"))
                {
                    Scanner s = new Scanner(Console.In);
                    TreeBuilder tb = new TreeBuilder();
                    Parser p = new Parser(s,tb);
                    Node n = (Node) p.parseExp();
                    if(n != null)
                    {
                        return n;
                    }
                }
                //newline
                else if (argsName.Equals("newline"))
                {
                    Console.WriteLine();
                    //check value upspecified
                    return new StringLit(" ");
                }
                //i-e
                else if (argsName.Equals("interaction-environment"))
                {
                    return Scheme4101.env;
                }
                //invalid argsName given
                else
                {
                    Console.Error.WriteLine("invalid arguemtent name given");
                    return Nil.getInstance();
                }
            } else if(argsAmount == 1)
            {
                //symbol?
                if (argsName.Equals("symbol?"))
                {
                    bool b = firstArgs.isSymbol();
                    return BoolLit.getInstance(b);
                }
                //number?
                else if (argsName.Equals("number?"))
                {
                    bool b = firstArgs.isNumber();
                    return BoolLit.getInstance(b);
                }
                //car
                else if (argsName.Equals("car"))
                {
                    return firstArgs.getCar();
                }
                //cdr
                else if (argsName.Equals("cdr"))
                {
                    return firstArgs.getCdr();
                }
                //null?
                //b1?
                else if (argsName.Equals("null?") && b1)
                {
                    bool b = firstArgs.isNull();
                    return BoolLit.getInstance(b);
                }
                //pair
                else if (argsName.Equals("pair?"))
                {
                    bool b = firstArgs.isPair();
                    return BoolLit.getInstance(b);
                }
                //precedure
                else if (argsName.Equals("procedure?"))
                {
                    bool b = firstArgs.isProcedure();
                    return BoolLit.getInstance(b);
                }
                //write
                else if (argsName.Equals("write"))
                {
                    firstArgs.print(0);
                    //return value unspecified
                    return new StringLit(" ");
                }
                //display
                else if (argsName.Equals("display"))
                {
                    //temp change value
                    StringLit.quoted = false;
                    firstArgs.print(0);
                    //reset changed value
                    StringLit.quoted = true;
                    //return value unspecified
                    return new StringLit(" ");
                }
                else if (argsAmount == 2)
                {
                    //eq?
                    if (argsName.Equals("eq?"))
                    {
                        bool bt1 = firstArgs.isSymbol();
                        bool bt2 = secondArgs.isSymbol();
                        if(bt1 && bt2)
                        {
                            bool bt3 = firstArgs.getName().Equals(secondArgs.getName());
                            return BoolLit.getInstance(bt3);
                        }
                        else
                        {
                            bool bt4 = firstArgs == secondArgs;
                            return BoolLit.getInstance(bt4);
                        }
                    }
                    //cons
                    else if (argsName.Equals("cons"))
                    {
                        return new Cons(firstArgs, secondArgs);
                    }
                    //set-car!
                    else if (argsName.Equals("set-car!"))
                    {
                        firstArgs.setCar(secondArgs);
                        //return value unspecified
                        return new StringLit(" ");
                    }
                    //set-cdr!
                    else if (argsName.Equals("set-cdr!"))
                    {
                        firstArgs.setCdr(secondArgs);
                        //return value unspecified
                        return new StringLit(" ");
                    }
                    //eval
                    else if (argsName.Equals("eval"))
                    {
                        return firstArgs.eval((Environment)secondArgs);
                    }
                    //apply
                    else if (argsName.Equals("apply"))
                    {
                        return firstArgs.apply(secondArgs);
                    }

                    //binary arithematic opecations tmp vars
                    int bo1;
                    int bo2;
                    if (firstArgs.isNumber())
                    {
                        bo1 = firstArgs.getValue();
                    }
                    else if (secondArgs.isNumber())
                    {
                        bo2 = secondArgs.getValue();
                    }
                    else
                    {
                        Console.Error.WriteLine("invalid arguemtent name given");
                        return Nil.getInstance();
                    }
                    //b+
                    if (argsName.Equals("b+") && firstArgs.isNumber() && secondArgs.isNumber())
                    {
                        return new IntLit(bo1 + bo2);
                    }
                    //b-
                }
                //invalid argsName given
                else
                {
                    Console.Error.WriteLine("invalid arguemtent name given");
                    return Nil.getInstance();
                }
                
            }
            else
            {
                Console.Error.WriteLine("invalid amount of arguements");
                return Nil.getInstance();
            }
           
    	}
    }    
}

