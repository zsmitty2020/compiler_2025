using System.ComponentModel.DataAnnotations;

namespace lab{

    public static class Grammar{
        public static List<Terminal> terminals = new();
        public static HashSet<string> allTerminals = [];
        public static List<Production> productions = new();
        public static HashSet<string> allNonterminals = new();
        public static HashSet<string> nullable = new();
        public static Dictionary<string,HashSet<string>> first = new();
        public static Dictionary<string, List<Production> > productionByLHS = new();    //New in DFA 2 lab


        /// <summary>
        /// DO THIS BEFORE DEFINE PRODUCTIONS
        /// </summary>
        /// <param name="terminals"></param>
        /// <exception cref="Exception"></exception>
        public static void defineTerminals( Terminal[] terminals){
            foreach(var t in terminals){
                if( isTerminal( t.sym ) )
                    throw new Exception($"THERE IS A COPY OF THIS TERMINAL! --> {t.sym}");
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

        //TEMP JHUDSON PRODUCTIONS
        public static int defineProductions(PSpec[] specs){
            //Return the index of the first production that we added
            int howMany = productions.Count;
            foreach( var pspec in specs){
                var idx = pspec.spec.IndexOf("::");
                if( idx == -1 )
                    throw new Exception("No :: in production spec");
                string lhs = pspec.spec.Substring(0,idx).Trim();
                Grammar.allNonterminals.Add(lhs);
                string rhss = pspec.spec.Substring(idx+2).Trim();
                string[] rhsl = rhss.Trim().Split("|",StringSplitOptions.RemoveEmptyEntries );
                foreach( string tmp in rhsl){
                    string[] rhs = tmp.Trim().Split(" ",StringSplitOptions.RemoveEmptyEntries);
                    if( rhs.Length == 1 && (rhs[0] == "lambda" || rhs[0] == "\u03bb" ) )
                        rhs = new string[0];
                    for(int i=0;i<rhs.Length;++i){
                        rhs[i]=rhs[i].Trim();
                    }

                    var tempProduction = new Production(pspec, lhs, rhs);
                    Grammar.productions.Add( tempProduction );
                    
                    //Added by me from my code
                    if( !productionByLHS.Keys.Contains(lhs) )
                        productionByLHS[lhs] = new();
                    productionByLHS[lhs].Add( tempProduction );
                }
            }
            return howMany;
        }

        /*
        /// <summary>
        /// DO THIS AFTER DEFINE TERMINALS
        /// </summary>
        /// <param name="terminals"></param>
        /// <exception cref="Exception"></exception>
        public static int defineProductions(PSpec[] specs){
            //Return the index of the first production we added
            int howMany = productions.Count;

            //parse stuff out of our pspec's and put it somewhere
            allNonterminals.Add("S");
            foreach( var psec in specs){
                //if( isNonterminal( psec.spec ) )
                //    throw new Exception("THERE IS A COPY OF THIS PRODUCTION!");
                var str = psec.spec;
                String[] strlist = str.Split("::", StringSplitOptions.RemoveEmptyEntries);

                var lhs = strlist[0].Replace('\n', ' ').Replace('\t', ' ').Trim();
                allNonterminals.Add(lhs);

                var rhsString = strlist[1].Replace('\n', ' ').Replace('\t', ' ').Trim();
                strlist = rhsString.Split("|", StringSplitOptions.RemoveEmptyEntries);

                foreach(string item in strlist){
                    string rhs = item;
                    String[] stmts = rhs.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    Production p = new Production(psec, lhs, stmts);

                    if ( productions.Contains(p)) throw new Exception("THERE IS A COPY OF THIS PRODUCTION! Production: " + p);
                    else productions.Add(p);

                    foreach(string stmt in stmts) if(!isTerminal(stmt) && !isNonterminal(stmt)) allNonterminals.Add(stmt);

                    //Console.WriteLine(p);

                    var newProd = new Production(psec, lhs, stmts);
                    Grammar.productions.Add( newProd );
                    if( !productionByLHS.Keys.Contains(lhs) )
                        productionByLHS[lhs] = new();
                    productionByLHS[lhs].Add( newProd );

                }
            }

            
            
            return howMany;
            
        }//End defineProductions Function */
        
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

            //Print Nullable
            Console.Write("NULLABLE: ");
            Console.WriteLine(String.Join<string>(", ", nullable));              

            //Print Firsts
            
            foreach(var sym in first.Keys){
                Console.Write($"first[{sym}] = ");
                Console.WriteLine(String.Join<string>(", ", first[sym]));
            }
        }

        public static void computeNullableAndFirst(){
            var flag = true;
            while(flag){
                flag = false;
                
                //Adds all explicitly nullable items to the nullableSet
                foreach(Production p in productions){
                    foreach(string s in p.rhs){
                        if( s == "lambda" && !nullable.Contains(p.lhs)){
                            nullable.Add(p.lhs);
                            flag = true;
                        }
                    }
                }
                
                //Adds all implicitly nullable items to the nullableSet
                foreach(Production p in productions){
                    var allNullable = true;
                    foreach(string s in p.rhs){
                        if( p.lhs == "S")
                            continue;
                            //Console.WriteLine($"THE RHS: {s}");
                        if( !nullable.Contains(s) && s != "lambda" && s != "\u03bb")
                            allNullable = false;
                    }

                    if(allNullable && !nullable.Contains(p.lhs)){
                        nullable.Add(p.lhs);
                        flag = true;
                    }
                }

            }//End while loop
            
            foreach( var sym in Grammar.allTerminals){
                if( sym != "WHITESPACE"){
                    first[sym] = new();
                    first[sym].Add(sym);
                }
            }
            foreach(var sym in Grammar.allNonterminals){
                first[sym] = new();
            }

            flag=true;
            while(flag){
                flag=false;
                
                foreach(var p in productions){
                    foreach(string s in p.rhs){

                        int sizeBefore = first[p.lhs].Count;
                        first[p.lhs].UnionWith(first[s]);
                        int sizeAfter = first[p.lhs].Count;
                        if(sizeBefore < sizeAfter)
                            flag = true;
                        if( !nullable.Contains(s))
                            break;
                    }
                }
            }//End while loop
        }



    }//End class Grammer
} //End namespace
