/*
using System.Text.RegularExpressions;

namespace lab{

    public class Token{
        public string sym; //use later?
        public string lexeme;
        public int line;
        public int col;

        public Token( string sym, string lexeme, int line, int col){
            this.sym = sym;
            this.lexeme = lexeme;
            this.line = line;
            this.col = col;
        }
        public override string ToString()
        {
            var lex = lexeme.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n");
            bool addCols = false;
            if(addCols){
                return $"{{ \"sym\": \"{this.sym}\", \"line\": {this.line}, \"col\": {this.col}, \"lexeme\" : \"{lex}\"  }}";
            }
            return $"{{ \"sym\": \"{this.sym}\", \"line\": {this.line}, \"lexeme\" : \"{lex}\"  }}";
        }
    
    }//End class Token

    public class Tokenizer{
        bool verbose=false;  //If we want extra output
        string input;   //stuff we are tokenizing
        int line;   //current line number
        int index;  //where we are at in the input
        int col;    //similar to index, but only for the output
        public Tokenizer(string inp){
            this.input = inp;
            this.line = 1;
            this.index = 0;
            this.col = 0;
        }

        public Token next(){

            if( this.index >= this.input.Length ){
                if(verbose){
                    Console.WriteLine("next(): At EOF!");
                }
                return null;
            }

            String sym = null;
            String lexeme = "";

            col++;
            foreach( var t in Grammar.terminals){
                Match M = t.rex.Match( this.input, this.index );
                if(verbose){
                    Console.WriteLine("Trying terminal " + t.sym + "\t\tMatched? " + M.Success);
                }

                //Dont break on success, keep track of the longest lexeme!!!
                //for maximal munch
                if( M.Success ){
                    sym = t.sym;
                    if(M.Groups[0].Value.Length >= lexeme.Length){
                        if(verbose)
                            Console.WriteLine("MAXMUNCH | Old: " + lexeme + " New: " + M.Groups[0].Value);
                        lexeme = M.Groups[0].Value;
                    }                        
                }
            }


            if( sym == null ){
                //print error message
                Console.WriteLine("Error at line " + this.line + ", col " + this.index);
                Environment.Exit(1);
            }
            this.index += lexeme.Length;

            //if(lexeme.Contains('\n'))
              //  this.line++;

            //this gets rid of whitespace and comments
            if(sym == "WHITESPACE" || sym == "COMMENT"){
                if (verbose){
                    Console.WriteLine("SKIPPING WHITESPACE OR COMMENT");
                }
                foreach( char c in lexeme){
                    if(c == '\n'){
                        this.line++;
                    }
                }
                return this.next();
            }
            

            var tok = new Token( sym , lexeme, line, col);


            foreach( char c in lexeme){
                if(c == '\n'){
                    this.line++;
                }
            }

            this.col += lexeme.Length - 1;  //Need this to make sure the column is at the front of tokens
            if( verbose ){
                Console.WriteLine("RETURNING TOKEN: " + tok);
            }


            return tok;
        }//next()

    } //End class Tokenizer
} //End namespace
*/
using System.Text.RegularExpressions;

namespace lab{

public class Token{
    public string sym; 
    //public string lastRealSym;
    public string lexeme;
    public int line;
    public Token( string sym, string lexeme, int line){
        this.sym = sym;
        //this.lastRealSym = "";
        this.lexeme = lexeme;
        this.line = line;
    }

    public void toJson(StreamWriter w){
        var lex = lexeme.Replace("\\","\\\\").Replace("\"","\\\"").Replace("\n","\\n");
        w.WriteLine($"{{ \"sym\": \"{this.sym}\" , \"line\" : {this.line}, \"lexeme\" : \"{lex}\"  }}");
    }

    public override string ToString()
    {
        var lex = lexeme.Replace("\\","\\\\").Replace("\"","\\\"").Replace("\n","\\n");

        return $"{{ \"sym\": \"{this.sym}\" , \"line\" : {this.line}, \"lexeme\" : \"{lex}\"  }}";
    }

}

public class Tokenizer{

