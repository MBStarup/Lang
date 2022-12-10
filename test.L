    {
        # My giga scuffed standard library LULE #
        _:;x;{x} # identity function since we can't use parenthesis in expressions #
        printDigit: ;num; print(48+num)
        if: ;bool action; bool & action()
        !: ;bool; bool < 1
        # stdlib end #

        # Prints "Hello world" followed by a newline #
        Greet: ;; {
            print(72)   # H #
            print(101)  # e #
            print(108)  # l #
            print(108)  # l #
            print(111)  # o #
            print(32)   #   #
            print(119)  # w #
            print(111)  # o #
            print(114)  # r #
            print(108)  # l #
            print(100)  # d #
            print(10)   # \n #
        }

        Greet()
    }