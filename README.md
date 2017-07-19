# ReCode
*The Fast Regular Expression compiler for C#.*


## Introduction

Inspired by **RE2C**, this library can be referenced from a T4 text template for the purpose of generating
code for parallel matching of a block of regular expressions.

Supported inputs for matching are pointers to a sequence of bytes or chars. This enables generation of fast scanners/tokenizers for almost any
purpose (even binary data).


## Usage

**ReCode** is enabled in a text template by referencing the dll:
```c#
<#@ assembly name="$(SolutionDir)packages\\ReCode.1.0.3\\lib\\452\\ReCode.dll" #>
<#@ import namespace="ReCode" #>
```

## Example

As an example of a compiled block of expressions, consider the following example:

```c#
<#@ template debug="false" hostspecific="false" language="C#" #>
<#@ assembly name="$(SolutionDir)packages\\ReCode.1.0.3\\lib\\452\\ReCode.dll" #>
<#@ import namespace="ReCode" #>
<#@ output extension=".cs" #>
using System.Collections.Generic;

namespace ReCodeTest {
    public class TestParser2 {
        public static unsafe KeyValuePair<ushort, int>? Parse(char * start, int len){
            var pNext = start;
            var pLimit = start + len;
            var pEnd = start; 
<#
foreach(var token in ReCode.RegularExpressionTokens(WriteLine)) {
    for(var i=0;i<10;i++)
      if(token.Matches($"'{25*i}'"))                            { #> return new KeyValuePair<ushort,int>(<#=i#>, (int)(pEnd - start)); <#}
    if(token.Matches(@"[+\-]?[0-9]*[\.][0-9]+[E][+\-]?[0-9]+")) { #> return new KeyValuePair<ushort,int>(10, (int)(pEnd - start)); <# }
    if(token.Matches(@"[+\-]?[0-9]*[\.][0-9]+"))                { #> return new KeyValuePair<ushort,int>(11, (int)(pEnd - start)); <# }
    if(token.Matches(@"[+\-]?[0-9]+"))                          { #> return new KeyValuePair<ushort,int>(12, (int)(pEnd - start)); <# }
    if(token.Fails)                                             { #> return null; <# }
}
#>
        }
    }
}
```
The resulting function matches the following sequences
  * _0_ maps to 0
  * _25_ maps to 1
  * _50_ maps to 2
  * _75_ maps to 3
  * _100_ maps to 4
  * _125_ maps to 5
  * _150_ maps to 6
  * _175_ maps to 7
  * _200_ maps to 8
  * _225_ maps to 9
  * _[+\\-]?[0-9]*[\\.][0-9]+[E][+\\-]?[0-9]+_ maps to 10
  * _[+\\-]?[0-9]*[\\.][0-9]+_ maps to 11
  * _[+\\-]?[0-9]+_ maps to 12

The block is executed with semantics corresponding to executing the expressions one by one
in the listed order until a match is reached. If nothing was matched, the **token.Fails** block
is executed.


## Internal Scanning and Parsing

**ReCode** internally uses a pre-compiled version of **ReCode** for constructing the scanner for the 
regular expression syntax.

The library also contains logic for constructing LR(1) grammar parsers from
a T4 template. This logic is used for parsing the regular expression patterns. 
This part of the library seems to work very well, but has not been extensively tested.


## Generated Code

The resulting code executes a deterministic finite automaton (DFA) for recognizing the 
supplied regular expressions (with documentation). 

As of version 1, the generated code for the above example would be the following:

