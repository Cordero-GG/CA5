public static class Program
{
    public static void Main()
    {
        
    }
}

public class AVLNode
{
    public int Key;
    public AVLNode Left;
    public AVLNode Right;
    public int Height;

    public AVLNode(int key)
    {
        Key = key;
        Height = 1;
        Left = null;
        Right = null;
    }
}
public class AVLTree
{
    private AVLNode root;

    public AVLNode GetRoot()
    {
        return root;
    }

    public int Height(AVLNode node)
    {
        if (node == null)
            return 0;
        return node.Height;
    }

    public int GetBalance(AVLNode node)
    {
        if (node == null)
            return 0;
        return Height(node.Left) - Height(node.Right);
    }

    public AVLNode RightRotate(AVLNode y)
    {
        AVLNode x = y.Left;
        AVLNode T2 = x.Right;

        // Perform rotation
        x.Right = y;
        y.Left = T2;

        // Update heights
        y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
        x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

        // Return new root
        return x;
    }

    public AVLNode LeftRotate(AVLNode x)
    {
        AVLNode y = x.Right;
        AVLNode T2 = y.Left;

        // Perform rotation
        y.Left = x;
        x.Right = T2;

        // Update heights
        x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
        y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

        // Return new root
        return y;
    }

    public void insert(int key)
    {
        root = InsertRecursive(root, key);
    }

    public AVLNode InsertRecursive(AVLNode node, int key)
    {
        // Perform the normal BST insertion
        if (node == null)
            return new AVLNode(key);

        if (key < node.Key)
            node.Left = InsertRecursive(node.Left, key);
        else if (key > node.Key)
            node.Right = InsertRecursive(node.Right, key);
        else // Duplicate keys are not allowed in the AVL tree
            return node;

        // Update height of this ancestor node
        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

        // Get the balance factor
        int balance = GetBalance(node);

        // If the node becomes unbalanced, then there are 4 cases

        // Left Left Case
        if (balance > 1 && key < node.Left.Key)
            return RightRotate(node);

        // Right Right Case
        if (balance < -1 && key > node.Right.Key)
            return LeftRotate(node);

        // Left Right Case
        if (balance > 1 && key > node.Left.Key)
        {
            node.Left = LeftRotate(node.Left);
            return RightRotate(node);
        }

        // Right Left Case
        if (balance < -1 && key < node.Right.Key)
        {
            node.Right = RightRotate(node.Right);
            return LeftRotate(node);
        }

        // Return the (unchanged) node pointer
        return node;
    }

    public void Eliminar(int key)
    {
        root = DeleteNode(root, key);
    }

    public AVLNode DeleteNode(AVLNode root, int key)
    {
        // Paso 1: Realizar la eliminación normal de BST
        if (root == null)
            return root;

        // Si la clave a eliminar es menor que la clave de la raíz,
        // entonces está en el subárbol izquierdo
        if (key < root.Key)
            root.Left = DeleteNode(root.Left, key);

        // Si la clave a eliminar es mayor que la clave de la raíz,
        // entonces está en el subárbol derecho
        else if (key > root.Key)
            root.Right = DeleteNode(root.Right, key);

        // Si la clave es igual a la clave de la raíz, este es el nodo a eliminar
        else
        {
            // Nodo con un solo hijo o sin hijos
            if (root.Left == null)
                return root.Right;
            else if (root.Right == null)
                return root.Left;

            // Nodo con dos hijos: obtener el sucesor inorden (el menor en el subárbol derecho)
            root.Key = MinValue(root.Right);

            // Eliminar el sucesor inorden
            root.Right = DeleteNode(root.Right, root.Key);
        }

        // Si el árbol tenía solo un nodo, retornarlo
        if (root == null)
            return root;

        // Paso 2: Actualizar la altura del nodo actual
        root.Height = Math.Max(Height(root.Left), Height(root.Right)) + 1;

        // Paso 3: Obtener el factor de balance
        int balance = GetBalance(root);

        // Casos de desbalance

        // Caso Izquierda Izquierda
        if (balance > 1 && GetBalance(root.Left) >= 0)
            return RightRotate(root);

        // Caso Izquierda Derecha
        if (balance > 1 && GetBalance(root.Left) < 0)
        {
            root.Left = LeftRotate(root.Left);
            return RightRotate(root);
        }

        // Caso Derecha Derecha
        if (balance < -1 && GetBalance(root.Right) <= 0)
            return LeftRotate(root);

        // Caso Derecha Izquierda
        if (balance < -1 && GetBalance(root.Right) > 0)
        {
            root.Right = RightRotate(root.Right);
            return LeftRotate(root);
        }

        return root;
    }

    // Método auxiliar para encontrar el valor mínimo en un árbol
    public int MinValue(AVLNode node)
    {
        int minValue = node.Key;
        while (node.Left != null)
        {
            minValue = node.Left.Key;
            node = node.Left;
        }
        return minValue;
    }

}