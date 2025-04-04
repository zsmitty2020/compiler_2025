namespace lab{


    public abstract class VarLocation{
    }


    public class GlobalLocation : VarLocation{
        public Label lbl;
        public GlobalLocation(Label lbl){
            this.lbl = lbl;
        }
        public override string ToString(){
            return $"\"storageClass\": \"global {lbl}\"";
        }
    }

    public class LocalLocation : VarLocation{
        public int num; //the number of the local (its spot on the stack)
        public LocalLocation(int num){
            this.num=num;
        }

        public override string ToString(){
            return $"\"storageClass\": \"local\", \"index\": {this.num}";
        }
    }

    public class ParameterLocation : VarLocation {
        public int num;
        public ParameterLocation(int num){
            this.num = num;
        }
        public override string ToString(){
            return $"\"storageClass\": \"parameter\", \"index\": {this.num}";
        }
    }

} // namespace