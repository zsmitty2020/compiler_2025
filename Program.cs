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
                //ParseTable.dump();
                return;
            }

            TreeNode root = null;
            string inp = File.ReadAllText(args[0]);
            var tokens = new List<Token>();
            var T = new Tokenizer(inp);
            root = Parser.parse(T);
            root.print();

            while(true){
                Token tok = T.next();
                
                if( tok.sym == "$" )
                    break;
                tokens.Add(tok);
            }
            
            using(var w = new StreamWriter("tree.json"))
                root.toJson(w);
            //build parse tree
            
            foreach(var t in tokens){
                Console.WriteLine(t);
            }
            
            return;

            }
            
        } //End class TheCompiler
        
    } //End namespace
