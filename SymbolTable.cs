using System.Reflection.Metadata.Ecma335;

namespace lab{
    
    public static class SymbolTable{

        static int numParameters=0;
        public static int numLocals=0;
        public static int nestingLevel=0;
        public static Stack< List<VarInfo> > shadowed = new();
        static Stack< HashSet<String> > locals = new();
        public static Dictionary<string, VarInfo> table = new();

        public static void enterFunctionScope(){ 
            numParameters = 0;
            numLocals=0;
            nestingLevel++;
            shadowed.Push(new());
            locals.Push( new() );
        }
        public static void leaveFunctionScope(){
            nestingLevel--;
            numLocals=0;        //bogus
            removeVariablesFromTableWithNestingLevelGreaterThanThreshold(nestingLevel);
            restoreShadowedVariables();
        }

        public static void enterLocalScope(){
            nestingLevel++;    
            shadowed.Push(new());
            locals.Push( new() );
        }
        public static void leaveLocalScope(){
            foreach( string name in locals.Peek() )
                table.Remove(name);
            locals.Pop();
            foreach( var vi in shadowed.Pop() )
                table[vi.token.lexeme] = vi;
        }
        /*
        public static void leaveLocalScope(){
            nestingLevel--;
            removeVariablesFromTableWithNestingLevelGreaterThanThreshold(nestingLevel);
            restoreShadowedVariables();
        }
        */
        static void removeVariablesFromTableWithNestingLevelGreaterThanThreshold(int v){
            //delete anything from table where 
            //table thing's nestinglevel > v
            List<string> badList = new();
            foreach(var t in table.Keys){
                if(table[t].nestingLevel > v)
                    badList.Add(t);
            }
            foreach(var e in badList){
                table.Remove(e);
            }
        }

        static void restoreShadowedVariables(){
            foreach(VarInfo vi in shadowed.Peek()){
                string varname = vi.token.lexeme;
                table[varname] = vi;
            }
            shadowed.Pop();
        }

        public static VarInfo lookup(Token id){
            //look in table
            //find thing
            //if not found, signal error
            //else return data    
            if( table.ContainsKey(id.lexeme) )
                return table[id.lexeme];
            else{
                Console.WriteLine($"No such lexeme {id.lexeme}");
                Environment.Exit(23);
            }
            return null;
        }
        public static VarInfo lookup(string id){
            if( table.ContainsKey(id) )
                return table[id];
            else{
                Console.WriteLine($"No such Id {id}");
                Environment.Exit(1);
            }
            return null;
        }

        public static void declareGlobal(Token token, NodeType type, Label lbl=null){
            if( lbl == null )
                lbl = new Label(token.lexeme);
            string varname = token.lexeme;
            if( table.ContainsKey(varname)){
                Utils.error(token, "Redeclaration of variable");
            }
            table[varname] = new VarInfo(token,
                nestingLevel, //always zero
                type, new GlobalLocation( lbl ));
        }
        public static void declareLocal(Token token, NodeType type){
            string varname = token.lexeme;
            if( table.ContainsKey(varname)){
                VarInfo vi = table[varname];
                if( vi.nestingLevel == nestingLevel ){
                    Utils.error(token, "Redeclaration of variable");
                } else if( vi.nestingLevel > nestingLevel ){
                    throw new Exception("ICE");
                } else {
                    //vi.nestingLevel must be < nestingLevel
                    shadowed.Peek().Add( table[varname] );
                }
            }
            table[varname] = new VarInfo(token, 
                    nestingLevel, 
                    type, 
                    new LocalLocation(numLocals, token.lexeme)
            );
            numLocals++;
        }
        public static void declareParameter(Token token, NodeType type){ 
            string varname = token.lexeme;
            if( table.ContainsKey(varname)){
                VarInfo vi = table[varname];
                if( vi.nestingLevel == locals.Count ){
                    Utils.error(token, "Redeclaration of variable");
                } else if( vi.nestingLevel > nestingLevel ){
                    throw new Exception("ICE");
                } else {
                    //vi.nestingLevel must be < nestingLevel
                    shadowed.Peek().Add( table[varname] );
                }
            }
            table[varname] = new VarInfo(token, 
                    nestingLevel, 
                    type, 
                    new ParameterLocation(numLocals, token.lexeme)
            );
            locals.Peek().Add(varname);
            numParameters++;
        }

        public static bool currentlyInGlobalScope(){
            if(nestingLevel == 0)
                return true;
            else
                return false;
        }

        public static void populateBuiltins(){
            SymbolTable.declareGlobal(
                new Token("ID", "putc", -1),
                new FunctionNodeType(NodeType.Int,
                    new List<NodeType>(){NodeType.Int}
                ),
                new Label("putc", "builtin function putc")
            );

            SymbolTable.declareGlobal(
                new Token("ID", "getc", -1),
                new FunctionNodeType(NodeType.Int,
                    new List<NodeType>(){}
                ),
                new Label("getc", "builtin function getc")
            );

            SymbolTable.declareGlobal(
                new Token("ID", "putv", -1),
                new FunctionNodeType(NodeType.Bool,
                    new List<NodeType>(){NodeType.Int, NodeType.Int}
                ),
                new Label("putv", "builtin function putv")
            );

            SymbolTable.declareGlobal(
                new Token("ID", "newline", -1),
                new FunctionNodeType(NodeType.Bool,
                    new List<NodeType>(){}
                ),
                new Label("newline", "builtin function newline")
            );

        }

    }
}