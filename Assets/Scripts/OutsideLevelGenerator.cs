using UnityEngine;
using System.Collections;

public class OutsideLevelGenerator : MonoBehaviour {

	public float flt_tilePadding = 0.5f;

	// LEVEL GENERATION
	// 0 - empty tile
	// 1 - dirt
	// 2 - marshdirt
	// 3 - grass
	// 4 - forestgrass
	// 5 - thickgrass
	// 6 - water
	// 7 - water edge (left facing)
	// 8 - sand
	public GameObject[] levelTiles;
	private Vector2 generationPoint = Vector2.zero;
	private int[,] int_array_levelGrid = new int[,]
	{
		{ 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 8, 8, 7, 6, 6 },
		{ 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 8, 8, 8, 7, 6, 6 },
		{ 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 1, 3, 3, 1, 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 8, 3, 8, 8, 8, 7, 6, 6 },
		{ 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 8, 8, 8, 8, 7, 6, 6 },
		{ 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 8, 8, 8, 8, 7, 6, 6 },
		{ 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 8, 8, 8, 8, 7, 6, 6 },
		{ 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 8, 8, 8, 8, 8, 7, 6, 6 },
		{ 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 3, 3, 3, 3, 3, 3, 3, 1, 3, 3, 3, 3, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 8, 8, 8, 8, 8, 7, 6, 6 },
		{ 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 1, 3, 3, 3, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 1, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 8, 8, 8, 8, 8, 7, 6, 6 },
		{ 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 3, 3, 3, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 8, 8, 8, 8, 8, 7, 6, 6 },
		{ 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 8, 8, 8, 8, 7, 6, 6 },
		{ 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 8, 8, 7, 6, 6 },
	};

	// Use this for initialization
	void Start () {
		for (int x = 0; x < int_array_levelGrid.GetLength (0); x++) { // first array dimension
			for (int y = 0; y < int_array_levelGrid.GetLength (1); y++) { // second array dimension
				switch (int_array_levelGrid[x,y]) {
				case 0: // empty tile
					Instantiate(levelTiles[0], new Vector3(generationPoint.x, generationPoint.y, 0), Quaternion.identity);
					break;
				case 1: // dirt
					Instantiate(levelTiles[1], new Vector3(generationPoint.x, generationPoint.y, 0), Quaternion.identity);
					break;
				case 2: // marsh dirt
					Instantiate(levelTiles[2], new Vector3(generationPoint.x, generationPoint.y, 0), Quaternion.identity);
					break;
				case 3: // grass
					Instantiate(levelTiles[3], new Vector3(generationPoint.x, generationPoint.y, 0), Quaternion.identity);
					break;
				case 4: // forest grass
					Instantiate(levelTiles[4], new Vector3(generationPoint.x, generationPoint.y, 0), Quaternion.identity);
					break;
				case 5: // thick grass
					Instantiate(levelTiles[5], new Vector3(generationPoint.x, generationPoint.y, 0), Quaternion.identity);
					break;
				case 6: // water
					Instantiate(levelTiles[6], new Vector3(generationPoint.x, generationPoint.y, 0), Quaternion.identity);
					break;
				case 7: // water edge, generates sand underneath as well
					GameObject newWater = Instantiate(levelTiles[7], new Vector3(generationPoint.x, generationPoint.y, 0), Quaternion.identity) as GameObject;
					newWater.transform.eulerAngles = new Vector3(0, 0, 90);
					Instantiate(levelTiles[8], new Vector3(generationPoint.x, generationPoint.y, 0), Quaternion.identity);
					break;
				case 8: // sand
					Instantiate(levelTiles[8], new Vector3(generationPoint.x, generationPoint.y, 0), Quaternion.identity);
					break;
				}
				// move to next x coordinate (right)
				generationPoint += new Vector2 (flt_tilePadding, 0);
			}
			// move to next y coordinate (down)
			generationPoint -= new Vector2 (0, flt_tilePadding);

			//reset x coordinate
			generationPoint = new Vector2(generationPoint.x - generationPoint.x, generationPoint.y);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
