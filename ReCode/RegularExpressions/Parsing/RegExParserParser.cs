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
  /* perform shift to state 2 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(2); goto __state_2;
  case 4:
  /* perform shift to state 3 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(3); goto __state_3;
  case 8:
  /* perform shift to state 4 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(4); goto __state_4;
  case 12:
  /* perform shift to state 5 */
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
  /* perform shift to state 2 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(2); goto __state_2;
  case 4:
  /* perform shift to state 3 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(3); goto __state_3;
  case 5:
  /* perform shift to state 7 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(7); goto __state_7;
  case 6:
  /* perform shift to state 8 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(8); goto __state_8;
  case 7:
  /* perform shift to state 9 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(9); goto __state_9;
  case 8:
  /* perform shift to state 4 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(4); goto __state_4;
  case 10:
  /* perform shift to state 10 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(10); goto __state_10;
  case 11:
  /* perform shift to state 11 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(11); goto __state_11;
  case 12:
  /* perform shift to state 5 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(5); goto __state_5;
  case 13:
  /* perform shift to state 12 */
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
  /* perform shift to state 14 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(14); goto __state_14;
  case 4:
  /* perform shift to state 15 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(15); goto __state_15;
  case 8:
  /* perform shift to state 16 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(16); goto __state_16;
  case 12:
  /* perform shift to state 17 */
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
  case 1: case 3: case 4: case 8: case 10: case 12:
  /* do reduce using production 5 */
  goto __action_5;
  case 5:
  /* perform shift to state 7 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(7); goto __state_7;
  case 6:
  /* perform shift to state 8 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(8); goto __state_8;
  case 7:
  /* perform shift to state 9 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(9); goto __state_9;
  case 11:
  /* perform shift to state 11 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(11); goto __state_11;
  case 13:
  /* perform shift to state 12 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(12); goto __state_12;
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
  /* perform shift to state 2 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(2); goto __state_2;
  case 4:
  /* perform shift to state 3 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(3); goto __state_3;
  case 8:
  /* perform shift to state 4 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(4); goto __state_4;
  case 12:
  /* perform shift to state 5 */
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
  /* perform shift to state 2 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(2); goto __state_2;
  case 4:
  /* perform shift to state 3 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(3); goto __state_3;
  case 8:
  /* perform shift to state 4 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(4); goto __state_4;
  case 12:
  /* perform shift to state 5 */
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
  /* perform shift to state 14 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(14); goto __state_14;
  case 4:
  /* perform shift to state 15 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(15); goto __state_15;
  case 5:
  /* perform shift to state 21 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(21); goto __state_21;
  case 6:
  /* perform shift to state 22 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(22); goto __state_22;
  case 7:
  /* perform shift to state 23 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(23); goto __state_23;
  case 8:
  /* perform shift to state 16 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(16); goto __state_16;
  case 9:
  /* perform shift to state 24 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(24); goto __state_24;
  case 10:
  /* perform shift to state 25 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(25); goto __state_25;
  case 11:
  /* perform shift to state 26 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(26); goto __state_26;
  case 12:
  /* perform shift to state 17 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(17); goto __state_17;
  case 13:
  /* perform shift to state 27 */
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
  /* perform shift to state 14 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(14); goto __state_14;
  case 4:
  /* perform shift to state 15 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(15); goto __state_15;
  case 8:
  /* perform shift to state 16 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(16); goto __state_16;
  case 12:
  /* perform shift to state 17 */
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
  case 1: case 3: case 4: case 8: case 10: case 12:
  /* do reduce using production 6 */
  goto __action_6;
  case 5:
  /* perform shift to state 7 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(7); goto __state_7;
  case 6:
  /* perform shift to state 8 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(8); goto __state_8;
  case 7:
  /* perform shift to state 9 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(9); goto __state_9;
  case 11:
  /* perform shift to state 11 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(11); goto __state_11;
  case 13:
  /* perform shift to state 12 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(12); goto __state_12;
  default:
    goto __syntaxError;
  }

__state_19:
  switch((int)currentToken.Key){
  case 1: case 3: case 4: case 5: case 6: case 7: case 8: case 10: case 11: case 12:
  /* do reduce using production 11 */
  goto __action_11;
  case 13:
  /* perform shift to state 12 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(12); goto __state_12;
  default:
    goto __syntaxError;
  }

__state_20:
  switch((int)currentToken.Key){
  case 3: case 4: case 8: case 9: case 10: case 12:
  /* do reduce using production 5 */
  goto __action_5;
  case 5:
  /* perform shift to state 21 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(21); goto __state_21;
  case 6:
  /* perform shift to state 22 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(22); goto __state_22;
  case 7:
  /* perform shift to state 23 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(23); goto __state_23;
  case 11:
  /* perform shift to state 26 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(26); goto __state_26;
  case 13:
  /* perform shift to state 27 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(27); goto __state_27;
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
  /* perform shift to state 14 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(14); goto __state_14;
  case 4:
  /* perform shift to state 15 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(15); goto __state_15;
  case 8:
  /* perform shift to state 16 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(16); goto __state_16;
  case 12:
  /* perform shift to state 17 */
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
  /* perform shift to state 14 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(14); goto __state_14;
  case 4:
  /* perform shift to state 15 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(15); goto __state_15;
  case 8:
  /* perform shift to state 16 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(16); goto __state_16;
  case 12:
  /* perform shift to state 17 */
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
  /* perform shift to state 14 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(14); goto __state_14;
  case 4:
  /* perform shift to state 15 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(15); goto __state_15;
  case 5:
  /* perform shift to state 21 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(21); goto __state_21;
  case 6:
  /* perform shift to state 22 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(22); goto __state_22;
  case 7:
  /* perform shift to state 23 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(23); goto __state_23;
  case 8:
  /* perform shift to state 16 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(16); goto __state_16;
  case 9:
  /* perform shift to state 31 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(31); goto __state_31;
  case 10:
  /* perform shift to state 25 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(25); goto __state_25;
  case 11:
  /* perform shift to state 26 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(26); goto __state_26;
  case 12:
  /* perform shift to state 17 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(17); goto __state_17;
  case 13:
  /* perform shift to state 27 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(27); goto __state_27;
  default:
    goto __syntaxError;
  }

__state_29:
  switch((int)currentToken.Key){
  case 3: case 4: case 8: case 9: case 10: case 12:
  /* do reduce using production 6 */
  goto __action_6;
  case 5:
  /* perform shift to state 21 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(21); goto __state_21;
  case 6:
  /* perform shift to state 22 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(22); goto __state_22;
  case 7:
  /* perform shift to state 23 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(23); goto __state_23;
  case 11:
  /* perform shift to state 26 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(26); goto __state_26;
  case 13:
  /* perform shift to state 27 */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(27); goto __state_27;
  default:
    goto __syntaxError;
  }

__state_30:
  switch((int)currentToken.Key){
  case 3: case 4: case 5: case 6: case 7: case 8: case 9: case 10: case 11: case 12:
  /* do reduce using production 11 */
  goto __action_11;
  case 13:
  /* perform shift to state 27 */
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
