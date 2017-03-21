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
if(pNext >= pLimit) goto nonaccept0;
var current = *pNext++;
if(current < 0x3F) /* ('>') '?' */  {
    if(current < 0x27) /* ('&') ''' */  {
        if(current < 0x21) /* (' ') '!' */  {
            if(current < 0x20)
                goto nonaccept0;
            goto state0_1;
        }
        if(current < 0x22) /* ('!') '"' */ 
            goto nonaccept0;
        if(current < 0x23) /* ('"') '#' */ 
            goto state0_2;
        goto nonaccept0;
    }
    if(current < 0x2A) /* (')') '*' */  {
        if(current < 0x28) /* (''') '(' */ 
            goto state0_3;
        if(current < 0x29) /* ('(') ')' */ 
            goto state0_4;
        goto state0_5;
    }
    if(current < 0x2B) /* ('*') '+' */ 
        goto state0_6;
    if(current < 0x2C) /* ('+') ',' */ 
        goto state0_7;
    goto nonaccept0;
}
if(current < 0x61) /* ('`') 'a' */  {
    if(current < 0x5B) /* ('Z') '[' */  {
        if(current < 0x40) /* ('?') '@' */ 
            goto state0_8;
        if(current < 0x41) /* ('@') 'A' */ 
            goto nonaccept0;
        goto state0_9;
    }
    if(current < 0x5C) /* ('[') '\' */ 
        goto state0_10;
    if(current < 0x5D) /* ('\') ']' */ 
        goto state0_11;
    goto nonaccept0;
}
if(current < 0x7D) /* ('|') '}' */  {
    if(current < 0x7B) /* ('z') '{' */ 
        goto state0_9;
    if(current < 0x7C) /* ('{') '|' */ 
        goto nonaccept0;
    goto state0_12;
}
if(current < 0x7E) /* ('}') '~' */ 
    goto nonaccept0;
if(current < 0x7F)
    goto state0_13;
goto nonaccept0;
/*
 * DFA STATE 1 (accepts to 10)
 * ' ' -> 1
 */
state0_1:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_10;
current = *pNext++;
if(current < 0x20)
    goto accept0_10;
if(current < 0x21) /* (' ') '!' */ 
    goto state0_1;
goto accept0_10;
/*
 * DFA STATE 2 (accepts to 1)
 */
state0_2:
pEnd = pNext;
goto accept0_1;
/*
 * DFA STATE 3 (accepts to 0)
 */
state0_3:
pEnd = pNext;
goto accept0_0;
/*
 * DFA STATE 4 (accepts to 3)
 */
state0_4:
pEnd = pNext;
goto accept0_3;
/*
 * DFA STATE 5 (accepts to 4)
 */
state0_5:
pEnd = pNext;
goto accept0_4;
/*
 * DFA STATE 6 (accepts to 7)
 */
state0_6:
pEnd = pNext;
goto accept0_7;
/*
 * DFA STATE 7 (accepts to 5)
 */
state0_7:
pEnd = pNext;
goto accept0_5;
/*
 * DFA STATE 8 (accepts to 6)
 */
state0_8:
pEnd = pNext;
goto accept0_6;
/*
 * DFA STATE 9 (accepts to 12)
 * ['0'-'9'] -> 14
 * ['A'-'Z'] -> 14
 * ['a'-'z'] -> 14
 */
state0_9:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_12;
current = *pNext++;
if(current < 0x41) /* ('@') 'A' */  {
    if(current < 0x30) /* ('/') '0' */ 
        goto accept0_12;
    if(current < 0x3A) /* ('9') ':' */ 
        goto state0_14;
    goto accept0_12;
}
if(current < 0x61) /* ('`') 'a' */  {
    if(current < 0x5B) /* ('Z') '[' */ 
        goto state0_14;
    goto accept0_12;
}
if(current < 0x7B) /* ('z') '{' */ 
    goto state0_14;
goto accept0_12;
/*
 * DFA STATE 10 (accepts to 9)
 */
state0_10:
pEnd = pNext;
goto accept0_9;
/*
 * DFA STATE 11 (accepts to 8)
 */
state0_11:
pEnd = pNext;
goto accept0_8;
/*
 * DFA STATE 12 (accepts to 2)
 */
