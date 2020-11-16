using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Astar
{
	List<Node> openNodes = new List<Node>();
	Node currentNode;

	/// <summary>
	/// Returns a path from the character to the end position
	/// </summary>
	/// <param name="startPos"></param>
	/// <param name="endPos"></param>
	/// <param name="grid"></param>
	/// <returns></returns>
	public List<Vector2Int> FindPathToTarget(Vector2Int startPos, Vector2Int endPos, Cell[,] grid)
	{
		Node[,] map = CreateNodeGrid(grid, endPos);
		openNodes.Clear();

		//Get nodes from startposition and endposition
		map[startPos.x, startPos.y].GScore = (int)Vector2.Distance(startPos, startPos);
		map[startPos.x, startPos.y].HScore = (int)Vector2.Distance(startPos, endPos);
		Node startNode = map[startPos.x, startPos.y];
		Node endNode = map[endPos.x, endPos.y];
		
		openNodes.Add(map[startPos.x, startPos.y]);

		//Loop through all open nodes
		while (openNodes.Count > 0)
		{
			currentNode = openNodes.OrderBy((x) => x.FScore).First();

			//Check if the endNode is reached and get the path
			if (currentNode == endNode)
			{
				return getPath(currentNode, startNode);
			}
			openNodes.Remove(currentNode);

			//Check for the best available neighbouring cell/node
			foreach (Cell neighbourCell in FindNeighbours(grid[currentNode.position.x,currentNode.position.y], grid))
			{
				Node neighbour = map[neighbourCell.gridPosition.x, neighbourCell.gridPosition.y];
				float tempG = currentNode.GScore + (int)Vector2.Distance(neighbour.position, currentNode.position);
				if (tempG < neighbour.GScore)
				{
					if (!openNodes.Contains(neighbour))
					{
						neighbour.parent = currentNode;
						neighbour.GScore = tempG;
						openNodes.Add(neighbour);
					} 
				}				
			}
		}
		return null;
	}

	//Create grid of nodes from grid of cells
	Node[,] CreateNodeGrid(Cell[,] grid, Vector2Int endPos)
	{
		Node[,] map = new Node[grid.GetLength(0), grid.GetLength(1)];

		foreach (Cell c in grid)
		{
			Vector2Int gridPos = c.gridPosition;
			map[gridPos.x, gridPos.y] = new Node(gridPos, null, int.MaxValue, (int)Vector2.Distance(gridPos, endPos)); //can me imprecise
		}

		return map;
	}

	//get the path from start to end
	List<Vector2Int> getPath(Node node, Node startNode)
	{
		List<Vector2Int> path = new List<Vector2Int>();
		while (currentNode.position != startNode.position)
		{
			path.Add(currentNode.position);
			currentNode = currentNode.parent;
			if (currentNode == null) break;
		}
		path.Reverse();
		return path;
	}

	//Find all neighbouring cells, while not goinh through walls
	private IEnumerable<Cell> FindNeighbours(Cell cell, Cell[,] grid)
	{
		List<Cell> neighbours = new List<Cell>();

		if (!cell.HasWall(Wall.UP))
		{
			neighbours.Add(grid[cell.gridPosition.x, cell.gridPosition.y + 1]);
		}

		if (!cell.HasWall(Wall.DOWN))
		{
			neighbours.Add(grid[cell.gridPosition.x, cell.gridPosition.y - 1]);
		}

		if (!cell.HasWall(Wall.LEFT))
		{
			neighbours.Add(grid[cell.gridPosition.x - 1, cell.gridPosition.y]);
		}

		if (!cell.HasWall(Wall.RIGHT))
		{
			neighbours.Add(grid[cell.gridPosition.x + 1, cell.gridPosition.y]);
		}

		return neighbours;
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

	public Node(Vector2Int position, Node parent, int GScore, int HScore)
    {
        this.position = position;
        this.parent = parent;
        this.GScore = GScore;
        this.HScore = HScore;
    }    
}
