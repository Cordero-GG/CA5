namespace PruebasUnitarias
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AVLTreeTests
    {
        [TestMethod]
        public void Insert_SingleNode_ShouldSetAsRoot()
        {
            // Arrange
            var tree = new AVLTree();

            // Act
            tree.insert(10);

            // Assert
            Assert.IsNotNull(tree.GetRoot());
            Assert.AreEqual(10, tree.GetRoot().Key);
        }

        [TestMethod]
        public void Insert_MultipleNodes_ShouldMaintainBalance()
        {
            // Arrange
            var tree = new AVLTree();

            // Act
            tree.insert(10);
            tree.insert(20);
            tree.insert(30); // Should trigger rotation

            // Assert
            var root = tree.GetRoot();
            Assert.AreEqual(20, root.Key);
            Assert.AreEqual(10, root.Left.Key);
            Assert.AreEqual(30, root.Right.Key);
        }

        [TestMethod]
        public void Insert_DuplicateKey_ShouldNotAddDuplicate()
        {
            // Arrange
            var tree = new AVLTree();
            tree.insert(10);

            // Act
            tree.insert(10); // Duplicate

            // Assert
            Assert.AreEqual(1, CountNodes(tree.GetRoot()));
        }

        [TestMethod]
        public void Delete_LeafNode_ShouldRemoveIt()
        {
            // Arrange
            var tree = new AVLTree();
            tree.insert(10);
            tree.insert(5);
            tree.insert(15);

            // Act
            tree.Eliminar(5);

            // Assert
            var root = tree.GetRoot();
            Assert.IsNull(root.Left);
            Assert.AreEqual(15, root.Right.Key);
        }

        [TestMethod]
        public void Delete_NodeWithOneChild_ShouldReplaceWithChild()
        {
            // Arrange
            var tree = new AVLTree();
            tree.insert(10);
            tree.insert(5);
            tree.insert(15);
            tree.insert(3);

            // Act
            tree.Eliminar(5); // Has left child (3)

            // Assert
            var root = tree.GetRoot();
            Assert.AreEqual(3, root.Left.Key);
        }

        [TestMethod]
        public void Delete_NodeWithTwoChildren_ShouldReplaceWithInorderSuccessor()
        {
            // Arrange
            var tree = new AVLTree();
            tree.insert(10);
            tree.insert(5);
            tree.insert(15);
            tree.insert(12);
            tree.insert(20);

            // Act
            tree.Eliminar(15); // Has two children (12 and 20)

            // Assert
            var root = tree.GetRoot();
            Assert.AreEqual(20, root.Right.Key);
            Assert.AreEqual(12, root.Right.Left.Key);
        }

        [TestMethod]
        public void Delete_RootNode_ShouldReplaceWithInorderSuccessor()
        {
            // Arrange
            var tree = new AVLTree();
            tree.insert(10);
            tree.insert(5);
            tree.insert(15);

            // Act
            tree.Eliminar(10); // Deleting root

            // Assert
            var newRoot = tree.GetRoot();
            Assert.AreEqual(15, newRoot.Key);
            Assert.AreEqual(5, newRoot.Left.Key);
        }

        [TestMethod]
        public void Delete_FromEmptyTree_ShouldNotThrow()
        {
            // Arrange
            var tree = new AVLTree();

            // Act & Assert (should not throw)
            tree.Eliminar(10);
        }

        [TestMethod]
        public void Height_EmptyTree_ShouldReturnZero()
        {
            // Arrange
            var tree = new AVLTree();

            // Act & Assert
            Assert.AreEqual(0, tree.Height(tree.GetRoot()));
        }

        [TestMethod]
        public void Height_SingleNode_ShouldReturnOne()
        {
            // Arrange
            var tree = new AVLTree();
            tree.insert(10);

            // Act & Assert
            Assert.AreEqual(1, tree.Height(tree.GetRoot()));
        }

        [TestMethod]
        public void GetBalance_EmptyTree_ShouldReturnZero()
        {
            // Arrange
            var tree = new AVLTree();

            // Act & Assert
            Assert.AreEqual(0, tree.GetBalance(tree.GetRoot()));
        }

        [TestMethod]
        public void GetBalance_LeftHeavyTree_ShouldReturnPositive()
        {
            // Arrange
            var tree = new AVLTree();
            tree.insert(10);
            tree.insert(5);

            // Act & Assert
            Assert.IsTrue(tree.GetBalance(tree.GetRoot()) > 0);
        }

        [TestMethod]
        public void GetBalance_RightHeavyTree_ShouldReturnNegative()
        {
            // Arrange
            var tree = new AVLTree();
            tree.insert(10);
            tree.insert(15);

            // Act & Assert
            Assert.IsTrue(tree.GetBalance(tree.GetRoot()) < 0);
        }

        // Helper method to count nodes in the tree
        private int CountNodes(AVLNode node)
        {
            if (node == null)
                return 0;
            return 1 + CountNodes(node.Left) + CountNodes(node.Right);
        }
    }
}
