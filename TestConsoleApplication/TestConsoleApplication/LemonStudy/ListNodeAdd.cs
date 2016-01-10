using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication.LemonStudy
{
    /**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
    public class Solution
    {
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode root = null, tmp = null;
            AddHelper hlp = new AddHelper(l1, l2);
            foreach (var n in hlp.GetSumNodes())
            {
                if (root == null)
                {
                    root = n;
                    tmp = n;
                }
                else
                {
                    tmp.next = n;
                    tmp = n;
                }
            }
            return root;
        }

        public class AddHelper
        {
            private ListNode left;
            private ListNode right;

            public AddHelper(ListNode l1, ListNode l2)
            {
                this.left = l1;
                this.right = l2;
            }

            public IEnumerable<ListNode> GetSumNodes()
            {
                ListNode x = left;
                ListNode y = right;
                int? carry = 0;

                while (x != null && y != null)
                {
                    ListNode n = Add(x, y, ref carry);
                    x = x.next;
                    y = y.next;
                    yield return n;
                }
                ListNode remain = null;
                if (x != null)
                {
                    remain = x;
                }
                else if (y != null)
                {
                    remain = y;
                }
                while (remain != null)
                {
                    ListNode n1 = CopyVal(remain, ref carry);
                    remain = remain.next;
                    yield return n1;
                }
            }

            private ListNode Add(ListNode x, ListNode y, ref int? c)
            {
                int sum = x.val + y.val + c.Value;
                bool needToCarry = sum >= 10;
                c = needToCarry ? 1 : 0;
                if (needToCarry)
                {
                    return new ListNode(sum - 10);
                }
                else
                {
                    return new ListNode(sum);
                }
            }

            private ListNode CopyVal(ListNode x, ref int? c)
            {
                ListNode cpy = c == 1 ?
                    new ListNode(x.val + 1)
                    : new ListNode(x.val);
                c = 0;
                return cpy;
            }
        }
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }
    }
}