```c#
using System.Collections.Generic;

namespace ReCodeTest {
	public class TestParser2 {
		public static unsafe KeyValuePair<ushort, int>? Parse(char * start, int len){
            var pNext = start;
            var pLimit = start + len;
            var pEnd = start; 
{
/*
 * Union
 *  +-Accept(0)
 *  |  +-Sequence(CaseInsensitive,'0')
 *  +-Accept(1)
 *  |  +-Sequence(CaseInsensitive,'25')
 *  +-Accept(2)
 *  |  +-Sequence(CaseInsensitive,'50')
 *  +-Accept(3)
 *  |  +-Sequence(CaseInsensitive,'75')
 *  +-Accept(4)
 *  |  +-Sequence(CaseInsensitive,'100')
 *  +-Accept(5)
 *  |  +-Sequence(CaseInsensitive,'125')
 *  +-Accept(6)
 *  |  +-Sequence(CaseInsensitive,'150')
 *  +-Accept(7)
 *  |  +-Sequence(CaseInsensitive,'175')
 *  +-Accept(8)
 *  |  +-Sequence(CaseInsensitive,'200')
 *  +-Accept(9)
 *  |  +-Sequence(CaseInsensitive,'225')
 *  +-Accept(10)
 *  |  +-Concat
 *  |     +-Repeat(ZeroOrOne)
 *  |     |  +-Ranges('+','-')
 *  |     +-Repeat(ZeroOrMore)
 *  |     |  +-Ranges(['0'-'9'])
 *  |     +-Ranges('.')
 *  |     +-Repeat(OneOrMore)
 *  |     |  +-Ranges(['0'-'9'])
 *  |     +-Ranges('E')
 *  |     +-Repeat(ZeroOrOne)
 *  |     |  +-Ranges('+','-')
 *  |     +-Repeat(OneOrMore)
 *  |        +-Ranges(['0'-'9'])
 *  +-Accept(11)
 *  |  +-Concat
 *  |     +-Repeat(ZeroOrOne)
 *  |     |  +-Ranges('+','-')
 *  |     +-Repeat(ZeroOrMore)
 *  |     |  +-Ranges(['0'-'9'])
 *  |     +-Ranges('.')
 *  |     +-Repeat(OneOrMore)
 *  |        +-Ranges(['0'-'9'])
 *  +-Accept(12)
 *     +-Concat
 *        +-Repeat(ZeroOrOne)
 *        |  +-Ranges('+','-')
 *        +-Repeat(OneOrMore)
 *           +-Ranges(['0'-'9'])
 */
/*
 * DFA STATE 0
 * '+' -> 1
 * '-' -> 1
 * '.' -> 2
 * '0' -> 3
 * '1' -> 4
 * '2' -> 5
 * ['3'-'4'] -> 6
 * '5' -> 7
 * '6' -> 6
 * '7' -> 8
 * ['8'-'9'] -> 6
 */
if(pNext >= pLimit) goto nonaccept0;
var current = *pNext++;
if(current < 0x31) /* ('0') '1' */  {
    if(current < 0x2D) /* (',') '-' */  {
        if(current < 0x2B) /* ('*') '+' */ 
            goto nonaccept0;
        if(current < 0x2C) /* ('+') ',' */ 
            goto state0_1;
        goto nonaccept0;
    }
    if(current < 0x2F) /* ('.') '/' */  {
        if(current < 0x2E) /* ('-') '.' */ 
            goto state0_1;
        goto state0_2;
    }
    if(current < 0x30) /* ('/') '0' */ 
        goto nonaccept0;
    goto state0_3;
}
if(current < 0x36) /* ('5') '6' */  {
    if(current < 0x33) /* ('2') '3' */  {
        if(current < 0x32) /* ('1') '2' */ 
            goto state0_4;
        goto state0_5;
    }
    if(current < 0x35) /* ('4') '5' */ 
        goto state0_6;
    goto state0_7;
}
if(current < 0x38) /* ('7') '8' */  {
    if(current < 0x37) /* ('6') '7' */ 
        goto state0_6;
    goto state0_8;
}
if(current < 0x3A) /* ('9') ':' */ 
    goto state0_6;
goto nonaccept0;
/*
 * DFA STATE 1
 * '.' -> 2
 * ['0'-'9'] -> 6
 */
state0_1:
if(pNext >= pLimit) goto nonaccept0;
current = *pNext++;
if(current < 0x2F) /* ('.') '/' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto nonaccept0;
    goto state0_2;
}
if(current < 0x30) /* ('/') '0' */ 
    goto nonaccept0;
if(current < 0x3A) /* ('9') ':' */ 
    goto state0_6;
goto nonaccept0;
/*
 * DFA STATE 2
 * ['0'-'9'] -> 9
 */
state0_2:
if(pNext >= pLimit) goto nonaccept0;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */ 
    goto nonaccept0;
if(current < 0x3A) /* ('9') ':' */ 
    goto state0_9;
goto nonaccept0;
/*
 * DFA STATE 3 (accepts to 0)
 */
state0_3:
pEnd = pNext;
goto accept0_0;
/*
 * DFA STATE 4 (accepts to 12)
 * '.' -> 10
 * '0' -> 11
 * '1' -> 6
 * '2' -> 12
 * ['3'-'4'] -> 6
 * '5' -> 13
 * '6' -> 6
 * '7' -> 14
 * ['8'-'9'] -> 6
 */
state0_4:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_12;
current = *pNext++;
if(current < 0x33) /* ('2') '3' */  {
    if(current < 0x30) /* ('/') '0' */  {
        if(current < 0x2E) /* ('-') '.' */ 
            goto accept0_12;
        if(current < 0x2F) /* ('.') '/' */ 
            goto state0_10;
        goto accept0_12;
    }
    if(current < 0x31) /* ('0') '1' */ 
        goto state0_11;
    if(current < 0x32) /* ('1') '2' */ 
        goto state0_6;
    goto state0_12;
}
if(current < 0x37) /* ('6') '7' */  {
    if(current < 0x35) /* ('4') '5' */ 
        goto state0_6;
    if(current < 0x36) /* ('5') '6' */ 
        goto state0_13;
    goto state0_6;
}
if(current < 0x38) /* ('7') '8' */ 
    goto state0_14;
if(current < 0x3A) /* ('9') ':' */ 
    goto state0_6;
goto accept0_12;
/*
 * DFA STATE 5 (accepts to 12)
 * '.' -> 10
 * '0' -> 15
 * '1' -> 6
 * '2' -> 16
 * ['3'-'4'] -> 6
 * '5' -> 17
 * ['6'-'9'] -> 6
 */
state0_5:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_12;
current = *pNext++;
if(current < 0x32) /* ('1') '2' */  {
    if(current < 0x2F) /* ('.') '/' */  {
        if(current < 0x2E) /* ('-') '.' */ 
            goto accept0_12;
        goto state0_10;
    }
    if(current < 0x30) /* ('/') '0' */ 
        goto accept0_12;
    if(current < 0x31) /* ('0') '1' */ 
        goto state0_15;
    goto state0_6;
}
if(current < 0x35) /* ('4') '5' */  {
    if(current < 0x33) /* ('2') '3' */ 
        goto state0_16;
    goto state0_6;
}
if(current < 0x36) /* ('5') '6' */ 
    goto state0_17;
if(current < 0x3A) /* ('9') ':' */ 
    goto state0_6;
goto accept0_12;
/*
 * DFA STATE 6 (accepts to 12)
 * '.' -> 10
 * ['0'-'9'] -> 6
 */
state0_6:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_12;
current = *pNext++;
if(current < 0x2F) /* ('.') '/' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto accept0_12;
    goto state0_10;
}
if(current < 0x30) /* ('/') '0' */ 
    goto accept0_12;
if(current < 0x3A) /* ('9') ':' */ 
    goto state0_6;
goto accept0_12;
/*
 * DFA STATE 7 (accepts to 12)
 * '.' -> 10
 * '0' -> 18
 * ['1'-'9'] -> 6
 */
state0_7:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto accept0_12;
    if(current < 0x2F) /* ('.') '/' */ 
        goto state0_10;
    goto accept0_12;
}
if(current < 0x31) /* ('0') '1' */ 
    goto state0_18;
if(current < 0x3A) /* ('9') ':' */ 
    goto state0_6;
goto accept0_12;
/*
 * DFA STATE 8 (accepts to 12)
 * '.' -> 10
 * ['0'-'4'] -> 6
 * '5' -> 19
 * ['6'-'9'] -> 6
 */
state0_8:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto accept0_12;
    if(current < 0x2F) /* ('.') '/' */ 
        goto state0_10;
    goto accept0_12;
}
if(current < 0x36) /* ('5') '6' */  {
    if(current < 0x35) /* ('4') '5' */ 
        goto state0_6;
    goto state0_19;
}
if(current < 0x3A) /* ('9') ':' */ 
    goto state0_6;
goto accept0_12;
/*
 * DFA STATE 9 (accepts to 11)
 * ['0'-'9'] -> 9
 * 'E' -> 20
 */
state0_9:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_11;
current = *pNext++;
if(current < 0x3A) /* ('9') ':' */  {
    if(current < 0x30) /* ('/') '0' */ 
        goto accept0_11;
    goto state0_9;
}
if(current < 0x45) /* ('D') 'E' */ 
    goto accept0_11;
if(current < 0x46) /* ('E') 'F' */ 
    goto state0_20;
goto accept0_11;
/*
 * DFA STATE 10
 * ['0'-'9'] -> 9
 */
state0_10:
if(pNext >= pLimit) goto accept0_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */ 
    goto accept0_12;
if(current < 0x3A) /* ('9') ':' */ 
    goto state0_9;
goto accept0_12;
/*
 * DFA STATE 11 (accepts to 12)
 * '.' -> 10
 * '0' -> 21
 * ['1'-'9'] -> 6
 */
state0_11:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto accept0_12;
    if(current < 0x2F) /* ('.') '/' */ 
        goto state0_10;
    goto accept0_12;
}
if(current < 0x31) /* ('0') '1' */ 
    goto state0_21;
if(current < 0x3A) /* ('9') ':' */ 
    goto state0_6;
goto accept0_12;
/*
 * DFA STATE 12 (accepts to 12)
 * '.' -> 10
 * ['0'-'4'] -> 6
 * '5' -> 22
 * ['6'-'9'] -> 6
 */
state0_12:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto accept0_12;
    if(current < 0x2F) /* ('.') '/' */ 
        goto state0_10;
    goto accept0_12;
}
if(current < 0x36) /* ('5') '6' */  {
    if(current < 0x35) /* ('4') '5' */ 
        goto state0_6;
    goto state0_22;
}
if(current < 0x3A) /* ('9') ':' */ 
    goto state0_6;
goto accept0_12;
/*
 * DFA STATE 13 (accepts to 12)
 * '.' -> 10
 * '0' -> 23
 * ['1'-'9'] -> 6
 */
state0_13:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto accept0_12;
    if(current < 0x2F) /* ('.') '/' */ 
        goto state0_10;
    goto accept0_12;
}
if(current < 0x31) /* ('0') '1' */ 
    goto state0_23;
if(current < 0x3A) /* ('9') ':' */ 
    goto state0_6;
goto accept0_12;
/*
 * DFA STATE 14 (accepts to 12)
 * '.' -> 10
 * ['0'-'4'] -> 6
 * '5' -> 24
 * ['6'-'9'] -> 6
 */
state0_14:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto accept0_12;
    if(current < 0x2F) /* ('.') '/' */ 
        goto state0_10;
    goto accept0_12;
}
if(current < 0x36) /* ('5') '6' */  {
    if(current < 0x35) /* ('4') '5' */ 
        goto state0_6;
    goto state0_24;
}
if(current < 0x3A) /* ('9') ':' */ 
    goto state0_6;
goto accept0_12;
/*
 * DFA STATE 15 (accepts to 12)
 * '.' -> 10
 * '0' -> 25
 * ['1'-'9'] -> 6
 */
state0_15:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto accept0_12;
    if(current < 0x2F) /* ('.') '/' */ 
        goto state0_10;
    goto accept0_12;
}
if(current < 0x31) /* ('0') '1' */ 
    goto state0_25;
if(current < 0x3A) /* ('9') ':' */ 
    goto state0_6;
goto accept0_12;
/*
 * DFA STATE 16 (accepts to 12)
 * '.' -> 10
 * ['0'-'4'] -> 6
 * '5' -> 26
 * ['6'-'9'] -> 6
 */
state0_16:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto accept0_12;
    if(current < 0x2F) /* ('.') '/' */ 
        goto state0_10;
    goto accept0_12;
}
if(current < 0x36) /* ('5') '6' */  {
    if(current < 0x35) /* ('4') '5' */ 
        goto state0_6;
    goto state0_26;
}
if(current < 0x3A) /* ('9') ':' */ 
    goto state0_6;
goto accept0_12;
/*
 * DFA STATE 17 (accepts to 1)
 */
state0_17:
pEnd = pNext;
goto accept0_1;
/*
 * DFA STATE 18 (accepts to 2)
 */
state0_18:
pEnd = pNext;
goto accept0_2;
/*
 * DFA STATE 19 (accepts to 3)
 */
state0_19:
pEnd = pNext;
goto accept0_3;
/*
 * DFA STATE 20
 * '+' -> 27
 * '-' -> 27
 * ['0'-'9'] -> 28
 */
state0_20:
if(pNext >= pLimit) goto accept0_11;
current = *pNext++;
if(current < 0x2D) /* (',') '-' */  {
    if(current < 0x2B) /* ('*') '+' */ 
        goto accept0_11;
    if(current < 0x2C) /* ('+') ',' */ 
        goto state0_27;
    goto accept0_11;
}
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto state0_27;
    goto accept0_11;
}
if(current < 0x3A) /* ('9') ':' */ 
    goto state0_28;
goto accept0_11;
/*
 * DFA STATE 21 (accepts to 4)
 */
state0_21:
pEnd = pNext;
goto accept0_4;
/*
 * DFA STATE 22 (accepts to 5)
 */
state0_22:
pEnd = pNext;
goto accept0_5;
/*
 * DFA STATE 23 (accepts to 6)
 */
state0_23:
pEnd = pNext;
goto accept0_6;
/*
 * DFA STATE 24 (accepts to 7)
 */
state0_24:
pEnd = pNext;
goto accept0_7;
/*
 * DFA STATE 25 (accepts to 8)
 */
state0_25:
pEnd = pNext;
goto accept0_8;
/*
 * DFA STATE 26 (accepts to 9)
 */
state0_26:
pEnd = pNext;
goto accept0_9;
/*
 * DFA STATE 27
 * ['0'-'9'] -> 28
 */
state0_27:
if(pNext >= pLimit) goto accept0_11;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */ 
    goto accept0_11;
if(current < 0x3A) /* ('9') ':' */ 
    goto state0_28;
goto accept0_11;
/*
 * DFA STATE 28 (accepts to 10)
 * ['0'-'9'] -> 28
 */
state0_28:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_10;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */ 
    goto accept0_10;
if(current < 0x3A) /* ('9') ':' */ 
    goto state0_28;
goto accept0_10;


accept0_0:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(0, (int)(pEnd - start)); 
accept0_1:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(1, (int)(pEnd - start)); 
accept0_2:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(2, (int)(pEnd - start)); 
accept0_3:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(3, (int)(pEnd - start)); 
accept0_4:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(4, (int)(pEnd - start)); 
accept0_5:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(5, (int)(pEnd - start)); 
accept0_6:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(6, (int)(pEnd - start)); 
accept0_7:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(7, (int)(pEnd - start)); 
accept0_8:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(8, (int)(pEnd - start)); 
accept0_9:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(9, (int)(pEnd - start)); 
accept0_10:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(10, (int)(pEnd - start)); 
accept0_11:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(11, (int)(pEnd - start)); 
accept0_12:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(12, (int)(pEnd - start)); 
nonaccept0:
 return null; }
        }
    }
}
```

