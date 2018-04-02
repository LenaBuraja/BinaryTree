using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree {
	class Node<T>: IComparable<T> where T: IComparable {
		public Node<T> Left { get; set; }
		public Node<T> Right { get; set; }
		public T Value { get; set; }

		public Node(T value) {
			this.Value = value;
		}

		public int CompareTo(T otherNode) {
			return Value.CompareTo(otherNode);
		}
	}
}
