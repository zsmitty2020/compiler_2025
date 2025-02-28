namespace lab{

    public class TheCompiler{
        public static void Main(string[] args){

            //initialize our grammar
            //Terminals.makeThem();
            
            //These terminals and productions are from the "most complexist" production list
            Grammar.defineTerminals( new Terminal[] {
                new("COMMENT",          @"//[^\n]*"),
                new("EQ",               @"="),
                new("LBRACE",           @"[{]"),
                new("LPAREN",           @"\("),
                new("NUM",              @"\d+" ),
                new("RBRACE",           @"\}"),
                new("RPAREN",           @"\)"),
                new("SEMI",             @";"),
                new("IF",               @"\bif\b"),
                new("ELSE",             @"\belse\b"),
                new("WHILE",            @"\bwhile\b"),
                new("ID",               @"(?!\d)\w+" )
            });

            Grammar.defineProductions( new PSpec[] {
                new( "S :: braceblock | lambda" ),
                new( "braceblock :: LBRACE stmts RBRACE"),
                new( "stmts :: stmt stmts | lambda" ),
                new( "stmt :: assign | func | cond | loop"),
                new( "assign :: ID EQ NUM SEMI"),
                new( "func :: ID LPAREN RPAREN SEMI" ),
                new( "cond :: IF LPAREN NUM RPAREN braceblock"),
                new( "cond :: IF LPAREN NUM RPAREN braceblock ELSE braceblock"),
                new( "loop :: WHILE LPAREN NUM RPAREN braceblock" )
            });

            //Productions.makeThem();
            Grammar.addWhitespace();

    
            Grammar.check();
            Grammar.computeNullableAndFirst();
            Grammar.dump();
            
            DFA.makeDFA();
            DFA.dump("DFAasDOT.txt");

            return;

            bool verbose = false;

            foreach(var item in args){
                
                if( verbose ){
                    Console.WriteLine("*******************************");
                    Console.WriteLine("OUTPUT FOR FILE: " + item);  //Writes what file we are outputting
                    Console.WriteLine("*******************************");
                }
                 
                

                string inp = File.ReadAllText(item);
                var tokens = new List<Token>();
                var T = new Tokenizer(inp);
                while(true){
                    Token tok = T.next();
                    if( tok == null )
                        break;
                    tokens.Add(tok);
                }
                
                Console.WriteLine("[");
                foreach(var t in tokens){
                    Console.WriteLine(t);
                    if( tokens.IndexOf(t) != tokens.Count - 1)
                        Console.WriteLine(",");
                }
                Console.WriteLine("]");
            }
            
            }

    } //End class TheCompiler

} //End namespace