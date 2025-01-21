using System.Text.RegularExpressions;

namespace lab{

    public class Terminal{
        public string sym;
        public Regex rex;

        public Terminal(string sym, string rex){
            this.sym=sym;
            this.rex= new Regex( "\\G(" + rex + ")" );
        }

    } //End class Terminal
} //End namespace