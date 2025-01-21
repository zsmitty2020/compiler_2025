
namespace lab{

public static class AllTerminals{
    public static void makeAllOfTheTerminals(){
        Grammar.addTerminals( new Terminal[] {
            new("WHITESPACE",       @"\s+" ),
            new("EQ",               @"="),
            new("NUM",              @"\d+" ),
            new("COMMENT",          @"//[^\n]*" ),
            new("STRCONST",   @"(?x)  "" (\\[nr""]  | [^\\] )*  ""  "),
            new("LPAREN",           @"\("),
            new("RPAREN",           @"\)"),
            new("LBRACE",           @"\{"),
            new("RBRACE",           @"\}"),
            new("SEMI",             @";"),
            new("IF",               @"\bif\b"),
            new("ELSE",             @"\belse\b"),
            new("RELOP",            @">=|<=|>|<|==|!="),
            new("AND",              @"\band\b"),
            new("ID",               @"(?!\d)\w+" )
        });
    }


    }// End class AllTerminals
}// End namespace
