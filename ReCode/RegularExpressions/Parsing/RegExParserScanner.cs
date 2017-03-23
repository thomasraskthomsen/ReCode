using System;
using System.Text;
using System.Collections.Generic;
using ReCode.RegularExpressions.Parsing.Nodes;

namespace ReCode.RegularExpressions.Parsing 
{
	public partial class RegExParser {
		public unsafe KeyValuePair<Token, RegExNode> NextToken(){
            fixed(char * ptr = _exp){
                var pLimit = ptr + _exp.Length;
                
                skip:
                {
                    var pStart = ptr + _position;
                    var pNext = pStart;
                    var pEnd = pStart; 
                    if(pNext >= pLimit) return new KeyValuePair<Token, RegExNode>(Token.END, null);
                    {
/*
 * Union
 *  +-Accept(0)
 *  |  +-Ranges(''')
 *  +-Accept(1)
 *  |  +-Ranges('"')
 *  +-Accept(2)
 *  |  +-Ranges('|')
 *  +-Accept(3)
 *  |  +-Ranges('(')
 *  +-Accept(4)
 *  |  +-Ranges(')')
 *  +-Accept(5)
 *  |  +-Ranges('+')
 *  +-Accept(6)
 *  |  +-Ranges('?')
 *  +-Accept(7)
 *  |  +-Ranges('*')
 *  +-Accept(8)
 *  |  +-Ranges('\')
 *  +-Accept(9)
 *  |  +-Ranges('[')
 *  +-Accept(10)
 *  |  +-Repeat(OneOrMore)
 *  |     +-Ranges(' ')
 *  +-Accept(11)
 *  |  +-Concat
 *  |     +-Ranges('~')
 *  |     +-Repeat(OneOrMore)
 *  |        +-Ranges(['0'-'9'])
 *  +-Accept(12)
 *     +-Concat
 *        +-Ranges(['A'-'Z'],['a'-'z'])
 *        +-Repeat(ZeroOrMore)
 *           +-Ranges(['A'-'Z'],['a'-'z'],['0'-'9'])
 */
/*
 * DFA STATE 0
 * ' ' -> 1
 * '"' -> 2
 * ''' -> 3
 * '(' -> 4
 * ')' -> 5
 * '*' -> 6
 * '+' -> 7
 * '?' -> 8
 * ['A'-'Z'] -> 9
 * '[' -> 10
 * '\' -> 11
 * ['a'-'z'] -> 9
 * '|' -> 12
 * '~' -> 13
 */
if(pNext >= pLimit) goto nonaccept22;
var current = *pNext++;
if(current < 0x3F) /* ('>') '?' */  {
    if(current < 0x27) /* ('&') ''' */  {
        if(current < 0x21) /* (' ') '!' */  {
            if(current < 0x20)
                goto nonaccept22;
            goto state22_1;
        }
        if(current < 0x22) /* ('!') '"' */ 
            goto nonaccept22;
        if(current < 0x23) /* ('"') '#' */ 
            goto state22_2;
        goto nonaccept22;
    }
    if(current < 0x2A) /* (')') '*' */  {
        if(current < 0x28) /* (''') '(' */ 
            goto state22_3;
        if(current < 0x29) /* ('(') ')' */ 
            goto state22_4;
        goto state22_5;
    }
    if(current < 0x2B) /* ('*') '+' */ 
        goto state22_6;
    if(current < 0x2C) /* ('+') ',' */ 
        goto state22_7;
    goto nonaccept22;
}
if(current < 0x61) /* ('`') 'a' */  {
    if(current < 0x5B) /* ('Z') '[' */  {
        if(current < 0x40) /* ('?') '@' */ 
            goto state22_8;
        if(current < 0x41) /* ('@') 'A' */ 
            goto nonaccept22;
        goto state22_9;
    }
    if(current < 0x5C) /* ('[') '\' */ 
        goto state22_10;
    if(current < 0x5D) /* ('\') ']' */ 
        goto state22_11;
    goto nonaccept22;
}
if(current < 0x7D) /* ('|') '}' */  {
    if(current < 0x7B) /* ('z') '{' */ 
        goto state22_9;
    if(current < 0x7C) /* ('{') '|' */ 
        goto nonaccept22;
    goto state22_12;
}
if(current < 0x7E) /* ('}') '~' */ 
    goto nonaccept22;
if(current < 0x7F)
    goto state22_13;
goto nonaccept22;
/*
 * DFA STATE 1 (accepts to 10)
 * ' ' -> 1
 */
state22_1:
pEnd = pNext;
if(pNext >= pLimit) goto accept22_10;
current = *pNext++;
if(current < 0x20)
    goto accept22_10;
if(current < 0x21) /* (' ') '!' */ 
    goto state22_1;
goto accept22_10;
/*
 * DFA STATE 2 (accepts to 1)
 */
state22_2:
pEnd = pNext;
goto accept22_1;
/*
 * DFA STATE 3 (accepts to 0)
 */
state22_3:
pEnd = pNext;
goto accept22_0;
/*
 * DFA STATE 4 (accepts to 3)
 */
state22_4:
pEnd = pNext;
goto accept22_3;
/*
 * DFA STATE 5 (accepts to 4)
 */
state22_5:
pEnd = pNext;
goto accept22_4;
/*
 * DFA STATE 6 (accepts to 7)
 */
state22_6:
pEnd = pNext;
goto accept22_7;
/*
 * DFA STATE 7 (accepts to 5)
 */
state22_7:
pEnd = pNext;
goto accept22_5;
/*
 * DFA STATE 8 (accepts to 6)
 */
state22_8:
pEnd = pNext;
goto accept22_6;
/*
 * DFA STATE 9 (accepts to 12)
 * ['0'-'9'] -> 14
 * ['A'-'Z'] -> 14
 * ['a'-'z'] -> 14
 */
state22_9:
pEnd = pNext;
if(pNext >= pLimit) goto accept22_12;
current = *pNext++;
if(current < 0x41) /* ('@') 'A' */  {
    if(current < 0x30) /* ('/') '0' */ 
        goto accept22_12;
    if(current < 0x3A) /* ('9') ':' */ 
        goto state22_14;
    goto accept22_12;
}
if(current < 0x61) /* ('`') 'a' */  {
    if(current < 0x5B) /* ('Z') '[' */ 
        goto state22_14;
    goto accept22_12;
}
if(current < 0x7B) /* ('z') '{' */ 
    goto state22_14;
goto accept22_12;
/*
 * DFA STATE 10 (accepts to 9)
 */
state22_10:
pEnd = pNext;
goto accept22_9;
/*
 * DFA STATE 11 (accepts to 8)
 */
state22_11:
pEnd = pNext;
goto accept22_8;
/*
 * DFA STATE 12 (accepts to 2)
 */
state22_12:
pEnd = pNext;
goto accept22_2;
/*
 * DFA STATE 13
 * ['0'-'9'] -> 15
 */
state22_13:
if(pNext >= pLimit) goto nonaccept22;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */ 
    goto nonaccept22;
if(current < 0x3A) /* ('9') ':' */ 
    goto state22_15;
goto nonaccept22;
/*
 * DFA STATE 14 (accepts to 12)
 * ['0'-'9'] -> 14
 * ['A'-'Z'] -> 14
 * ['a'-'z'] -> 14
 */
state22_14:
pEnd = pNext;
if(pNext >= pLimit) goto accept22_12;
current = *pNext++;
if(current < 0x41) /* ('@') 'A' */  {
    if(current < 0x30) /* ('/') '0' */ 
        goto accept22_12;
    if(current < 0x3A) /* ('9') ':' */ 
        goto state22_14;
    goto accept22_12;
}
if(current < 0x61) /* ('`') 'a' */  {
    if(current < 0x5B) /* ('Z') '[' */ 
        goto state22_14;
    goto accept22_12;
}
if(current < 0x7B) /* ('z') '{' */ 
    goto state22_14;
goto accept22_12;
/*
 * DFA STATE 15 (accepts to 11)
 * ['0'-'9'] -> 15
 */
state22_15:
pEnd = pNext;
if(pNext >= pLimit) goto accept22_11;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */ 
    goto accept22_11;
if(current < 0x3A) /* ('9') ':' */ 
    goto state22_15;
goto accept22_11;


accept22_0:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); pNext = pEnd; goto incaseString; 
accept22_1:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); pNext = pEnd; goto caseString; 
accept22_2:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.Bar, null); 
accept22_3:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.ParBegin, null); 
accept22_4:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.ParEnd, null); 
accept22_5:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.Plus, null); 
accept22_6:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.Question, null); 
accept22_7:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.Star, null); 
accept22_8:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.Except, null); 
accept22_9:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); pNext = pEnd; goto range; 
accept22_10:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); pNext = pEnd; goto skip; 
accept22_11:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.Accept, new RegExNodeAccept(null, ushort.Parse(new string(pStart+1, 0, ((int)(pEnd-pStart))-1)))); 
accept22_12:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.Name, new RegExNodeName(new string(pStart, 0, (int)(pEnd-pStart)))); 
nonaccept22:
 throw new Exception("Syntax error."); }
                }


                incaseString:
                {
                    var sb = new StringBuilder();
                    nextIncaseChar:
                    var pStart = ptr + _position;
                    var pNext = pStart;
                    var pEnd = pStart; 
                    {
/*
 * Union
 *  +-Accept(0)
 *  |  +-Ranges(''')
 *  +-Accept(1)
 *  |  +-Concat
 *  |     +-Ranges('\')
 *  |     +-Ranges('t')
 *  +-Accept(2)
 *  |  +-Concat
 *  |     +-Ranges('\')
 *  |     +-Ranges('n')
 *  +-Accept(3)
 *  |  +-Concat
 *  |     +-Ranges('\')
 *  |     +-Ranges('r')
 *  +-Accept(4)
 *  |  +-Concat
 *  |     +-Ranges('\')
 *  |     +-Ranges([0x00-0xFFFF])
 *  +-Accept(5)
 *     +-Ranges([0x00-0xFFFF])
 */
/*
 * DFA STATE 0
 * [0x00-'&'] -> 1
 * ''' -> 2
 * ['('-'['] -> 1
 * '\' -> 3
 * [']'-0xFFFF] -> 1
 */
if(pNext >= pLimit) goto nonaccept23;
var current = *pNext++;
if(current < 0x28) /* (''') '(' */  {
    if(current < 0x27) /* ('&') ''' */ 
        goto state23_1;
    goto state23_2;
}
if(current < 0x5C) /* ('[') '\' */ 
    goto state23_1;
if(current < 0x5D) /* ('\') ']' */ 
    goto state23_3;
goto state23_1;
/*
 * DFA STATE 1 (accepts to 5)
 */
state23_1:
pEnd = pNext;
goto accept23_5;
/*
 * DFA STATE 2 (accepts to 0)
 */
state23_2:
pEnd = pNext;
goto accept23_0;
/*
 * DFA STATE 3 (accepts to 5)
 * [0x00-'m'] -> 4
 * 'n' -> 5
 * ['o'-'q'] -> 4
 * 'r' -> 6
 * 's' -> 4
 * 't' -> 7
 * ['u'-0xFFFF] -> 4
 */
state23_3:
pEnd = pNext;
if(pNext >= pLimit) goto accept23_5;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */  {
    if(current < 0x6E) /* ('m') 'n' */ 
        goto state23_4;
    if(current < 0x6F) /* ('n') 'o' */ 
        goto state23_5;
    goto state23_4;
}
if(current < 0x74) /* ('s') 't' */  {
    if(current < 0x73) /* ('r') 's' */ 
        goto state23_6;
    goto state23_4;
}
if(current < 0x75) /* ('t') 'u' */ 
    goto state23_7;
goto state23_4;
/*
 * DFA STATE 4 (accepts to 4)
 */
state23_4:
pEnd = pNext;
goto accept23_4;
/*
 * DFA STATE 5 (accepts to 2)
 */
state23_5:
pEnd = pNext;
goto accept23_2;
/*
 * DFA STATE 6 (accepts to 3)
 */
state23_6:
pEnd = pNext;
goto accept23_3;
/*
 * DFA STATE 7 (accepts to 1)
 */
state23_7:
pEnd = pNext;
goto accept23_1;


accept23_0:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.Phrase, new RegExNodeSequence(sb.ToString(), RegExCasing.Insensitive)); 
accept23_1:
 pNext = pEnd;
 sb.Append((char)9); _position = (int)(pEnd-ptr); goto nextIncaseChar;  
accept23_2:
 pNext = pEnd;
 sb.Append((char)10); _position = (int)(pEnd-ptr); goto nextIncaseChar; 
accept23_3:
 pNext = pEnd;
 sb.Append((char)13); _position = (int)(pEnd-ptr); goto nextIncaseChar; 
accept23_4:
 pNext = pEnd;
 sb.Append(pStart[1]); _position = (int)(pEnd-ptr); goto nextIncaseChar; 
accept23_5:
 pNext = pEnd;
 sb.Append(pStart[0]); _position = (int)(pEnd-ptr); goto nextIncaseChar; 
nonaccept23:
 throw new Exception("Syntax error."); }
                }
                caseString:
                {
                    var sb = new StringBuilder();
                    nextCaseChar:
                    var pStart = ptr + _position;
                    var pNext = pStart;
                    var pEnd = pStart; 
                    {
/*
 * Union
 *  +-Accept(0)
 *  |  +-Ranges('"')
 *  +-Accept(1)
 *  |  +-Concat
 *  |     +-Ranges('\')
 *  |     +-Ranges('t')
 *  +-Accept(2)
 *  |  +-Concat
 *  |     +-Ranges('\')
 *  |     +-Ranges('n')
 *  +-Accept(3)
 *  |  +-Concat
 *  |     +-Ranges('\')
 *  |     +-Ranges('r')
 *  +-Accept(4)
 *  |  +-Concat
 *  |     +-Ranges('\')
 *  |     +-Ranges([0x00-0xFFFF])
 *  +-Accept(5)
 *     +-Ranges([0x00-0xFFFF])
 */
/*
 * DFA STATE 0
 * [0x00-'!'] -> 1
 * '"' -> 2
 * ['#'-'['] -> 1
 * '\' -> 3
 * [']'-0xFFFF] -> 1
 */
if(pNext >= pLimit) goto nonaccept24;
var current = *pNext++;
if(current < 0x23) /* ('"') '#' */  {
    if(current < 0x22) /* ('!') '"' */ 
        goto state24_1;
    goto state24_2;
}
if(current < 0x5C) /* ('[') '\' */ 
    goto state24_1;
if(current < 0x5D) /* ('\') ']' */ 
    goto state24_3;
goto state24_1;
/*
 * DFA STATE 1 (accepts to 5)
 */
state24_1:
pEnd = pNext;
goto accept24_5;
/*
 * DFA STATE 2 (accepts to 0)
 */
state24_2:
pEnd = pNext;
goto accept24_0;
/*
 * DFA STATE 3 (accepts to 5)
 * [0x00-'m'] -> 4
 * 'n' -> 5
 * ['o'-'q'] -> 4
 * 'r' -> 6
 * 's' -> 4
 * 't' -> 7
 * ['u'-0xFFFF] -> 4
 */
state24_3:
pEnd = pNext;
if(pNext >= pLimit) goto accept24_5;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */  {
    if(current < 0x6E) /* ('m') 'n' */ 
        goto state24_4;
    if(current < 0x6F) /* ('n') 'o' */ 
        goto state24_5;
    goto state24_4;
}
if(current < 0x74) /* ('s') 't' */  {
    if(current < 0x73) /* ('r') 's' */ 
        goto state24_6;
    goto state24_4;
}
if(current < 0x75) /* ('t') 'u' */ 
    goto state24_7;
goto state24_4;
/*
 * DFA STATE 4 (accepts to 4)
 */
state24_4:
pEnd = pNext;
goto accept24_4;
/*
 * DFA STATE 5 (accepts to 2)
 */
state24_5:
pEnd = pNext;
goto accept24_2;
/*
 * DFA STATE 6 (accepts to 3)
 */
state24_6:
pEnd = pNext;
goto accept24_3;
/*
 * DFA STATE 7 (accepts to 1)
 */
state24_7:
pEnd = pNext;
goto accept24_1;


accept24_0:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.Phrase, new RegExNodeSequence(sb.ToString(), RegExCasing.Sensitive)); 
accept24_1:
 pNext = pEnd;
 sb.Append((char)9); _position = (int)(pEnd-ptr); goto nextCaseChar;  
accept24_2:
 pNext = pEnd;
 sb.Append((char)10); _position = (int)(pEnd-ptr); goto nextCaseChar; 
accept24_3:
 pNext = pEnd;
 sb.Append((char)13); _position = (int)(pEnd-ptr); goto nextCaseChar; 
accept24_4:
 pNext = pEnd;
 sb.Append(pStart[1]); _position = (int)(pEnd-ptr); goto nextCaseChar; 
accept24_5:
 pNext = pEnd;
 sb.Append(pStart[0]); _position = (int)(pEnd-ptr); goto nextCaseChar; 
nonaccept24:
 throw new Exception("Syntax error."); }
                }
                range:
                {   
                    var ranges = new List<RegExInputRange>();
                    nextRangeChar:
                    var pStart = ptr + _position;
                    var pNext = pStart;
                    var pEnd = pStart; 
                    {
/*
 * Union
 *  +-Accept(0)
 *  |  +-Ranges(']')
 *  +-Accept(1)
 *  |  +-Concat
 *  |     +-Ranges('\')
 *  |     +-Ranges('t')
 *  +-Accept(2)
 *  |  +-Concat
 *  |     +-Ranges('\')
 *  |     +-Ranges('n')
 *  +-Accept(3)
 *  |  +-Concat
 *  |     +-Ranges('\')
 *  |     +-Ranges('r')
 *  +-Accept(4)
 *  |  +-Concat
 *  |     +-Ranges('\')
 *  |     +-Ranges([0x00-0xFFFF])
 *  +-Accept(5)
 *  |  +-Concat
 *  |     +-Ranges([0x00-0xFFFF])
 *  |     +-Sequence(CaseInsensitive,'-')
 *  |     +-Ranges([0x00-0xFFFF])
 *  +-Accept(6)
 *  |  +-Ranges('.')
 *  +-Accept(7)
 *     +-Ranges([0x00-0xFFFF])
 */
/*
 * DFA STATE 0
 * [0x00-'-'] -> 1
 * '.' -> 2
 * ['/'-'['] -> 1
 * '\' -> 3
 * ']' -> 4
 * ['^'-0xFFFF] -> 1
 */
if(pNext >= pLimit) goto nonaccept25;
var current = *pNext++;
if(current < 0x5C) /* ('[') '\' */  {
    if(current < 0x2E) /* ('-') '.' */ 
        goto state25_1;
    if(current < 0x2F) /* ('.') '/' */ 
        goto state25_2;
    goto state25_1;
}
if(current < 0x5D) /* ('\') ']' */ 
    goto state25_3;
if(current < 0x5E) /* (']') '^' */ 
    goto state25_4;
goto state25_1;
/*
 * DFA STATE 1 (accepts to 7)
 * '-' -> 5
 */
state25_1:
pEnd = pNext;
if(pNext >= pLimit) goto accept25_7;
current = *pNext++;
if(current < 0x2D) /* (',') '-' */ 
    goto accept25_7;
if(current < 0x2E) /* ('-') '.' */ 
    goto state25_5;
goto accept25_7;
/*
 * DFA STATE 2 (accepts to 6)
 * '-' -> 6
 */
state25_2:
pEnd = pNext;
if(pNext >= pLimit) goto accept25_6;
current = *pNext++;
if(current < 0x2D) /* (',') '-' */ 
    goto accept25_6;
if(current < 0x2E) /* ('-') '.' */ 
    goto state25_6;
goto accept25_6;
/*
 * DFA STATE 3 (accepts to 7)
 * [0x00-','] -> 7
 * '-' -> 8
 * ['.'-'m'] -> 7
 * 'n' -> 9
 * ['o'-'q'] -> 7
 * 'r' -> 10
 * 's' -> 7
 * 't' -> 11
 * ['u'-0xFFFF] -> 7
 */
state25_3:
pEnd = pNext;
if(pNext >= pLimit) goto accept25_7;
current = *pNext++;
if(current < 0x6F) /* ('n') 'o' */  {
    if(current < 0x2E) /* ('-') '.' */  {
        if(current < 0x2D) /* (',') '-' */ 
            goto state25_7;
        goto state25_8;
    }
    if(current < 0x6E) /* ('m') 'n' */ 
        goto state25_7;
    goto state25_9;
}
if(current < 0x73) /* ('r') 's' */  {
    if(current < 0x72) /* ('q') 'r' */ 
        goto state25_7;
    goto state25_10;
}
if(current < 0x74) /* ('s') 't' */ 
    goto state25_7;
if(current < 0x75) /* ('t') 'u' */ 
    goto state25_11;
goto state25_7;
/*
 * DFA STATE 4 (accepts to 0)
 */
state25_4:
pEnd = pNext;
goto accept25_0;
/*
 * DFA STATE 5
 * [0x00-0xFFFF] -> 12
 */
state25_5:
if(pNext >= pLimit) goto accept25_7;
current = *pNext++;
goto state25_12;
/*
 * DFA STATE 6
 * [0x00-0xFFFF] -> 12
 */
state25_6:
if(pNext >= pLimit) goto accept25_6;
current = *pNext++;
goto state25_12;
/*
 * DFA STATE 7 (accepts to 4)
 */
state25_7:
pEnd = pNext;
goto accept25_4;
/*
 * DFA STATE 8 (accepts to 4)
 */
state25_8:
pEnd = pNext;
goto accept25_4;
/*
 * DFA STATE 9 (accepts to 2)
 */
state25_9:
pEnd = pNext;
goto accept25_2;
/*
 * DFA STATE 10 (accepts to 3)
 */
state25_10:
pEnd = pNext;
goto accept25_3;
/*
 * DFA STATE 11 (accepts to 1)
 */
state25_11:
pEnd = pNext;
goto accept25_1;
/*
 * DFA STATE 12 (accepts to 5)
 */
state25_12:
pEnd = pNext;
goto accept25_5;


accept25_0:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.Range, new RegExNodeRanges(ranges)); 
accept25_1:
 pNext = pEnd;
 ranges.Add(new RegExInputRange((char)9)); _position = (int)(pEnd-ptr); goto nextRangeChar;  
accept25_2:
 pNext = pEnd;
 ranges.Add(new RegExInputRange((char)10)); _position = (int)(pEnd-ptr); goto nextRangeChar;  
accept25_3:
 pNext = pEnd;
 ranges.Add(new RegExInputRange((char)13)); _position = (int)(pEnd-ptr); goto nextRangeChar;  
accept25_4:
 pNext = pEnd;
 ranges.Add(new RegExInputRange(pStart[1])); _position = (int)(pEnd-ptr); goto nextRangeChar; 
accept25_5:
 pNext = pEnd;
 ranges.Add(new RegExInputRange(pStart[0], pStart[2])); _position = (int)(pEnd-ptr); goto nextRangeChar;  
accept25_6:
 pNext = pEnd;
 ranges.Add(new RegExInputRange()); _position = (int)(pEnd-ptr); goto nextRangeChar; 
accept25_7:
 pNext = pEnd;
 ranges.Add(new RegExInputRange(pStart[0])); _position = (int)(pEnd-ptr); goto nextRangeChar;  
nonaccept25:
 throw new Exception("Syntax error."); }
                }
            }
        }
    }
}