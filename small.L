{
    # My giga scuffed standard library LULE #
    _:;x;{x} # identity function since we can't use parenthesis in expressions #
    printDigit: ;num; print(48+num)
    newline: ;; print(10)
    if: ;bool action; bool & action()
    !: ;bool; bool < 1
    # stdlib end #

    # Takes a predicate acting on the loop count, and an action acting on the loop count to perform while the predicate evaluates true #
    loop: ;predicate action; {
        
        recusiveHelper: ;count; {
            action(count)
            if(predicate(count + 1) ;; { recusiveHelper(count + 1) } )
        }
        
        if(predicate(0) ;; { recusiveHelper(0) } )
    }

    # Prints the numbers 0 - 9 #
    loop(;i;i<10 ;i;printDigit(i) )
    newline()

    add: ;x; ;y; y + x
    addFour: add(4)
    if (addFour(5) = 9 ;;  { print(80) print(111) print(103) newline() })

    0
}