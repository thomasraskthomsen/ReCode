using System;
using System.Collections.Generic;

namespace ReCodeTest {
    public partial class TestLr1 {
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
    private readonly ArrayStack<Node> _valueStack = new ArrayStack<Node>(DefaultStackSize);

        
public enum Token {
  ItemRoot,
  End,
  Item,
  List,
  MemberList,
  Property,
  BrBegin,
  BrEnd,
  CbBegin,
  CbEnd,
  Comma,
  Equal,
  Letters,
}
public Node Parse() {
Node res = default(Node);
var targetTokenType = 0;
_stateStack.Reset();
_valueStack.Reset();
KeyValuePair<Token,Node> currentToken = NextToken();
_stateStack.Push(0);
goto __state_0;
__postReduce:
/* find on the stack the state where we need to handle post-reduce goto. */
var gotoState = _stateStack.Elements[_stateStack.Top];
/* put information on the stack regarding the reduced type. */
_stateStack.Push(targetTokenType);
/* jump to the resulting state. */
switch(gotoState){
case 0:
switch(targetTokenType){
case 2:
_stateStack.Push(1);
goto __state_1;
default:
  goto __syntaxError;
}
case 1:
switch(targetTokenType){
default:
  goto __syntaxError;
}
case 2:
switch(targetTokenType){
case 2:
_stateStack.Push(6);
goto __state_6;
case 3:
_stateStack.Push(7);
goto __state_7;
default:
  goto __syntaxError;
}
case 3:
switch(targetTokenType){
case 4:
_stateStack.Push(8);
goto __state_8;
case 5:
_stateStack.Push(9);
goto __state_9;
default:
  goto __syntaxError;
}
case 4:
switch(targetTokenType){
default:
  goto __syntaxError;
}
case 5:
switch(targetTokenType){
default:
  goto __syntaxError;
}
case 6:
switch(targetTokenType){
default:
  goto __syntaxError;
}
case 7:
switch(targetTokenType){
default:
  goto __syntaxError;
}
case 8:
switch(targetTokenType){
case 5:
_stateStack.Push(14);
goto __state_14;
default:
  goto __syntaxError;
}
case 9:
switch(targetTokenType){
default:
  goto __syntaxError;
}
case 10:
switch(targetTokenType){
default:
  goto __syntaxError;
}
case 11:
switch(targetTokenType){
default:
  goto __syntaxError;
}
case 12:
switch(targetTokenType){
default:
  goto __syntaxError;
}
case 13:
switch(targetTokenType){
case 2:
_stateStack.Push(17);
goto __state_17;
default:
  goto __syntaxError;
}
case 14:
switch(targetTokenType){
default:
  goto __syntaxError;
}
case 15:
switch(targetTokenType){
default:
  goto __syntaxError;
}
case 16:
switch(targetTokenType){
case 2:
_stateStack.Push(18);
goto __state_18;
default:
  goto __syntaxError;
}
case 17:
switch(targetTokenType){
default:
  goto __syntaxError;
}
case 18:
switch(targetTokenType){
default:
  goto __syntaxError;
}
default: goto __syntaxError;
}
__state_0:
switch((int)currentToken.Key){
  case 6:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(2);
  goto __state_2;
  case 8:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(3);
  goto __state_3;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(4);
  goto __state_4;
default:
  goto __syntaxError;
}
__state_1:
switch((int)currentToken.Key){
  case 1:
  /* accept value from stack */
  return _valueStack.Elements[_valueStack.Top];
default:
  goto __syntaxError;
}
__state_2:
switch((int)currentToken.Key){
  case 6:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(2);
  goto __state_2;
  case 8:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(3);
  goto __state_3;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(4);
  goto __state_4;
default:
  goto __syntaxError;
}
__state_3:
switch((int)currentToken.Key){
  case 9:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(10);
  goto __state_10;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(11);
  goto __state_11;
default:
  goto __syntaxError;
}
__state_4:
switch((int)currentToken.Key){
  case 1:
  case 7:
  case 9:
  case 10:
  case 12:
  /* do reduce using production 1 */
  goto __action_1;
default:
  goto __syntaxError;
}
__state_5:
switch((int)currentToken.Key){
default:
  goto __syntaxError;
}
__state_6:
switch((int)currentToken.Key){
  case 7:
  case 10:
  /* do reduce using production 5 */
  goto __action_5;
default:
  goto __syntaxError;
}
__state_7:
switch((int)currentToken.Key){
  case 7:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(12);
  goto __state_12;
  case 10:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(13);
  goto __state_13;
default:
  goto __syntaxError;
}
__state_8:
switch((int)currentToken.Key){
  case 9:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(15);
  goto __state_15;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(11);
  goto __state_11;
default:
  goto __syntaxError;
}
__state_9:
switch((int)currentToken.Key){
  case 9:
  case 12:
  /* do reduce using production 7 */
  goto __action_7;
default:
  goto __syntaxError;
}
__state_10:
switch((int)currentToken.Key){
  case 1:
  case 7:
  case 9:
  case 10:
  case 12:
  /* do reduce using production 3 */
  goto __action_3;
default:
  goto __syntaxError;
}
__state_11:
switch((int)currentToken.Key){
  case 11:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(16);
  goto __state_16;
default:
  goto __syntaxError;
}
__state_12:
switch((int)currentToken.Key){
  case 1:
  case 7:
  case 9:
  case 10:
  case 12:
  /* do reduce using production 2 */
  goto __action_2;
default:
  goto __syntaxError;
}
__state_13:
switch((int)currentToken.Key){
  case 6:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(2);
  goto __state_2;
  case 8:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(3);
  goto __state_3;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(4);
  goto __state_4;
default:
  goto __syntaxError;
}
__state_14:
switch((int)currentToken.Key){
  case 9:
  case 12:
  /* do reduce using production 8 */
  goto __action_8;
default:
  goto __syntaxError;
}
__state_15:
switch((int)currentToken.Key){
  case 1:
  case 7:
  case 9:
  case 10:
  case 12:
  /* do reduce using production 4 */
  goto __action_4;
default:
  goto __syntaxError;
}
__state_16:
switch((int)currentToken.Key){
  case 6:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(2);
  goto __state_2;
  case 8:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(3);
  goto __state_3;
  case 12:
  /* perform shift to state {action.Target} */
  _valueStack.Push(currentToken.Value);
  _stateStack.Push((int)currentToken.Key);
  currentToken = NextToken();
  _stateStack.Push(4);
  goto __state_4;
default:
  goto __syntaxError;
}
__state_17:
switch((int)currentToken.Key){
  case 7:
  case 10:
  /* do reduce using production 6 */
  goto __action_6;
default:
  goto __syntaxError;
}
__state_18:
switch((int)currentToken.Key){
  case 9:
  case 12:
  /* do reduce using production 9 */
  goto __action_9;
default:
  goto __syntaxError;
}
__action_1:
{
res = default(Node);
var arg0 = _valueStack.Elements[_valueStack.Top-0];
 res = arg0; }
_valueStack.Pop(1);
_valueStack.Push(res);
// find goto state and adjust state stack
_stateStack.Pop(2);
targetTokenType = 2;
goto __postReduce;
__action_2:
{
res = default(Node);
var arg0 = _valueStack.Elements[_valueStack.Top-2];
var arg1 = _valueStack.Elements[_valueStack.Top-1];
var arg2 = _valueStack.Elements[_valueStack.Top-0];
 res = arg1; }
_valueStack.Pop(3);
_valueStack.Push(res);
// find goto state and adjust state stack
_stateStack.Pop(6);
targetTokenType = 2;
goto __postReduce;
__action_3:
{
res = default(Node);
var arg0 = _valueStack.Elements[_valueStack.Top-1];
var arg1 = _valueStack.Elements[_valueStack.Top-0];
 res = new NodeObject(); }
_valueStack.Pop(2);
_valueStack.Push(res);
// find goto state and adjust state stack
_stateStack.Pop(4);
targetTokenType = 2;
goto __postReduce;
__action_4:
{
res = default(Node);
var arg0 = _valueStack.Elements[_valueStack.Top-2];
var arg1 = _valueStack.Elements[_valueStack.Top-1];
var arg2 = _valueStack.Elements[_valueStack.Top-0];
 res = arg1; }
_valueStack.Pop(3);
_valueStack.Push(res);
// find goto state and adjust state stack
_stateStack.Pop(6);
targetTokenType = 2;
goto __postReduce;
__action_5:
{
res = default(Node);
var arg0 = _valueStack.Elements[_valueStack.Top-0];
 res = new NodeArray(arg0); }
_valueStack.Pop(1);
_valueStack.Push(res);
// find goto state and adjust state stack
_stateStack.Pop(2);
targetTokenType = 3;
goto __postReduce;
__action_6:
{
res = default(Node);
var arg0 = _valueStack.Elements[_valueStack.Top-2];
var arg1 = _valueStack.Elements[_valueStack.Top-1];
var arg2 = _valueStack.Elements[_valueStack.Top-0];
 res = (arg0 as NodeArray).Add(arg2); }
_valueStack.Pop(3);
_valueStack.Push(res);
// find goto state and adjust state stack
_stateStack.Pop(6);
targetTokenType = 3;
goto __postReduce;
__action_7:
{
res = default(Node);
var arg0 = _valueStack.Elements[_valueStack.Top-0];
 res = arg0; }
_valueStack.Pop(1);
_valueStack.Push(res);
// find goto state and adjust state stack
_stateStack.Pop(2);
targetTokenType = 4;
goto __postReduce;
__action_8:
{
res = default(Node);
var arg0 = _valueStack.Elements[_valueStack.Top-1];
var arg1 = _valueStack.Elements[_valueStack.Top-0];
 res = (arg0 as NodeObject).AddRange((arg1 as NodeObject).Properties); }
_valueStack.Pop(2);
_valueStack.Push(res);
// find goto state and adjust state stack
_stateStack.Pop(4);
targetTokenType = 4;
goto __postReduce;
__action_9:
{
res = default(Node);
var arg0 = _valueStack.Elements[_valueStack.Top-2];
var arg1 = _valueStack.Elements[_valueStack.Top-1];
var arg2 = _valueStack.Elements[_valueStack.Top-0];
 res = new NodeObject(arg0 as NodeString, arg2); }
_valueStack.Pop(3);
_valueStack.Push(res);
// find goto state and adjust state stack
_stateStack.Pop(6);
targetTokenType = 5;
goto __postReduce;
  __syntaxError:
  throw new Exception("Syntax error");
}
    }
}
