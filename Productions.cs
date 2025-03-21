
namespace lab{

public class Productions{
    public static void makeThem(){
        
        Grammar.defineTerminals( new Terminal[] {
            new("COMMENT",          @"//[^\n]*"),
            new("EQ",               @"="),
            new("LPAREN",           @"\("),
            new("MUL",              @"\*"),
            new("NUM",              @"\d+" ),
            new("PLUS",             @"\+"),
            new("RPAREN",           @"\)"),
            new("SEMI",             @";"),
            new("ID",               @"(?!\d)\w+" )
        });

        Grammar.defineProductions( new PSpec[] {
            new("S :: sum SEMI"),
            new("sum :: sum PLUS prod | prod"),
            new("prod :: prod MUL factor | factor"),
            new("factor :: NUM | LPAREN sum RPAREN")
        });
        
        
    }//end makeThem()
} //end class Productions

} //namespace
