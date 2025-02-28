namespace lab{

    public class LRItem{
        public readonly Production production;
        public readonly int dpos;

    public LRItem(Production p, int DPOS){
        this.production = p;
        this.dpos = DPOS;
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
        foreach(var thing in production.rhs){
            if (Array.FindIndex(production.rhs, x => x.Contains(thing)) == dpos)
                s += "\u2022 ";
            if( thing == production.rhs.Last() )
                s += thing;
            else
                s += thing + " ";
        }
        if( this.dposAtEnd )
            s += " \u2022";
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