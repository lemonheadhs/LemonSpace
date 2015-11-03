using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonBiz
{
    /// <summary>
    /// 二叉树节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BTNode<T>
    {
        public T Value { get; set; }
        public BTNode<T> Left { get; set; }
        public BTNode<T> Right { get; set; }

        public bool IsLeaf { get { return Left == null && Right == null; } }
    }

    public class BinaryTree<T> : ICollection<BTNode<T>>
    {
        public BTNode<T> Root { get; set; }

        public int Height { get; set; }

        public void Add(BTNode<T> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(BTNode<T> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(BTNode<T>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(BTNode<T> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<BTNode<T>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Reverse()
        {
            throw new NotImplementedException();
        }

        public List<BTNode<T>> DoPreOrderTraversal()
        {
            return DoTraversal(PreOrderTraversal); 
        }

        protected List<BTNode<T>> DoTraversal(Action<BTNode<T>, Action<BTNode<T>>> traversalMethod)
        {
            List<BTNode<T>> list = new List<BTNode<T>>(Count);

            Action<BTNode<T>> travel = n => list.Add(n);
            traversalMethod(Root, travel);

            return list;
        }

        public void PreOrderTraversal(BTNode<T> node, Action<BTNode<T>> travel)
        {
            travel(node);
            if (node.Left!=null)
            {
                PreOrderTraversal(node.Left, travel);
            }
            if (node.Right!=null)
            {
                PreOrderTraversal(node.Right, travel);
            }
        }

        public void InOrderTraversal(BTNode<T> node, Action<BTNode<T>> travel)
        {
            if (node.Left != null)
            {
                InOrderTraversal(node.Left, travel);
            }
            travel(node);
            if (node.Right != null)
            {
                InOrderTraversal(node.Right, travel);
            }
        }

        public void PostOrderTraversal(BTNode<T> node, Action<BTNode<T>> travel)
        {
            if (node.Left != null)
            {
                PostOrderTraversal(node.Left, travel);
            }
            if (node.Right != null)
            {
                PostOrderTraversal(node.Right, travel);
            }
            travel(node);
        }
    }
}
