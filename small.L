{
    # My giga scuffed standard library LULE #
    _:;x;{x} # identity function since we can't use parenthesis in expressions #
    printDigit: ;num; print(48+num)
    if: ;bool action; bool & action()
    !: ;bool; bool < 1
    # stdlib end #

    if(1 ;;printDigit(!(1)))
    if(1 ;;printDigit(!(0)))

}