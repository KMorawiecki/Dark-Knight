using UnityEngine;
using System;
using System.Collections.Generic; 		//Allows us to use Lists.
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

namespace Completed
	
{
	
	public class BoardManager : MonoBehaviour
	{
		// Using Serializable allows us to embed a class with sub properties in the inspector.
		[Serializable]
		public class Count
		{
			public int minimum; 			//Minimum value for our Count class.
			public int maximum; 			//Maximum value for our Count class.
			
			
			//Assignment constructor.
			public Count (int min, int max)
			{
				minimum = min;
				maximum = max;
			}
		}

        public GameObject floorTile;									//All tiles
        public GameObject leftWallTile;
        public GameObject rightWallTile;
        public GameObject topWallTile;
        public GameObject botWallTile;
        public GameObject topLeftWallTile;
        public GameObject leftRightWallTile;
        public GameObject leftBotWallTile;
        public GameObject topRightWallTile;
        public GameObject topBotWallTile;
        public GameObject rightBotWallTile;
        public GameObject leftTopRightWallTile;
        public GameObject leftTopBotWallTile;
        public GameObject leftRightBotWallTile;
        public GameObject topRightBotWallTile;

        public int columns = 16; 										//Number of columns in our game board.
		public int rows = 16;											//Number of rows in our game board.
		public Count wallCount = new Count (5, 9);						//Lower and upper limit for our random number of walls per level.
		public Count foodCount = new Count (1, 5);						//Lower and upper limit for our random number of food items per level.
		public GameObject exit;											//Prefab to spawn for exit.
		public GameObject[] floorTiles;									//Array of floor prefabs.
		public GameObject[] wallTiles;									//Array of wall prefabs.
		public GameObject[] foodTiles;									//Array of food prefabs.
		public GameObject[] enemyTiles;									//Array of enemy prefabs.
		public GameObject[] outerWallTiles;                             //Array of outer tile prefabs.

        private Dictionary<Tuple<int, int>, GameObject> outerWallList = new Dictionary<Tuple<int, int>, GameObject>();
		private Transform boardHolder;									//A variable to store a reference to the transform of our Board object.
		private List <Vector3> gridPositions = new List <Vector3> ();	//A list of possible locations to place tiles.
        public List<List<Tile>> tiles = new List<List<Tile>>();
        private GameObject exitInstance;                                //for sprite renderer reasons


        //Clears our list gridPositions and prepares it to generate a new board.
        void InitialiseList ()
		{
			//Clear our list gridPositions.
			gridPositions.Clear ();
			
			//Loop through x axis (columns).
			for(int x = 1; x < columns-1; x++)
			{
				//Within each column, loop through y axis (rows).
				for(int y = 1; y < rows-1; y++)
				{
					//At each index add a new Vector3 to our list with the x and y coordinates of that position.
					gridPositions.Add (new Vector3(x, y, 0f));
				}
			}
		}
		
		
		//Sets up the outer walls and floor (background) of the game board.
		void BoardSetup ()
		{
			//Instantiate Board and set boardHolder to its transform.
			boardHolder = new GameObject ("Board").transform;

            //clear list of all tiles from previous level
            tiles.Clear();

            //clear list of outer walls
            outerWallList.Clear();

            for (int x = -1; x < columns + 1; x++)
            {
                List<Tile> newRow = new List<Tile>();
                tiles.Add(newRow);

                //int iter = 0; //to hold array of outer wall instances

                for (int y = -1; y < rows + 1; y++)
                {
                    //Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
                    if (x == -1 || x == columns || y == -1 || y == rows)
                    {
                        //iter++;
                        GameObject toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];

                        //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                        GameObject instance =
                            Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                        //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                        instance.transform.SetParent(boardHolder);

                        //color it black
                        instance.GetComponent<SpriteRenderer>().color = Color.black;

                        //add to list
                        Tuple<int, int> newKey = new Tuple<int, int>(x,y);
                        outerWallList.Add(newKey, instance);
                    }
                    else
                    {
                        GameObject go = new GameObject("Tile");
                        Tile tile = go.AddComponent<Tile>();

                        tile.SetPosition(x, y);
                        tile.SetBoardMAnager(this);

                        tiles[x].Add(tile);
                    }
                }
            }
        }

