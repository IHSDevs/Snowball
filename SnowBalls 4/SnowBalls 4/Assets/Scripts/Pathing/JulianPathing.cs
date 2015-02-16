using UnityEngine;
using System.Collections;
using System.Diagnostics;//for timer

public class JulianPathing : MonoBehaviour {

	public Grid grid;
	public Transform start;
	public Transform finish;
	public Transform test;

	int count = 0;

	bool oneShot = true;
	bool oneShotTwo = true;

	Queue processQueue;

	void Start ()
	{
		Stopwatch timer1 = new Stopwatch();
		if (oneShotTwo) {
			timer1.Start();
		}

		PopulateGrid ();
		
		if (oneShotTwo) {
			timer1.Stop ();
			print(timer1.ElapsedMilliseconds + "initial");
		}

		//print (grid.NodeFromPos(start.position).X + " " + grid.NodeFromPos(start.position).Y);
		//grid.CreateGrid ();
		//print (grid.NodeFromPos(finish.position).Distance);
	}

	void Update ()
	{
		Stopwatch timer = new Stopwatch();
		if (oneShot) {
			timer.Start ();
		}

		Node testNode = grid.NodeFromPos(finish.position);
		while (testNode.Parent != null) {
			testNode.testColor = true;
			testNode = testNode.Parent;
		}

		timer.Stop ();
		if (oneShot) {
			print (timer.ElapsedMilliseconds);
			oneShot = false;
		}
	
		//grid.NodeFromPos (test.position).Traversible = false;
	}

	void PopulateGrid()
	{
		processQueue = new Queue ();
		grid.CreateGrid ();
		//int goal = grid.Obstacles;
		Node startNode = grid.NodeFromPos (start.position);
		startNode.Distance = 0;

		processQueue.Enqueue (startNode);
		while (processQueue.Count > 0)
		{
			EnqueueAdjacentNodes ((Node)processQueue.Dequeue());

		} 
		print (count);
	}

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

						if (n.Distance + 1 < temp.Distance && temp.Traversible || temp.Diagonal && n.Distance + 1 == temp.Distance) {//n path faster than temp path, or n path equal to temp path and temp set diagonally

							temp.Distance = n.Distance + 1;//set temp path equal to n path
							temp.Direction = dir;

							if (temp.Direction == n.Direction) {//if directions are same
								temp.Parent = n.Parent;//temp's parent set to n's parent
							}
							else {
								temp.Parent = n;//temp's parent set to n
							}

							temp.Parent = n;

							if (dir % 2 == 0) {//if not diagonal, diagonal set to false. 5 never comes up because 5 is self
								temp.Diagonal = false;
							}
							else {
								temp.Diagonal = true;
							}
							count ++;
							processQueue.Enqueue (temp);//Enqueues temp.
						}
					}
				}
			}
		}
	}
}