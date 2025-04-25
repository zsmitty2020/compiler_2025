using System.Text.Json.Serialization;

namespace lab{

    public class TreeNode{
        public string sym;  //terminal name or nonterminal
                            //"NUM", "cond"
        public Token token = null; //only meaningful for terminals
        public List<TreeNode> children = new();
        public int productionNumber;

        [JsonIgnore]
        public TreeNode parent = null;

        //only meaningful for fundecl nodes: Number of locals
        //declared in that function
        public int numLocals = -1;

        //only meaningful for tree nodes that are ID's and
        //which are variables
        public VarInfo varInfo = null;

        //true if we can prove that some code under this node
        //absolutely definitely positively no doubt about it returns.
        public bool returns=false;

        //only defined for function declaration nodes
        public List<Tuple<string,NodeType> > locals;

        //only meaningful for loop nodes; otherwise they are null
        public Label entry=null;
        public Label exit=null;
        public Label test=null;

        
        [JsonConverter(typeof(NodeTypeJsonConverter))]
        public NodeType nodeType = null;

        public TreeNode this[string childSym] {
            get {
                foreach( var c in this.children ){
                    if( c.sym == childSym ){
                        return c;
                    }
                }
                throw new Exception("No such child");
            }
        }

        Production production {
            get {
                if( this.productionNumber >= 0 )
                    return Grammar.productions[this.productionNumber];
                return null;
            }
        }

        public TreeNode(string sym, Token tok, int prodNum){
            this.sym=sym;
            this.token = tok;
            this.productionNumber = prodNum;
        }

        public void collectClassNames(){
            this.production?.pspec.collectClassNames(this);
        }

        public void collectFunctionNames(){
            this.production?.pspec.collectFunctionNames(this);
        }

        public void setNodeTypes(){
            this.production?.pspec.setNodeTypes(this);
        }
        public void generateCode(){
            this.production?.pspec.generateCode(this);
        }

        public void returnCheck(){
            this.production?.pspec.returnCheck(this);
        }

        public void pushAddressToStack(){
            if( this.production != null )
                this.production.pspec.pushAddressToStack(this);
            else
                Utils.error(this.firstToken(),"Cannot get address");
        }
        

        //nonterminal node
        public TreeNode(string sym, int prodNum) : this(sym,null,prodNum){}

        //terminal node
        public TreeNode(Token tok) : this( tok.sym, tok, -1 ){}

        public void appendChild(TreeNode n){
            n.parent = this;
            this.children.Add(n);
        }

        public void prependChild(TreeNode n){
            n.parent = this;
            this.children.Insert(0,n);
        }

        public void toJson(StreamWriter w){
            w.WriteLine("{");
            w.WriteLine( $"\t\"sym\" : \"{this.sym}\",");
            w.Write( $"\t\"token\" : ");
            if(this.token == null){
                w.Write("null");
            }
            else{
                this.token.toJson(w);
            }
            w.WriteLine(",");
            w.WriteLine( $"\t\"productionNumber\" : {this.productionNumber},");
            if(this.nodeType == null){
                w.WriteLine($"\t\"nodeType\" : null,");
            }
            else{
                w.WriteLine( $"\t\"nodeType\" : \"{this.nodeType}\",");
            }
            w.WriteLine( "\t\t\"children\" : [");
            for(int i=0;i<this.children.Count;i++){
                this.children[i].toJson(w);
                if( i != this.children.Count-1)
                    w.WriteLine(",");
            }
            w.WriteLine("\t\t\t\t\t],");
            if(this.varInfo == null){
                w.WriteLine($"\t\"varInfo\" : null");
            }
            else{
                w.WriteLine($"\t\"varInfo\" : ");
                this.varInfo.toJson(w);
            }
            
            w.WriteLine("}");
        }

        public void print(string prefix=""){
            
            string HLINE = "─"; // (Unicode \u2500)
            string VLINE = "│";  //(\u2502)
            string TEE = "├";  //(\u251c)
            string ELL = "└"; // (\u2514) 

            bool lastChild = this.parent != null && this == this.parent.children[^1];
            //if this node is the last child
            if( this.parent == null ){
                //root
                Console.WriteLine(this.ToString());
            } else {
                if( lastChild ){
                    Console.WriteLine(prefix+"  "+ELL+HLINE+this.ToString());
                } else {
                    Console.WriteLine(prefix+"  "+TEE+HLINE+this.ToString());
                }
            }

            foreach(var c in this.children){
                if( this.parent == null )
                    c.print("");
                else {
                    if( lastChild )
                        c.print(prefix + "   " );
                    else
                        c.print(prefix + "  " + VLINE );
                }
            }
        }

        public override string ToString(){
            string s = $"{this.sym}";

            if( this.token != null )
                s += $" ({this.token.lexeme})";

            if( this.nodeType != null )
                s += $" {this.nodeType}";
            if( this.varInfo != null )
                s += $" varInfo[{this.varInfo}]";
            return s;
        }

        public void removeUnitProductions(){
            for(int i=0;i<this.children.Count;++i)
                this.children[i].removeUnitProductions();

            if( this.children.Count == 1 && this.parent != null){
                this.parent.replaceChild(this, this.children[0] );
            }
        }

        public void replaceChild( TreeNode n, TreeNode c){
            //replace child n with c
            for(int i=0;i<this.children.Count;++i){
                if( this.children[i] == n ){
                    this.children[i] = c;
                    c.parent=this;
                    n.parent=null;
                    return;
                }
            }
            throw new Exception();
        }

        public Token firstToken(){
            if( this.token != null)
                return this.token;
            foreach(var c in this.children){
                Token t = c.firstToken();
                if(t!=null)
                    return t;
            }
            return null;
        }
        public Token lastToken(){
            if( this.token != null)
                return this.token;
            for(int i=this.children.Count-1;i>=0;i--){
                Token t = this.children[i].lastToken();
                if(t!=null)
                    return t;
            }
            return null;
        }

    } //end TreeNode

} //end namespace lab