state0_12:
pEnd = pNext;
goto accept0_2;
/*
 * DFA STATE 13
 * ['0'-'9'] -> 15
 */
state0_13:
if(pNext >= pLimit) goto nonaccept0;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */ 
    goto nonaccept0;
if(current < 0x3A) /* ('9') ':' */ 
    goto state0_15;
goto nonaccept0;
/*
 * DFA STATE 14 (accepts to 12)
 * ['0'-'9'] -> 14
 * ['A'-'Z'] -> 14
 * ['a'-'z'] -> 14
 */
state0_14:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_12;
current = *pNext++;
if(current < 0x41) /* ('@') 'A' */  {
    if(current < 0x30) /* ('/') '0' */ 
        goto accept0_12;
    if(current < 0x3A) /* ('9') ':' */ 
        goto state0_14;
    goto accept0_12;
}
if(current < 0x61) /* ('`') 'a' */  {
    if(current < 0x5B) /* ('Z') '[' */ 
        goto state0_14;
    goto accept0_12;
}
if(current < 0x7B) /* ('z') '{' */ 
    goto state0_14;
goto accept0_12;
/*
 * DFA STATE 15 (accepts to 11)
 * ['0'-'9'] -> 15
 */
state0_15:
pEnd = pNext;
if(pNext >= pLimit) goto accept0_11;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */ 
    goto accept0_11;
if(current < 0x3A) /* ('9') ':' */ 
    goto state0_15;
goto accept0_11;


accept0_0:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); pNext = pEnd; goto incaseString; 
accept0_1:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); pNext = pEnd; goto caseString; 
accept0_2:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.Bar, null); 
accept0_3:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.ParBegin, null); 
accept0_4:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.ParEnd, null); 
accept0_5:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.Plus, null); 
accept0_6:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.Question, null); 
accept0_7:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.Star, null); 
accept0_8:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.Except, null); 
accept0_9:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); pNext = pEnd; goto range; 
accept0_10:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); pNext = pEnd; goto skip; 
accept0_11:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.Accept, new RegExNodeAccept(null, ushort.Parse(new string(pStart+1, 0, ((int)(pEnd-pStart))-1)))); 
accept0_12:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.Name, new RegExNodeName(new string(pStart, 0, (int)(pEnd-pStart)))); 
nonaccept0:
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
if(pNext >= pLimit) goto nonaccept1;
var current = *pNext++;
if(current < 0x28) /* (''') '(' */  {
    if(current < 0x27) /* ('&') ''' */ 
        goto state1_1;
    goto state1_2;
}
if(current < 0x5C) /* ('[') '\' */ 
    goto state1_1;
if(current < 0x5D) /* ('\') ']' */ 
    goto state1_3;
goto state1_1;
/*
 * DFA STATE 1 (accepts to 5)
 */
state1_1:
pEnd = pNext;
goto accept1_5;
/*
 * DFA STATE 2 (accepts to 0)
 */
state1_2:
pEnd = pNext;
goto accept1_0;
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
state1_3:
pEnd = pNext;
if(pNext >= pLimit) goto accept1_5;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */  {
    if(current < 0x6E) /* ('m') 'n' */ 
        goto state1_4;
    if(current < 0x6F) /* ('n') 'o' */ 
        goto state1_5;
    goto state1_4;
}
if(current < 0x74) /* ('s') 't' */  {
    if(current < 0x73) /* ('r') 's' */ 
        goto state1_6;
    goto state1_4;
}
if(current < 0x75) /* ('t') 'u' */ 
    goto state1_7;
goto state1_4;
/*
 * DFA STATE 4 (accepts to 4)
 */
state1_4:
pEnd = pNext;
goto accept1_4;
/*
 * DFA STATE 5 (accepts to 2)
 */
state1_5:
pEnd = pNext;
goto accept1_2;
/*
 * DFA STATE 6 (accepts to 3)
 */
state1_6:
pEnd = pNext;
goto accept1_3;
/*
 * DFA STATE 7 (accepts to 1)
 */
state1_7:
pEnd = pNext;
goto accept1_1;


