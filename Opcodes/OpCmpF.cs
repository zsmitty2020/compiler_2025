namespace lab{

    public class OpCmpF: Opcode {
        FloatRegister value1;
        FloatRegister value2;
        string thecs;
        public OpCmpF(string cc, FloatRegister op1, FloatRegister op2){
            this.value1=op1;
            this.value2=op2;
            this.thecs = cc;
        }

        public override void output(StreamWriter w){
            w.WriteLine($"    cmp{thecs}sd {this.value2}, {this.value1}");
            w.WriteLine($"    movsd {this.value1}, {this.value1}");
        }
    }

}