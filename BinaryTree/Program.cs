using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree {
	class Program {
		static void Main(string[ ] args) {
			Tree<int> myTree = new Tree<int>("First", new Node<int>(4));
			myTree.AddNode(new Node<int>(2));
			myTree.AddNode(new Node<int>(1));
			myTree.AddNode(new Node<int>(3));
			myTree.AddNode(new Node<int>(5));
			myTree.AddNode(new Node<int>(7));
			myTree.AddNode(new Node<int>(6));
			myTree.AddNode(new Node<int>(8));

			myTree.Remove(5);

			myTree.Inorder( );
			myTree.Preorder( );
			myTree.Postorder( );
		}
	}
}
