namespace lab{

public class ProductionsExpr{
    public static void makeThem(){

        Grammar.defineProductions( new PSpec[] {

            //convenience: Starts the whole expression hierarchy
            new("expr :: orexp",
                setNodeTypes: (n) => {
                    foreach(var c in n.children){
                        c.setNodeTypes();
                    }
                    n.nodeType = n["orexp"].nodeType;
                }
            ),

            //boolean OR
            new("orexp :: orexp OROP andexp",
                setNodeTypes: (n) => {
                    Utils.typeCheck(n, NodeType.Bool, NodeType.Bool);
                }
            ),
            new("orexp :: andexp"),

            //boolean AND
            new("andexp :: andexp ANDOP relexp",
                setNodeTypes: (n) => {
                    Utils.typeCheck(n,NodeType.Bool, NodeType.Bool);
                }
            ),
            new("andexp :: relexp"),

            //relational: x>y
            new("relexp :: bitexp RELOP bitexp",
                setNodeTypes: (n) => {
                    Utils.typeCheck(n,NodeType.Bool, NodeType.Int, NodeType.Float, NodeType.String);
                }
            ),
            new("relexp :: bitexp"),

            //bitwise: or, and, xor
            new("bitexp :: bitexp BITOP shiftexp",
                setNodeTypes: (n) => {
                    Utils.typeCheck(n,NodeType.Int, NodeType.Int);
                },
                generateCode: (n) => {
                    n["bitexp"].generateCode();
                    n["shiftexp"].generateCode();
    
                    Asm.add(new OpPop(Register.rcx,null));
                    Asm.add(new OpPop(Register.rax,null));
                    if( n["BITOP"].token.lexeme == "&" ){
                        Asm.add(new OpAnd(Register.rax, Register.rcx));
                    }
                    if( n["BITOP"].token.lexeme == "|" ){
                        Asm.add(new OpOr(Register.rax, Register.rcx));
                    }
                    if( n["BITOP"].token.lexeme == "^" ){
                        Asm.add(new OpXor(Register.rax, Register.rcx));
                    }
                    Asm.add( new OpPush( Register.rax, StorageClass.PRIMITIVE));
                }
            ),
            new("bitexp :: shiftexp"),

            new("shiftexp :: shiftexp SHIFTOP sumexp",
                setNodeTypes: (n) => {
                    Utils.typeCheck(n,NodeType.Int, NodeType.Int);
                },
                generateCode: (n) => {
                    //ex: 4 << 2
                    //ex: 4
                    n["shiftexp"].generateCode();
                    //ex: 2
                    n["sumexp"].generateCode();
    
                    Asm.add(new OpPop(Register.rcx,null));      //ex: 2
                    Asm.add(new OpPop(Register.rax,null));      //ex: 4
                    if( n["SHIFTOP"].token.lexeme == "<<" ){
                        Asm.add(new OpShl(Register.rax, Register.rcx));
                    }
                    if( n["SHIFTOP"].token.lexeme == ">>" ){
                        Asm.add(new OpShr(Register.rax, Register.rcx));
                    }
                    Asm.add( new OpPush( Register.rax, StorageClass.PRIMITIVE));
                }
            ),
            new("shiftexp :: sumexp"),

            //addition and subtraction
            new("sumexp :: sumexp ADDOP prodexp",
                setNodeTypes: (n) => {
                    
                    foreach(var c in n.children){
                        c.setNodeTypes();
                    }
                    var t1 = n["sumexp"].nodeType;
                    var t2 = n["prodexp"].nodeType;
                    var addop = n["ADDOP"].token;
                    if( t1 != t2 )
                        Utils.error(addop,$"Type mismatch for add/subtract ({t1} and {t2})");

                    if( t1 != NodeType.Int && t1 != NodeType.Float && t1 != NodeType.String ){
                        n.print();
                        Utils.error(addop,$"Bad type for add/subtract ({t1})");
                    }

                    if( t1 == NodeType.String && n["ADDOP"].token.lexeme != "+" )
                        Utils.error(addop,"Cannot subtract strings");

                    n.nodeType = t1;
                },
                generateCode: (n) => {
                    if( n.nodeType == NodeType.Int ){
                        // 2 + 1 or 2 - 1
                        n["sumexp"].generateCode(); // 1
                        n["prodexp"].generateCode();// 2

                        Asm.add(new OpPop( Register.rbx, null));
                        Asm.add(new OpPop( Register.rax, null));
                        
                        //Addition
                        if( n["ADDOP"].token.lexeme == "+"){
                            Asm.add(new OpAdd( Register.rax, Register.rbx));
                        }

                        //Subtraction
                        if( n["ADDOP"].token.lexeme == "-"){
                            Asm.add(new OpSub( Register.rax, Register.rbx));
                        }
                        Asm.add(new OpPush( Register.rax, StorageClass.PRIMITIVE ));

                        if( n["ADDOP"].token.lexeme != "+" && n["ADDOP"].token.lexeme != "-"){
                            throw new Exception("ADDOP NOT + OR -!");
                        }

                    }
                    else{
                        n["sumexp"].generateCode(); // 1
                        n["prodexp"].generateCode();// 2

                        Asm.add(new OpPopF( Register.xmm1, null));
                        Asm.add(new OpPopF( Register.xmm0, null));
                        
                        //Addition
                        if( n["ADDOP"].token.lexeme == "+"){
                            Asm.add(new OpAddF( Register.xmm0, Register.xmm1));
                        }

                        //Subtraction
                        if( n["ADDOP"].token.lexeme == "-"){
                            Asm.add(new OpSubF( Register.xmm0, Register.xmm1));
                        }
                        Asm.add(new OpPushF( Register.xmm0, StorageClass.PRIMITIVE ));
                    }
                    
                }
            ),
            new("sumexp :: prodexp"),

            //multiplication, division, modulo
            new("prodexp :: prodexp MULOP powexp",
                setNodeTypes: (n) => {
                    Utils.typeCheck(n,null, NodeType.Int, NodeType.Float);
                },
                generateCode: (n) => {
                    if( n.nodeType == NodeType.Int ){
                        n["prodexp"].generateCode();
                        n["powexp"].generateCode();
                        Asm.add(new OpPop( Register.rbx, null));
                        Asm.add(new OpPop( Register.rax, null));
                        if( n["MULOP"].token.lexeme == "*"){
                            Asm.add(new OpMul( Register.rax, Register.rbx));
                            Asm.add(new OpPush(Register.rax, StorageClass.PRIMITIVE));
                        }
                        if( n["MULOP"].token.lexeme == "/"){
                            Asm.add(new OpDiv( Register.rax, Register.rbx));
                            //push remainder to stack
                            Asm.add(new OpPush( Register.rax, StorageClass.PRIMITIVE ));
                        }
                        if( n["MULOP"].token.lexeme == "%"){
                            Asm.add(new OpDiv( Register.rax, Register.rbx));
                            //push remainder to stack
                            Asm.add(new OpPush( Register.rdx, StorageClass.PRIMITIVE ));
                        }
                        
                        
                    } else if( n.nodeType == NodeType.Float ){
                        n["prodexp"].generateCode();
                        n["powexp"].generateCode();
                        Asm.add(new OpPopF( Register.xmm1, null));
                        Asm.add(new OpPopF( Register.xmm0, null));
                        if( n["MULOP"].token.lexeme == "*"){
                            Asm.add(new OpMulF( Register.xmm0, Register.xmm1));
                            Asm.add(new OpPushF( Register.xmm0, StorageClass.PRIMITIVE));
                        }
                        if( n["MULOP"].token.lexeme == "/"){
                            Asm.add(new OpDivF( Register.xmm0, Register.xmm1));
                            Asm.add(new OpPushF( Register.xmm0, StorageClass.PRIMITIVE ));
                        }
                    }
                
                }
            ),
            new("prodexp :: powexp"),

            //exponentiation
            new("powexp :: unaryexp POWOP powexp"),
            new("powexp :: unaryexp"),

            //bitwise not, negation, unary plus
            new("unaryexp :: BITNOTOP unaryexp",
                setNodeTypes: (n) => {
                    foreach(var c in n.children){
                        c.setNodeTypes();
                    }
                    var t1 = n["unaryexp"].nodeType;
                    var bitnotop = n["BITNOTOP"].token;
                    if(t1 != NodeType.Int){
                        Utils.error(bitnotop,$"Type must be an Int! ({t1})");
                    }
                    n.nodeType = t1;
                },
                generateCode: (n) => {
                    n["unaryexp"].generateCode();
                    Asm.add(new OpPop(Register.rax,null));
                    Asm.add(new OpNot(Register.rax));
                    Asm.add(new OpPush(Register.rax,StorageClass.PRIMITIVE));
                }
            ),
            new("unaryexp :: ADDOP unaryexp",
                setNodeTypes: (n) => {
                    foreach(var c in n.children){
                        c.setNodeTypes();
                    }
                    var t1 = n["unaryexp"].nodeType;
                    var addop = n["ADDOP"].token;
                    if(t1 != NodeType.Int && t1 != NodeType.Float){
                        Utils.error(addop,$"Type must be an Int or Float! ({t1})");
                    }
                    n.nodeType = t1;
                },
                generateCode: (n) => {
                    if( n.nodeType == NodeType.Int){
                        if( n["ADDOP"].token.lexeme == "+" ){

                            n["unaryexp"].generateCode();
                        }
                        if( n["ADDOP"].token.lexeme == "-" ){
                            n["unaryexp"].generateCode();
                            Asm.add(new OpPop(Register.rax,null));
                            Asm.add(new OpNeg(Register.rax));
                            Asm.add(new OpPush(Register.rax, StorageClass.PRIMITIVE));
                        }
                    }
                    else{
                        if( n["ADDOP"].token.lexeme == "+" ){
                            n["unaryexp"].generateCode();
                        }
                        if( n["ADDOP"].token.lexeme == "-" ){
                            n["unaryexp"].generateCode();
                            Asm.add(new OpPop(Register.rax, null));
                            Asm.add(new OpMov(0x8000000000000000, Register.rbx));
                            Asm.add(new OpXor(Register.rax, Register.rbx));
                            Asm.add(new OpPush( Register.rax, StorageClass.PRIMITIVE));
                        }
                    }
                }
            ),
            new("unaryexp :: NOTOP unaryexp",
                setNodeTypes: (n) => {
                    foreach(var c in n.children){
                        c.setNodeTypes();
                    }
                    var t1 = n["unaryexp"].nodeType;
                    var notop = n["NOTOP"].token;
                    if(t1 != NodeType.Bool){
                        Utils.error(notop,$"Type must be a Bool! ({t1})");
                    }
                    n.nodeType = NodeType.Bool;
                }
            ),
            new("unaryexp :: preincexp"),

            //preincrement, predecrement
            new("preincexp :: PLUSPLUS preincexp"),
            new("preincexp :: postincexp"),

            //postincrement, postdecrement
            new("postincexp :: postincexp PLUSPLUS"),
            new("postincexp :: amfexp"),

            //array, member, function call
            new("amfexp :: amfexp DOT factor"),
            new("amfexp :: amfexp LBRACKET expr RBRACKET"),
            new("amfexp :: amfexp LPAREN calllist RPAREN"),
            new("amfexp :: factor"),

            //indivisible atom
            new("factor :: NUM",
                setNodeTypes: (n) => {
                    n.nodeType = NodeType.Int;
                },
                generateCode: (n) => {
                    //make code for factor
                    string s = n["NUM"].token.lexeme;
                    long value = Int64.Parse(s);
                    Asm.add( new OpMov(value, Register.rax));
                    Asm.add( new OpPush(Register.rax, StorageClass.PRIMITIVE));
                }
            ),
            new("factor :: LPAREN expr RPAREN",
                setNodeTypes: (n) => {
                    n["expr"].setNodeTypes();
                    n.nodeType = n["expr"].nodeType;
                },
                generateCode: (n) => {
                    throw new Exception("FINISH ME");
                }
            ),
            new("factor :: ID",
                setNodeTypes: (n) => {
                    var tok = n.children[0].token;
                    VarInfo vi =  SymbolTable.lookup(tok);
                    n["ID"].varInfo = vi;
                    n["ID"].nodeType = n.nodeType = vi.type;
                }
            ),
            new("factor :: FNUM",
                setNodeTypes: (n) => {
                    n.nodeType = NodeType.Float;
                },
                generateCode: (n) =>{
                    string s = n["FNUM"].token.lexeme;
                    double value = Double.Parse(s);
                    long ivalue = BitConverter.DoubleToInt64Bits(value);
                    Asm.add( new OpMov(ivalue, Register.rax));
                    Asm.add( new OpPush(Register.rax, StorageClass.PRIMITIVE));
                }
            ),
            new("factor :: STRINGCONST",
                setNodeTypes: (n) => {
                    n.nodeType = NodeType.String;
                }),
            new("factor :: BOOLCONST",
                setNodeTypes: (n) => {
                    n.nodeType = NodeType.Bool;
                }
            ),


            //function call
            //calllist = zero or more arguments
            //calllist2 = 1 or more arguments
            new("calllist :: lambda"),
            new("calllist :: calllist2 COMMA expr"),
            new("calllist2 :: expr"),
            new("calllist2 :: calllist2 COMMA expr")

        });

    }
}
}