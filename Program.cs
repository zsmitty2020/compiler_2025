namespace lab{

    public class TheCompiler{
        public static void Main(string[] args){

            //initialize our grammar
            Terminals.makeThem();
            
            Productions.makeThem();
            ProductionsExpr.makeThem();
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
            TreeNode root = null;
            string inp = File.ReadAllText(args[0]);
            var tokens = new List<Token>();
            var T = new Tokenizer(inp);
            root = Parser.parse(T);
            root.collectClassNames();
            root.setNodeTypes();
            root.print();


            //debug output: Write the tree in JSON format
            var opts = new System.Text.Json.JsonSerializerOptions();
            opts.IncludeFields=true;
            opts.WriteIndented=true;
            opts.MaxDepth=1000000;
            string J = System.Text.Json.JsonSerializer.Serialize(root,opts);
            using(var w = new StreamWriter("tree.json")){
                w.WriteLine(J);
            }
            return;

            }
            
        } //End class TheCompiler
        
    } //End namespace
