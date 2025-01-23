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
            return $"{{ \"sym\": \"{this.sym}\", \"line\": {this.line}, \"lexeme\" : \"{this.lexeme}\"  }}";
        }
    
    }//End class Token

    public class Tokenizer{
        bool verbose=true;  //If we want extra output
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

            String sym=null;
            String lexeme=null;
            var tmp = 0;

            col++;
            foreach( var t in Grammar.terminals){
                tmp++;
                Match M = t.rex.Match( this.input, this.index );
                if(verbose){
                    Console.WriteLine("Trying terminal " + t.sym + "\t\tMatched? " + M.Success);
                }
                if( M.Success ){
                    sym = t.sym;
                    lexeme = M.Groups[0].Value;
                    break;
                }
            }

            if( sym == null ){
                //print error message
                Console.WriteLine("Error at line " + this.line + ", col " + this.index);
                Environment.Exit(1);
            }
            this.index += lexeme.Length;

            if(lexeme.Contains('\n')){
                if(verbose){
                    Console.WriteLine("FOUND NEWLINE");
                }
                this.col = 0;
                this.line++;
            }

            //this gets rid of whitespace and comments
            if(sym == "WHITESPACE" || sym == "COMMENT"){
                if (verbose){
                    Console.WriteLine("SKIPPING WHITESPACE OR COMMENT");
                }
                return this.next();
            }

            var tok = new Token( sym , lexeme, line, col);
            this.col += lexeme.Length - 1;  //Need this to make sure the column is at the front of tokens
            if( verbose ){
                Console.WriteLine("RETURNING TOKEN: " + tok);
            }


            return tok;
        }//next()

    } //End class Tokenizer
} //End namespace