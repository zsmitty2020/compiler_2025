namespace lab{

    public class TheCompiler{
        public static void Main(string[] args){

            //initialize our grammar
            //Terminals.makeThem();
            
            Productions.makeThem();
            Grammar.addWhitespace();

    
            Grammar.check();
            Grammar.computeNullableAndFirst();
            //Grammar.dump();
            
            DFA.makeDFA();
            DFA.dump();

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