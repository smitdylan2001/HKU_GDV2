using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Astar
{
	List<Node> openNodes = new List<Node>();
	List<Node> closedNodes = new List<Node>();
	List<Vector2Int> pathToTarget;

	/// <summary>
	/// TODO: Implement this function so that it returns a list of Vector2Int positions which describes a path
	/// Note that you will probably need to add some helper functions
	/// from the startPos to the endPos
	/// </summary>
	/// <param name="startPos"></param>
	/// <param name="endPos"></param>
	/// <param name="grid"></param>
	/// <returns></returns>
	public List<Vector2Int> FindPathToTarget(Vector2Int startPos, Vector2Int endPos, Cell[,] grid)
	{
		openNodes.Clear();
		closedNodes.Clear();
		
		Node startNode = new Node(startPos, null, 0, 0);
		Node endNode = new Node(endPos, null, 0, 0);
		Node currentNode;
		openNodes.Add(startNode);

		int Heuristic(Node a, Node b)
		{
			int x = Mathf.Abs(a.position.x - b.position.x);
			int y = Mathf.Abs(a.position.y - b.position.y);
			return x + y;
		}

		while (openNodes.Count > 0)
		{
			currentNode = openNodes[0];
			for (int i = 0; i < openNodes.Count; i++)
			{
				if (openNodes[i].FScore < currentNode.FScore)
				{
					currentNode = openNodes[i];
				}

				if (openNodes[i].FScore == currentNode.FScore)
				{
					if (openNodes[i].GScore > currentNode.GScore)
					{
						currentNode = openNodes[i];
					}
				}
			}
			if (currentNode == endNode)
			{
				GetPath(currentNode);
				break;
			}

			openNodes.Remove(currentNode);
			closedNodes.Add(currentNode);

			foreach (Node neighbour in currentNode.GetNeighbours(grid))
			{
				if (!closedNodes.Contains(neighbour))
				{
					float tempG = currentNode.GScore + Heuristic(neighbour, currentNode);
					if (!openNodes.Contains(neighbour))
					{
						openNodes.Add(neighbour);
					}
					else if (tempG >= neighbour.GScore)
					{
						continue;
					}

					neighbour.GScore = tempG;
					neighbour.HScore = Heuristic(neighbour, endNode);
					float temp = neighbour.FScore;
					neighbour.previous = currentNode;
				}
			}
		}

		List<Node> GetPath(Node node)
		{
			pathToTarget.Clear();
			List<Node> path = new List<Node>();
			Node temp = node;
			path.Add(temp);
			while (temp.previous != null)
			{
				path.Add(temp.previous);
				temp = temp.previous;
				pathToTarget.Add(new Vector2Int(temp.previous.position.x, temp.previous.position.y));
			}
			return path;
		}

		return pathToTarget;
	}
}


/// <summary>
/// This is the Node class you can use this class to store calculated FScores for the cells of the grid, you can leave this as it is
/// </summary>
public class Node
    {
        public Vector2Int position; //Position on the grid
        public Node parent; //Parent Node of this node

        public float FScore { //GScore + HScore
            get { return GScore + HScore; }
        }
        public float GScore; //Current Travelled Distance
        public float HScore; //Distance estimated based on Heuristic
        public bool walkable = true;
        public Node previous = null;

    public Node() { }

	public Node(Vector2Int position)
	{
		this.position = position;
	}

	public Node(Vector2Int position, Node parent, int GScore, int HScore)
    {
        this.position = position;
        this.parent = parent;
        this.GScore = GScore;
        this.HScore = HScore;
    }

    public List<Node> GetNeighbours(Cell[,] map)
    {
        List<Node> neighbours = new List<Node>();
        Vector2Int[] vector2Ints = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
        foreach (Vector2Int vector2Int in vector2Ints)
        {
            Vector2Int possibleNeighbourPosition = vector2Int + position;
            if (possibleNeighbourPosition.x >= 0 &&
                possibleNeighbourPosition.y >= 0 &&
                possibleNeighbourPosition.x <= map.GetUpperBound(0) &&
                possibleNeighbourPosition.y <= map.GetUpperBound(1))
            {
                Node neighbour = new Node(new Vector2Int(possibleNeighbourPosition.x, possibleNeighbourPosition.y));
                if (neighbour.walkable)
                {
                    neighbours.Add(neighbour);
                }
            }
        }

        return neighbours;
    }
}
