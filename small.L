    {
        a:10
        b: {
            a: 2*a
            a+ 4
        }
        doMath: ;a b; { c: a*b c-b}
        func: ;x y; {
            zoom: 7 * y
            x + zoom
        }
        a + doMath(b 10) * func(1 3)
    }