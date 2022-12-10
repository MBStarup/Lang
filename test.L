    {
        a:10
        b: {
            a: 2*a
            a+ 4
        }
        doMath: ;a b; { b * a - b}
        func: ;x y; {
            zoom: 7 * y
            x + zoom
        }
        print(a + doMath(b 10) * func(1 3))
    }