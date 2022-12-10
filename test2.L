{
    _:;x;{x} # identity function since we can't use parenthesis in expressions #
    printNum: ;num; print(48+num)
    if: ;bool action; bool & action()

    # Takes a predicate acting on the loop count, and an action acting on the loop count to perform while the predicate evaluates true #
    loop: ;predicate action; {
        
        recusiveHelper: ;count; {
            action(count)
            if(predicate(count + 1) ;; { recusiveHelper(count + 1) } )
        }
        
        if(predicate(0) ;; { recusiveHelper(0) } )
    }

    # Prints the numbers 0 - 9 #
    loop(;i;i<10 ;i;printNum(i) )
    print(10)

    add: ;x; ;y; y + x
    addFour: add(4)
    printNum(addFour(5))


    0
}