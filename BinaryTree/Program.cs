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

			//myTree.AddNode(new Node<int>(4));
			//myTree.AddNodes(new []{ new Node<int>(2), new Node<int>(1), new Node<int>(3)});
			//myTree.AddValue(5);
			//myTree.AddValues(new[ ] { 7, 6, 8 });

			//myTree.Remove(5);

			myTree.Inorder( );
			myTree.Preorder( );
			myTree.Postorder( );
		}
	}
}
