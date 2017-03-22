namespace ReCodeTest {
	public class TestParser3 {
		public static unsafe int Parse(char * start, int len){
            var pNext = start;
            var pLimit = start + len;
            var pEnd = start; 
	    textMode:
