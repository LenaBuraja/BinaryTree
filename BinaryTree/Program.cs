using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree {
	class Program {
		static void Main(string[ ] args) {
			Tree<int> myTree = new Tree<int>("First");

			myTree.AddValues(new[ ] { 5, 2, 1, 3, 5, 7, 6, 8 });

			myTree.Remove(5);

			Tree<int> myTreeSecond = new Tree<int>("Second");
			myTreeSecond.AddNode(new Node<int>(4));
			myTreeSecond.AddNodes(new[ ] { new Node<int>(2), new Node<int>(1), new Node<int>(3) });
			myTreeSecond.AddValue(5);
			myTreeSecond.AddValues(new[ ] { 7, 6, 8 });

			myTree.Balancing( );

			myTree.Remove(5);

			if (myTree.IsBalance(myTree.Root)) {
				Console.WriteLine("Tree is balances");
			} else {
				Console.WriteLine("Tree isn't balances");
			}
			myTree.AutoBalances( );

			Tree<int> myTreeTherd = new Tree<int>("Therd");
			myTreeTherd.AddValues(new[ ] { 50, 25, 15, 10, 9, 12, 18, 17, 43, 30, 28, 45, 44, 47, 75, 60 });
			myTreeTherd.Remove(15);
			myTreeTherd.Remove(18);
			if (myTreeTherd.IsBalance(myTreeTherd.Root)) {
				Console.WriteLine("Tree is balances");
			} else {
				Console.WriteLine("Tree isn't balances");
			}
			Console.ReadLine( );
			myTreeTherd.Balancing( );
			if (myTree.IsBalance(myTreeTherd.Root)) {
				Console.WriteLine("Tree is balances");
			} else {
				Console.WriteLine("Tree isn't balances");
			}


			myTree.Inorder( );
			myTree.Preorder( );
			myTree.Postorder( );
		}
	}
}
