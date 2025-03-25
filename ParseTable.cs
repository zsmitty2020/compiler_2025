namespace lab{
public static class ParseTable{
    public static List<Dictionary<string,ParseAction> > table = new() {
        // DFA STATE 0
        new Dictionary<string,ParseAction>(){
                {"S" , new ParseAction(PAction.SHIFT, 1, null, -1)},
                {"decls" , new ParseAction(PAction.SHIFT, 2, null, -1)},
                {"funcdecl" , new ParseAction(PAction.SHIFT, 3, null, -1)},
                {"classdecl" , new ParseAction(PAction.SHIFT, 4, null, -1)},
                {"vardecl" , new ParseAction(PAction.SHIFT, 5, null, -1)},
                {"SEMI" , new ParseAction(PAction.SHIFT, 6, null, -1)},
                {"FUNC" , new ParseAction(PAction.SHIFT, 7, null, -1)},
                {"CLASS" , new ParseAction(PAction.SHIFT, 8, null, -1)},
                {"VAR" , new ParseAction(PAction.SHIFT, 9, null, -1)},
            // decls :: • ║ $ 
            {"$",new ParseAction(PAction.REDUCE, 0, "decls", 9)},
        },
        // DFA STATE 1
        new Dictionary<string,ParseAction>(){
            // S' :: S • ║ $ 
            {"$",new ParseAction(PAction.REDUCE, 1, "S'", 47)},
        },
        // DFA STATE 2
        new Dictionary<string,ParseAction>(){
            // S :: decls • ║ $ 
            {"$",new ParseAction(PAction.REDUCE, 1, "S", 28)},
        },
        // DFA STATE 3
        new Dictionary<string,ParseAction>(){
                {"decls" , new ParseAction(PAction.SHIFT, 52, null, -1)},
                {"funcdecl" , new ParseAction(PAction.SHIFT, 3, null, -1)},
                {"classdecl" , new ParseAction(PAction.SHIFT, 4, null, -1)},
                {"vardecl" , new ParseAction(PAction.SHIFT, 5, null, -1)},
                {"SEMI" , new ParseAction(PAction.SHIFT, 6, null, -1)},
                {"FUNC" , new ParseAction(PAction.SHIFT, 7, null, -1)},
                {"CLASS" , new ParseAction(PAction.SHIFT, 8, null, -1)},
                {"VAR" , new ParseAction(PAction.SHIFT, 9, null, -1)},
            // decls :: • ║ $ RBRACE 
            {"$",new ParseAction(PAction.REDUCE, 0, "decls", 9)},
            {"RBRACE",new ParseAction(PAction.REDUCE, 0, "decls", 9)},
        },
        // DFA STATE 4
        new Dictionary<string,ParseAction>(){
                {"decls" , new ParseAction(PAction.SHIFT, 51, null, -1)},
                {"funcdecl" , new ParseAction(PAction.SHIFT, 3, null, -1)},
                {"classdecl" , new ParseAction(PAction.SHIFT, 4, null, -1)},
                {"vardecl" , new ParseAction(PAction.SHIFT, 5, null, -1)},
                {"SEMI" , new ParseAction(PAction.SHIFT, 6, null, -1)},
                {"FUNC" , new ParseAction(PAction.SHIFT, 7, null, -1)},
                {"CLASS" , new ParseAction(PAction.SHIFT, 8, null, -1)},
                {"VAR" , new ParseAction(PAction.SHIFT, 9, null, -1)},
            // decls :: • ║ $ RBRACE 
            {"$",new ParseAction(PAction.REDUCE, 0, "decls", 9)},
            {"RBRACE",new ParseAction(PAction.REDUCE, 0, "decls", 9)},
        },
        // DFA STATE 5
        new Dictionary<string,ParseAction>(){
                {"decls" , new ParseAction(PAction.SHIFT, 50, null, -1)},
                {"funcdecl" , new ParseAction(PAction.SHIFT, 3, null, -1)},
                {"classdecl" , new ParseAction(PAction.SHIFT, 4, null, -1)},
                {"vardecl" , new ParseAction(PAction.SHIFT, 5, null, -1)},
                {"SEMI" , new ParseAction(PAction.SHIFT, 6, null, -1)},
                {"FUNC" , new ParseAction(PAction.SHIFT, 7, null, -1)},
                {"CLASS" , new ParseAction(PAction.SHIFT, 8, null, -1)},
                {"VAR" , new ParseAction(PAction.SHIFT, 9, null, -1)},
            // decls :: • ║ $ RBRACE 
            {"$",new ParseAction(PAction.REDUCE, 0, "decls", 9)},
            {"RBRACE",new ParseAction(PAction.REDUCE, 0, "decls", 9)},
        },
        // DFA STATE 6
        new Dictionary<string,ParseAction>(){
                {"decls" , new ParseAction(PAction.SHIFT, 49, null, -1)},
                {"funcdecl" , new ParseAction(PAction.SHIFT, 3, null, -1)},
                {"classdecl" , new ParseAction(PAction.SHIFT, 4, null, -1)},
                {"vardecl" , new ParseAction(PAction.SHIFT, 5, null, -1)},
                {"SEMI" , new ParseAction(PAction.SHIFT, 6, null, -1)},
                {"FUNC" , new ParseAction(PAction.SHIFT, 7, null, -1)},
                {"CLASS" , new ParseAction(PAction.SHIFT, 8, null, -1)},
                {"VAR" , new ParseAction(PAction.SHIFT, 9, null, -1)},
            // decls :: • ║ $ RBRACE 
            {"$",new ParseAction(PAction.REDUCE, 0, "decls", 9)},
            {"RBRACE",new ParseAction(PAction.REDUCE, 0, "decls", 9)},
        },
        // DFA STATE 7
        new Dictionary<string,ParseAction>(){
                {"ID" , new ParseAction(PAction.SHIFT, 31, null, -1)},
        },
        // DFA STATE 8
        new Dictionary<string,ParseAction>(){
                {"ID" , new ParseAction(PAction.SHIFT, 14, null, -1)},
        },
        // DFA STATE 9
        new Dictionary<string,ParseAction>(){
                {"ID" , new ParseAction(PAction.SHIFT, 10, null, -1)},
        },
        // DFA STATE 10
        new Dictionary<string,ParseAction>(){
                {"COLON" , new ParseAction(PAction.SHIFT, 11, null, -1)},
        },
        // DFA STATE 11
        new Dictionary<string,ParseAction>(){
                {"TYPE" , new ParseAction(PAction.SHIFT, 12, null, -1)},
        },
        // DFA STATE 12
        new Dictionary<string,ParseAction>(){
                {"SEMI" , new ParseAction(PAction.SHIFT, 13, null, -1)},
        },
        // DFA STATE 13
        new Dictionary<string,ParseAction>(){
            // vardecl :: VAR ID COLON TYPE SEMI • ║ CLASS SEMI FUNC VAR $ RBRACE 
            {"CLASS",new ParseAction(PAction.REDUCE, 5, "vardecl", 10)},
            {"SEMI",new ParseAction(PAction.REDUCE, 5, "vardecl", 10)},
            {"FUNC",new ParseAction(PAction.REDUCE, 5, "vardecl", 10)},
            {"VAR",new ParseAction(PAction.REDUCE, 5, "vardecl", 10)},
            {"$",new ParseAction(PAction.REDUCE, 5, "vardecl", 10)},
            {"RBRACE",new ParseAction(PAction.REDUCE, 5, "vardecl", 10)},
        },
        // DFA STATE 14
        new Dictionary<string,ParseAction>(){
                {"LBRACE" , new ParseAction(PAction.SHIFT, 15, null, -1)},
        },
        // DFA STATE 15
        new Dictionary<string,ParseAction>(){
                {"memberdecls" , new ParseAction(PAction.SHIFT, 16, null, -1)},
                {"SEMI" , new ParseAction(PAction.SHIFT, 17, null, -1)},
                {"membervardecl" , new ParseAction(PAction.SHIFT, 18, null, -1)},
                {"memberfuncdecl" , new ParseAction(PAction.SHIFT, 19, null, -1)},
                {"VAR" , new ParseAction(PAction.SHIFT, 20, null, -1)},
                {"funcdecl" , new ParseAction(PAction.SHIFT, 21, null, -1)},
                {"FUNC" , new ParseAction(PAction.SHIFT, 7, null, -1)},
            // memberdecls :: • ║ RBRACE 
            {"RBRACE",new ParseAction(PAction.REDUCE, 0, "memberdecls", 19)},
        },
        // DFA STATE 16
        new Dictionary<string,ParseAction>(){
                {"RBRACE" , new ParseAction(PAction.SHIFT, 29, null, -1)},
        },
        // DFA STATE 17
        new Dictionary<string,ParseAction>(){
                {"memberdecls" , new ParseAction(PAction.SHIFT, 28, null, -1)},
                {"SEMI" , new ParseAction(PAction.SHIFT, 17, null, -1)},
                {"membervardecl" , new ParseAction(PAction.SHIFT, 18, null, -1)},
                {"memberfuncdecl" , new ParseAction(PAction.SHIFT, 19, null, -1)},
                {"VAR" , new ParseAction(PAction.SHIFT, 20, null, -1)},
                {"funcdecl" , new ParseAction(PAction.SHIFT, 21, null, -1)},
                {"FUNC" , new ParseAction(PAction.SHIFT, 7, null, -1)},
            // memberdecls :: • ║ RBRACE 
            {"RBRACE",new ParseAction(PAction.REDUCE, 0, "memberdecls", 19)},
        },
        // DFA STATE 18
        new Dictionary<string,ParseAction>(){
                {"memberdecls" , new ParseAction(PAction.SHIFT, 27, null, -1)},
                {"SEMI" , new ParseAction(PAction.SHIFT, 17, null, -1)},
                {"membervardecl" , new ParseAction(PAction.SHIFT, 18, null, -1)},
                {"memberfuncdecl" , new ParseAction(PAction.SHIFT, 19, null, -1)},
                {"VAR" , new ParseAction(PAction.SHIFT, 20, null, -1)},
                {"funcdecl" , new ParseAction(PAction.SHIFT, 21, null, -1)},
                {"FUNC" , new ParseAction(PAction.SHIFT, 7, null, -1)},
            // memberdecls :: • ║ RBRACE 
            {"RBRACE",new ParseAction(PAction.REDUCE, 0, "memberdecls", 19)},
        },
        // DFA STATE 19
        new Dictionary<string,ParseAction>(){
                {"memberdecls" , new ParseAction(PAction.SHIFT, 26, null, -1)},
                {"SEMI" , new ParseAction(PAction.SHIFT, 17, null, -1)},
                {"membervardecl" , new ParseAction(PAction.SHIFT, 18, null, -1)},
                {"memberfuncdecl" , new ParseAction(PAction.SHIFT, 19, null, -1)},
                {"VAR" , new ParseAction(PAction.SHIFT, 20, null, -1)},
                {"funcdecl" , new ParseAction(PAction.SHIFT, 21, null, -1)},
                {"FUNC" , new ParseAction(PAction.SHIFT, 7, null, -1)},
            // memberdecls :: • ║ RBRACE 
            {"RBRACE",new ParseAction(PAction.REDUCE, 0, "memberdecls", 19)},
        },
        // DFA STATE 20
        new Dictionary<string,ParseAction>(){
                {"ID" , new ParseAction(PAction.SHIFT, 22, null, -1)},
        },
        // DFA STATE 21
        new Dictionary<string,ParseAction>(){
            // memberfuncdecl :: funcdecl • ║ SEMI VAR FUNC RBRACE 
            {"SEMI",new ParseAction(PAction.REDUCE, 1, "memberfuncdecl", 24)},
            {"VAR",new ParseAction(PAction.REDUCE, 1, "memberfuncdecl", 24)},
            {"FUNC",new ParseAction(PAction.REDUCE, 1, "memberfuncdecl", 24)},
            {"RBRACE",new ParseAction(PAction.REDUCE, 1, "memberfuncdecl", 24)},
        },
        // DFA STATE 22
        new Dictionary<string,ParseAction>(){
                {"COLON" , new ParseAction(PAction.SHIFT, 23, null, -1)},
        },
        // DFA STATE 23
        new Dictionary<string,ParseAction>(){
                {"TYPE" , new ParseAction(PAction.SHIFT, 24, null, -1)},
        },
        // DFA STATE 24
        new Dictionary<string,ParseAction>(){
                {"SEMI" , new ParseAction(PAction.SHIFT, 25, null, -1)},
        },
        // DFA STATE 25
        new Dictionary<string,ParseAction>(){
            // membervardecl :: VAR ID COLON TYPE SEMI • ║ SEMI VAR FUNC RBRACE 
            {"SEMI",new ParseAction(PAction.REDUCE, 5, "membervardecl", 23)},
            {"VAR",new ParseAction(PAction.REDUCE, 5, "membervardecl", 23)},
            {"FUNC",new ParseAction(PAction.REDUCE, 5, "membervardecl", 23)},
            {"RBRACE",new ParseAction(PAction.REDUCE, 5, "membervardecl", 23)},
        },
        // DFA STATE 26
        new Dictionary<string,ParseAction>(){
            // memberdecls :: memberfuncdecl memberdecls • ║ RBRACE 
            {"RBRACE",new ParseAction(PAction.REDUCE, 2, "memberdecls", 22)},
        },
        // DFA STATE 27
        new Dictionary<string,ParseAction>(){
            // memberdecls :: membervardecl memberdecls • ║ RBRACE 
            {"RBRACE",new ParseAction(PAction.REDUCE, 2, "memberdecls", 21)},
        },
        // DFA STATE 28
        new Dictionary<string,ParseAction>(){
            // memberdecls :: SEMI memberdecls • ║ RBRACE 
            {"RBRACE",new ParseAction(PAction.REDUCE, 2, "memberdecls", 20)},
        },
        // DFA STATE 29
        new Dictionary<string,ParseAction>(){
                {"SEMI" , new ParseAction(PAction.SHIFT, 30, null, -1)},
        },
        // DFA STATE 30
        new Dictionary<string,ParseAction>(){
            // classdecl :: CLASS ID LBRACE memberdecls RBRACE SEMI • ║ CLASS SEMI FUNC VAR $ RBRACE 
            {"CLASS",new ParseAction(PAction.REDUCE, 6, "classdecl", 0)},
            {"SEMI",new ParseAction(PAction.REDUCE, 6, "classdecl", 0)},
            {"FUNC",new ParseAction(PAction.REDUCE, 6, "classdecl", 0)},
            {"VAR",new ParseAction(PAction.REDUCE, 6, "classdecl", 0)},
            {"$",new ParseAction(PAction.REDUCE, 6, "classdecl", 0)},
            {"RBRACE",new ParseAction(PAction.REDUCE, 6, "classdecl", 0)},
        },
        // DFA STATE 31
        new Dictionary<string,ParseAction>(){
                {"LPAREN" , new ParseAction(PAction.SHIFT, 32, null, -1)},
        },
        // DFA STATE 32
        new Dictionary<string,ParseAction>(){
                {"optionalPdecls" , new ParseAction(PAction.SHIFT, 33, null, -1)},
                {"pdecls" , new ParseAction(PAction.SHIFT, 34, null, -1)},
                {"pdecl" , new ParseAction(PAction.SHIFT, 35, null, -1)},
                {"ID" , new ParseAction(PAction.SHIFT, 36, null, -1)},
            // optionalPdecls :: • ║ RPAREN 
            {"RPAREN",new ParseAction(PAction.REDUCE, 0, "optionalPdecls", 13)},
        },
        // DFA STATE 33
        new Dictionary<string,ParseAction>(){
                {"RPAREN" , new ParseAction(PAction.SHIFT, 41, null, -1)},
        },
        // DFA STATE 34
        new Dictionary<string,ParseAction>(){
            // optionalPdecls :: pdecls • ║ RPAREN 
            {"RPAREN",new ParseAction(PAction.REDUCE, 1, "optionalPdecls", 14)},
        },
        // DFA STATE 35
        new Dictionary<string,ParseAction>(){
                {"COMMA" , new ParseAction(PAction.SHIFT, 39, null, -1)},
            // pdecls :: pdecl • ║ RPAREN 
            {"RPAREN",new ParseAction(PAction.REDUCE, 1, "pdecls", 15)},
        },
        // DFA STATE 36
        new Dictionary<string,ParseAction>(){
                {"COLON" , new ParseAction(PAction.SHIFT, 37, null, -1)},
        },
        // DFA STATE 37
        new Dictionary<string,ParseAction>(){
                {"TYPE" , new ParseAction(PAction.SHIFT, 38, null, -1)},
        },
        // DFA STATE 38
        new Dictionary<string,ParseAction>(){
            // pdecl :: ID COLON TYPE • ║ RPAREN COMMA 
            {"RPAREN",new ParseAction(PAction.REDUCE, 3, "pdecl", 17)},
            {"COMMA",new ParseAction(PAction.REDUCE, 3, "pdecl", 17)},
        },
        // DFA STATE 39
        new Dictionary<string,ParseAction>(){
                {"pdecls" , new ParseAction(PAction.SHIFT, 40, null, -1)},
                {"pdecl" , new ParseAction(PAction.SHIFT, 35, null, -1)},
                {"ID" , new ParseAction(PAction.SHIFT, 36, null, -1)},
        },
        // DFA STATE 40
        new Dictionary<string,ParseAction>(){
            // pdecls :: pdecl COMMA pdecls • ║ RPAREN 
            {"RPAREN",new ParseAction(PAction.REDUCE, 3, "pdecls", 16)},
        },
        // DFA STATE 41
        new Dictionary<string,ParseAction>(){
                {"optionalReturn" , new ParseAction(PAction.SHIFT, 42, null, -1)},
                {"COLON" , new ParseAction(PAction.SHIFT, 43, null, -1)},
            // optionalReturn :: • ║ LBRACE 
            {"LBRACE",new ParseAction(PAction.REDUCE, 0, "optionalReturn", 11)},
        },
        // DFA STATE 42
        new Dictionary<string,ParseAction>(){
                {"LBRACE" , new ParseAction(PAction.SHIFT, 45, null, -1)},
        },
        // DFA STATE 43
        new Dictionary<string,ParseAction>(){
                {"TYPE" , new ParseAction(PAction.SHIFT, 44, null, -1)},
        },
        // DFA STATE 44
        new Dictionary<string,ParseAction>(){
            // optionalReturn :: COLON TYPE • ║ LBRACE 
            {"LBRACE",new ParseAction(PAction.REDUCE, 2, "optionalReturn", 12)},
        },
        // DFA STATE 45
        new Dictionary<string,ParseAction>(){
                {"decls" , new ParseAction(PAction.SHIFT, 46, null, -1)},
                {"funcdecl" , new ParseAction(PAction.SHIFT, 3, null, -1)},
                {"classdecl" , new ParseAction(PAction.SHIFT, 4, null, -1)},
                {"vardecl" , new ParseAction(PAction.SHIFT, 5, null, -1)},
                {"SEMI" , new ParseAction(PAction.SHIFT, 6, null, -1)},
                {"FUNC" , new ParseAction(PAction.SHIFT, 7, null, -1)},
                {"CLASS" , new ParseAction(PAction.SHIFT, 8, null, -1)},
                {"VAR" , new ParseAction(PAction.SHIFT, 9, null, -1)},
            // decls :: • ║ RBRACE 
            {"RBRACE",new ParseAction(PAction.REDUCE, 0, "decls", 9)},
        },
        // DFA STATE 46
        new Dictionary<string,ParseAction>(){
                {"RBRACE" , new ParseAction(PAction.SHIFT, 47, null, -1)},
        },
        // DFA STATE 47
        new Dictionary<string,ParseAction>(){
                {"SEMI" , new ParseAction(PAction.SHIFT, 48, null, -1)},
        },
        // DFA STATE 48
        new Dictionary<string,ParseAction>(){
            // funcdecl :: FUNC ID LPAREN optionalPdecls RPAREN optionalReturn LBRACE decls RBRACE SEMI • ║ CLASS SEMI FUNC VAR $ RBRACE 
            {"CLASS",new ParseAction(PAction.REDUCE, 10, "funcdecl", 18)},
            {"SEMI",new ParseAction(PAction.REDUCE, 10, "funcdecl", 18)},
            {"FUNC",new ParseAction(PAction.REDUCE, 10, "funcdecl", 18)},
            {"VAR",new ParseAction(PAction.REDUCE, 10, "funcdecl", 18)},
            {"$",new ParseAction(PAction.REDUCE, 10, "funcdecl", 18)},
            {"RBRACE",new ParseAction(PAction.REDUCE, 10, "funcdecl", 18)},
        },
        // DFA STATE 49
        new Dictionary<string,ParseAction>(){
            // decls :: SEMI decls • ║ $ RBRACE 
            {"$",new ParseAction(PAction.REDUCE, 2, "decls", 8)},
            {"RBRACE",new ParseAction(PAction.REDUCE, 2, "decls", 8)},
        },
        // DFA STATE 50
        new Dictionary<string,ParseAction>(){
            // decls :: vardecl decls • ║ $ RBRACE 
            {"$",new ParseAction(PAction.REDUCE, 2, "decls", 7)},
            {"RBRACE",new ParseAction(PAction.REDUCE, 2, "decls", 7)},
        },
        // DFA STATE 51
        new Dictionary<string,ParseAction>(){
            // decls :: classdecl decls • ║ $ RBRACE 
            {"$",new ParseAction(PAction.REDUCE, 2, "decls", 6)},
            {"RBRACE",new ParseAction(PAction.REDUCE, 2, "decls", 6)},
        },
        // DFA STATE 52
        new Dictionary<string,ParseAction>(){
            // decls :: funcdecl decls • ║ $ RBRACE 
            {"$",new ParseAction(PAction.REDUCE, 2, "decls", 5)},
            {"RBRACE",new ParseAction(PAction.REDUCE, 2, "decls", 5)},
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
