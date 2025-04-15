.section .text
.global _start
_start:
    andq $~0xf, %rsp  /*align the stack*/
    call lbl0  /* main */
    mov %rax, %rcx
    sub $32,%rsp
    call ExitProcess
lbl0:      /* main */
    push %rbp  /* value */
    movq %rsp, %rbp    /*  */
    /* num loc * 16 = 80 */
    sub $80, %rsp
    lea -16(%rbp), %rax  /* x */
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    movq $0, %rax    /*  */
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    pop %rbx  /* storage class */
    pop %rax  /* value */
    add $8, %rsp   /* discard storage class */
    pop %rcx  /* value */
    movq %rbx, 0(%rcx)    /*  */
    movq %rax, 8(%rcx)    /*  */
    lea -32(%rbp), %rax  /* y */
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    movq $0, %rax    /*  */
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    pop %rbx  /* storage class */
    pop %rax  /* value */
    add $8, %rsp   /* discard storage class */
    pop %rcx  /* value */
    movq %rbx, 0(%rcx)    /*  */
    movq %rax, 8(%rcx)    /*  */
    lea -48(%rbp), %rax  /* z */
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    movq $0, %rax    /*  */
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    pop %rbx  /* storage class */
    pop %rax  /* value */
    add $8, %rsp   /* discard storage class */
    pop %rcx  /* value */
    movq %rbx, 0(%rcx)    /*  */
    movq %rax, 8(%rcx)    /*  */
    lea -64(%rbp), %rax  /* flag */
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    movq $1, %rax    /*  */
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    pop %rbx  /* storage class */
    pop %rax  /* value */
    add $8, %rsp   /* discard storage class */
    pop %rcx  /* value */
    movq %rbx, 0(%rcx)    /*  */
    movq %rax, 8(%rcx)    /*  */
lbl1:      /* top of while loop at line 10 */
    lea -64(%rbp), %rax  /* flag */
    movq 0(%rax), %rbx    /*  */
    movq 8(%rax), %rax    /*  */
    push %rax  /* value */
    push %rbx  /* storage class */
    add $8, %rsp   /* discard storage class */
    pop %rax  /* value */
    test %rax, %rax
    jz lbl2  /* end of while loop at line 10 */
    lea -80(%rbp), %rax  /* tmp */
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    lea -32(%rbp), %rax  /* y */
    movq 0(%rax), %rbx    /*  */
    movq 8(%rax), %rax    /*  */
    push %rax  /* value */
    push %rbx  /* storage class */
    lea -16(%rbp), %rax  /* x */
    movq 0(%rax), %rbx    /*  */
    movq 8(%rax), %rax    /*  */
    push %rax  /* value */
    push %rbx  /* storage class */
    add $8, %rsp   /* discard storage class */
    pop %rbx  /* value */
    add $8, %rsp   /* discard storage class */
    pop %rax  /* value */
    add %rbx, %rax /*Adding registers*/
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    pop %rbx  /* storage class */
    pop %rax  /* value */
    add $8, %rsp   /* discard storage class */
    pop %rcx  /* value */
    movq %rbx, 0(%rcx)    /*  */
    movq %rax, 8(%rcx)    /*  */
    lea -32(%rbp), %rax  /* y */
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    lea -80(%rbp), %rax  /* tmp */
    movq 0(%rax), %rbx    /*  */
    movq 8(%rax), %rax    /*  */
    push %rax  /* value */
    push %rbx  /* storage class */
    pop %rbx  /* storage class */
    pop %rax  /* value */
    add $8, %rsp   /* discard storage class */
    pop %rcx  /* value */
    movq %rbx, 0(%rcx)    /*  */
    movq %rax, 8(%rcx)    /*  */
    lea -16(%rbp), %rax  /* x */
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    lea -16(%rbp), %rax  /* x */
    movq 0(%rax), %rbx    /*  */
    movq 8(%rax), %rax    /*  */
    push %rax  /* value */
    push %rbx  /* storage class */
    movq $1, %rax    /*  */
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    add $8, %rsp   /* discard storage class */
    pop %rbx  /* value */
    add $8, %rsp   /* discard storage class */
    pop %rax  /* value */
    add %rbx, %rax /*Adding registers*/
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    pop %rbx  /* storage class */
    pop %rax  /* value */
    add $8, %rsp   /* discard storage class */
    pop %rcx  /* value */
    movq %rbx, 0(%rcx)    /*  */
    movq %rax, 8(%rcx)    /*  */
    lea -16(%rbp), %rax  /* x */
    movq 0(%rax), %rbx    /*  */
    movq 8(%rax), %rax    /*  */
    push %rax  /* value */
    push %rbx  /* storage class */
    movq $10, %rax    /*  */
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    add $8, %rsp   /* discard storage class */
    pop %rbx  /* value */
    add $8, %rsp   /* discard storage class */
    pop %rax  /* value */
    cmp %rbx, %rax
    setge %al
    movzx %al, %rax
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    add $8, %rsp   /* discard storage class */
    pop %rax  /* value */
    test %rax, %rax
    jz lbl3  /* end of if starting at line 15 */
    lea -64(%rbp), %rax  /* flag */
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    movq $0, %rax    /*  */
    push %rax  /* value */
    push $0  /* storage class PRIMITIVE*/
    pop %rbx  /* storage class */
    pop %rax  /* value */
    add $8, %rsp   /* discard storage class */
    pop %rcx  /* value */
    movq %rbx, 0(%rcx)    /*  */
    movq %rax, 8(%rcx)    /*  */
lbl3:      /* end of if starting at line 15 */
    jmp lbl1  /* top of while loop at line 10 */
lbl2:      /* end of while loop at line 10 */
    /* Return at line 19 */
    lea -32(%rbp), %rax  /* y */
    movq 0(%rax), %rbx    /*  */
    movq 8(%rax), %rax    /*  */
    push %rax  /* value */
    push %rbx  /* storage class */
    add $8, %rsp   /* discard storage class */
    pop %rax  /* value */
    /* Epilogue at line 19 */
    movq %rbp, %rsp    /*  */
    /* Popping register %rbp... */
    pop %rbp  /* value */
    ret
    /* Epilogue at line 20 */
    movq %rbp, %rsp    /*  */
    /* Popping register %rbp... */
    pop %rbp  /* value */
    ret
.section .data
