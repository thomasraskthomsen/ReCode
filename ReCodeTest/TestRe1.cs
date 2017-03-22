using System.Collections.Generic;

namespace ReCodeTest {
	public class TestParser1 {
		public static unsafe KeyValuePair<ushort, int>? Parse(char * start, int len){
            var pNext = start;
            var pLimit = start + len;
            var pEnd = start;
{
/*
 * Union
 *  +-Accept(0)
 *  |  +-Repeat(OneOrMore)
 *  |     +-Ranges()
 *  +-Accept(1)
 *     +-Repeat(OneOrMore)
 *        +-Ranges(['a'-'b'],'e',['h'-'i'],['A'-'B'],'E',['H'-'I'],['a'-'b'],'e',['h'-'i'],['A'-'B'],'E',['H'-'I'])
 */
/*
 * DFA STATE 0
 * ['A'-'B'] -> 1
 * 'E' -> 1
 * ['H'-'I'] -> 1
 * ['a'-'b'] -> 1
 * 'e' -> 1
 * ['h'-'i'] -> 1
 */
if(pNext >= pLimit) goto nonaccept2;
var current = *pNext++;
if(current < 0x4A) /* ('I') 'J' */  {
    if(current < 0x45) /* ('D') 'E' */  {
        if(current < 0x41) /* ('@') 'A' */ 
            goto nonaccept2;
        if(current < 0x43) /* ('B') 'C' */ 
            goto state2_1;
        goto nonaccept2;
    }
    if(current < 0x46) /* ('E') 'F' */ 
        goto state2_1;
    if(current < 0x48) /* ('G') 'H' */ 
        goto nonaccept2;
    goto state2_1;
}
if(current < 0x65) /* ('d') 'e' */  {
    if(current < 0x61) /* ('`') 'a' */ 
        goto nonaccept2;
    if(current < 0x63) /* ('b') 'c' */ 
        goto state2_1;
    goto nonaccept2;
}
if(current < 0x68) /* ('g') 'h' */  {
    if(current < 0x66) /* ('e') 'f' */ 
        goto state2_1;
    goto nonaccept2;
}
if(current < 0x6A) /* ('i') 'j' */ 
    goto state2_1;
goto nonaccept2;
/*
 * DFA STATE 1 (accepts to 1)
 * ['A'-'B'] -> 1
 * 'E' -> 1
 * ['H'-'I'] -> 1
 * ['a'-'b'] -> 1
 * 'e' -> 1
 * ['h'-'i'] -> 1
 */
state2_1:
pEnd = pNext;
if(pNext >= pLimit) goto accept2_1;
current = *pNext++;
if(current < 0x4A) /* ('I') 'J' */  {
    if(current < 0x45) /* ('D') 'E' */  {
        if(current < 0x41) /* ('@') 'A' */ 
            goto accept2_1;
        if(current < 0x43) /* ('B') 'C' */ 
            goto state2_1;
        goto accept2_1;
    }
    if(current < 0x46) /* ('E') 'F' */ 
        goto state2_1;
    if(current < 0x48) /* ('G') 'H' */ 
        goto accept2_1;
    goto state2_1;
}
if(current < 0x65) /* ('d') 'e' */  {
    if(current < 0x61) /* ('`') 'a' */ 
        goto accept2_1;
    if(current < 0x63) /* ('b') 'c' */ 
        goto state2_1;
    goto accept2_1;
}
if(current < 0x68) /* ('g') 'h' */  {
    if(current < 0x66) /* ('e') 'f' */ 
        goto state2_1;
    goto accept2_1;
}
if(current < 0x6A) /* ('i') 'j' */ 
    goto state2_1;
goto accept2_1;


accept2_0:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(0, (int)(pEnd - start)); 
accept2_1:
 pNext = pEnd;
 return new KeyValuePair<ushort,int>(0, (int)(pEnd - start)); 
nonaccept2:
 return null; }

        }
    }
}