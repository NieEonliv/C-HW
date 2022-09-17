using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab7
{
    public class TreeExpasion : IBinaryTree
    {
        private TreeExpasion(char data)
        {
            Data = data;
        }
        public TreeExpasion() { }
        public string Expasion { get; set; }
        private char Data = '`'; 
        public object Value
        {
            get { return Data; }
            set { Data = value.ToString()[0]; }
        }
        public TreeExpasion left;
        public TreeExpasion right;
        public TreeExpasion Left { get; private set; }
        public TreeExpasion Right { get; private set; }

        private int GetLoyPriorytiIndex()
        {
            int bed = 0;
            for (int i = 0; i < Expasion.Length; i++)
            {
                if (Expasion[i] == '(')
                    bed += 1;
                if (Expasion[i] == ')')
                    bed -= 1;
                if (Expasion[i] == '+' && bed == 0)
                    return i;
                if (Expasion[i] == '-' && bed == 0)
                    return i;
                if (Expasion[i] == '*' && bed == 0)
                    return i;
                if (Expasion[i] == ':' && bed == 0)
                    return i;
            }
            return -1;
        }
        private void OperandSplit(char znak, int index)
        {
            if (Data == '`')
                Data = znak;

            StringBuilder sb = new StringBuilder(Expasion);
            sb[index] = '|';
            Expasion = sb.ToString();

            var operands = Expasion.Split('|');

            CalculateNode(ref left, ref operands[0]);
            CalculateNode(ref right, ref operands[1]);

            RemoveGrop(ref left, ref operands[0]);
            RemoveGrop(ref right, ref operands[1]);
        }
        private void RemoveGrop(ref TreeExpasion node, ref string operand)
        {
            if (Regex.IsMatch(operand, "^\\(.*\\)$"))
            {
                operand = operand.TrimEnd(')');
                operand = operand.TrimStart('(');
            }
            CalculateNode(ref node, ref operand);
        }
        private void CalculateNode(ref TreeExpasion node, ref string operand)
        {
            if (operand.Length == 1)
                node = new TreeExpasion(operand.ToCharArray()[0]);
            else
            {
                node = new TreeExpasion();
                node.Expasion = operand;
                node = node.ShowTree();
            }
        }
        public TreeExpasion ShowTree()
        {
            var index = GetLoyPriorytiIndex();
            if (index != -1)
                OperandSplit(Expasion[index], index);

            Right = right;
            Left = left;
            return this;
        }
        object IBinaryTree.Value => Value;
        IBinaryTree IBinaryTree.Left => Left;
        IBinaryTree IBinaryTree.Right => right;
    }
    public interface IBinaryTree
    {
        object Value { get; }
        IBinaryTree  Left { get; }
        IBinaryTree Right { get; }
        string Expasion { get; set; }
        TreeExpasion ShowTree();
    }
}
