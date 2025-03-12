namespace lab{

    public class LRItem{
        public readonly Production production;
        public readonly int dpos;
        public HashSet<string> lookaheads;

    public LRItem(Production p, int DPOS){
        this.production = p;
        this.dpos = DPOS;
        this.lookaheads = new ();
    }

    public string symAfterDPos{
        get { 
            if(this.dpos >= this.production.rhs.Length)  
                return null;
            else 
                return this.production.rhs[this.dpos]; 
            }
        //set { this.foo = value; } //Neat thing in c# that lets you do sets and gets, set is always using something called "value"
    }

    public void calculateLookaheads(){
        bool flag = true;
        while(flag){
            flag = false;
            foreach(DFAState Q in DFA.allStates){
                foreach(LRItem I in Q.label.items){
                    if(I.dposAtEnd == false){
                        string x = I.symAfterDPos;
                        DFAState Q2 = Q.transitions[x];
                        var I2 = findItemCreatedFromItem( Q2, I );
                        
                        var size = I2.lookaheads.Count();
                        I2.lookaheads.UnionWith(I.lookaheads);
                        if( I2.lookaheads.Count() > size ){
                            flag = true;
                        }

                        if( Grammar.isNonterminal(x) ){
                            HashSet<string> syms = findFirst(I);
                            foreach( Production P in Grammar.productions ){
                                if( P.lhs == x ){
                                    LRItem I3 = findItemCreatedByProduction( Q, P );

                                    var size2 = I3.lookaheads.Count();
                                    I3.lookaheads.UnionWith(syms);
                                    if( I3.lookaheads.Count() > size2){
                                        flag = true;
                                    }

                                }
                            }
                        }
                    }
                }
            }
            //also write dfa.dumps :P
        }
    }
    
    private LRItem findItemCreatedFromItem( DFAState theState, LRItem i){
        foreach(LRItem item in theState.label.items){
            if(i.production == item.production && 
                i.dpos+1 == item.dpos)
                return item;
        }
        throw new Exception("ERROR IN findItemCreatedFromItem");  // :P
    }
    private HashSet<string> findFirst( LRItem I ){
        HashSet<string> f = new HashSet<string>{};
        List<string> temparray = [];    //use this to get all items after the dpos
        for(int i = I.dpos+1; i < I.rhs.Count(); i++){
            temparray.Add(I.rhs[i]);
        }

        foreach( string sym in temparray){
            f.UnionWith(Grammar.first[sym]);
            if( !Grammar.nullable.Contains( sym ) ){
                return f;
            }
        }
        //if all rhs is nullable
        f.UnionWith(I.lookaheads);
        return f;

    }

    private LRItem findItemCreatedByProduction( DFAState theState, Production theProduction ){
        foreach( LRItem I in theState.label.items ){
            if( I.production == theProduction &&
                I.dpos == 0)
                return I;
        }
        throw new Exception("ERROR IN findItemCreatedByProduction"); // :P
    }

    public string lhs{        get { return this.production.lhs; }    }      //get left hand side

    public string[] rhs{        get { return this.production.rhs; }    }    //get right hand side

    public bool dposAtEnd{
        get{
            if( this.dpos == this.production.rhs.Length){
                return true;
            }
            return false;
        }
    }

    public override string ToString(){
        string s = "";
        s += production.lhs + " :: ";
        for(int i = 0; i < production.rhs.Count(); i++){
            if (i == dpos)
                s += "\u2022 "; // the •
            if( i < rhs.Count() )
                s += rhs[i] + " ";
        }
        if( this.dposAtEnd )
            s += "\u2022";  // the •
        s += " \u2551 ";    // the ║
        foreach( var la in lookaheads){
            s += la + " ";
        }
        return s;
    }

    public override int GetHashCode(){
        return this.production.unique ^ ( dpos<<16 );
    }

    public override bool Equals(object obj)
    {
        if ( Object.ReferenceEquals( obj, null ) )
            return false;

        LRItem I = obj as LRItem;

        if ( Object.ReferenceEquals( I, null ) )
            return false; //obj was not an LRItem

        return this.production.unique == I.production.unique &&
            this.dpos == I.dpos;
    }

    public static bool operator==(LRItem o1, LRItem o2){
        if( Object.ReferenceEquals(o1, null))
            return Object.ReferenceEquals(o2, null);
        return o1.Equals(o2);
    }

    public static bool operator!=(LRItem o1, LRItem o2){
        return !(o1 == o2);
    }


    }//End class LRItem

    
}//End namespace