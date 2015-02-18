using UnityEngine;
using System.Collections;

//meshing around :]
[RequireComponent (typeof (MeshFilter), typeof (MeshRenderer), typeof (MeshCollider))]
public class TerrainGenerator : MonoBehaviour {

	[Range (2, 100)] public int width, length;
	[Range (0,10)] public int smoothIterations;
	[Range (0, 10)] public float range;
	[Range (.2f, 5f)] public float vertexDensity;

	private float[,] terrainGrid;
	private MeshFilter mf;
	private Mesh mesh;
	private MeshCollider mc;
	private int lastW, lastL, lastS, gridWith, gridLength;
	private float lastR, lastV;

	// Use this for initialization
	void Start () {
		lastW = width;
		lastL = length;
		lastR = range;
		lastS = smoothIterations;
		lastV = vertexDensity;

		Reset();
	}

	void UpdateValues () {
		gridWith = (int)((float)width/vertexDensity);
		gridLength = (int)((float)length/vertexDensity);

		terrainGrid = new float[gridWith, gridLength];

		mf = GetComponent<MeshFilter>();
		mesh = new Mesh();
		mf.mesh = mesh;
		
		mc = GetComponent<MeshCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		if (lastW != width || lastL != length || lastR != range || lastS != smoothIterations || lastV != vertexDensity) {
			Reset();
		}

		lastW = width;
		lastL = length;
		lastR = range;
		lastS = smoothIterations;
		lastV = vertexDensity;
	}

	void Reset () {
		UpdateValues ();
		PopulateGrid();
		for (int i = 0; i < smoothIterations; i ++) {
			SmoothGrid();
		}
		CreateMesh();
	}

	// Populates terrainGrid with random values
	void PopulateGrid () {
		//x by y grid
		for (int z = 0; z < gridLength; z ++) {
			for (int x = 0; x < gridWith; x ++) {
				terrainGrid[x,z] = Random.Range(-range/2, range/2);
			}
		}
	}

	void SmoothGrid () {
		for (int z = 0; z < gridLength; z ++) {
			for (int x = 0; x < gridWith; x ++) {
				terrainGrid[x,z] = AvgAdjacentHeight(x,z);
			}
		}
	}

	// 
	float AvgAdjacentHeight(int _x, int _z) {
		float totalHeight = 0;
		for (int z = -1; z <= 1; z ++) {
			for (int x = -1; x <= 1; x ++) {
				if (!(_x + x < 0) && !(_x + x >= gridWith) && !(_z + z < 0) && !(_z + z >= gridLength)) {
					totalHeight += terrainGrid[_x + x, _z + z];
				}
				else {
					//totalHeight += range / 2;
				}
			}
		}
		return totalHeight/8;
	}

	// Creates a mesh based on terrainGrid
	void CreateMesh () {

		//Vertices of grid
		Vector3 [] vertices = new Vector3 [terrainGrid.Length];

		int index = 0;
		for (int y = 0; y < gridLength; y ++) {
			for (int x = 0; x < gridWith; x ++) {
				vertices[index] = new Vector3 (vertexDensity*x, terrainGrid[x,y], vertexDensity*y);
				//GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				//cube.transform.position = vertices[index];
				index ++;
			}
		}

		int triangleCount = 2 * (gridLength - 1) * (gridWith * 1);
		int [] triangles = new int[triangleCount * 3];

		int vertIndex = 0;
		int triIndex = 0;

		for (int y = 0; y < gridLength - 1; y ++) {
			for (int x = 0; x < gridWith - 1; x ++) {
				//assigns tris in pairs based on bottom left vertex of quad

				triangles[triIndex + 0] = vertIndex;
				triangles[triIndex + 1] = vertIndex + gridWith;
				triangles[triIndex + 2] = vertIndex + gridWith + 1;

				triangles[triIndex + 3] = vertIndex + 1;
				triangles[triIndex + 4] = vertIndex;
				triangles[triIndex + 5] = vertIndex + gridWith + 1;

				vertIndex ++;
				triIndex += 6;
			}
			vertIndex ++;//skips over OOB index
		}

		//Assigns uvs
		Vector2 [] uvs = new Vector2[vertices.Length];

		for (int i = 0; i < vertices.Length; i ++) {
			uvs[i] = new Vector2 (vertices[i].x, vertices[i].z);
		}

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;

		mesh.RecalculateBounds();
		mesh.RecalculateNormals();

		mc.sharedMesh = mesh;
	}
}
