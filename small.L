{
    # My giga scuffed standard library LULE #
    
    _:;x;{x} # identity function since we can't use parenthesis in expressions #
    
    printDigit: ;num; print(48+num)

    newline: ;; print(10)
    
    if: ;bool action; { res:0 bool & res:action() res } # if "statement" using short circuit evaluation. Returns 0 if it didn't run, otherwise return the result of the action #
    ifElse: ;bool ifAction elseAction; { res:elseAction bool & { res:ifAction 1 } res() } 
    
    !: ;bool; bool = 0

    loopWithBase: ;predicate action base; { # Takes a predicate acting on the loop count, and an action acting on the loop count to perform while the predicate evaluates true #

        recusiveHelper: ;count prev; {

            ifElse(predicate(count + 1) ;;recusiveHelper(count + 1 action(count prev)) ;;action(count prev))
                                                                      
        }
        
        ifElse(predicate(0) ;;recusiveHelper(0 base) ;;base )
    }

    pow: ;base exponent; loopWithBase(;i; {i < exponent + 1} ;i res; {res * base} 1)/base

    printNum: ;x; { # xd no shot I figure out how to do this with the loop func, garbage lang #
        
        recusiveHelper: ;i prev; {
            if(_(prev/10) > 0 ;;recusiveHelper(i + 1 prev/10))
            printDigit(prev - _( _(prev/10) * 10))                                           
        }
        
        if(x > 0 ;;recusiveHelper(0 x))
    }

    # stdlib end #

    newline()
    printNum(pow(3 10))
    newline()
}