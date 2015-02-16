using UnityEngine;
using System.Collections;
//using System.Diagnostics;//for timer

public class JulianPathing : MonoBehaviour 
{

	public bool spawnNodeBoys, showPath;
	public Grid grid;
	public Transform start, finish, test;

	Queue processQueue;

	void Start ()
	{
		PopulateGrid ();
		InvokeRepeating("SpawnNodeBoy", 0, .05f);
	}

	void Update () 
	{
		GetPathFromPos(finish.position);
	}

	void SpawnNodeBoy() 
	{
		if (spawnNodeBoys && grid.NodeFromPos(finish.position).Traversible) 
		{
			NodeBoy myBoi = transform.gameObject.AddComponent<NodeBoy> ();
			myBoi.Position = finish.position;
			myBoi.Path = GetPathFromPos(finish.position);
		}
	}

	//Given a Vector3 position, returns an array of Vector3 containing waypoint positions 
	public Vector3[] GetPathFromPos(Vector3 pos)
	{
		//Stopwatch myTimer = new Stopwatch();
		//myTimer.Start();
		Node temp = grid.NodeFromPos(pos);
		Vector3[] path = new Vector3[temp.DegreesSeperation];

		//adds all waypoint Nodes to path
		for (int i = 0; temp.Parent != null; i ++)
		{
			temp.testColor = (showPath) ? true : false;
			temp = temp.Parent;
			path[i] = temp.Position;
		}

		//myTimer.Stop();
		//print(myTimer.ElapsedMilliseconds);
		return path;
	}

	//Populates Nodes of grid with parents
	void PopulateGrid()
	{
		processQueue = new Queue ();

		grid.CreateGrid ();

		Node startNode = grid.NodeFromPos (start.position);

		startNode.Distance = 0;
		startNode.DegreesSeperation = 0;

		processQueue.Enqueue (startNode);

		while (processQueue.Count > 0)
		{
			EnqueueAdjacentNodes ((Node)processQueue.Dequeue());
		} 
	}

	//Enqueues nodes adjacent to n and updates appropriate values
	void EnqueueAdjacentNodes (Node n)
	{
		int xPos = n.X;
		int yPos = n.Y;

		Node temp;

		int dir = 0;

		//All adjacent nodes including self
		for (int x = -1; x <= 1; x ++) {
			for (int y = -1; y <= 1; y ++) {
				dir ++;//increment dir
				if (!(x == 0 && y == 0)) {//index is not self
					if(!grid.IsOOB(x+xPos, y+yPos)) {//if not out of bounds

						temp = grid.NodeFromCoord(x+xPos,y+yPos);//sets temp Node to current index

						if ((n.Distance + 10 < temp.Distance || temp.Distance < 0) && temp.Traversible) {//n path faster than temp path, or temp path is virgin

							//sets direction of updated Node
							temp.Direction = dir;

							//Updates degrees of seperation
							temp.DegreesSeperation = n.DegreesSeperation;

							//limits number of waypoints to minimum required
							if (temp.Direction == n.Direction) {//if directions are same
								temp.Parent = n.Parent;//temp's parent set to n's parent
							}
							else {
								temp.Parent = n;//temp's parent set to n
								temp.DegreesSeperation += 1;
							}

							if (dir % 2 == 0) {//diagonal moves cost more
								temp.Distance = n.Distance + 10;//set temp path equal to n path
							}
							else {
								temp.Distance = n.Distance + 14;//set temp path equal to n path
							}

							processQueue.Enqueue (temp);//Enqueues temp.
						}
					}
				}
			}
		}
	}
}