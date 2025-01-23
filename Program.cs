namespace lab{

    public class TheCompiler{
        public static void Main(string[] args){

            //initialize our grammar
            AllTerminals.makeAllOfTheTerminals();
            Grammar.addWhitespace();

            bool verbose = falsee;

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
                
                foreach(var t in tokens){
                    Console.WriteLine(t);
                }
            }
            
            }

    } //End class TheCompiler

} //End namespace