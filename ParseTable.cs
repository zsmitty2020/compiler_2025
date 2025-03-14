namespace lab{
public static class ParseTable{
    public static List<Dictionary<string,ParseAction> > table = new() {
        // DFA STATE 0
        new Dictionary<string,ParseAction>(){
                {"S" , new ParseAction(PAction.SHIFT, 1, null)},
                {"decls" , new ParseAction(PAction.SHIFT, 2, null)},
                {"decl" , new ParseAction(PAction.SHIFT, 3, null)},
                {"vardecl" , new ParseAction(PAction.SHIFT, 4, null)},
                {"funcdecl" , new ParseAction(PAction.SHIFT, 5, null)},
                {"nonVoidType" , new ParseAction(PAction.SHIFT, 6, null)},
                {"anyType" , new ParseAction(PAction.SHIFT, 7, null)},
                {"TYPE" , new ParseAction(PAction.SHIFT, 8, null)},
                {"VOID" , new ParseAction(PAction.SHIFT, 9, null)},
        },
        // DFA STATE 1
        new Dictionary<string,ParseAction>(){
            // S' :: S • ║ $ 
            {"$",new ParseAction(PAction.REDUCE, 1, "S'")},
        },
        // DFA STATE 2
        new Dictionary<string,ParseAction>(){
            // S :: decls • ║ $ 
            {"$",new ParseAction(PAction.REDUCE, 1, "S")},
        },
        // DFA STATE 3
        new Dictionary<string,ParseAction>(){
                {"decls" , new ParseAction(PAction.SHIFT, 16, null)},
                {"decl" , new ParseAction(PAction.SHIFT, 3, null)},
                {"vardecl" , new ParseAction(PAction.SHIFT, 4, null)},
                {"funcdecl" , new ParseAction(PAction.SHIFT, 5, null)},
                {"nonVoidType" , new ParseAction(PAction.SHIFT, 6, null)},
                {"anyType" , new ParseAction(PAction.SHIFT, 7, null)},
                {"TYPE" , new ParseAction(PAction.SHIFT, 8, null)},
                {"VOID" , new ParseAction(PAction.SHIFT, 9, null)},
        },
        // DFA STATE 4
        new Dictionary<string,ParseAction>(){
            // decl :: vardecl • ║ TYPE VOID 
            {"TYPE",new ParseAction(PAction.REDUCE, 1, "decl")},
            {"VOID",new ParseAction(PAction.REDUCE, 1, "decl")},
        },
        // DFA STATE 5
        new Dictionary<string,ParseAction>(){
            // decl :: funcdecl • ║ TYPE VOID 
            {"TYPE",new ParseAction(PAction.REDUCE, 1, "decl")},
            {"VOID",new ParseAction(PAction.REDUCE, 1, "decl")},
        },
        // DFA STATE 6
        new Dictionary<string,ParseAction>(){
                {"ID" , new ParseAction(PAction.SHIFT, 14, null)},
        },
        // DFA STATE 7
        new Dictionary<string,ParseAction>(){
                {"ID" , new ParseAction(PAction.SHIFT, 10, null)},
        },
        // DFA STATE 8
        new Dictionary<string,ParseAction>(){
            // nonVoidType :: TYPE • ║ ID 
            {"ID",new ParseAction(PAction.REDUCE, 1, "nonVoidType")},
            // anyType :: TYPE • ║ ID 
        },
        // DFA STATE 9
        new Dictionary<string,ParseAction>(){
            // anyType :: VOID • ║ ID 
            {"ID",new ParseAction(PAction.REDUCE, 1, "anyType")},
        },
        // DFA STATE 10
        new Dictionary<string,ParseAction>(){
                {"LP" , new ParseAction(PAction.SHIFT, 11, null)},
        },
        // DFA STATE 11
        new Dictionary<string,ParseAction>(){
                {"RP" , new ParseAction(PAction.SHIFT, 12, null)},
        },
        // DFA STATE 12
        new Dictionary<string,ParseAction>(){
                {"SEMI" , new ParseAction(PAction.SHIFT, 13, null)},
        },
        // DFA STATE 13
        new Dictionary<string,ParseAction>(){
            // funcdecl :: anyType ID LP RP SEMI • ║ TYPE VOID 
            {"TYPE",new ParseAction(PAction.REDUCE, 5, "funcdecl")},
            {"VOID",new ParseAction(PAction.REDUCE, 5, "funcdecl")},
        },
        // DFA STATE 14
        new Dictionary<string,ParseAction>(){
                {"SEMI" , new ParseAction(PAction.SHIFT, 15, null)},
        },
        // DFA STATE 15
        new Dictionary<string,ParseAction>(){
            // vardecl :: nonVoidType ID SEMI • ║ TYPE VOID 
            {"TYPE",new ParseAction(PAction.REDUCE, 3, "vardecl")},
            {"VOID",new ParseAction(PAction.REDUCE, 3, "vardecl")},
        },
        // DFA STATE 16
        new Dictionary<string,ParseAction>(){
            // decls :: decl decls • ║ $ 
            {"$",new ParseAction(PAction.REDUCE, 2, "decls")},
        }
    }; //close the table initializer
	public static List< Tuple<string, int> > badList = new List< Tuple<string, int> >(){};
    public static void dump(){
        int count = 0;
        foreach(var dict in table){
            Console.WriteLine($"Row {count}:");
            for( int i = 0; i < badList.Count(); i++){
                if( count == badList[i].Item2){
                    Console.WriteLine($"Shift-Reduce conflict in state {count} on symbol {badList[i].Item1}");
                }
            }
                if( count == 8 ){
                    Console.WriteLine("Reduce-Reduce conflict in state 8 on symbol ID");
                    Environment.Exit(99);
                }
            foreach(var key in dict.Keys){
                Console.WriteLine($"\t{key} {dict[key].action} {dict[key].num} {dict[key].sym}");
            }
            count++;
        }
    } //End dump
} //close the ParseTable class
} //close the namespace lab thing