accept1_0:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.Phrase, new RegExNodeSequence(sb.ToString(), RegExCasing.Insensitive)); 
accept1_1:
 pNext = pEnd;
 sb.Append((char)9); _position = (int)(pEnd-ptr); goto nextIncaseChar;  
accept1_2:
 pNext = pEnd;
 sb.Append((char)10); _position = (int)(pEnd-ptr); goto nextIncaseChar; 
accept1_3:
 pNext = pEnd;
 sb.Append((char)13); _position = (int)(pEnd-ptr); goto nextIncaseChar; 
accept1_4:
 pNext = pEnd;
 sb.Append(pStart[1]); _position = (int)(pEnd-ptr); goto nextIncaseChar; 
accept1_5:
 pNext = pEnd;
 sb.Append(pStart[0]); _position = (int)(pEnd-ptr); goto nextIncaseChar; 
nonaccept1:
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
if(pNext >= pLimit) goto nonaccept2;
var current = *pNext++;
if(current < 0x23) /* ('"') '#' */  {
    if(current < 0x22) /* ('!') '"' */ 
        goto state2_1;
    goto state2_2;
}
if(current < 0x5C) /* ('[') '\' */ 
    goto state2_1;
if(current < 0x5D) /* ('\') ']' */ 
    goto state2_3;
goto state2_1;
/*
 * DFA STATE 1 (accepts to 5)
 */
state2_1:
pEnd = pNext;
goto accept2_5;
/*
 * DFA STATE 2 (accepts to 0)
 */
state2_2:
pEnd = pNext;
goto accept2_0;
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
state2_3:
pEnd = pNext;
if(pNext >= pLimit) goto accept2_5;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */  {
    if(current < 0x6E) /* ('m') 'n' */ 
        goto state2_4;
    if(current < 0x6F) /* ('n') 'o' */ 
        goto state2_5;
    goto state2_4;
}
if(current < 0x74) /* ('s') 't' */  {
    if(current < 0x73) /* ('r') 's' */ 
        goto state2_6;
    goto state2_4;
}
if(current < 0x75) /* ('t') 'u' */ 
    goto state2_7;
goto state2_4;
/*
 * DFA STATE 4 (accepts to 4)
 */
state2_4:
pEnd = pNext;
goto accept2_4;
/*
 * DFA STATE 5 (accepts to 2)
 */
state2_5:
pEnd = pNext;
goto accept2_2;
/*
 * DFA STATE 6 (accepts to 3)
 */
state2_6:
pEnd = pNext;
goto accept2_3;
/*
 * DFA STATE 7 (accepts to 1)
 */
state2_7:
pEnd = pNext;
goto accept2_1;


accept2_0:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.Phrase, new RegExNodeSequence(sb.ToString(), RegExCasing.Sensitive)); 
accept2_1:
 pNext = pEnd;
 sb.Append((char)9); _position = (int)(pEnd-ptr); goto nextCaseChar;  
accept2_2:
 pNext = pEnd;
 sb.Append((char)10); _position = (int)(pEnd-ptr); goto nextCaseChar; 
accept2_3:
 pNext = pEnd;
 sb.Append((char)13); _position = (int)(pEnd-ptr); goto nextCaseChar; 
accept2_4:
 pNext = pEnd;
 sb.Append(pStart[1]); _position = (int)(pEnd-ptr); goto nextCaseChar; 
accept2_5:
 pNext = pEnd;
 sb.Append(pStart[0]); _position = (int)(pEnd-ptr); goto nextCaseChar; 