        //tu dzieje sie magia
        void PutWalls(int threshold, int start_hor, int start_ver)
        {
            //stop if map is full
            if (rows < (int) (Math.Pow(2, threshold)))
                return;
            //put last wall randomly
            else if(rows == (int)(Math.Pow(2, threshold)))
            {
                int lastWallPosition = Random.Range(0, 4);

                switch(lastWallPosition)
                {
                    case 0:
                        tiles[(int)(start_hor + columns / Math.Pow(2, threshold) - 1)][start_ver].right = true;
                        break;
                    case 1:
                        tiles[(int)(start_hor + columns / Math.Pow(2, threshold) - 1)][start_ver + 1].right = true;
                        break;
                    case 2:
                        tiles[start_hor][(int)(start_ver + rows / Math.Pow(2, threshold) - 1)].top = true;
                        break;
                    case 3:
                        tiles[start_hor + 1][(int)(start_ver + rows / Math.Pow(2, threshold) - 1)].top = true;
                        break;
                }

                return;
            }

            int hole = Random.Range(start_ver, (int) (start_ver + rows / Math.Pow(2, threshold - 1)));

            //put vertical wall in the middle
            for (int i = start_ver; i < start_ver + rows / Math.Pow(2, threshold - 1); i++)
            {
                if (i != hole)
                    tiles[(int) (start_hor + columns / Math.Pow(2, threshold) - 1)][i].right = true;
                //TODO: jakos to zabezpieczyc przed skrajnymi
                else
                    tiles[(int)(start_hor + columns / Math.Pow(2, threshold) - 1)][i].right = false;
            }

            hole = Random.Range(start_hor, (int)(start_hor + columns / Math.Pow(2, threshold)));
            //put horizontal wall on the left
            for (int i = start_hor; i < start_hor + columns / Math.Pow(2, threshold); i++)
            {
                if (i != hole)
                    tiles[i][(int)(start_ver + rows / Math.Pow(2, threshold) - 1)].top = true;
                else
                    tiles[i][(int)(start_ver + rows / Math.Pow(2, threshold) - 1)].top = false;
            }

            //invoke function recursively for left side
            PutWalls(threshold + 1, start_hor, start_ver);
            PutWalls(threshold + 1, start_hor, (int)(start_ver + rows / Math.Pow(2, threshold)));

            hole = Random.Range((int)(start_hor + columns / Math.Pow(2, threshold)), (int)(start_hor + columns / Math.Pow(2, threshold - 1)));
            //put horizontal wall on the right
            for (int i = (int) (start_hor + columns / Math.Pow(2, threshold)); i < (int)(start_hor + columns / Math.Pow(2, threshold - 1)); i++)
            {
                if (i != hole)
                    tiles[i][(int)(start_ver + rows / Math.Pow(2, threshold) - 1)].top = true;
                else
                    tiles[i][(int)(start_ver + rows / Math.Pow(2, threshold) - 1)].top = false;
            }

            //invoke function recursively for right side
            PutWalls(threshold + 1, (int)(start_hor + (columns / Math.Pow(2, threshold))), start_ver);
            PutWalls(threshold + 1, (int)(start_hor + (columns / Math.Pow(2, threshold))), (int)(start_ver + (rows / Math.Pow(2, threshold))));
        }


