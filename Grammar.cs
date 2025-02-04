namespace lab{

    public static class Grammar{
        public static List<Terminal> terminals = new();
        public static HashSet<string> allTerminals = [];

        public static void defineTerminals( Terminal[] terminals){
            foreach(var t in terminals){
                if( isTerminal( t.sym ) )
                    throw new Exception("THERE IS A COPY OF THIS TERMINAL!");
                Grammar.terminals.Add(t);
                allTerminals.Add(t.sym);
            }
        }

        public static void addWhitespace(){
            Grammar.defineTerminals(new Terminal[] {new("WHITESPACE",       @"\s+" )});
        }

        public static bool isTerminal(string sym){
            return allTerminals.Contains(sym);
        }

    }//End class Grammer
} //End namespace