## The Parser Generator

As a free benefit, the library includes a *LR(1)* parser generator as well.

We can consider the source code used in **ReCode** for parsing regular expression syntax:

```C#
<#@ template debug="false" hostspecific="false" language="C#" #>
<#@ assembly name="$(SolutionDir)packages\\ReCode.1.0.3\\lib\\452\\ReCode.dll" #>
<#@ import namespace="ReCode" #>
<#@ output extension=".cs" #>
using System;
using System.Collections.Generic;
using ReCode.RegularExpressions.Parsing.Nodes;

namespace ReCode.RegularExpressions.Parsing {
    public partial class RegExParser {
        <#
    foreach (var language in ReCode.ParserTokens(WriteLine, "RegExNode"))
    {
        var g = language.GrammarFromTokens("Expr", "Phrase", "Range", "Star", "Plus", "Question", "ParBegin", "ParEnd", "Bar", "Accept", "Name", "Except");
        var g1 = g.WithPrecedenceGroup();
        if(g1.Matches("Expr", "ParBegin", "Expr", "ParEnd")) {#> res = arg1; <# }
        if(g1.Matches("Expr", "Phrase"))                     {#> res = arg0; <# }
        if(g1.Matches("Expr", "Range"))                      {#> res = arg0; <# }
        if(g1.Matches("Expr", "Name"))                       {#> res = _namedExpressions[(arg0 as RegExNodeName).Name]; <# }
        if(g1.MatchesLeft("Expr", "Expr", "Expr"))           {#> res = new RegExNodeConcat(arg0,arg1); <# }
        if(g1.MatchesLeft("Expr", "Expr", "Bar", "Expr"))    {#> res = RegExNodeUnion.Of(arg0,arg2); <# }
        var g2 = g.WithPrecedenceGroup();
        if(g2.Matches("Expr", "Expr", "Accept"))             {#> res = new RegExNodeAccept(arg0, (arg1 as RegExNodeAccept).AcceptState); <# }
        if(g2.Matches("Expr", "Expr", "Plus"))               {#> res = new RegExNodeRepeat(arg0, RegExRepeatType.OneOrMore); <# }
        if(g2.Matches("Expr", "Expr", "Star"))               {#> res = new RegExNodeRepeat(arg0, RegExRepeatType.ZeroOrMore);<# }
        if(g2.Matches("Expr", "Expr", "Question"))           {#> res = new RegExNodeRepeat(arg0, RegExRepeatType.ZeroOrOne); <# }
        var g3 = g.WithPrecedenceGroup();
        if(g3.MatchesRight("Expr", "Expr", "Except", "Expr")) {#> res = RegExNodeRanges.Except(arg0, arg2); <# }
    }
        #>
    }
}
```

