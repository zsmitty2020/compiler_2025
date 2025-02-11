using System.ComponentModel.DataAnnotations;

namespace lab{

    public static class Grammar{
        public static List<Terminal> terminals = new();
        public static HashSet<string> allTerminals = [];
        public static List<Production> productions = new();
        public static HashSet<string> allNonterminals = new();

        /// <summary>
        /// DO THIS BEFORE DEFINE PRODUCTIONS
        /// </summary>
        /// <param name="terminals"></param>
        /// <exception cref="Exception"></exception>
        public static void defineTerminals( Terminal[] terminals){
            foreach(var t in terminals){
                if( isTerminal( t.sym ) )
                    throw new Exception("THERE IS A COPY OF THIS TERMINAL!");
                Grammar.terminals.Add(t);
                allTerminals.Add(t.sym);
            }
        }

        public static void addWhitespace(){
            Grammar.defineTerminals(new Terminal[] {new("WHITESPACE",       @"\s+" )});
        }

        public static bool isTerminal(string sym){
            return allTerminals.Contains(sym);
        }

        public static bool isNonterminal(string sym){
            return allNonterminals.Contains(sym);
        }

        /// <summary>
        /// DO THIS AFTER DEFINE TERMINALS
        /// </summary>
        /// <param name="terminals"></param>
        /// <exception cref="Exception"></exception>
        public static void defineProductions(PSpec[] specs){
            //parse stuff out of our pspec's and put it somewhere

            foreach( var psec in specs){
                //if( isNonterminal( psec.spec ) )
                //    throw new Exception("THERE IS A COPY OF THIS PRODUCTION!");
                var str = psec.spec;
                String[] strlist = str.Split("::", StringSplitOptions.RemoveEmptyEntries);
                /*for( var i = 0 ; i < strlist.Length; i++){
                    Console.WriteLine("PRINT");
                    Console.WriteLine(strlist[i]);
                }*/
                var lhs = strlist[0];
                var rhsString = strlist[1];
                strlist = rhsString.Split("|", StringSplitOptions.RemoveEmptyEntries);

                foreach(string item in strlist){
                    string rhs = item;
                    String[] stmts = rhs.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    Production p = new Production(psec, lhs, stmts);

                    if ( productions.Contains(p)) throw new Exception("THERE IS A COPY OF THIS PRODUCTION! Production: " + p);
                    else productions.Add(p);

                    foreach(string stmt in stmts) if(!isTerminal(stmt)) allNonterminals.Add(stmt);

                    //Console.WriteLine(p);

                }
                
            }
            
            /*
            Console.WriteLine("PRODUCTIONS");
            foreach(var prod in productions) Console.WriteLine(prod);
            */

        }

        public static void check(){
                //check for problems. panic if so.
                foreach( Production p in productions){
                    foreach( string sym in p.rhs){
                        if(!isTerminal(sym) && !isNonterminal(sym)){
                            throw new Exception("Undefined symbol: "+sym);
                        }
                    }
                }
            }

        public static void dump(){
                //dump grammar stuff to the screen (debugging)
                foreach( var p in productions ){
                    Console.WriteLine(p);
                }
            }

    }//End class Grammer
} //End namespace