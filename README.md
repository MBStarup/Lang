# Lang
Lang lang

A ***very shitty*** lang interpreted in C#

```
Multiline comment
# text here #


Multi expression, evaluates to the last expression in the multi expression
{
expr expr
expr expr
expr
}

Assign (and declare) operator, assigns the value of the righ hand to the identifier on the left
identifier: expr

Function creation? evaluates to a function taking x and y as parameters and executes expression
; x y ; expr

Function call, executes/evaluates the function with x as a parameter
function( x )

Numbers
12378912309

Math operators (no precedence)
2 + 4 * 8
1 - 3 / 4

Logic operators (no precedence)
0 & 0 | 1
```

The "print()" function is "injected" from the C# runtime

# Examples code

**Hello World**
```
{
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
```

**Some potentionally usefull functions**
```
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
}
```


# Ignore this
Tried to write out an example of the different structures to make sense of them.
Never finished the examples up, but they're staying here to help me if I ever come back to this.
This is also from an earliere version with different syntax...
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
                                                                                                            
le programme   pepeW                                                                                                                               

                  Assign                                                                                                                                        
                    |                                                                                                                                       
             _______|________                                                                                                                                       
            |               |                                                                                                                    
            |               |                                                                                                                    
         Symbol            [ ]                                                                                                                     
            |               |                                                                                                                                               
            |               |                                                                                                                                               
          Main             Func                                                                                                                                         
                            |                                                                                                                     
                         ___|___                                                                                                                  
                         |     |                                                                                                                  
                         |     |                                                                                                                
                        [ ]   [ ]                                                                                                        
                               |                                                                                                                
            ___________________|____________________________________________                                                                                                            
            |             |                               |                                                                                    
            |             |                               |                                                                                    
         Assign        Assign                          Assign                                                                             
            |             |                               |                                                                                   
         ___|___       ___|____                        ___|___                                                                                     
         |     |       |      |                        |     |                                                                        
         |     |       |      |                        |     |                                                                        
      Symbol  [ ]   Symbol   [ ]                    Symbol  [ ]                                                                      
         |     |       |      |                        |     |                                                                               
         |     |       |   ___|__________              |     |                                                                                   
         a    Int      b   |            |           doMath  Func                                                    
               |           |            |                    |                                                 
               |        Assign       BiExpr            ______|______                                                          
              10           |            |              |           |                                                          
                   ________|__    ______|______        |           |                                                          
                   |         |    |     |     |       [ ]         [ ]                                                    
                   |         |    |     |     |        |           |                                                    
                Symbol    BiExpr  +  Symbol  Int    ___|____       |                                           
                   |         |          |     |     |      |     BiExpr                                                             
                   |   ______|______    |     |     |      |        |                                                        
                   a   |     |     |    a    10  Symbol Symbol  ____|______                                                 
                       |     |     |                |      |    |   |     |                                                                    
                       *    Int  Symbol             |      |    |   |     |                                                                     
                             |     |                a      a    *  Int  Symbol                                                                 
                             |     |                                |     |                                                                               
                             2     a                       _________|___  |                                                                               
                                                           |     |     |  b                                                                          
                                                           |     |     |                                                                            
                                                           -  Symbol Symbol                                                                         
                                                                 |     |                                                                            
                                                                 |     |                                                                            
                                                                 a     b                                                                            
                                                                                                                                               
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
                                                                                                                                                