    bool verbose=false;

    string input;   //stuff we are tokenizing
    string lastSym = "TEMP"; //last Sym
    int line;   //current line number
    int index;  //where we are at in the input

    Stack<Token> nesting = new();

    public Tokenizer(string inp){
        this.input = inp;
        this.line = 1;
        this.index = 0;
    }

    //we can insert an implicit semicolon after these things
    List<string> implicitSemiAfter = new(){"NUM","RPAREN","BOOLCONST","BREAK",
                                            "CONTINUE", "FNUM", "PLUSPLUS", "RBRACE",
                                            "RBRACKET", "RETURN", "STRINGCONST", "TYPE", "ID"
                                            };

    public Token next(){

        String sym=null;
        String lexeme=null;



        

        //If we've exhausted the input, return EOF
        if( this.index >= this.input.Length ){
            if(verbose){
                Console.WriteLine("next(): At EOF!");
            }
            if(nesting.Count != 0){
                Console.WriteLine("NOT EMPTY STACK: " + nesting.Pop());
                Environment.Exit(2);
            }
            return new Token("$","",this.line);
        }

        var maxMamunch = -1;
        
        foreach( var t in Grammar.terminals){
            Match M = t.rex.Match( this.input, this.index );

            if( sym == "WHITESPACE" || sym == "COMMENT"){
                this.index += lexeme.Length;
                foreach(var c in lexeme){
                    if( c == '\n' )
                        this.line++;
                    }

                return this.next();
            }

            if(verbose){
                Console.WriteLine("Trying terminal "+t.sym+ "   Matched? "+M.Success);
            }

            if( M.Success ){
                if( maxMamunch < 0 ){
                    sym = t.sym;
                    lexeme = M.Groups[0].Value;
                    maxMamunch = lexeme.Length;
                }
                else if (maxMamunch < lexeme.Length){
                    sym = t.sym;
                    lexeme = M.Groups[0].Value;
                    maxMamunch = lexeme.Length;
                }
            }
        }

        if( sym == null ){
            //print error message
            Console.WriteLine("Error at line "+this.line);
            Environment.Exit(1);
        }

        this.index += lexeme.Length;
        var tok = new Token( sym , lexeme, line);
        if( verbose ){
            Console.WriteLine("GOT TOKEN: "+tok);
        }
        
        if(sym == "LPAREN" || sym == "LBRACKET"){
            nesting.Push(tok);
        }

        if( sym == "RPAREN"){
            if(nesting.Count > 0){
                if(nesting.Peek().lexeme != "("){
                    Console.WriteLine("Error at line " + line + ": Expected LPAREN to match with RPAREN\n");
                    Environment.Exit(2);
                }
                else nesting.Pop();
            }
            else{
                Console.WriteLine("Error at line " + line + ": Unexpected RPAREN\n");
                Environment.Exit(2);
            }
        }

        if (sym == "RBRACKET"){
            if(nesting.Count > 0){
                if(nesting.Peek().lexeme != "["){
                    Console.WriteLine("Error at line " + line + ": Expected LBRACKET to match with RBRACKET\n");
                    Environment.Exit(2);
                }
                else nesting.Pop();
            }
            else{
                Console.WriteLine("Error at line " + line + ": Unexpected RBRACKET\n");
                Environment.Exit(2);
            }
        } 


        if( sym == "WHITESPACE" && implicitSemiAfter.Contains(lastSym) && nesting.Count == 0 && lexeme.Contains('\n')){
                if(verbose){Console.WriteLine(lastSym);}      //print
                
                var tmptok = new Token( "SEMI" , "", line);
                foreach(var c in lexeme){
                    if( c == '\n' )
                        this.line++;
                    }

                
                return tmptok;
        }

        if( sym == "WHITESPACE"){
            //this.index += lexeme.Length;
            foreach(var c in lexeme){
                if( c == '\n' )
                    this.line++;
                }

            return this.next();
            }

        else {      
            lastSym = sym; 
            return tok;
        }

    }//next()

    } //End class Tokenizer
} //End namespace