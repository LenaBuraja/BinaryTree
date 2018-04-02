using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree {
	class Tree<Tn> where Tn : IComparable {
		public string Title;
		public Node<Tn> Root = null;
		public bool AutoBalancing = false;

		private enum SIDE {
			LEFT, RIGHT, NONE
		}

		public Tree(string _title, bool _autoBalancing = true) {
			this.Title = _title;
			this.AutoBalancing = _autoBalancing;
		}

		private void AddNode(Node<Tn> newNode) {
			if (Root == null) {
				Root = newNode;
			} else {
				InsertNode(newNode);
			}
		}

		private void AddNodes(Node<Tn>[ ] newNodes) {
			foreach (Node<Tn> node in newNodes) {
				this.AddNode(node);
			}
		}

		public void AddValue(Tn value) {
			this.AddNode(new Node<Tn>(value));
		}

		public void AddValues(Tn[ ] values) {
			foreach (Tn value in values) {
				this.AddValue(value);
			}
		}

		/// <summary>
		/// Insert new node in subtree
		/// </summary>
		/// <param name="newNode">Node to insert</param>
		/// <param name="startNode">Root node of subtree (default = root node of current tree)</param>
		private void InsertNode(Node<Tn> newNode, Node<Tn> startNode = null) {
			if (startNode == null) {
				startNode = Root;
			}
			if (newNode.Value.CompareTo(startNode.Value) < 0) {
				if (startNode.Left == null) {
					startNode.Left = newNode;
				} else {
					InsertNode(newNode, startNode.Left);
				}
			} else {
				if (startNode.Right == null) {
					startNode.Right = newNode;
				} else {
					InsertNode(newNode, startNode.Right);
				}
			}
			if (AutoBalancing) {
				this.Balance( );
			}
		}

		public Node<Tn> Find(Tn keyToFind, out Node<Tn> rootNode) {
			Node<Tn> nodeToFind = Root;
			rootNode = null;
			while (nodeToFind.Value.CompareTo(keyToFind) != 0) {
				rootNode = nodeToFind;
				if (nodeToFind.Value.CompareTo(keyToFind) < 0) {
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
				var compare = node.Value.CompareTo(keyToRemove);
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
					node.Value = nodeForReplacement.Value;
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
			if (AutoBalancing) {
				this.Balance( );
			}
		}

		public List<Tn> LCR(Node<Tn> current = null) {
			if (current == null) {
				current = this.Root;
			}
			List<Tn> result = new List<Tn>( );
			if (current.Left != null) {
				result.AddRange(LCR(current.Left));
			}
			result.Add(current.Value);
			if (current.Right != null) {
				result.AddRange(LCR(current.Right));
			}
			return result;
		}

		public List<Tn> LRC(Node<Tn> current = null) {
			if (current == null) {
				current = this.Root;
			}
			List<Tn> result = new List<Tn>( );
			result.Add(current.Value);
			if (current.Left != null) {
				result.AddRange(LCR(current.Left));
			}
			if (current.Right != null) {
				result.AddRange(LCR(current.Right));
			}
			return result;
		}

		public List<Tn> RCL(Node<Tn> current = null) {
			if (current == null) {
				current = this.Root;
			}
			List<Tn> result = new List<Tn>( );
			if (current.Left != null) {
				result.AddRange(LCR(current.Left));
			}
			if (current.Right != null) {
				result.AddRange(LCR(current.Right));
			}
			result.Add(current.Value);
			return result;
		}

		/// <summary>
		/// Check for tree is already balanced
		/// </summary>
		/// <param name="node">Root node of subtree (default = root node of current tree)</param>
		/// <returns>true if subtree is balanced or false otherwise</returns>
		public bool IsBalance(Node<Tn> node = null) {
			if (node == null) {
				node = this.Root;
			}
			bool leftIsBalance = true;
			bool rightIsBalance = true;
			int leftCountLevel = 0;
			int rightCountLevel = 0;
			if (node.Left != null) {
				leftIsBalance = IsBalance(node.Left);
				leftCountLevel = LevelsCount(node.Left);
			}
			if (node.Right != null) {
				rightIsBalance = IsBalance(node.Right);
				rightCountLevel = LevelsCount(node.Right);
			}
			return (
				leftIsBalance &&
				rightIsBalance &&
				Math.Abs(leftCountLevel - rightCountLevel) <= 1
			);
		}

		/// <summary>
		/// Returns count of levels of tree (or subtree)
		/// </summary>
		/// <param name="node">Root node of subtree (default = root node of current tree)</param>
		/// <returns>Levels' count</returns>
		private int LevelsCount(Node<Tn> node = null) {
			if (node == null) {
				node = this.Root;
			}
			int leftCountLevel = node.Left != null ? LevelsCount(node.Left) : 0;
			int rightCountLevel = node.Right != null ? LevelsCount(node.Right) : 0;
			return Math.Max(leftCountLevel, rightCountLevel) + 1;
		}

		public void Balance( ) {
			this.Root = SortedArrayToSubtree(this.GetSortedValues( ).ToArray( ));
		}

		/// <summary>
		/// Returns sorted values of tree (or subtree)
		/// </summary>
		/// <param name="current">Node, from which result will be generated (default = root node of current tree)</param>
		/// <returns>Sorted list of values</returns>
		private List<Tn> GetSortedValues(Node<Tn> current = null) {
			if (current == null) {
				current = this.Root;
			}
			List<Tn> result = new List<Tn>( );
			if (current.Left != null) {
				result.AddRange(GetSortedValues(current.Left));
			}
			result.Add(current.Value);
			if (current.Right != null) {
				result.AddRange(GetSortedValues(current.Right));
			}
			return result;
		}

		/// <summary>
		/// Returns balanced subtree from sorted array
		/// </summary>
		/// <param name="values">Sorted array</param>
		/// <param name="start">Start of sorted array (default = 0)</param>
		/// <param name="end">End of sorted array (default = index of last element)</param>
		/// <returns>Return Node (it can be used to make it Root)</returns>
		private Node<Tn> SortedArrayToSubtree(Tn[ ] values, int start = 0, int end = -2) {
			if (end == -2) {
				end = values.Length - 1;
			}
			if (start > end) {
				return null;
			}
			int middle = (start + end) / 2;
			Node<Tn> result = new Node<Tn>(values[middle]);
			result.Left = SortedArrayToSubtree(values, start, middle - 1);
			result.Right = SortedArrayToSubtree(values, middle + 1, end);
			return result;
		}

	}
}
