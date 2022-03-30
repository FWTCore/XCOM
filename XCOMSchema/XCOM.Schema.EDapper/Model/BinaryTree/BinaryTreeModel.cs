using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.EDapper.Model.BinaryTree
{
    internal class BinaryTreeModel<T>
    {
        public BinaryTreeNodeModel<T> Root { get; }

        public BinaryTreeModel(BinaryTreeNodeModel<T> rootNode)
        {
            Root = rootNode;
        }
        public BinaryTreeModel(T data)
        {
            Root = new BinaryTreeNodeModel<T>(data);
        }



    }
}
