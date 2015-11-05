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

        public BTNode(T data)
        {
            this.Value = data;
        }


        public bool IsLeaf { get { return Left == null && Right == null; } }
    }

    public class BinaryTree<T> 
    {
        public BTNode<T> Root { get; set; }

        public int Height { get; set; }

        public BinaryTree(BTNode<T> root)
        {
            this.Root = root;
        }        

        public void Reverse()
        {
            Func<BTNode<T>, bool> travel = n => 
            {
                var tmp = n.Left;
                n.Left = n.Right;
                n.Right = tmp;
                return true;
            };

            PostOrderTraversal(Root, travel);
        }

        public List<BTNode<T>> DoPreOrderTraversal()
        {
            return DoTraversal(PreOrderTraversal); 
        }

        public List<BTNode<T>> DoInOrderTraversal()
        {
            return DoTraversal(InOrderTraversal);
        }

        public List<BTNode<T>> DoPostOrderTraversal()
        {
            return DoTraversal(PostOrderTraversal);
        }


        protected List<BTNode<T>> DoTraversal(Func<BTNode<T>, Func<BTNode<T>, bool>, bool> traversalMethod)
        {
            List<BTNode<T>> list = new List<BTNode<T>>();

            Func<BTNode<T>, bool> travel = n => { list.Add(n); return true; };
            traversalMethod(Root, travel);

            return list;
        }

        public static bool PreOrderTraversal(BTNode<T> node, Func<BTNode<T>, bool> travel)
        {
            bool success = travel(node);
            if (!success)
            {
                return false;
            }

            if (node.Left!=null)
            {
                if (!PreOrderTraversal(node.Left, travel))
                {
                    return false;
                }
            }
            if (node.Right!=null)
            {
                if (!PreOrderTraversal(node.Right, travel))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool InOrderTraversal(BTNode<T> node, Func<BTNode<T>, bool> travel)
        {
            if (node.Left != null)
            {
                if (!InOrderTraversal(node.Left, travel))
                {
                    return false;
                }
            }
            if (!travel(node))
            {
                return false;
            }
            if (node.Right != null)
            {
                if (!InOrderTraversal(node.Right, travel)) 
                {
                    return false;
                }
            }

            return true;
        }

        public static bool PostOrderTraversal(BTNode<T> node, Func<BTNode<T>, bool> travel)
        {
            if (node.Left != null)
            {
                if(!PostOrderTraversal(node.Left, travel))
                {
                    return false;
                }
            }
            if (node.Right != null)
            {
                if(!PostOrderTraversal(node.Right, travel))
                {
                    return false;
                }
            }
            if(!travel(node))
            {
                return false;
            }
            return true;
        }
    }

    public class BinarySearchTree<T> : BinaryTree<T> where T : IComparable
    {
        public BinarySearchTree(BTNode<T> root)
            : base(root)
        { }

        public bool Contains(T data)
        {
            var result = false;
            Search(null,
                whenMatch: () => { result = true; },
                whenNotFound: null,
                data: data);

            return result;
        }

        public virtual void Add(T data)
        {
            Search(null, null, 
                whenNotFound: (n, i) => 
                {
                    if (i>0)
                    {
                        n.Right = new BTNode<T>(data);
                    }
                    else
                    {
                        n.Left = new BTNode<T>(data);
                    }
                },
                data: data);
        }

        public void Search(Action<BTNode<T>> withEachNodeThrouth, 
                           Action whenMatch, 
                           Action<BTNode<T>, int> whenNotFound,
                           T data)
        {
            int result = 0;
            BTNode<T> prev = null;
            var node = Root;
            while (node != null)
            {
                if (withEachNodeThrouth!=null)
                {
                    withEachNodeThrouth(node);
                }
                prev = node;
                result = node.Value.CompareTo(data);
                switch (result)
                {
                    case 0:
                        if (whenMatch!=null)
                        {
                            whenMatch();
                        }
                        return;
                    case -1: node = node.Left; break;
                    case 1: node = node.Right; break;
                }
            }
            if (whenNotFound!=null)
            {
                whenNotFound(prev, result);
            }
        }



        public bool Remove(T data);

        public void Clear();

        public int Count { get; private set; }



    }

}
