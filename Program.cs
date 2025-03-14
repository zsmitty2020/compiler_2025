namespace lab{

    public class TheCompiler{
        public static void Main(string[] args){

            //initialize our grammar
            //Terminals.makeThem();
            
            Productions.makeThem();
            Grammar.addWhitespace();

    
            Grammar.check();

            if( args.Length == 1 && args[0] == "-g" ){
                Grammar.computeNullableAndFirst();
                //Grammar.dump();
                DFA.makeDFA(); //time consuming
                //DFA.dump();
                TableWriter.create();
                ParseTable.dump();
                return;
            }
            return;
            string inp = File.ReadAllText(args[0]);
                var tokens = new List<Token>();
                var T = new Tokenizer(inp);
                while(true){
                    Token tok = T.next();
                    if( tok == null )
                        break;
                    tokens.Add(tok);
                }
            

                //build parse tree

                foreach(var t in tokens){
                    Console.WriteLine(t);
                }
            
            return;

            }
            
        } //End class TheCompiler

    } //End namespace