        //RandomPosition returns a random position from our list gridPositions.
        Vector3 RandomPosition ()
		{
			//Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
			int randomIndex = Random.Range (0, gridPositions.Count);
			
			//Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
			Vector3 randomPosition = gridPositions[randomIndex];
			
			//Remove the entry at randomIndex from the list so that it can't be re-used.
			gridPositions.RemoveAt (randomIndex);
			
			//Return the randomly selected Vector3 position.
			return randomPosition;
		}
		
		
		//LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
		void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum)
		{
			//Choose a random number of objects to instantiate within the minimum and maximum limits
			int objectCount = Random.Range (minimum, maximum+1);
			
			//Instantiate objects until the randomly chosen limit objectCount is reached
			for(int i = 0; i < objectCount; i++)
			{
				//Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
				Vector3 randomPosition = RandomPosition();
				
				//Choose a random tile from tileArray and assign it to tileChoice
				GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
				
				//Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
				Instantiate(tileChoice, randomPosition, Quaternion.identity);
			}
		}

        //initialise tiles
        void LayoutFloor()
        {
            for (int row = 0; row < tiles.Count; row++)
                for (int col = 0; col < tiles[row].Count; col++)
                    tiles[row][col].Create();
        }


        //add walls on neighbor tiles
        void UpdateTiles()
        {
            for (int row = 0; row < tiles.Count; row++)
                for (int col = 0; col < tiles[row].Count; col++)
                {
                    if(row > 0)
                        if (tiles[row - 1][col].right == true)
                            tiles[row][col].left = true;
                    if(col > 0)
                        if (tiles[row][col - 1].top == true)
                            tiles[row][col].bottom = true;
                }
        }

        //add additional passages to simplify maze
        void SimplifyMaze()
        {
            int passageCount = 32;

            for(int i = 0; i < passageCount; i++)
            {
                int randomTileY = Random.Range(0, columns - 1);
                int randomTileX = Random.Range(0, rows - 1);
                //TODO: moznaby dodac sprawdzanie czy sie nie powtarzaja

                tiles[randomTileX][randomTileY].right = false;
                tiles[randomTileX][randomTileY].left = false;
                tiles[randomTileX][randomTileY].top = false;
                tiles[randomTileX][randomTileY].bottom = false;
            }
        }

        //SetupScene initializes our level and calls the previous functions to lay out the game board
        public void SetupScene (int level)
		{
			//Creates the outer walls and floor.
			BoardSetup ();
			
			//Reset our list of gridpositions.
			InitialiseList ();

            PutWalls(1, 0, 0);
            SimplifyMaze();
            UpdateTiles();
            LayoutFloor();

            //Determine number of enemies based on current level number, based on a logarithmic progression
            int enemyCount = (int)Mathf.Log(level, 2f) + 1;
            //Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
            LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);

            //Instantiate the exit tile in the upper right hand corner of our game board
            exitInstance = Instantiate (exit, new Vector3 (columns - 1, rows - 1, 0f), Quaternion.identity);
		}

        //adds lighting each step
        public void ColorBoard()
        {
            Transform playerPos = GameObject.FindGameObjectWithTag("Player").transform;

            //first we color everything black
            for (int row = 0; row < tiles.Count; row++)
                for (int col = 0; col < tiles[row].Count; col++)
                   tiles[row][col].SetColor(Color.black);

            //next we add lighting recursively
            AddLighting(0, (int)playerPos.position.x, (int)playerPos.position.y);

            //color exit 
            exitInstance.GetComponent<SpriteRenderer>().color = GetTile(columns - 1, rows - 1).GetColor();

            //color outer walls
            for(int row = 0; row < rows; row++)
            {
                Tuple<int, int> newKeyTop = new Tuple<int, int>(row, columns);
                outerWallList[newKeyTop].GetComponent<SpriteRenderer>().color = GetTile(row, columns - 1).GetColor();

                Tuple<int, int> newKeyBot = new Tuple<int, int>(row, -1);
                outerWallList[newKeyBot].GetComponent<SpriteRenderer>().color = GetTile(row, 0).GetColor();
            }
            for (int col = 0; col < columns; col++)
            {
                Tuple<int, int> newKeyLeft = new Tuple<int, int>(rows, col);
                outerWallList[newKeyLeft].GetComponent<SpriteRenderer>().color = GetTile(rows - 1, col).GetColor();

                Tuple<int, int> newKeyRight = new Tuple<int, int>(-1, col);
                outerWallList[newKeyRight].GetComponent<SpriteRenderer>().color = GetTile(0, col).GetColor();
            }
        }

        private void AddLighting(int thresh, int tilePosX, int tilePosY)
        {
            if (thresh >= 3)
                return;

            switch(thresh)
            {
                case 0:
                    if(tiles[tilePosX][tilePosY].GetColor() == Color.black)
                        tiles[tilePosX][tilePosY].SetColor(Color.white);
                    break;
                case 1:
                    if (tiles[tilePosX][tilePosY].GetColor() == Color.black)
                        tiles[tilePosX][tilePosY].SetColor(Color.white);
                    break;
                case 2:
                    if (tiles[tilePosX][tilePosY].GetColor() == Color.black)
                        tiles[tilePosX][tilePosY].SetColor(Color.grey);
                    break;
                default:
                    tiles[tilePosX][tilePosY].SetColor(Color.black);
                    break;
            }

            if (tilePosX != 0 && tiles[tilePosX][tilePosY].left != true)
                AddLighting(thresh + 1, tilePosX - 1, tilePosY);
            if (tilePosX != columns - 1 && tiles[tilePosX][tilePosY].right != true)
                AddLighting(thresh + 1, tilePosX + 1, tilePosY);
            if (tilePosY != 0 && tiles[tilePosX][tilePosY].bottom != true)
                AddLighting(thresh + 1, tilePosX, tilePosY - 1);
            if (tilePosY != rows -1 && tiles[tilePosX][tilePosY].top != true)
                AddLighting(thresh + 1, tilePosX, tilePosY + 1);
        }

        public Transform GetBoardHolder()
        {
            return boardHolder;
        }

        public Tile GetTile(int x, int y)
        {
            return tiles[x][y];
        }
    }
}
