
namespace lab{

public class Productions{
    public static void makeThem(){
        
        Grammar.defineTerminals( new Terminal[] {
            new("COMMENT",          @"//[^\n]*"),
            new("EQ",               @"="),
            new("LB",               @"\["),
            new("LP",               @"\("),
            new("MUL",              @"\*"),
            new("NUM",              @"\d+" ),
            new("PLUS",             @"\+"),
            new("RB",               @"\]"),
            new("RP",               @"\)"),
            new("ID",               @"(?!\d)\w+" )
        });

        Grammar.defineProductions( new PSpec[] {
            new("S :: ID LB sum RB EQ sum"),
            new("sum :: sum PLUS prod | prod"),
            new("prod :: prod MUL factor | factor"),
            new("factor :: NUM | LP sum RP")
        });
        
    }//end makeThem()
} //end class Productions

} //namespace
