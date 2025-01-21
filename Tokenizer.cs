
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
    string input;   //stuff we are tokenizing
    int line;   //current line number
    int index;  //where we are at in the input

    public Tokenizer(string inp){
        this.input = inp;
        this.line = 1;
        this.index = 0;
    }

    public Token next(){

        String sym=null;
        String lexeme=null;
        foreach( var t in Grammar.terminals){
            Match M = t.rex.Match( this.input, this.index );
            if( M.Success ){
                sym = t.sym;
                lexeme = M.Groups[0].Value;
                break;
            }
        }

        if( sym == null ){
            //print error message
            Environment.Exit(1);
        }
        var tok = new Token( sym , lexeme, line);
        return tok;
    }

}

}