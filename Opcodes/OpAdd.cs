namespace lab{

    public class OpAdd: Opcode {
        IntRegister rax;
        IntRegister rbx=null;
        int op2Constant;
        public OpAdd( IntRegister r1, IntRegister r2){
            this.rax=r1;
            this.rbx=r2;
        }

        public OpAdd( IntRegister r1, int const2){
            this.rax=r1;
            this.op2Constant=const2;
        }

        public override void output(StreamWriter w){
            if(rbx != null){
                w.WriteLine($"    add {this.rbx}, {this.rax} /*Adding registers*/");
            }
            else{
                w.WriteLine($"    add ${this.op2Constant}, {this.rax} /*Adding const*/");
            }
        }
    }

}