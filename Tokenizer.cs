

using System.Text.RegularExpressions;

namespace lab{

public class Token{
    public string sym; 
    public string lexeme;
    public int line;
    public Token( string sym, string lexeme, int line){
        this.sym = sym;
        this.lexeme = lexeme;
        this.line = line;
    }
    public override string ToString()
    {
        return $"{{ \"sym\": \"{this.sym}\" , \"line\" : {this.line}, \"lexeme\" : \"{this.lexeme}\"  }}";
    }

}

public class Tokenizer{

    bool verbose=false;

    string input;   //stuff we are tokenizing
    int line;   //current line number
    int index;  //where we are at in the input

    public Tokenizer(string inp){
        this.input = inp;
        this.line = 1;
        this.index = 0;
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
        foreach( var t in Grammar.terminals){
            Match M = t.rex.Match( this.input, this.index );
            if(verbose){
                Console.WriteLine("Trying terminal "+t.sym+ "   Matched? "+M.Success);
            }
            if( M.Success ){
                sym = t.sym;
                lexeme = M.Groups[0].Value;
                break;
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
            Console.WriteLine("RETURNING TOKEN: "+tok);
        }
        return tok;
    }//next()

} //class Tokenizer

} //namespace