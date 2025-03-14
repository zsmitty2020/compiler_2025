
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
            new("IF",               @"\bif\b"),
            new("ELSE",             @"\belse\b"),
            new("TYPE",             @"\bint\b"),
            new("SEMI",             @";"),
            new("VOID",             @"\bvoid\b"),
            new("ID",               @"(?!\d)\w+" )
        });

        Grammar.defineProductions( new PSpec[] {
            new("S :: decls"),
            new("decls :: decl decls"),
            new("decl :: vardecl | funcdecl"),
            new("vardecl :: nonVoidType ID SEMI"),
            new("funcdecl :: anyType ID LP RP SEMI"),
            new("nonVoidType :: TYPE"),
            new("anyType :: VOID | TYPE")
        });
        
        
    }//end makeThem()
} //end class Productions

} //namespace
