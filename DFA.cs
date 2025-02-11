using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace lab{

    public class ItemSet{
        public HashSet<LRItem> items;
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public static bool operator==(ItemSet o1, ItemSet o2){
            if( Object.ReferenceEquals(o1, null))
                return Object.ReferenceEquals(o2, null);
            return o1.Equals(o2);
        }

        public static bool operator!=(ItemSet o1, ItemSet o2){
            return !(o1 == o2);
        }

    }

    public class DFAState{
        public HashSet<LRItem> label = new();
        public Dictionary<string, DFAState> transitions = new();

        public DFAState( HashSet<LRItem> label){
            this.label = label;
        }//End Constructor

    }//End class DFAState

    public static class DFA{

        static HashSet<LRItem> computeClosure(HashSet<LRItem> kernal){
            var s = new HashSet<LRItem>();
            s.UnionWith(kernal);
            bool looping = true;

            while (looping){
                looping = false;

                HashSet<LRItem> tmp = new();

                foreach(LRItem I in s){
                    string sym = I.symAfterDPos;
                    if(Grammar.allNonterminals.Contains(sym)){
                        //sym is a nonterminal
                        foreach( Production p in Grammar.productionByLHS[sym] ){
                            var I2 = new LRItem( p,0 );
                            s.Add(I2);
                        }

                    }
                }

                int sizeBefore = s.Count;
                s.UnionWith(tmp);
                int sizeAfter = s.Count;
                looping = (sizeAfter > sizeBefore);
            }

            return s;
        }
        public static void makeDFA(){
            int productionNumber = Grammar.defineProductions(
                new PSpec[] {
                    new PSpec("S' :: S")
                
                }
            );

            LRItem I = new LRItem( Grammar.productions[productionNumber], 0);
            DFAState startState = new DFAState
            ( 
                computeClosure(
                    new HashSet<LRItem>(){I}
                )
            );

            var todo = new Stack<DFAState>();
            todo.Push(startState);

            Dictionary< HashSet<LRItem>,  DFAState> stateMap = new();



            while( todo.Count > 0 ){
                DFAState q = todo.Pop();
                var tr = getOutgoingTransitions(q);
                foreach( string sym in tr.Keys){
                    var lbl = computeClosure( tr[sym] );
                    var q2 = new DFAState(lbl);
                    todo.Push(q2);

                    if( q.transitions.ContainsKey( sym ))
                        throw new Exception("BUG!");

                    q.transitions[sym] = q2;
                }

            }

        }//End makeDFA

        
        static Dictionary<string, HashSet<LRItem>> getOutgoingTransitions(DFAState q){
            var tr = new Dictionary<string, HashSet<LRItem> >();
            foreach( LRItem I in q.label){
                string sym = I.symAfterDPos;
                if( !I.dposAtEnd){  //TODO: NEED TO WRITE dposAtEnd
                    if( !tr.ContainsKey(sym))
                        tr[sym] = new();

                    //there's an outgoing transition on the symbol sym
                    LRItem I2 = new LRItem( I.production, I.dpos+1 );
                    tr[sym].Add( I2 );
                }
            }
            return tr;
        }
        


    }//End class DFA
}//End namespace