While this code does not look like much, it translates into the following:

```C#
using System;
using System.Collections.Generic;
using ReCode.RegularExpressions.Parsing.Nodes;

namespace ReCode.RegularExpressions.Parsing {
    public partial class RegExParser {
        
        private class ArrayStack<TElm>
        {
          public readonly TElm[] Elements;
          public int Top;
          [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
          public void Reset() { Top = -1; }
          public ArrayStack(int capacity) { Elements = new TElm[capacity]; Top=-1; }
          [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
          public void Push(TElm elm) { Elements[++Top] = elm; }
          [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
          public void Pop(int count = 1) { Top -= count; }
        }
        const int DefaultStackSize = 50;
        private readonly ArrayStack<int> _stateStack = new ArrayStack<int>(2 * DefaultStackSize);
        private readonly ArrayStack<RegExNode> _valueStack = new ArrayStack<RegExNode>(DefaultStackSize);

        
public enum Token {
  END = 1,
  Phrase = 3,
  Range = 4,
  Star = 5,
  Plus = 6,
  Question = 7,
  ParBegin = 8,
  ParEnd = 9,
  Bar = 10,
  Accept = 11,
  Name = 12,
  Except = 13,
}
public RegExNode Parse() {
  RegExNode res = default(RegExNode);
  var targetTokenType = 0;
  _stateStack.Reset();
  _valueStack.Reset();
  KeyValuePair<Token,RegExNode> currentToken = NextToken();
  _stateStack.Push(0); goto __state_0;

__postReduce:
  /* find on the stack the state where we need to handle post-reduce goto. */
  var gotoState = _stateStack.Elements[_stateStack.Top];
  /* put information on the stack regarding the reduced type. */
  _stateStack.Push(targetTokenType);
  /* jump to the resulting state. */
  switch(gotoState){
  case 0:
    switch(targetTokenType){
    case 2: _stateStack.Push(1); goto __state_1;
    default: goto __syntaxError;
  }
  case 1:
    switch(targetTokenType){
    case 2: _stateStack.Push(6); goto __state_6;
    default: goto __syntaxError;
  }
  case 4:
    switch(targetTokenType){
    case 2: _stateStack.Push(13); goto __state_13;
    default: goto __syntaxError;
  }
  case 6:
    switch(targetTokenType){
    case 2: _stateStack.Push(6); goto __state_6;
    default: goto __syntaxError;
  }
  case 10:
    switch(targetTokenType){
    case 2: _stateStack.Push(18); goto __state_18;
    default: goto __syntaxError;
  }
  case 12:
    switch(targetTokenType){
    case 2: _stateStack.Push(19); goto __state_19;
    default: goto __syntaxError;
  }
  case 13:
    switch(targetTokenType){
    case 2: _stateStack.Push(20); goto __state_20;
    default: goto __syntaxError;
  }
  case 16:
    switch(targetTokenType){
    case 2: _stateStack.Push(28); goto __state_28;
    default: goto __syntaxError;
  }
  case 18:
    switch(targetTokenType){
    case 2: _stateStack.Push(6); goto __state_6;
    default: goto __syntaxError;
  }
  case 19:
    switch(targetTokenType){
    case 2: _stateStack.Push(6); goto __state_6;
    default: goto __syntaxError;
  }
  case 20:
    switch(targetTokenType){
    case 2: _stateStack.Push(20); goto __state_20;
    default: goto __syntaxError;
  }
  case 25:
    switch(targetTokenType){
    case 2: _stateStack.Push(29); goto __state_29;
    default: goto __syntaxError;
  }
  case 27:
    switch(targetTokenType){
    case 2: _stateStack.Push(30); goto __state_30;
    default: goto __syntaxError;
  }
  case 28:
    switch(targetTokenType){
    case 2: _stateStack.Push(20); goto __state_20;
    default: goto __syntaxError;
  }
  case 29:
    switch(targetTokenType){
    case 2: _stateStack.Push(20); goto __state_20;
    default: goto __syntaxError;
  }
  case 30:
    switch(targetTokenType){
    case 2: _stateStack.Push(20); goto __state_20;
    default: goto __syntaxError;
  }
  default:
    goto __syntaxError;
  }

__state_0:
  switch((int)currentToken.Key){
  case 3:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(2); goto __state_2;
  case 4:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(3); goto __state_3;
  case 8:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(4); goto __state_4;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(5); goto __state_5;
  default:
    goto __syntaxError;
  }

__state_1:
  switch((int)currentToken.Key){
  case 1:
  /* accept value from stack */
  return _valueStack.Elements[_valueStack.Top];
  case 3:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(2); goto __state_2;
  case 4:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(3); goto __state_3;
  case 5:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(7); goto __state_7;
  case 6:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(8); goto __state_8;
  case 7:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(9); goto __state_9;
  case 8:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(4); goto __state_4;
  case 10:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(10); goto __state_10;
  case 11:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(11); goto __state_11;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(5); goto __state_5;
  case 13:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(12); goto __state_12;
  default:
    goto __syntaxError;
  }

__state_2:
  switch((int)currentToken.Key){
  case 1: case 3: case 4: case 5: case 6: case 7: case 8: case 10: case 11: case 12: case 13:
  /* do reduce using production 2 */
  goto __action_2;
  default:
    goto __syntaxError;
  }

__state_3:
  switch((int)currentToken.Key){
  case 1: case 3: case 4: case 5: case 6: case 7: case 8: case 10: case 11: case 12: case 13:
  /* do reduce using production 3 */
  goto __action_3;
  default:
    goto __syntaxError;
  }

__state_4:
  switch((int)currentToken.Key){
  case 3:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(14); goto __state_14;
  case 4:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(15); goto __state_15;
  case 8:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(16); goto __state_16;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(17); goto __state_17;
  default:
    goto __syntaxError;
  }

__state_5:
  switch((int)currentToken.Key){
  case 1: case 3: case 4: case 5: case 6: case 7: case 8: case 10: case 11: case 12: case 13:
  /* do reduce using production 4 */
  goto __action_4;
  default:
    goto __syntaxError;
  }

__state_6:
  switch((int)currentToken.Key){
  case 1: case 10: case 13:
  /* do reduce using production 5 */
  goto __action_5;
  case 3:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(2); goto __state_2;
  case 4:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(3); goto __state_3;
  case 5:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(7); goto __state_7;
  case 6:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(8); goto __state_8;
  case 7:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(9); goto __state_9;
  case 8:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(4); goto __state_4;
  case 11:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(11); goto __state_11;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(5); goto __state_5;
  default:
    goto __syntaxError;
  }

__state_7:
  switch((int)currentToken.Key){
  case 1: case 3: case 4: case 5: case 6: case 7: case 8: case 10: case 11: case 12: case 13:
  /* do reduce using production 9 */
  goto __action_9;
  default:
    goto __syntaxError;
  }

__state_8:
  switch((int)currentToken.Key){
  case 1: case 3: case 4: case 5: case 6: case 7: case 8: case 10: case 11: case 12: case 13:
  /* do reduce using production 8 */
  goto __action_8;
  default:
    goto __syntaxError;
  }

__state_9:
  switch((int)currentToken.Key){
  case 1: case 3: case 4: case 5: case 6: case 7: case 8: case 10: case 11: case 12: case 13:
  /* do reduce using production 10 */
  goto __action_10;
  default:
    goto __syntaxError;
  }

__state_10:
  switch((int)currentToken.Key){
  case 3:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(2); goto __state_2;
  case 4:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(3); goto __state_3;
  case 8:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(4); goto __state_4;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(5); goto __state_5;
  default:
    goto __syntaxError;
  }

__state_11:
  switch((int)currentToken.Key){
  case 1: case 3: case 4: case 5: case 6: case 7: case 8: case 10: case 11: case 12: case 13:
  /* do reduce using production 7 */
  goto __action_7;
  default:
    goto __syntaxError;
  }

__state_12:
  switch((int)currentToken.Key){
  case 3:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(2); goto __state_2;
  case 4:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(3); goto __state_3;
  case 8:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(4); goto __state_4;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(5); goto __state_5;
  default:
    goto __syntaxError;
  }

__state_13:
  switch((int)currentToken.Key){
  case 3:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(14); goto __state_14;
  case 4:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(15); goto __state_15;
  case 5:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(21); goto __state_21;
  case 6:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(22); goto __state_22;
  case 7:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(23); goto __state_23;
  case 8:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(16); goto __state_16;
  case 9:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(24); goto __state_24;
  case 10:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(25); goto __state_25;
  case 11:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(26); goto __state_26;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(17); goto __state_17;
  case 13:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(27); goto __state_27;
  default:
    goto __syntaxError;
  }

__state_14:
  switch((int)currentToken.Key){
  case 3: case 4: case 5: case 6: case 7: case 8: case 9: case 10: case 11: case 12: case 13:
  /* do reduce using production 2 */
  goto __action_2;
  default:
    goto __syntaxError;
  }

__state_15:
  switch((int)currentToken.Key){
  case 3: case 4: case 5: case 6: case 7: case 8: case 9: case 10: case 11: case 12: case 13:
  /* do reduce using production 3 */
  goto __action_3;
  default:
    goto __syntaxError;
  }

__state_16:
  switch((int)currentToken.Key){
  case 3:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(14); goto __state_14;
  case 4:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(15); goto __state_15;
  case 8:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(16); goto __state_16;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(17); goto __state_17;
  default:
    goto __syntaxError;
  }

__state_17:
  switch((int)currentToken.Key){
  case 3: case 4: case 5: case 6: case 7: case 8: case 9: case 10: case 11: case 12: case 13:
  /* do reduce using production 4 */
  goto __action_4;
  default:
    goto __syntaxError;
  }

__state_18:
  switch((int)currentToken.Key){
  case 1: case 10: case 13:
  /* do reduce using production 6 */
  goto __action_6;
  case 3:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(2); goto __state_2;
  case 4:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(3); goto __state_3;
  case 5:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(7); goto __state_7;
  case 6:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(8); goto __state_8;
  case 7:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(9); goto __state_9;
  case 8:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(4); goto __state_4;
  case 11:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(11); goto __state_11;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(5); goto __state_5;
  default:
    goto __syntaxError;
  }

__state_19:
  switch((int)currentToken.Key){
  case 1:
  /* do reduce using production 11 */
  goto __action_11;
  case 3:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(2); goto __state_2;
  case 4:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(3); goto __state_3;
  case 5:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(7); goto __state_7;
  case 6:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(8); goto __state_8;
  case 7:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(9); goto __state_9;
  case 8:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(4); goto __state_4;
  case 10:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(10); goto __state_10;
  case 11:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(11); goto __state_11;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(5); goto __state_5;
  case 13:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(12); goto __state_12;
  default:
    goto __syntaxError;
  }

__state_20:
  switch((int)currentToken.Key){
  case 3:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(14); goto __state_14;
  case 4:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(15); goto __state_15;
  case 5:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(21); goto __state_21;
  case 6:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(22); goto __state_22;
  case 7:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(23); goto __state_23;
  case 8:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(16); goto __state_16;
  case 9: case 10: case 13:
  /* do reduce using production 5 */
  goto __action_5;
  case 11:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(26); goto __state_26;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(17); goto __state_17;
  default:
    goto __syntaxError;
  }

__state_21:
  switch((int)currentToken.Key){
  case 3: case 4: case 5: case 6: case 7: case 8: case 9: case 10: case 11: case 12: case 13:
  /* do reduce using production 9 */
  goto __action_9;
  default:
    goto __syntaxError;
  }

__state_22:
  switch((int)currentToken.Key){
  case 3: case 4: case 5: case 6: case 7: case 8: case 9: case 10: case 11: case 12: case 13:
  /* do reduce using production 8 */
  goto __action_8;
  default:
    goto __syntaxError;
  }

__state_23:
  switch((int)currentToken.Key){
  case 3: case 4: case 5: case 6: case 7: case 8: case 9: case 10: case 11: case 12: case 13:
  /* do reduce using production 10 */
  goto __action_10;
  default:
    goto __syntaxError;
  }

__state_24:
  switch((int)currentToken.Key){
  case 1: case 3: case 4: case 5: case 6: case 7: case 8: case 10: case 11: case 12: case 13:
  /* do reduce using production 1 */
  goto __action_1;
  default:
    goto __syntaxError;
  }

__state_25:
  switch((int)currentToken.Key){
  case 3:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(14); goto __state_14;
  case 4:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(15); goto __state_15;
  case 8:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(16); goto __state_16;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(17); goto __state_17;
  default:
    goto __syntaxError;
  }

__state_26:
  switch((int)currentToken.Key){
  case 3: case 4: case 5: case 6: case 7: case 8: case 9: case 10: case 11: case 12: case 13:
  /* do reduce using production 7 */
  goto __action_7;
  default:
    goto __syntaxError;
  }

__state_27:
  switch((int)currentToken.Key){
  case 3:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(14); goto __state_14;
  case 4:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(15); goto __state_15;
  case 8:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(16); goto __state_16;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(17); goto __state_17;
  default:
    goto __syntaxError;
  }

__state_28:
  switch((int)currentToken.Key){
  case 3:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(14); goto __state_14;
  case 4:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(15); goto __state_15;
  case 5:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(21); goto __state_21;
  case 6:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(22); goto __state_22;
  case 7:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(23); goto __state_23;
  case 8:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(16); goto __state_16;
  case 9:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(31); goto __state_31;
  case 10:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(25); goto __state_25;
  case 11:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(26); goto __state_26;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(17); goto __state_17;
  case 13:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(27); goto __state_27;
  default:
    goto __syntaxError;
  }

__state_29:
  switch((int)currentToken.Key){
  case 3:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(14); goto __state_14;
  case 4:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(15); goto __state_15;
  case 5:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(21); goto __state_21;
  case 6:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(22); goto __state_22;
  case 7:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(23); goto __state_23;
  case 8:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(16); goto __state_16;
  case 9: case 10: case 13:
  /* do reduce using production 6 */
  goto __action_6;
  case 11:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(26); goto __state_26;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(17); goto __state_17;
  default:
    goto __syntaxError;
  }

__state_30:
  switch((int)currentToken.Key){
  case 3:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(14); goto __state_14;
  case 4:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(15); goto __state_15;
  case 5:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(21); goto __state_21;
  case 6:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(22); goto __state_22;
  case 7:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(23); goto __state_23;
  case 8:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(16); goto __state_16;
  case 9:
  /* do reduce using production 11 */
  goto __action_11;
  case 10:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(25); goto __state_25;
  case 11:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(26); goto __state_26;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(17); goto __state_17;
  case 13:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(27); goto __state_27;
  default:
    goto __syntaxError;
  }

__state_31:
  switch((int)currentToken.Key){
  case 3: case 4: case 5: case 6: case 7: case 8: case 9: case 10: case 11: case 12: case 13:
  /* do reduce using production 1 */
  goto __action_1;
  default:
    goto __syntaxError;
  }


__action_1:
  {
    res = default(RegExNode);
    var arg0 = _valueStack.Elements[_valueStack.Top-2];
    var arg1 = _valueStack.Elements[_valueStack.Top-1];
    var arg2 = _valueStack.Elements[_valueStack.Top-0];
 res = arg1;   }
  _valueStack.Pop(3);
  _valueStack.Push(res);
  // find goto state and adjust state stack
  _stateStack.Pop(6);
  targetTokenType = 2;
goto __postReduce;

__action_2:
  {
    res = default(RegExNode);
    var arg0 = _valueStack.Elements[_valueStack.Top-0];
 res = arg0;   }
  _valueStack.Pop(1);
  _valueStack.Push(res);
  // find goto state and adjust state stack
  _stateStack.Pop(2);
  targetTokenType = 2;
goto __postReduce;

__action_3:
  {
    res = default(RegExNode);
    var arg0 = _valueStack.Elements[_valueStack.Top-0];
 res = arg0;   }
  _valueStack.Pop(1);
  _valueStack.Push(res);
  // find goto state and adjust state stack
  _stateStack.Pop(2);
  targetTokenType = 2;
goto __postReduce;

__action_4:
  {
    res = default(RegExNode);
    var arg0 = _valueStack.Elements[_valueStack.Top-0];
 res = _namedExpressions[(arg0 as RegExNodeName).Name];   }
  _valueStack.Pop(1);
  _valueStack.Push(res);
  // find goto state and adjust state stack
  _stateStack.Pop(2);
  targetTokenType = 2;
goto __postReduce;

__action_5:
  {
    res = default(RegExNode);
    var arg0 = _valueStack.Elements[_valueStack.Top-1];
    var arg1 = _valueStack.Elements[_valueStack.Top-0];
 res = new RegExNodeConcat(arg0,arg1);   }
  _valueStack.Pop(2);
  _valueStack.Push(res);
  // find goto state and adjust state stack
  _stateStack.Pop(4);
  targetTokenType = 2;
goto __postReduce;

__action_6:
  {
    res = default(RegExNode);
    var arg0 = _valueStack.Elements[_valueStack.Top-2];
    var arg1 = _valueStack.Elements[_valueStack.Top-1];
    var arg2 = _valueStack.Elements[_valueStack.Top-0];
 res = RegExNodeUnion.Of(arg0,arg2);   }
  _valueStack.Pop(3);
  _valueStack.Push(res);
  // find goto state and adjust state stack
  _stateStack.Pop(6);
  targetTokenType = 2;
goto __postReduce;

__action_7:
  {
    res = default(RegExNode);
    var arg0 = _valueStack.Elements[_valueStack.Top-1];
    var arg1 = _valueStack.Elements[_valueStack.Top-0];
 res = new RegExNodeAccept(arg0, (arg1 as RegExNodeAccept).AcceptState);   }
  _valueStack.Pop(2);
  _valueStack.Push(res);
  // find goto state and adjust state stack
  _stateStack.Pop(4);
  targetTokenType = 2;
goto __postReduce;

__action_8:
  {
    res = default(RegExNode);
    var arg0 = _valueStack.Elements[_valueStack.Top-1];
    var arg1 = _valueStack.Elements[_valueStack.Top-0];
 res = new RegExNodeRepeat(arg0, RegExRepeatType.OneOrMore);   }
  _valueStack.Pop(2);
  _valueStack.Push(res);
  // find goto state and adjust state stack
  _stateStack.Pop(4);
  targetTokenType = 2;
goto __postReduce;

__action_9:
  {
    res = default(RegExNode);
    var arg0 = _valueStack.Elements[_valueStack.Top-1];
    var arg1 = _valueStack.Elements[_valueStack.Top-0];
 res = new RegExNodeRepeat(arg0, RegExRepeatType.ZeroOrMore);  }
  _valueStack.Pop(2);
  _valueStack.Push(res);
  // find goto state and adjust state stack
  _stateStack.Pop(4);
  targetTokenType = 2;
goto __postReduce;

__action_10:
  {
    res = default(RegExNode);
    var arg0 = _valueStack.Elements[_valueStack.Top-1];
    var arg1 = _valueStack.Elements[_valueStack.Top-0];
 res = new RegExNodeRepeat(arg0, RegExRepeatType.ZeroOrOne);   }
  _valueStack.Pop(2);
  _valueStack.Push(res);
  // find goto state and adjust state stack
  _stateStack.Pop(4);
  targetTokenType = 2;
goto __postReduce;

__action_11:
  {
    res = default(RegExNode);
    var arg0 = _valueStack.Elements[_valueStack.Top-2];
    var arg1 = _valueStack.Elements[_valueStack.Top-1];
    var arg2 = _valueStack.Elements[_valueStack.Top-0];
 res = RegExNodeRanges.Except(arg0, arg2);   }
  _valueStack.Pop(3);
  _valueStack.Push(res);
  // find goto state and adjust state stack
  _stateStack.Pop(6);
  targetTokenType = 2;
goto __postReduce;
  __syntaxError:
  throw new Exception("Syntax error");
}
    }
}
```