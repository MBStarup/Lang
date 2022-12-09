# Lang
Lang lang

--------------------------------------------------

    main: ;; {
        a:10
        b: {
            a: 2*a
            a+ 4
        }
        doMath: ;a b; { (a - b) * b}
        func: ;x y; {
            #zoom: 7 * y
            x + zoom
        }
        print(a + doMath(b 10) * func(1 3))
    }
--------------------------------------------------
    ("SYMBOL", "a")
    (":", "")
    ("NUMBER", "10")
    ("SYMBOL", "b")
    (":", "")
    ("{", "")
    ("SYMBOL", "a")
    (":", "")
    ("NUMBER", "2")
    ("OPERATOR", "*")
    ("SYMBOL", "a")
    ("SYMBOL", "a")
    ("OPERATOR", "+")
    ("NUMBER", "4")
    ("}", "")
    ("SYMBOL", "doMath")
    (":", "")
    (";", "")
    ("SYMBOL", "a")
    ("SYMBOL", "b")
    (";", "")
    ("{", "")
    ("(", "")
    ("SYMBOL", "a")
    ("OPERATOR", "-")
    ("SYMBOL", "b")
    (")", "")
    ("OPERATOR", "*")
    ("SYMBOL", "b")
    ("}", "")
    ("SYMBOL", "func")
    (":", "")
    (";", "")
    ("SYMBOL", "x")
    ("SYMBOL", "y")
    (";", "")
    ("{", "")
    ("OPERATOR", "#")
    ("SYMBOL", "zoom")
    (":", "")
    ("NUMBER", "7")
    ("OPERATOR", "*")
    ("SYMBOL", "y")
    ("SYMBOL", "x")
    ("OPERATOR", "+")
    ("SYMBOL", "zoom")
    ("}", "")
    ("SYMBOL", "print")
    ("(", "")
    ("SYMBOL", "a")
    ("OPERATOR", "+")
    ("SYMBOL", "doMath")
    ("(", "")
    ("SYMBOL", "b")
    ("NUMBER", "10")
    (")", "")
    ("OPERATOR", "*")
    ("SYMBOL", "func")
    ("(", "")
    ("NUMBER", "1")
    ("NUMBER", "3")
    (")", "")
    (")", "")
--------------------------------------------------
        
    (
        "Assign",
        "main",
        (
            "Func",
            [],
            [    
                (
                    "Assign", 
                    "a", 
                    (
                        "Int", 
                        "10"
                    )
                )

                (
                    "Assign", 
                    "b", 
                    [
                        (
                            "Assign", 
                            "a", 
                            (
                                "BiExpr", 
                                "*", 
                                (
                                    "Int", 
                                    "2"
                                ), 
                                "a"
                            ), 
                            (
                                "BiExpr", 
                                "+", 
                                "a", 
                                (
                                    "Int", 
                                    "4"
                                )
                            )
                        )
                    ]
                )

                (
                    "Assign", 
                    "doMath", 
                    (
                        "Func", 
                        [
                            "a", 
                            "b"
                        ], 
                        [
                            (
                                "BiExpr", 
                                "*", 
                                (
                                    "BiExpr", 
                                    "-", 
                                    "a", 
                                    "b"
                                ), 
                                "b"
                            )
                        ]
                        )
                    )
                )

                (
                    "Assign", 
                    "func", 
                    (
                        "Func", 
                        [
                            "x", 
                            "y"
                        ], 
                        [
                            (
                                "Assign", 
                                "zoom", 
                                (
                                    "BiExpr", 
                                    "*", 
                                    (
                                        "Int", 
                                        "7"
                                    ), 
                                    "y"
                                )
                            ), 
                            (
                                "BiExpr"
                                "+", 
                                "x", 
                                "zoom"
                            ), 
                        ]
                        )
                    )
                )
                (
                    "Call", 
                    "print", 
                    [
                        (
                            "BiExpr", 
                            "+", 
                            "a", 
                            (
                                "BiExpr", 
                                "*", 
                                (
                                    "Call", 
                                    "doMath", 
                                    [
                                        "b", 
                                        (
                                            "Int", 
                                            "10"
                                        )
                                    ]
                                ), 
                                (
                                    "Call", 
                                    "func", 
                                    [
                                        (
                                            "Int", 
                                            "1"
                                        ), 
                                        (
                                            "Int", 
                                            "3"
                                        )
                                    ]
                                )
                            )
                        )
                    ]
                )
            ]
        )
    )



1 + 2 + 4:


                ^
                |
               (7)
                |
                +
                |
             ___|___
             ^     ^
             |     |
            (3)   (4)
             |     |
             +     4
             |
          ___|___
          ^     ^
          |     |
         (1)   (2)
          |     |
          1     2



1 + (2 + 4):


                ^
                |
               (7)
                |
                +
                |
             ___|___
             ^     ^
             |     |
            (1)   (6)
             |     |
             1     +
                   |
                ___|___
                ^     ^
                |     |
               (2)   (4)
                |     |
                2     4

func(1 3)

                ^
                |
               (22)
                |
               func
                |
             ___|___
             ^     ^
             |     |
            (1)   (3)
             |     |
             1     3