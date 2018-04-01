using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree {
	class Tree<Tn> where Tn : IComparable {
		public string Title;
		public Node<Tn> Root = null;

		public Tree(string _title) {
			this.Title = _title;
		}

		public void AddNode(Node<Tn> newNode) {
			if (Root == null) {
				Root = newNode;
			} else {
				InsertPlace(Root, newNode);
			}
		}

		public void AddNodes(Node<Tn>[] newNodes) {
			foreach(Node<Tn> node in newNodes) {
				this.AddNode(node);
			}
		}

		public void AddValue(Tn value) {
			this.AddNode(new Node<Tn>(value));
		}

		public void AddValues(Tn[] values) {
			foreach(Tn value in values) {
				this.AddValue(value);
			}
		}

		public Node<Tn> Find(Tn keyToFind, out Node<Tn> rootNode) {
			Node<Tn> nodeToFind = Root;
			rootNode = null;
			while (nodeToFind.Key.CompareTo(keyToFind) != 0) {
				rootNode = nodeToFind;
				if (nodeToFind.Key.CompareTo(keyToFind) < 0) {
					nodeToFind = nodeToFind.Right;
				} else {
					nodeToFind = nodeToFind.Left;
				}
			}
			return nodeToFind;
		}

		public void Remove(Tn keyToRemove) {
			Node<Tn> nodeToRemove = Find(keyToRemove, out Node<Tn> rootNode);

			if (nodeToRemove.Right == null) {
				if (rootNode == null) {
					Root = nodeToRemove.Left;
				} else {
					if (rootNode.Key.CompareTo(nodeToRemove.Key) > 0) {
						rootNode.Left = nodeToRemove.Left;
					} else if (rootNode.Key.CompareTo(nodeToRemove.Key) < 0) {
						rootNode.Right = nodeToRemove.Right;
					}
				}

			} else if (nodeToRemove.Right.Left == null) {
				nodeToRemove.Right.Left = nodeToRemove.Left;
				if (rootNode == null) {
					Root = nodeToRemove.Right;
				} else {
					if (rootNode.Key.CompareTo(nodeToRemove.Key) > 0) {
						rootNode.Left = nodeToRemove.Right;
					} else if (rootNode.Key.CompareTo(nodeToRemove.Key) < 0) {
						rootNode.Right = nodeToRemove.Right;
					}
				}

			} else {
				Node<Tn> lastLeft = nodeToRemove.Right.Left;
				Node<Tn> rootLastLeft = nodeToRemove.Right;
				while (lastLeft.Left != null) {
					rootLastLeft = lastLeft;
					lastLeft = lastLeft.Left;
				}
				rootLastLeft.Left = lastLeft.Right;
				lastLeft.Left = nodeToRemove.Left;
				lastLeft.Right = nodeToRemove.Right;
				if (rootNode == null) {
					Root = lastLeft;
				} else {
					if (rootNode.Key.CompareTo(nodeToRemove.Key) > 0) {
						rootNode.Left = lastLeft;
					} else if (rootNode.Key.CompareTo(nodeToRemove.Key) < 0) {
						rootNode.Right = lastLeft;
					}
				}
			}
		}

		public void InsertPlace(Node<Tn> startNode, Node<Tn> newNode) {
			if (newNode.Key.CompareTo(startNode.Key) < 0) {
				if (startNode.Left == null) {
					startNode.Left = newNode;
				} else {
					InsertPlace(startNode.Left, newNode);
				}
			} else {
				if (startNode.Right == null) {
					startNode.Right = newNode;
				} else {
					InsertPlace(startNode.Right, newNode);
				}
			}
		}

		public void Inorder( ) {
			Console.Write("Inorder:");
			Tree<Tn> copyTree = this;
			LCR(copyTree.Root);
			Console.Write("\nPress to continue...");
			Console.ReadLine( );
		}

		private void LCR(Node<Tn> current) {
			if (current != null) {
				LCR(current.Left);
				Console.Write(" " + current.Key);
				LCR(current.Right);
			}
		}

		public void Preorder( ) {
			Console.Write("Preorder:");
			LRC(this.Root);
			Console.Write("\nPress to continue...");
			Console.ReadLine( );
		}

		private void LRC(Node<Tn> current) {
			if (current != null) {
				Console.Write(" " + current.Key);
				LRC(current.Left);
				LRC(current.Right);
			}
		} 

		public void Postorder( ) {
			Console.Write("Postorder:");
			RCL(this.Root);
			Console.Write("\nPress to continue...");
			Console.ReadLine( );
		}

		private void RCL(Node<Tn> current) {
			if (current != null) {
				RCL(current.Right);
				RCL(current.Left);
				Console.Write(" " + current.Key);
			}
		}
	}
}
