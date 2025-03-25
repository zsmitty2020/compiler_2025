
namespace lab{
    public class Productions{
        public static void makeThem(){
            Grammar.defineProductions( new PSpec[] {
                //declaring a class
                new("classdecl :: CLASS ID LBRACE memberdecls RBRACE SEMI",
                    collectClassNames: (n) => {
                        Console.WriteLine("CLASS:"+n.children[1].token.lexeme);
                    }
                ),
                    //simple, one production
                        new PSpec( "assign :: ID EQ NUM SEMI"),

                        //several prod's with common lhs
                        new( @" decl :: VAR ID COLON TYPE SEMI
                                    |  VAR ID COLON TYPE EQ NUM SEMI
                                    |  VAR ID EQ NUM SEMI
                        "),

                        new( "decls :: funcdecl decls | classdecl decls | vardecl decls | SEMI decls | lambda"),
                        new( "vardecl :: VAR ID COLON TYPE SEMI"),


                        new(@"optionalReturn :: lambda | COLON TYPE"),
                        new(@"optionalPdecls :: lambda | pdecls"),
                        new(@"pdecls :: pdecl | pdecl COMMA pdecls"),
                        new(@"pdecl :: ID COLON TYPE"),
                    new(@"funcdecl :: FUNC ID LPAREN optionalPdecls RPAREN optionalReturn LBRACE decls RBRACE SEMI",
                        collectClassNames: (n) => {
                            if(n.parent.sym == "memberfuncdecl"){}
                            else{
                                Console.WriteLine("FUNC:"+n.children[1].token.lexeme);
                            }
                        }
                    ),
                        //new(@"memberdecls :: SEMI"),
                        new(@"memberdecls :: lambda | SEMI memberdecls | membervardecl memberdecls | memberfuncdecl memberdecls"),
                        new(@"membervardecl :: VAR ID COLON TYPE SEMI"),
                        new(@"memberfuncdecl :: funcdecl"),
                        new(@"loop :: WHILE LPAREN expr RPAREN braceblock
                                    | REPEAT braceblock UNTIL LPAREN expr RPAREN"),

                        //NEW WRITTEN BY MEEE
                        new( "param :: ID COLON TYPE"),

                        new( "S :: decls"),

                        //several prod's, same lhs, all on one line
                        new("stmt :: assign| loop |cond"),

                        //production split over two lines
                        new( @"cond :: IF expr braceblock
                                    | IF expr braceblock
                                    ELSE braceblock
                        "),

                        new( "braceblock :: LBRACE stmts RBRACE" ),

                        //lambda production
                        new( " stmts::stmt SEMI stmts|lambda"),

                        //nice plain vanilla productions
                        new("expr :: NUM | ID | NUM ADDOP NUM"),

                        //utf-8
                        new( "optionalType :: λ | TYPE"),

                        new( "func-call :: ID LPAREN plist RPAREN "),
                        new( "plist :: param plist' | λ "),
                        new( "plist' :: lambda | COMMA param plist'")
                    });
        }//end makeThem()
    } //end class Productions
} //namespace
