using System.Reflection.PortableExecutable;

namespace lab{

public class Token{
    public string sym;  // maybe use later
    public string lexeme;
    public int line;
    public Token( string lexeme, int line){
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
        while( this.index < this.input.Length && Char.IsWhiteSpace(this.input[this.index]) ){
            //if this character is a newline, this.line += 1
            this.index++;
        }

        string tmp="";
        while( this.index < this.input.Length && !Char.IsWhiteSpace(this.input[this.index]) ){
            tmp += this.input[this.index];
            this.index++;
        }
        if( tmp.Length == 0 )
            return null;        //at eof
        return new Token(tmp,this.line);
    }

}

}