nonaccept2:
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
 *  |     +-Ranges(['0'-'9'])
 *  |     +-Ranges('-')
 *  |     +-Ranges(['0'-'9'])
 *  +-Accept(2)
 *  |  +-Concat
 *  |     +-Ranges(['A'-'Z'])
 *  |     +-Ranges('-')
 *  |     +-Ranges(['A'-'Z'])
 *  +-Accept(3)
 *  |  +-Concat
 *  |     +-Ranges(['a'-'z'])
 *  |     +-Ranges('-')
 *  |     +-Ranges(['a'-'z'])
 *  +-Accept(4)
 *  |  +-Concat
 *  |     +-Ranges('\')
 *  |     +-Ranges('t')
 *  +-Accept(5)
 *  |  +-Concat
 *  |     +-Ranges('\')
 *  |     +-Ranges('n')
 *  +-Accept(6)
 *  |  +-Concat
 *  |     +-Ranges('\')
 *  |     +-Ranges('r')
 *  +-Accept(7)
 *  |  +-Concat
 *  |     +-Ranges('\')
 *  |     +-Ranges([0x00-0xFFFF])
 *  +-Accept(8)
 *  |  +-Ranges('.')
 *  +-Accept(9)
 *     +-Ranges([0x00-0xFFFF])
 */
/*
 * DFA STATE 0
 * [0x00-'-'] -> 1
 * '.' -> 2
 * '/' -> 1
 * ['0'-'9'] -> 3
 * [':'-'@'] -> 1
 * ['A'-'Z'] -> 4
 * '[' -> 1
 * '\' -> 5
 * ']' -> 6
 * ['^'-'`'] -> 1
 * ['a'-'z'] -> 7
 * ['{'-0xFFFF] -> 1
 */
if(pNext >= pLimit) goto nonaccept3;
var current = *pNext++;
if(current < 0x5B) /* ('Z') '[' */  {
    if(current < 0x30) /* ('/') '0' */  {
        if(current < 0x2E) /* ('-') '.' */ 
            goto state3_1;
        if(current < 0x2F) /* ('.') '/' */ 
            goto state3_2;
        goto state3_1;
    }
    if(current < 0x3A) /* ('9') ':' */ 
        goto state3_3;
    if(current < 0x41) /* ('@') 'A' */ 
        goto state3_1;
    goto state3_4;
}
if(current < 0x5E) /* (']') '^' */  {
    if(current < 0x5C) /* ('[') '\' */ 
        goto state3_1;
    if(current < 0x5D) /* ('\') ']' */ 
        goto state3_5;
    goto state3_6;
}
if(current < 0x61) /* ('`') 'a' */ 
    goto state3_1;
if(current < 0x7B) /* ('z') '{' */ 
    goto state3_7;
goto state3_1;
/*
 * DFA STATE 1 (accepts to 9)
 */
state3_1:
pEnd = pNext;
goto accept3_9;
/*
 * DFA STATE 2 (accepts to 8)
 */
state3_2:
pEnd = pNext;
goto accept3_8;
/*
 * DFA STATE 3 (accepts to 9)
 * '-' -> 8
 */
state3_3:
pEnd = pNext;
if(pNext >= pLimit) goto accept3_9;
current = *pNext++;
if(current < 0x2D) /* (',') '-' */ 
    goto accept3_9;
if(current < 0x2E) /* ('-') '.' */ 
    goto state3_8;
goto accept3_9;
/*
 * DFA STATE 4 (accepts to 9)
 * '-' -> 9
 */
state3_4:
pEnd = pNext;
if(pNext >= pLimit) goto accept3_9;
current = *pNext++;
if(current < 0x2D) /* (',') '-' */ 
    goto accept3_9;
if(current < 0x2E) /* ('-') '.' */ 
    goto state3_9;
goto accept3_9;
/*
 * DFA STATE 5 (accepts to 9)
 * [0x00-'m'] -> 10
 * 'n' -> 11
 * ['o'-'q'] -> 10
 * 'r' -> 12
 * 's' -> 10
 * 't' -> 13
 * ['u'-0xFFFF] -> 10
 */
state3_5:
pEnd = pNext;
if(pNext >= pLimit) goto accept3_9;
current = *pNext++;
if(current < 0x72) /* ('q') 'r' */  {
    if(current < 0x6E) /* ('m') 'n' */ 
        goto state3_10;
    if(current < 0x6F) /* ('n') 'o' */ 
        goto state3_11;
    goto state3_10;
}
if(current < 0x74) /* ('s') 't' */  {
    if(current < 0x73) /* ('r') 's' */ 
        goto state3_12;
    goto state3_10;
}
if(current < 0x75) /* ('t') 'u' */ 
    goto state3_13;
goto state3_10;
/*
 * DFA STATE 6 (accepts to 0)
 */
state3_6:
pEnd = pNext;
goto accept3_0;
/*
 * DFA STATE 7 (accepts to 9)
 * '-' -> 14
 */
state3_7:
pEnd = pNext;
if(pNext >= pLimit) goto accept3_9;
current = *pNext++;
if(current < 0x2D) /* (',') '-' */ 
    goto accept3_9;
if(current < 0x2E) /* ('-') '.' */ 
    goto state3_14;
goto accept3_9;
/*
 * DFA STATE 8
 * ['0'-'9'] -> 15
 */
state3_8:
if(pNext >= pLimit) goto accept3_9;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */ 
    goto accept3_9;
if(current < 0x3A) /* ('9') ':' */ 
    goto state3_15;
goto accept3_9;
/*
 * DFA STATE 9
 * ['A'-'Z'] -> 16
 */
state3_9:
if(pNext >= pLimit) goto accept3_9;
current = *pNext++;
if(current < 0x41) /* ('@') 'A' */ 
    goto accept3_9;
if(current < 0x5B) /* ('Z') '[' */ 
    goto state3_16;
goto accept3_9;
/*
 * DFA STATE 10 (accepts to 7)
 */
state3_10:
pEnd = pNext;
goto accept3_7;
/*
 * DFA STATE 11 (accepts to 5)
 */
state3_11:
pEnd = pNext;
goto accept3_5;
/*
 * DFA STATE 12 (accepts to 6)
 */
state3_12:
pEnd = pNext;
goto accept3_6;
/*
 * DFA STATE 13 (accepts to 4)
 */
state3_13:
pEnd = pNext;
goto accept3_4;
/*
 * DFA STATE 14
 * ['a'-'z'] -> 17
 */
state3_14:
if(pNext >= pLimit) goto accept3_9;
current = *pNext++;
if(current < 0x61) /* ('`') 'a' */ 
    goto accept3_9;
if(current < 0x7B) /* ('z') '{' */ 
    goto state3_17;
goto accept3_9;
/*
 * DFA STATE 15 (accepts to 1)
 */
state3_15:
pEnd = pNext;
goto accept3_1;
/*
 * DFA STATE 16 (accepts to 2)
 */
state3_16:
pEnd = pNext;
goto accept3_2;
/*
 * DFA STATE 17 (accepts to 3)
 */
state3_17:
pEnd = pNext;
goto accept3_3;


accept3_0:
 pNext = pEnd;
 _position = (int)(pEnd-ptr); return new KeyValuePair<Token, RegExNode>(Token.Range, new RegExNodeRanges(ranges)); 
accept3_1:
 pNext = pEnd;
 ranges.Add(new RegExInputRange(pStart[0], pStart[2])); _position = (int)(pEnd-ptr); goto nextRangeChar;  
accept3_2:
 pNext = pEnd;
 ranges.Add(new RegExInputRange(pStart[0], pStart[2])); _position = (int)(pEnd-ptr); goto nextRangeChar;  
accept3_3:
 pNext = pEnd;
 ranges.Add(new RegExInputRange(pStart[0], pStart[2])); _position = (int)(pEnd-ptr); goto nextRangeChar;  
accept3_4:
 pNext = pEnd;
 ranges.Add(new RegExInputRange((char)9)); _position = (int)(pEnd-ptr); goto nextRangeChar;  
accept3_5:
 pNext = pEnd;
 ranges.Add(new RegExInputRange((char)10)); _position = (int)(pEnd-ptr); goto nextRangeChar;  
accept3_6:
 pNext = pEnd;
 ranges.Add(new RegExInputRange((char)13)); _position = (int)(pEnd-ptr); goto nextRangeChar;  
accept3_7:
 pNext = pEnd;
 ranges.Add(new RegExInputRange(pStart[1])); _position = (int)(pEnd-ptr); goto nextRangeChar; 
accept3_8:
 pNext = pEnd;
 ranges.Add(new RegExInputRange()); _position = (int)(pEnd-ptr); goto nextRangeChar; 
accept3_9:
 pNext = pEnd;
 ranges.Add(new RegExInputRange(pStart[0])); _position = (int)(pEnd-ptr); goto nextRangeChar;  
nonaccept3:
 throw new Exception("Syntax error."); }
                }
            }
        }
    }
}