# ReCode
*The Fast Regular Expression compiler for C#.*


## Introduction

Inspired by **re2c**, this library can be referenced from a T4 text template for the purpose of generating
code for parallel matching of a block of regular expressions.

Supported inputs for matching are pointers to a sequence of bytes or chars. This enables generation fast tokenizers for almost any
purpose (even binary data).


## Usage

**ReCode** is enabled in a text template by referencing the dll:
```c#
<#@ assembly name="$(SolutionDir)packages\\ReCode-1.0\\ReCode.dll" #>
<#@ import namespace="ReCode" #>
```

## Example

As an example of a compiled block of expressions, consider the following example:

```c#
<#@ template debug="false" hostspecific="false" language="C#" #>
<#@ assembly name="$(SolutionDir)packages\\ReCode-1.0\\ReCode.dll" #>
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

ReCode internally uses a pre-compiled version of ReCode for constructing the scanner for the 
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
if(pNext >= pLimit) goto nonaccept5;
var current = *pNext++;
if(current < 0x31) /* ('0') '1' */  {
    if(current < 0x2D) /* (',') '-' */  {
        if(current < 0x2B) /* ('*') '+' */ 
            goto nonaccept5;
        if(current < 0x2C) /* ('+') ',' */ 
            goto state5_1;
        goto nonaccept5;
    }
    if(current < 0x2F) /* ('.') '/' */  {
        if(current < 0x2E) /* ('-') '.' */ 
            goto state5_1;
        goto state5_2;
    }
    if(current < 0x30) /* ('/') '0' */ 
        goto nonaccept5;
    goto state5_3;
}
if(current < 0x36) /* ('5') '6' */  {
    if(current < 0x33) /* ('2') '3' */  {
        if(current < 0x32) /* ('1') '2' */ 
            goto state5_4;
        goto state5_5;
    }
    if(current < 0x35) /* ('4') '5' */ 
        goto state5_6;
    goto state5_7;
}
if(current < 0x38) /* ('7') '8' */  {
    if(current < 0x37) /* ('6') '7' */ 
        goto state5_6;
    goto state5_8;
}
if(current < 0x3A) /* ('9') ':' */ 
    goto state5_6;
goto nonaccept5;
/*
 * DFA STATE 1
 * '.' -> 2
 * ['0'-'9'] -> 6
 */
state5_1:
if(pNext >= pLimit) goto nonaccept5;
current = *pNext++;
if(current < 0x2F) /* ('.') '/' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto nonaccept5;
    goto state5_2;
}
if(current < 0x30) /* ('/') '0' */ 
    goto nonaccept5;
if(current < 0x3A) /* ('9') ':' */ 
    goto state5_6;
goto nonaccept5;
/*
 * DFA STATE 2
 * ['0'-'9'] -> 9
 */
state5_2:
if(pNext >= pLimit) goto nonaccept5;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */ 
    goto nonaccept5;
if(current < 0x3A) /* ('9') ':' */ 
    goto state5_9;
goto nonaccept5;
/*
 * DFA STATE 3 (accepts to 0)
 */
state5_3:
pEnd = pNext;
goto accept5_0;
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
state5_4:
pEnd = pNext;
if(pNext >= pLimit) goto accept5_12;
current = *pNext++;
if(current < 0x33) /* ('2') '3' */  {
    if(current < 0x30) /* ('/') '0' */  {
        if(current < 0x2E) /* ('-') '.' */ 
            goto accept5_12;
        if(current < 0x2F) /* ('.') '/' */ 
            goto state5_10;
        goto accept5_12;
    }
    if(current < 0x31) /* ('0') '1' */ 
        goto state5_11;
    if(current < 0x32) /* ('1') '2' */ 
        goto state5_6;
    goto state5_12;
}
if(current < 0x37) /* ('6') '7' */  {
    if(current < 0x35) /* ('4') '5' */ 
        goto state5_6;
    if(current < 0x36) /* ('5') '6' */ 
        goto state5_13;
    goto state5_6;
}
if(current < 0x38) /* ('7') '8' */ 
    goto state5_14;
if(current < 0x3A) /* ('9') ':' */ 
    goto state5_6;
goto accept5_12;
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
state5_5:
pEnd = pNext;
if(pNext >= pLimit) goto accept5_12;
current = *pNext++;
if(current < 0x32) /* ('1') '2' */  {
    if(current < 0x2F) /* ('.') '/' */  {
        if(current < 0x2E) /* ('-') '.' */ 
            goto accept5_12;
        goto state5_10;
    }
    if(current < 0x30) /* ('/') '0' */ 
        goto accept5_12;
    if(current < 0x31) /* ('0') '1' */ 
        goto state5_15;
    goto state5_6;
}
if(current < 0x35) /* ('4') '5' */  {
    if(current < 0x33) /* ('2') '3' */ 
        goto state5_16;
    goto state5_6;
}
if(current < 0x36) /* ('5') '6' */ 
    goto state5_17;
if(current < 0x3A) /* ('9') ':' */ 
    goto state5_6;
goto accept5_12;
/*
 * DFA STATE 6 (accepts to 12)
 * '.' -> 10
 * ['0'-'9'] -> 6
 */
state5_6:
pEnd = pNext;
if(pNext >= pLimit) goto accept5_12;
current = *pNext++;
if(current < 0x2F) /* ('.') '/' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto accept5_12;
    goto state5_10;
}
if(current < 0x30) /* ('/') '0' */ 
    goto accept5_12;
if(current < 0x3A) /* ('9') ':' */ 
    goto state5_6;
goto accept5_12;
/*
 * DFA STATE 7 (accepts to 12)
 * '.' -> 10
 * '0' -> 18
 * ['1'-'9'] -> 6
 */
state5_7:
pEnd = pNext;
if(pNext >= pLimit) goto accept5_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto accept5_12;
    if(current < 0x2F) /* ('.') '/' */ 
        goto state5_10;
    goto accept5_12;
}
if(current < 0x31) /* ('0') '1' */ 
    goto state5_18;
if(current < 0x3A) /* ('9') ':' */ 
    goto state5_6;
goto accept5_12;
/*
 * DFA STATE 8 (accepts to 12)
 * '.' -> 10
 * ['0'-'4'] -> 6
 * '5' -> 19
 * ['6'-'9'] -> 6
 */
state5_8:
pEnd = pNext;
if(pNext >= pLimit) goto accept5_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto accept5_12;
    if(current < 0x2F) /* ('.') '/' */ 
        goto state5_10;
    goto accept5_12;
}
if(current < 0x36) /* ('5') '6' */  {
    if(current < 0x35) /* ('4') '5' */ 
        goto state5_6;
    goto state5_19;
}
if(current < 0x3A) /* ('9') ':' */ 
    goto state5_6;
goto accept5_12;
/*
 * DFA STATE 9 (accepts to 11)
 * ['0'-'9'] -> 9
 * 'E' -> 20
 */
state5_9:
pEnd = pNext;
if(pNext >= pLimit) goto accept5_11;
current = *pNext++;
if(current < 0x3A) /* ('9') ':' */  {
    if(current < 0x30) /* ('/') '0' */ 
        goto accept5_11;
    goto state5_9;
}
if(current < 0x45) /* ('D') 'E' */ 
    goto accept5_11;
if(current < 0x46) /* ('E') 'F' */ 
    goto state5_20;
goto accept5_11;
/*
 * DFA STATE 10
 * ['0'-'9'] -> 9
 */
state5_10:
if(pNext >= pLimit) goto accept5_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */ 
    goto accept5_12;
if(current < 0x3A) /* ('9') ':' */ 
    goto state5_9;
goto accept5_12;
/*
 * DFA STATE 11 (accepts to 12)
 * '.' -> 10
 * '0' -> 21
 * ['1'-'9'] -> 6
 */
state5_11:
pEnd = pNext;
if(pNext >= pLimit) goto accept5_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto accept5_12;
    if(current < 0x2F) /* ('.') '/' */ 
        goto state5_10;
    goto accept5_12;
}
if(current < 0x31) /* ('0') '1' */ 
    goto state5_21;
if(current < 0x3A) /* ('9') ':' */ 
    goto state5_6;
goto accept5_12;
/*
 * DFA STATE 12 (accepts to 12)
 * '.' -> 10
 * ['0'-'4'] -> 6
 * '5' -> 22
 * ['6'-'9'] -> 6
 */
state5_12:
pEnd = pNext;
if(pNext >= pLimit) goto accept5_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto accept5_12;
    if(current < 0x2F) /* ('.') '/' */ 
        goto state5_10;
    goto accept5_12;
}
if(current < 0x36) /* ('5') '6' */  {
    if(current < 0x35) /* ('4') '5' */ 
        goto state5_6;
    goto state5_22;
}
if(current < 0x3A) /* ('9') ':' */ 
    goto state5_6;
goto accept5_12;
/*
 * DFA STATE 13 (accepts to 12)
 * '.' -> 10
 * '0' -> 23
 * ['1'-'9'] -> 6
 */
state5_13:
pEnd = pNext;
if(pNext >= pLimit) goto accept5_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto accept5_12;
    if(current < 0x2F) /* ('.') '/' */ 
        goto state5_10;
    goto accept5_12;
}
if(current < 0x31) /* ('0') '1' */ 
    goto state5_23;
if(current < 0x3A) /* ('9') ':' */ 
    goto state5_6;
goto accept5_12;
/*
 * DFA STATE 14 (accepts to 12)
 * '.' -> 10
 * ['0'-'4'] -> 6
 * '5' -> 24
 * ['6'-'9'] -> 6
 */
state5_14:
pEnd = pNext;
if(pNext >= pLimit) goto accept5_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto accept5_12;
    if(current < 0x2F) /* ('.') '/' */ 
        goto state5_10;
    goto accept5_12;
}
if(current < 0x36) /* ('5') '6' */  {
    if(current < 0x35) /* ('4') '5' */ 
        goto state5_6;
    goto state5_24;
}
if(current < 0x3A) /* ('9') ':' */ 
    goto state5_6;
goto accept5_12;
/*
 * DFA STATE 15 (accepts to 12)
 * '.' -> 10
 * '0' -> 25
 * ['1'-'9'] -> 6
 */
state5_15:
pEnd = pNext;
if(pNext >= pLimit) goto accept5_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto accept5_12;
    if(current < 0x2F) /* ('.') '/' */ 
        goto state5_10;
    goto accept5_12;
}
if(current < 0x31) /* ('0') '1' */ 
    goto state5_25;
if(current < 0x3A) /* ('9') ':' */ 
    goto state5_6;
goto accept5_12;
/*
 * DFA STATE 16 (accepts to 12)
 * '.' -> 10
 * ['0'-'4'] -> 6
 * '5' -> 26
 * ['6'-'9'] -> 6
 */
state5_16:
pEnd = pNext;
if(pNext >= pLimit) goto accept5_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto accept5_12;
    if(current < 0x2F) /* ('.') '/' */ 
        goto state5_10;
    goto accept5_12;
}
if(current < 0x36) /* ('5') '6' */  {
    if(current < 0x35) /* ('4') '5' */ 
        goto state5_6;
    goto state5_26;
}
if(current < 0x3A) /* ('9') ':' */ 
    goto state5_6;
goto accept5_12;
/*
 * DFA STATE 17 (accepts to 1)
 */
state5_17:
pEnd = pNext;
goto accept5_1;
/*
 * DFA STATE 18 (accepts to 2)
 */
state5_18:
pEnd = pNext;
goto accept5_2;
/*
 * DFA STATE 19 (accepts to 3)
 */
state5_19:
pEnd = pNext;
goto accept5_3;
/*
 * DFA STATE 20
 * '+' -> 27
 * '-' -> 27
 * ['0'-'9'] -> 28
 */
state5_20:
if(pNext >= pLimit) goto accept5_11;
current = *pNext++;
if(current < 0x2D) /* (',') '-' */  {
    if(current < 0x2B) /* ('*') '+' */ 
        goto accept5_11;
    if(current < 0x2C) /* ('+') ',' */ 
        goto state5_27;
    goto accept5_11;
}
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto state5_27;
    goto accept5_11;
}
if(current < 0x3A) /* ('9') ':' */ 
    goto state5_28;
goto accept5_11;
/*
 * DFA STATE 21 (accepts to 4)
 */
state5_21:
pEnd = pNext;
goto accept5_4;
/*
 * DFA STATE 22 (accepts to 5)
 */
state5_22:
pEnd = pNext;
goto accept5_5;
/*
 * DFA STATE 23 (accepts to 6)
 */
state5_23:
pEnd = pNext;
goto accept5_6;
/*
 * DFA STATE 24 (accepts to 7)
 */
state5_24:
pEnd = pNext;
goto accept5_7;
/*
 * DFA STATE 25 (accepts to 8)
 */
state5_25:
pEnd = pNext;
goto accept5_8;
/*
 * DFA STATE 26 (accepts to 9)
 */
state5_26:
pEnd = pNext;
goto accept5_9;
/*
 * DFA STATE 27
 * ['0'-'9'] -> 28
 */
state5_27:
if(pNext >= pLimit) goto accept5_11;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */ 
    goto accept5_11;
if(current < 0x3A) /* ('9') ':' */ 
    goto state5_28;
goto accept5_11;
/*
 * DFA STATE 28 (accepts to 10)
 * ['0'-'9'] -> 28
 */
state5_28:
pEnd = pNext;
if(pNext >= pLimit) goto accept5_10;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */ 
    goto accept5_10;
if(current < 0x3A) /* ('9') ':' */ 
    goto state5_28;
goto accept5_10;


accept5_0:
 return new KeyValuePair<ushort,int>(0, (int)(pEnd - start)); 
accept5_1:
 return new KeyValuePair<ushort,int>(1, (int)(pEnd - start)); 
accept5_2:
 return new KeyValuePair<ushort,int>(2, (int)(pEnd - start)); 
accept5_3:
 return new KeyValuePair<ushort,int>(3, (int)(pEnd - start)); 
accept5_4:
 return new KeyValuePair<ushort,int>(4, (int)(pEnd - start)); 
accept5_5:
 return new KeyValuePair<ushort,int>(5, (int)(pEnd - start)); 
accept5_6:
 return new KeyValuePair<ushort,int>(6, (int)(pEnd - start)); 
accept5_7:
 return new KeyValuePair<ushort,int>(7, (int)(pEnd - start)); 
accept5_8:
 return new KeyValuePair<ushort,int>(8, (int)(pEnd - start)); 
accept5_9:
 return new KeyValuePair<ushort,int>(9, (int)(pEnd - start)); 
accept5_10:
 return new KeyValuePair<ushort,int>(10, (int)(pEnd - start)); 
accept5_11:
 return new KeyValuePair<ushort,int>(11, (int)(pEnd - start)); 
accept5_12:
 return new KeyValuePair<ushort,int>(12, (int)(pEnd - start)); 
nonaccept5:
 return null;         }
    }
}
```
