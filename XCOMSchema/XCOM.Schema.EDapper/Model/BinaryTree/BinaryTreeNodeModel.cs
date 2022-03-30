using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.Standard.Utility;

namespace XCOM.Schema.EDapper.Model.BinaryTree
{
    internal class BinaryTreeNodeModel<T>
    {

        public T Data { get; set; }
        public BinaryTreeNodeModel<T> LeftChild { get; set; }
        public BinaryTreeNodeModel<T> RightNode { get; set; }
        public BinaryTreeNodeModel()
        {

        }
        public BinaryTreeNodeModel(T data)
        {
            this.Data = data;
        }

        public override string ToString()
        {
            return XMJson.Serailze(this.Data);
        }

    }
}
