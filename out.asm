.section .text
.global _start
_start:
    andq $~0xf, %rsp  /*align the stack*/
    call lbl0  /* main */
    mov %rax, %rcx
    sub $32,%rsp
    call ExitProcess
lbl0:      /* main */
lbl3:      /* top of while loop at line 2 */
    jmp lbl1  /* end of test comparison at line 5 */
    /* Return at line 4 */
    movq $102, %rax    /*  */
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    add $8, %rsp   /* discard storage class */
    pop %rax  /* value */
    ret
lbl1:      /* end of test comparison at line 5 */
    movq $3, %rax    /*  */
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    movq $4, %rax    /*  */
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    add $8, %rsp   /* discard storage class */
    pop %rbx  /* value */
    add $8, %rsp   /* discard storage class */
    pop %rax  /* value */
    cmp %rbx, %rax
    setg %al
    movzx %al, %rax
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    add $8, %rsp   /* discard storage class */
    pop %rax  /* value */
    test %rax, %rax
    jz lbl3  /* top of while loop at line 2 */
lbl2:      /* end of while loop at line lbl1 */
    /* Return at line 6 */
    movq $100, %rax    /*  */
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    add $8, %rsp   /* discard storage class */
    pop %rax  /* value */
    ret
    ret
