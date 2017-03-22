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
 *  |     +-Repeat(ZeroOrMore)
 *  |     |  +-Ranges('+',['\'-']'],'?','[',['0'-'9'])
 *  |     +-Ranges('.')
 *  |     +-Repeat(OneOrMore)
 *  |     |  +-Ranges(['0'-'9'])
 *  |     +-Ranges('E')
 *  |     +-Repeat(OneOrMore)
 *  |        +-Ranges('+',['\'-']'],'?','[',['0'-'9'])
 *  +-Accept(11)
 *  |  +-Concat
 *  |     +-Repeat(ZeroOrMore)
 *  |     |  +-Ranges('+',['\'-']'],'?','[',['0'-'9'])
 *  |     +-Ranges('.')
 *  |     +-Repeat(OneOrMore)
 *  |        +-Ranges(['0'-'9'])
 *  +-Accept(12)
 *  |  +-Repeat(OneOrMore)
 *  |     +-Ranges('+',['\'-']'],'?','[',['0'-'9'])
 *  +-Accept(13)
 *  |  +-Repeat(OneOrMore)
 *  |     +-Ranges('+',['\'-']'],'?','[',['0'-'9'])
 *  +-Accept(14)
 *  |  +-Sequence(CaseInsensitive,'fløden')
 *  +-Accept(15)
 *  |  +-Sequence(CaseInsensitive,'fløde')
 *  +-Accept(16)
 *     +-Ranges([0x00-0xFFFE])
 */
/*
 * DFA STATE 0
 * [0x00-'*'] -> 1
 * '+' -> 2
 * [','-'-'] -> 1
 * '.' -> 3
 * '/' -> 1
 * '0' -> 4
 * '1' -> 5
 * '2' -> 6
 * ['3'-'4'] -> 2
 * '5' -> 7
 * '6' -> 2
 * '7' -> 8
 * ['8'-'9'] -> 2
 * [':'-'>'] -> 1
 * '?' -> 2
 * ['@'-'E'] -> 1
 * 'F' -> 9
 * ['G'-'Z'] -> 1
 * '[' -> 2
 * ['\'-']'] -> 2
 * ['^'-'e'] -> 1
 * 'f' -> 9
 * ['g'-0xFFFE] -> 1
 */
if(pNext >= pLimit) goto nonaccept1;
var current = *pNext++;
if(current < 0x37) /* ('6') '7' */  {
    if(current < 0x30) /* ('/') '0' */  {
        if(current < 0x2C) /* ('+') ',' */  {
            if(current < 0x2B) /* ('*') '+' */ 
                goto state1_1;
            goto state1_2;
        }
        if(current < 0x2E) /* ('-') '.' */ 
            goto state1_1;
        if(current < 0x2F) /* ('.') '/' */ 
            goto state1_3;
        goto state1_1;
    }
    if(current < 0x33) /* ('2') '3' */  {
        if(current < 0x31) /* ('0') '1' */ 
            goto state1_4;
        if(current < 0x32) /* ('1') '2' */ 
            goto state1_5;
        goto state1_6;
    }
    if(current < 0x35) /* ('4') '5' */ 
        goto state1_2;
    if(current < 0x36) /* ('5') '6' */ 
        goto state1_7;
    goto state1_2;
}
if(current < 0x47) /* ('F') 'G' */  {
    if(current < 0x3F) /* ('>') '?' */  {
        if(current < 0x38) /* ('7') '8' */ 
            goto state1_8;
        if(current < 0x3A) /* ('9') ':' */ 
            goto state1_2;
        goto state1_1;
    }
    if(current < 0x40) /* ('?') '@' */ 
        goto state1_2;
    if(current < 0x46) /* ('E') 'F' */ 
        goto state1_1;
    goto state1_9;
}
if(current < 0x66) /* ('e') 'f' */  {
    if(current < 0x5B) /* ('Z') '[' */ 
        goto state1_1;
    if(current < 0x5E) /* (']') '^' */ 
        goto state1_2;
    goto state1_1;
}
if(current < 0x67) /* ('f') 'g' */ 
    goto state1_9;
if(current < 0xFFFF)
    goto state1_1;
goto nonaccept1;
/*
 * DFA STATE 1 (accepts to 16)
 */
state1_1:
pEnd = pNext;
goto accept1_16;
/*
 * DFA STATE 2 (accepts to 12)
 * '+' -> 10
 * '.' -> 11
 * ['0'-'9'] -> 10
 * '?' -> 10
 * '[' -> 10
 * ['\'-']'] -> 10
 */
state1_2:
pEnd = pNext;
if(pNext >= pLimit) goto accept1_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2C) /* ('+') ',' */  {
        if(current < 0x2B) /* ('*') '+' */ 
            goto accept1_12;
        goto state1_10;
    }
    if(current < 0x2E) /* ('-') '.' */ 
        goto accept1_12;
    if(current < 0x2F) /* ('.') '/' */ 
        goto state1_11;
    goto accept1_12;
}
if(current < 0x40) /* ('?') '@' */  {
    if(current < 0x3A) /* ('9') ':' */ 
        goto state1_10;
    if(current < 0x3F) /* ('>') '?' */ 
        goto accept1_12;
    goto state1_10;
}
if(current < 0x5B) /* ('Z') '[' */ 
    goto accept1_12;
if(current < 0x5E) /* (']') '^' */ 
    goto state1_10;
goto accept1_12;
/*
 * DFA STATE 3 (accepts to 16)
 * ['0'-'9'] -> 12
 */
state1_3:
pEnd = pNext;
if(pNext >= pLimit) goto accept1_16;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */ 
    goto accept1_16;
if(current < 0x3A) /* ('9') ':' */ 
    goto state1_12;
goto accept1_16;
/*
 * DFA STATE 4 (accepts to 0)
 */
state1_4:
pEnd = pNext;
goto accept1_0;
/*
 * DFA STATE 5 (accepts to 12)
 * '+' -> 10
 * '.' -> 11
 * '0' -> 13
 * '1' -> 10
 * '2' -> 14
 * ['3'-'4'] -> 10
 * '5' -> 15
 * '6' -> 10
 * '7' -> 16
 * ['8'-'9'] -> 10
 * '?' -> 10
 * '[' -> 10
 * ['\'-']'] -> 10
 */
state1_5:
pEnd = pNext;
if(pNext >= pLimit) goto accept1_12;
current = *pNext++;
if(current < 0x35) /* ('4') '5' */  {
    if(current < 0x2F) /* ('.') '/' */  {
        if(current < 0x2C) /* ('+') ',' */  {
            if(current < 0x2B) /* ('*') '+' */ 
                goto accept1_12;
            goto state1_10;
        }
        if(current < 0x2E) /* ('-') '.' */ 
            goto accept1_12;
        goto state1_11;
    }
    if(current < 0x31) /* ('0') '1' */  {
        if(current < 0x30) /* ('/') '0' */ 
            goto accept1_12;
        goto state1_13;
    }
    if(current < 0x32) /* ('1') '2' */ 
        goto state1_10;
    if(current < 0x33) /* ('2') '3' */ 
        goto state1_14;
    goto state1_10;
}
if(current < 0x3A) /* ('9') ':' */  {
    if(current < 0x37) /* ('6') '7' */  {
        if(current < 0x36) /* ('5') '6' */ 
            goto state1_15;
        goto state1_10;
    }
    if(current < 0x38) /* ('7') '8' */ 
        goto state1_16;
    goto state1_10;
}
if(current < 0x40) /* ('?') '@' */  {
    if(current < 0x3F) /* ('>') '?' */ 
        goto accept1_12;
    goto state1_10;
}
if(current < 0x5B) /* ('Z') '[' */ 
    goto accept1_12;
if(current < 0x5E) /* (']') '^' */ 
    goto state1_10;
goto accept1_12;
/*
 * DFA STATE 6 (accepts to 12)
 * '+' -> 10
 * '.' -> 11
 * '0' -> 17
 * '1' -> 10
 * '2' -> 18
 * ['3'-'4'] -> 10
 * '5' -> 19
 * ['6'-'9'] -> 10
 * '?' -> 10
 * '[' -> 10
 * ['\'-']'] -> 10
 */
state1_6:
pEnd = pNext;
if(pNext >= pLimit) goto accept1_12;
current = *pNext++;
if(current < 0x33) /* ('2') '3' */  {
    if(current < 0x2F) /* ('.') '/' */  {
        if(current < 0x2C) /* ('+') ',' */  {
            if(current < 0x2B) /* ('*') '+' */ 
                goto accept1_12;
            goto state1_10;
        }
        if(current < 0x2E) /* ('-') '.' */ 
            goto accept1_12;
        goto state1_11;
    }
    if(current < 0x31) /* ('0') '1' */  {
        if(current < 0x30) /* ('/') '0' */ 
            goto accept1_12;
        goto state1_17;
    }
    if(current < 0x32) /* ('1') '2' */ 
        goto state1_10;
    goto state1_18;
}
if(current < 0x3F) /* ('>') '?' */  {
    if(current < 0x36) /* ('5') '6' */  {
        if(current < 0x35) /* ('4') '5' */ 
            goto state1_10;
        goto state1_19;
    }
    if(current < 0x3A) /* ('9') ':' */ 
        goto state1_10;
    goto accept1_12;
}
if(current < 0x5B) /* ('Z') '[' */  {
    if(current < 0x40) /* ('?') '@' */ 
        goto state1_10;
    goto accept1_12;
}
if(current < 0x5E) /* (']') '^' */ 
    goto state1_10;
goto accept1_12;
/*
 * DFA STATE 7 (accepts to 12)
 * '+' -> 10
 * '.' -> 11
 * '0' -> 20
 * ['1'-'9'] -> 10
 * '?' -> 10
 * '[' -> 10
 * ['\'-']'] -> 10
 */
state1_7:
pEnd = pNext;
if(pNext >= pLimit) goto accept1_12;
current = *pNext++;
if(current < 0x31) /* ('0') '1' */  {
    if(current < 0x2E) /* ('-') '.' */  {
        if(current < 0x2B) /* ('*') '+' */ 
            goto accept1_12;
        if(current < 0x2C) /* ('+') ',' */ 
            goto state1_10;
        goto accept1_12;
    }
    if(current < 0x2F) /* ('.') '/' */ 
        goto state1_11;
    if(current < 0x30) /* ('/') '0' */ 
        goto accept1_12;
    goto state1_20;
}
if(current < 0x40) /* ('?') '@' */  {
    if(current < 0x3A) /* ('9') ':' */ 
        goto state1_10;
    if(current < 0x3F) /* ('>') '?' */ 
        goto accept1_12;
    goto state1_10;
}
if(current < 0x5B) /* ('Z') '[' */ 
    goto accept1_12;
if(current < 0x5E) /* (']') '^' */ 
    goto state1_10;
goto accept1_12;
/*
 * DFA STATE 8 (accepts to 12)
 * '+' -> 10
 * '.' -> 11
 * ['0'-'4'] -> 10
 * '5' -> 21
 * ['6'-'9'] -> 10
 * '?' -> 10
 * '[' -> 10
 * ['\'-']'] -> 10
 */
state1_8:
pEnd = pNext;
if(pNext >= pLimit) goto accept1_12;
current = *pNext++;
if(current < 0x35) /* ('4') '5' */  {
    if(current < 0x2E) /* ('-') '.' */  {
        if(current < 0x2B) /* ('*') '+' */ 
            goto accept1_12;
        if(current < 0x2C) /* ('+') ',' */ 
            goto state1_10;
        goto accept1_12;
    }
    if(current < 0x2F) /* ('.') '/' */ 
        goto state1_11;
    if(current < 0x30) /* ('/') '0' */ 
        goto accept1_12;
    goto state1_10;
}
if(current < 0x3F) /* ('>') '?' */  {
    if(current < 0x36) /* ('5') '6' */ 
        goto state1_21;
    if(current < 0x3A) /* ('9') ':' */ 
        goto state1_10;
    goto accept1_12;
}
if(current < 0x5B) /* ('Z') '[' */  {
    if(current < 0x40) /* ('?') '@' */ 
        goto state1_10;
    goto accept1_12;
}
if(current < 0x5E) /* (']') '^' */ 
    goto state1_10;
goto accept1_12;
/*
 * DFA STATE 9 (accepts to 16)
 * 'L' -> 22
 * 'l' -> 22
 */
state1_9:
pEnd = pNext;
if(pNext >= pLimit) goto accept1_16;
current = *pNext++;
if(current < 0x4D) /* ('L') 'M' */  {
    if(current < 0x4C) /* ('K') 'L' */ 
        goto accept1_16;
    goto state1_22;
}
if(current < 0x6C) /* ('k') 'l' */ 
    goto accept1_16;
if(current < 0x6D) /* ('l') 'm' */ 
    goto state1_22;
goto accept1_16;
/*
 * DFA STATE 10 (accepts to 12)
 * '+' -> 10
 * '.' -> 11
 * ['0'-'9'] -> 10
 * '?' -> 10
 * '[' -> 10
 * ['\'-']'] -> 10
 */
state1_10:
pEnd = pNext;
if(pNext >= pLimit) goto accept1_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */  {
    if(current < 0x2C) /* ('+') ',' */  {
        if(current < 0x2B) /* ('*') '+' */ 
            goto accept1_12;
        goto state1_10;
    }
    if(current < 0x2E) /* ('-') '.' */ 
        goto accept1_12;
    if(current < 0x2F) /* ('.') '/' */ 
        goto state1_11;
    goto accept1_12;
}
if(current < 0x40) /* ('?') '@' */  {
    if(current < 0x3A) /* ('9') ':' */ 
        goto state1_10;
    if(current < 0x3F) /* ('>') '?' */ 
        goto accept1_12;
    goto state1_10;
}
if(current < 0x5B) /* ('Z') '[' */ 
    goto accept1_12;
if(current < 0x5E) /* (']') '^' */ 
    goto state1_10;
goto accept1_12;
/*
 * DFA STATE 11
 * ['0'-'9'] -> 12
 */
state1_11:
if(pNext >= pLimit) goto accept1_12;
current = *pNext++;
if(current < 0x30) /* ('/') '0' */ 
    goto accept1_12;
if(current < 0x3A) /* ('9') ':' */ 
    goto state1_12;
goto accept1_12;
/*
 * DFA STATE 12 (accepts to 11)
 * ['0'-'9'] -> 12
 * 'E' -> 23
 */
state1_12:
pEnd = pNext;
if(pNext >= pLimit) goto accept1_11;
current = *pNext++;
if(current < 0x3A) /* ('9') ':' */  {
    if(current < 0x30) /* ('/') '0' */ 
        goto accept1_11;
    goto state1_12;
}
if(current < 0x45) /* ('D') 'E' */ 
    goto accept1_11;
if(current < 0x46) /* ('E') 'F' */ 
    goto state1_23;
goto accept1_11;
/*
 * DFA STATE 13 (accepts to 12)
 * '+' -> 10
 * '.' -> 11
 * '0' -> 24
 * ['1'-'9'] -> 10
 * '?' -> 10
 * '[' -> 10
 * ['\'-']'] -> 10
 */
state1_13:
pEnd = pNext;
if(pNext >= pLimit) goto accept1_12;
current = *pNext++;
if(current < 0x31) /* ('0') '1' */  {
    if(current < 0x2E) /* ('-') '.' */  {
        if(current < 0x2B) /* ('*') '+' */ 
            goto accept1_12;
        if(current < 0x2C) /* ('+') ',' */ 
            goto state1_10;
        goto accept1_12;
    }
    if(current < 0x2F) /* ('.') '/' */ 
        goto state1_11;
    if(current < 0x30) /* ('/') '0' */ 
        goto accept1_12;
    goto state1_24;
}
if(current < 0x40) /* ('?') '@' */  {
    if(current < 0x3A) /* ('9') ':' */ 
        goto state1_10;
    if(current < 0x3F) /* ('>') '?' */ 
        goto accept1_12;
    goto state1_10;
}
if(current < 0x5B) /* ('Z') '[' */ 
    goto accept1_12;
if(current < 0x5E) /* (']') '^' */ 
    goto state1_10;
goto accept1_12;
/*
 * DFA STATE 14 (accepts to 12)
 * '+' -> 10
 * '.' -> 11
 * ['0'-'4'] -> 10
 * '5' -> 25
 * ['6'-'9'] -> 10
 * '?' -> 10
 * '[' -> 10
 * ['\'-']'] -> 10
 */
state1_14:
pEnd = pNext;
if(pNext >= pLimit) goto accept1_12;
current = *pNext++;
if(current < 0x35) /* ('4') '5' */  {
    if(current < 0x2E) /* ('-') '.' */  {
        if(current < 0x2B) /* ('*') '+' */ 
            goto accept1_12;
        if(current < 0x2C) /* ('+') ',' */ 
            goto state1_10;
        goto accept1_12;
    }
    if(current < 0x2F) /* ('.') '/' */ 
        goto state1_11;
    if(current < 0x30) /* ('/') '0' */ 
        goto accept1_12;
    goto state1_10;
}
if(current < 0x3F) /* ('>') '?' */  {
    if(current < 0x36) /* ('5') '6' */ 
        goto state1_25;
    if(current < 0x3A) /* ('9') ':' */ 
        goto state1_10;
    goto accept1_12;
}
if(current < 0x5B) /* ('Z') '[' */  {
    if(current < 0x40) /* ('?') '@' */ 
        goto state1_10;
    goto accept1_12;
}
if(current < 0x5E) /* (']') '^' */ 
    goto state1_10;
goto accept1_12;
/*
 * DFA STATE 15 (accepts to 12)
 * '+' -> 10
 * '.' -> 11
 * '0' -> 26
 * ['1'-'9'] -> 10
 * '?' -> 10
 * '[' -> 10
 * ['\'-']'] -> 10
 */
state1_15:
pEnd = pNext;
if(pNext >= pLimit) goto accept1_12;
current = *pNext++;
if(current < 0x31) /* ('0') '1' */  {
    if(current < 0x2E) /* ('-') '.' */  {
        if(current < 0x2B) /* ('*') '+' */ 
            goto accept1_12;
        if(current < 0x2C) /* ('+') ',' */ 
            goto state1_10;
        goto accept1_12;
    }
    if(current < 0x2F) /* ('.') '/' */ 
        goto state1_11;
    if(current < 0x30) /* ('/') '0' */ 
        goto accept1_12;
    goto state1_26;
}
if(current < 0x40) /* ('?') '@' */  {
    if(current < 0x3A) /* ('9') ':' */ 
        goto state1_10;
    if(current < 0x3F) /* ('>') '?' */ 
        goto accept1_12;
    goto state1_10;
}
if(current < 0x5B) /* ('Z') '[' */ 
    goto accept1_12;
if(current < 0x5E) /* (']') '^' */ 
    goto state1_10;
goto accept1_12;
/*
 * DFA STATE 16 (accepts to 12)
 * '+' -> 10
 * '.' -> 11
 * ['0'-'4'] -> 10
 * '5' -> 27
 * ['6'-'9'] -> 10
 * '?' -> 10
 * '[' -> 10
 * ['\'-']'] -> 10
 */
state1_16:
pEnd = pNext;
if(pNext >= pLimit) goto accept1_12;
current = *pNext++;
if(current < 0x35) /* ('4') '5' */  {
    if(current < 0x2E) /* ('-') '.' */  {
        if(current < 0x2B) /* ('*') '+' */ 
            goto accept1_12;
        if(current < 0x2C) /* ('+') ',' */ 
            goto state1_10;
        goto accept1_12;
    }
    if(current < 0x2F) /* ('.') '/' */ 
        goto state1_11;
    if(current < 0x30) /* ('/') '0' */ 
        goto accept1_12;
    goto state1_10;
}
if(current < 0x3F) /* ('>') '?' */  {
    if(current < 0x36) /* ('5') '6' */ 
        goto state1_27;
    if(current < 0x3A) /* ('9') ':' */ 
        goto state1_10;
    goto accept1_12;
}
if(current < 0x5B) /* ('Z') '[' */  {
    if(current < 0x40) /* ('?') '@' */ 
        goto state1_10;
    goto accept1_12;
}
if(current < 0x5E) /* (']') '^' */ 
    goto state1_10;
goto accept1_12;
/*
 * DFA STATE 17 (accepts to 12)
 * '+' -> 10
 * '.' -> 11
 * '0' -> 28
 * ['1'-'9'] -> 10
 * '?' -> 10
 * '[' -> 10
 * ['\'-']'] -> 10
 */
state1_17:
pEnd = pNext;
if(pNext >= pLimit) goto accept1_12;
current = *pNext++;
if(current < 0x31) /* ('0') '1' */  {
    if(current < 0x2E) /* ('-') '.' */  {
        if(current < 0x2B) /* ('*') '+' */ 
            goto accept1_12;
        if(current < 0x2C) /* ('+') ',' */ 
            goto state1_10;
        goto accept1_12;
    }
    if(current < 0x2F) /* ('.') '/' */ 
        goto state1_11;
    if(current < 0x30) /* ('/') '0' */ 
        goto accept1_12;
    goto state1_28;
}
if(current < 0x40) /* ('?') '@' */  {
    if(current < 0x3A) /* ('9') ':' */ 
        goto state1_10;
    if(current < 0x3F) /* ('>') '?' */ 
        goto accept1_12;
    goto state1_10;
}
if(current < 0x5B) /* ('Z') '[' */ 
    goto accept1_12;
if(current < 0x5E) /* (']') '^' */ 
    goto state1_10;
goto accept1_12;
/*
 * DFA STATE 18 (accepts to 12)
 * '+' -> 10
 * '.' -> 11
 * ['0'-'4'] -> 10
 * '5' -> 29
 * ['6'-'9'] -> 10
 * '?' -> 10
 * '[' -> 10
 * ['\'-']'] -> 10
 */
state1_18:
pEnd = pNext;
if(pNext >= pLimit) goto accept1_12;
current = *pNext++;
if(current < 0x35) /* ('4') '5' */  {
    if(current < 0x2E) /* ('-') '.' */  {
        if(current < 0x2B) /* ('*') '+' */ 
            goto accept1_12;
        if(current < 0x2C) /* ('+') ',' */ 
            goto state1_10;
        goto accept1_12;
    }
    if(current < 0x2F) /* ('.') '/' */ 
        goto state1_11;
    if(current < 0x30) /* ('/') '0' */ 
        goto accept1_12;
    goto state1_10;
}
if(current < 0x3F) /* ('>') '?' */  {
    if(current < 0x36) /* ('5') '6' */ 
        goto state1_29;
    if(current < 0x3A) /* ('9') ':' */ 
        goto state1_10;
    goto accept1_12;
}
if(current < 0x5B) /* ('Z') '[' */  {
    if(current < 0x40) /* ('?') '@' */ 
        goto state1_10;
    goto accept1_12;
}
if(current < 0x5E) /* (']') '^' */ 
    goto state1_10;
goto accept1_12;
/*
 * DFA STATE 19 (accepts to 1)
 */
state1_19:
pEnd = pNext;
goto accept1_1;
/*
 * DFA STATE 20 (accepts to 2)
 */
state1_20:
pEnd = pNext;
goto accept1_2;
/*
 * DFA STATE 21 (accepts to 3)
 */
state1_21:
pEnd = pNext;
goto accept1_3;
/*
 * DFA STATE 22
 * 'Ø' -> 30
 * 'ø' -> 30
 */
state1_22:
if(pNext >= pLimit) goto accept1_16;
current = *pNext++;
if(current < 0xD9) {
    if(current < 0xD8)
        goto accept1_16;
    goto state1_30;
}
if(current < 0xF8)
    goto accept1_16;
if(current < 0xF9)
    goto state1_30;
goto accept1_16;
/*
 * DFA STATE 23
 * '+' -> 31
 * ['0'-'9'] -> 31
 * '?' -> 31
 * '[' -> 31
 * ['\'-']'] -> 31
 */
state1_23:
if(pNext >= pLimit) goto accept1_11;
current = *pNext++;
if(current < 0x3A) /* ('9') ':' */  {
    if(current < 0x2C) /* ('+') ',' */  {
        if(current < 0x2B) /* ('*') '+' */ 
            goto accept1_11;
        goto state1_31;
    }
    if(current < 0x30) /* ('/') '0' */ 
        goto accept1_11;
    goto state1_31;
}
if(current < 0x40) /* ('?') '@' */  {
    if(current < 0x3F) /* ('>') '?' */ 
        goto accept1_11;
    goto state1_31;
}
if(current < 0x5B) /* ('Z') '[' */ 
    goto accept1_11;
if(current < 0x5E) /* (']') '^' */ 
    goto state1_31;
goto accept1_11;
/*
 * DFA STATE 24 (accepts to 4)
 */
state1_24:
pEnd = pNext;
goto accept1_4;
/*
 * DFA STATE 25 (accepts to 5)
 */
state1_25:
pEnd = pNext;
goto accept1_5;
/*
 * DFA STATE 26 (accepts to 6)
 */
state1_26:
pEnd = pNext;
goto accept1_6;
/*
 * DFA STATE 27 (accepts to 7)
 */
state1_27:
pEnd = pNext;
goto accept1_7;
/*
 * DFA STATE 28 (accepts to 8)
 */
state1_28:
pEnd = pNext;
goto accept1_8;
/*
 * DFA STATE 29 (accepts to 9)
 */
state1_29:
pEnd = pNext;
goto accept1_9;
/*
 * DFA STATE 30
 * 'D' -> 32
 * 'd' -> 32
 */
state1_30:
if(pNext >= pLimit) goto accept1_16;
current = *pNext++;
if(current < 0x45) /* ('D') 'E' */  {
    if(current < 0x44) /* ('C') 'D' */ 
        goto accept1_16;
    goto state1_32;
}
if(current < 0x64) /* ('c') 'd' */ 
    goto accept1_16;
if(current < 0x65) /* ('d') 'e' */ 
    goto state1_32;
goto accept1_16;
/*
 * DFA STATE 31 (accepts to 10)
 * '+' -> 31
 * ['0'-'9'] -> 31
 * '?' -> 31
 * '[' -> 31
 * ['\'-']'] -> 31
 */
state1_31:
pEnd = pNext;
if(pNext >= pLimit) goto accept1_10;
current = *pNext++;
if(current < 0x3A) /* ('9') ':' */  {
    if(current < 0x2C) /* ('+') ',' */  {
        if(current < 0x2B) /* ('*') '+' */ 
            goto accept1_10;
        goto state1_31;
    }
    if(current < 0x30) /* ('/') '0' */ 
        goto accept1_10;
    goto state1_31;
}
if(current < 0x40) /* ('?') '@' */  {
    if(current < 0x3F) /* ('>') '?' */ 
        goto accept1_10;
    goto state1_31;
}
if(current < 0x5B) /* ('Z') '[' */ 
    goto accept1_10;
if(current < 0x5E) /* (']') '^' */ 
    goto state1_31;
goto accept1_10;
/*
 * DFA STATE 32
 * 'E' -> 33
 * 'e' -> 33
 */
state1_32:
if(pNext >= pLimit) goto accept1_16;
current = *pNext++;
if(current < 0x46) /* ('E') 'F' */  {
    if(current < 0x45) /* ('D') 'E' */ 
        goto accept1_16;
    goto state1_33;
}
if(current < 0x65) /* ('d') 'e' */ 
    goto accept1_16;
if(current < 0x66) /* ('e') 'f' */ 
    goto state1_33;
goto accept1_16;
/*
 * DFA STATE 33 (accepts to 15)
 * 'N' -> 34
 * 'n' -> 34
 */
state1_33:
pEnd = pNext;
if(pNext >= pLimit) goto accept1_15;
current = *pNext++;
if(current < 0x4F) /* ('N') 'O' */  {
    if(current < 0x4E) /* ('M') 'N' */ 
        goto accept1_15;
    goto state1_34;
}
if(current < 0x6E) /* ('m') 'n' */ 
    goto accept1_15;
if(current < 0x6F) /* ('n') 'o' */ 
    goto state1_34;
goto accept1_15;
/*
 * DFA STATE 34 (accepts to 14)
 */
state1_34:
pEnd = pNext;
goto accept1_14;


accept1_0:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(0, (int)(pEnd - start)); 
accept1_1:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(1, (int)(pEnd - start)); 
accept1_2:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(2, (int)(pEnd - start)); 
accept1_3:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(3, (int)(pEnd - start)); 
accept1_4:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(4, (int)(pEnd - start)); 
accept1_5:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(5, (int)(pEnd - start)); 
accept1_6:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(6, (int)(pEnd - start)); 
accept1_7:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(7, (int)(pEnd - start)); 
accept1_8:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(8, (int)(pEnd - start)); 
accept1_9:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(9, (int)(pEnd - start)); 
accept1_10:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(10, (int)(pEnd - start)); 
accept1_11:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(11, (int)(pEnd - start)); 
accept1_12:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(12, (int)(pEnd - start)); 
accept1_13:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(12, (int)(pEnd - start)); 
accept1_14:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(14, (int)(pEnd - start)); 
accept1_15:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(13, (int)(pEnd - start)); 
accept1_16:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(14, (int)(pEnd - start)); 
nonaccept1:
 return null; }
        }
    }
}