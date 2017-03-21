using System.Collections.Generic;
using System.Text;

namespace ReCodeTest
{

    public abstract class Node
    {
    }

    public class NodeArray : Node
    {
        public readonly List<Node> Items = new List<Node>();

        public NodeArray()
        {
        }

        public NodeArray(Node node)
        {
            Items.Add(node);
        }

        public NodeArray Add(Node node)
        {
            Items.Add(node);
            return this;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            var first = true;
            foreach (var item in Items)
            {
                if (first)
                    first = false;
                else
                    sb.Append(",");
                sb.Append(item);
            }
            sb.Append("]");
            return sb.ToString();
        }
    }

    public class NodeString : Node
    {
        public string Value;

        public override string ToString()
        {
            return $"\"{Value}\"";
        }
    }

    public class NodeObject : Node
    {
        public readonly List<KeyValuePair<NodeString, Node>> Properties = new List<KeyValuePair<NodeString, Node>>();

        public NodeObject() { }

        public NodeObject(NodeString key, Node value)
        {
            Properties.Add(new KeyValuePair<NodeString, Node>(key, value));
        }

        public NodeObject AddRange(IEnumerable<KeyValuePair<NodeString, Node>> range)
        {
            Properties.AddRange(range);
            return this;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("{");
            var first = true;
            foreach (var property in Properties)
            {
                if (first)
                    first = false;
                else
                    sb.Append(",");
                sb.Append($"{property.Key}={property.Value}");
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}