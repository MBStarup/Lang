{
    # My giga scuffed standard library LULE #
    _:;x;{x} # identity function since we can't use parenthesis in expressions #
    printDigit: ;num; print(48+num)
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
    print(10)

    add: ;x; ;y; y + x
    addFour: add(4)
    printDigit(addFour(5))


    0
}