using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree {
	class Tree<Tn> where Tn : IComparable {
		public string Title;
		public Node<Tn> Root = null;

		private enum SIDE {
			LEFT, RIGHT, NONE
		}

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
			Node<Tn> prevNode = null;
			SIDE prevStep = SIDE.NONE;
			Node<Tn> node = this.Root;
			while (node != null) {
				var compare = node.Key.CompareTo(keyToRemove);
				if (compare == 0) {
					if (node.Left == null && node.Right == null) {
						if (prevNode == null) {
							this.Root = null;
							return;
						}
						if (prevStep == SIDE.LEFT) {
							prevNode.Left = null;
							return;
						}
						prevNode.Right = null;
						return;
					}
					if (node.Left == null) {
						if (prevNode == null) {
							this.Root = node.Right;
							return;
						}
						if (prevStep == SIDE.LEFT) {
							prevNode.Left = node.Right;
						} else {
							prevNode.Right = node.Right;
						}
						return;
					}
					if (node.Right == null) {
						if (prevNode == null) {
							this.Root = node.Left;
							return;
						}
						if (prevStep == SIDE.LEFT) {
							prevNode.Left = node.Left;
						} else {
							prevNode.Right = node.Left;
						}
						return;
					}
					Node<Tn> prevNodeForReplacement = null;
					Node<Tn> nodeForReplacement = node.Right;
					while (nodeForReplacement.Left != null) {
						prevNodeForReplacement = nodeForReplacement;
						nodeForReplacement = nodeForReplacement.Left;
					}
					node.Key = nodeForReplacement.Key;
					if (prevNodeForReplacement == null) {
						node.Right = nodeForReplacement.Right;
					} else {
						prevNodeForReplacement.Left = nodeForReplacement.Right;
					}
					return;
				}
				prevNode = node;
				if (compare > 0) {
					prevStep = SIDE.LEFT;
					node = node.Left;
				} else {
					prevStep = SIDE.RIGHT;
					node = node.Right;
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
