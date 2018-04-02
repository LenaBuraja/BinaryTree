using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree {
	class Program {
		static void Main(string[ ] args) {

			Tree<int> balancedTree = new Tree<int>("Balanced");
			Tree<int> notBalancedTree = new Tree<int>("NotBalanced", false);
			foreach (Tree<int> tree in new[ ] { balancedTree, notBalancedTree }) {
				tree.AddValues(new[ ] { 2, 5, 2, 1, 3, 5, 7, 6, 8 });
				tree.Remove(5);
				tree.Remove(5);
			}

			Tree<int> largeTree = new Tree<int>("Large");
			largeTree.AddValues(new[ ] {
				50, 25, 15, 10, 9, 12, 18, 17, 43, 30, 28, 45, 44, 47, 75, 60
			});
			largeTree.Remove(15);
			largeTree.Remove(18);

			foreach (Tree<int> tree in new[ ] { balancedTree, notBalancedTree, largeTree }) {
				Console.WriteLine("     Title: {0}", tree.Title);
				Console.WriteLine("   InOrder: {0}", string.Join(" ", tree.LCR( ).ToArray( )));
				Console.WriteLine("  PreOrder: {0}", string.Join(" ", tree.LRC( ).ToArray( )));
				Console.WriteLine(" PostOrder: {0}", string.Join(" ", tree.RCL( ).ToArray( )));
			}
		}
	}
}
