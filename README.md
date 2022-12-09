# Lang
Lang lang

--------------------------------------------------

    #a:10
    #b: {
        a: 2*a
        a+ 4
    }
    #doMath: ;a b; { (a - b) * b}
    #func: ;x y; {
        #zoom: 7 * y
        x + zoom
    }
    print(a + doMath(b 10) * func(1 3))

--------------------------------------------------
("Assign", "a", ("Int", "10"))
("Assign", "b", [("Assign", "a", ("BiExpr", "*", ("Int", "2"), "a"), ("BiExpr", "+", "a", ("Int", "4")))])
("Assign", "doMath", ("Func", ["a", "b"], [("BiExpr", "*", ("BiExpr", "-", "a", "b"), "b")])))("Assign", "func", ("Func", ["x", "y"], [("Assign", "zoom", ("BiExpr", "*", ("Int", "7"), "y")), ("BiExpr""+", "x", "zoom"), ])))
("Assign", "func", ("Func", ["x", "y"], [("Assign", "zoom", ("BiExpr", "*", ("Int", "7"), "y")), ("BiExpr""+", "x", "zoom"), ])))
("Call", "print", [("BiExpr", "+", "a", ("BiExpr", "*", ("Call", "doMath", ["b", ("Int", "10")]), ("Call", "func", [("Int", "1"), ("Int", "3")])))])
--------------------------------------------------

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
