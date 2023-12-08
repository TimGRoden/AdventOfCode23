using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8
{
    public class Node
    {
        public string ID;
        public Node LeftNode { get; private set; }
        public Node RightNode { get; private set; }

        public Node(string line)
        {
            ID = line.Split(' ')[0];
        }
        public void linkNodes(Node L, Node R) => (LeftNode, RightNode) = (L,R);
        public string getNode() => $"{ID} = ({LeftNode.ID}, {RightNode.ID})";
    }
}
