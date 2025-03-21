namespace lab{
public static class ParseTable{
    public static List<Dictionary<string,ParseAction> > table = new() {
        // DFA STATE 0
        new Dictionary<string,ParseAction>(){
                {"S" , new ParseAction(PAction.SHIFT, 1, null, -1)},
                {"sum" , new ParseAction(PAction.SHIFT, 2, null, -1)},
                {"prod" , new ParseAction(PAction.SHIFT, 3, null, -1)},
                {"factor" , new ParseAction(PAction.SHIFT, 4, null, -1)},
                {"NUM" , new ParseAction(PAction.SHIFT, 5, null, -1)},
                {"LPAREN" , new ParseAction(PAction.SHIFT, 6, null, -1)},
        },
        // DFA STATE 1
        new Dictionary<string,ParseAction>(){
            // S' :: S • ║ $ 
            {"$",new ParseAction(PAction.REDUCE, 1, "S'", 7)},
        },
        // DFA STATE 2
        new Dictionary<string,ParseAction>(){
                {"SEMI" , new ParseAction(PAction.SHIFT, 13, null, -1)},
                {"PLUS" , new ParseAction(PAction.SHIFT, 9, null, -1)},
        },
        // DFA STATE 3
        new Dictionary<string,ParseAction>(){
                {"MUL" , new ParseAction(PAction.SHIFT, 11, null, -1)},
            // sum :: prod • ║ SEMI PLUS RPAREN 
            {"SEMI",new ParseAction(PAction.REDUCE, 1, "sum", 2)},
            {"PLUS",new ParseAction(PAction.REDUCE, 1, "sum", 2)},
            {"RPAREN",new ParseAction(PAction.REDUCE, 1, "sum", 2)},
        },
        // DFA STATE 4
        new Dictionary<string,ParseAction>(){
            // prod :: factor • ║ SEMI PLUS MUL RPAREN 
            {"SEMI",new ParseAction(PAction.REDUCE, 1, "prod", 4)},
            {"PLUS",new ParseAction(PAction.REDUCE, 1, "prod", 4)},
            {"MUL",new ParseAction(PAction.REDUCE, 1, "prod", 4)},
            {"RPAREN",new ParseAction(PAction.REDUCE, 1, "prod", 4)},
        },
        // DFA STATE 5
        new Dictionary<string,ParseAction>(){
            // factor :: NUM • ║ SEMI PLUS MUL RPAREN 
            {"SEMI",new ParseAction(PAction.REDUCE, 1, "factor", 5)},
            {"PLUS",new ParseAction(PAction.REDUCE, 1, "factor", 5)},
            {"MUL",new ParseAction(PAction.REDUCE, 1, "factor", 5)},
            {"RPAREN",new ParseAction(PAction.REDUCE, 1, "factor", 5)},
        },
        // DFA STATE 6
        new Dictionary<string,ParseAction>(){
                {"sum" , new ParseAction(PAction.SHIFT, 7, null, -1)},
                {"prod" , new ParseAction(PAction.SHIFT, 3, null, -1)},
                {"factor" , new ParseAction(PAction.SHIFT, 4, null, -1)},
                {"NUM" , new ParseAction(PAction.SHIFT, 5, null, -1)},
                {"LPAREN" , new ParseAction(PAction.SHIFT, 6, null, -1)},
        },
        // DFA STATE 7
        new Dictionary<string,ParseAction>(){
                {"RPAREN" , new ParseAction(PAction.SHIFT, 8, null, -1)},
                {"PLUS" , new ParseAction(PAction.SHIFT, 9, null, -1)},
        },
        // DFA STATE 8
        new Dictionary<string,ParseAction>(){
            // factor :: LPAREN sum RPAREN • ║ SEMI PLUS MUL RPAREN 
            {"SEMI",new ParseAction(PAction.REDUCE, 3, "factor", 6)},
            {"PLUS",new ParseAction(PAction.REDUCE, 3, "factor", 6)},
            {"MUL",new ParseAction(PAction.REDUCE, 3, "factor", 6)},
            {"RPAREN",new ParseAction(PAction.REDUCE, 3, "factor", 6)},
        },
        // DFA STATE 9
        new Dictionary<string,ParseAction>(){
                {"prod" , new ParseAction(PAction.SHIFT, 10, null, -1)},
                {"factor" , new ParseAction(PAction.SHIFT, 4, null, -1)},
                {"NUM" , new ParseAction(PAction.SHIFT, 5, null, -1)},
                {"LPAREN" , new ParseAction(PAction.SHIFT, 6, null, -1)},
        },
        // DFA STATE 10
        new Dictionary<string,ParseAction>(){
                {"MUL" , new ParseAction(PAction.SHIFT, 11, null, -1)},
            // sum :: sum PLUS prod • ║ SEMI RPAREN PLUS 
            {"SEMI",new ParseAction(PAction.REDUCE, 3, "sum", 1)},
            {"RPAREN",new ParseAction(PAction.REDUCE, 3, "sum", 1)},
            {"PLUS",new ParseAction(PAction.REDUCE, 3, "sum", 1)},
        },
        // DFA STATE 11
        new Dictionary<string,ParseAction>(){
                {"factor" , new ParseAction(PAction.SHIFT, 12, null, -1)},
                {"NUM" , new ParseAction(PAction.SHIFT, 5, null, -1)},
                {"LPAREN" , new ParseAction(PAction.SHIFT, 6, null, -1)},
        },
        // DFA STATE 12
        new Dictionary<string,ParseAction>(){
            // prod :: prod MUL factor • ║ SEMI PLUS RPAREN MUL 
            {"SEMI",new ParseAction(PAction.REDUCE, 3, "prod", 3)},
            {"PLUS",new ParseAction(PAction.REDUCE, 3, "prod", 3)},
            {"RPAREN",new ParseAction(PAction.REDUCE, 3, "prod", 3)},
            {"MUL",new ParseAction(PAction.REDUCE, 3, "prod", 3)},
        },
        // DFA STATE 13
        new Dictionary<string,ParseAction>(){
            // S :: sum SEMI • ║ $ 
            {"$",new ParseAction(PAction.REDUCE, 2, "S", 0)},
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
            foreach(var key in dict.Keys){
                Console.WriteLine($"\t{key} {dict[key].action} {dict[key].num} {dict[key].sym}");
            }
            count++;
        }
    } //End dump
} //close the ParseTable class
} //close the namespace lab thing
