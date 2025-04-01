namespace lab{

    public class VarInfo{
        public Token token;
        public int nestingLevel;
        public NodeType type;
        public VarLocation location;
        public VarInfo(Token t, int nl, NodeType nt, VarLocation loc){
            this.token=t;
            this.nestingLevel=nl;
            this.type=nt;
            this.location = loc;
        }

        public override string ToString(){
            //string tmp = $"nesting={this.nestingLevel} type={this.type} loc={this.location}";
            var tmp = $"\"type\": {this.type}, \"nesting\": {this.nestingLevel}, \"location\": {this.location}";
            return tmp;
        }
        public void toJson(StreamWriter w){
            w.Write($"{{\"type\": \"{this.type}\", \"nesting\": {this.nestingLevel}, \"location\": {{{this.location}}}}}");
        }

    }
} //namespace lab