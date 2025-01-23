
namespace lab{

public static class AllTerminals{
    public static void makeAllOfTheTerminals(){
        Grammar.addTerminals( new Terminal[] {
            new("EQ",               @"="),
            new("NUM",              @"\d+" ),
            
            //new("COMMENT", @"(?sx)              # '\x' = verbose mode. it let's you split the regex between multiple lines, all white spaces are ignored
            //        //[^\n]*                        # 's' let's the '.' include the \n char,
            //        |                               # '$' is a meta character that signifies end of file
            //        /[*] .*? [*]/
            //    "),                                 // '#' let's you comment in the regex system
            
            new("STRINGCONSTANT",   @"(?x)  "" (\\[nr""]  | [^\\""] )*  ""  "),
            new("LPAREN",           @"\("),
            new("RPAREN",           @"\)"),
            new("LBRACE",           @"\{"),
            new("RBRACE",           @"\}"),
            //new("COLON",            @":"),
            //new("SEMI",           @";"),           //No semicolons in Tokenization 1 :(
            new("IF",               @"\bif\b"),
            new("ELSE",             @"\belse\b"),
            new("RELOP",            @">=|<=|>|<|==|!="),
            new("AND",              @"\band\b"),
            new("ID",               @"(?!\d)\w+" )
        });
    }


    }// End class AllTerminals
}// End namespace
