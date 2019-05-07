using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Completed
{
    public class Wolf : Enemy
    {
        private List<List<bool>> visited = new List<List<bool>>();
        private List<List<Direction>> correctPath = new List<List<Direction>>();
        private bool canMove = true;
        public GameObject foodToken;
        private GameObject[] foodToDestroy;


        // Start is called before the first frame update
        protected override void Start()
        {
            //Find the Player GameObject using it's tag and store a reference to its transform component.
            target = GameObject.FindGameObjectWithTag("Player").transform;

            //Call the start function of our base class MovingObject.
            base.Start();
            SetupParameters();
        }

        public override void MoveEnemy()
        {
            //Declare variables for X and Y axis move directions, these range from -1 to 1.
            //These values allow us to choose between the cardinal directions: up, down, left and right.
            int xDir = 0;
            int yDir = 0;

            //Debug.Log(correctPath[(int)transform.position.x][(int)transform.position.y]);

            switch (correctPath[(int)transform.position.x][(int)transform.position.y])
            {
                case Direction.GoLeft:
                    xDir = -1;
                    break;
                case Direction.GoRight:
                    xDir = 1;
                    break;
                case Direction.GoTop:
                    yDir = 1;
                    break;
                case Direction.GoBot:
                    yDir = -1;
                    break;
                case Direction.Finish:
                    SetupParameters();
                    break;
                case Direction.DontGo:
                    SetupParameters();
                    break;
            }         

            if (canMove)
                AttemptMove<Player>(xDir, yDir);

        }

        public void SetupParameters()
        {
            correctPath.Clear();
            visited.Clear();

            for (int r = 0; r < board.rows; r++)
            {
                List<bool> newVisited = new List<bool>();
                List<Direction> newCorrect = new List<Direction>();
                visited.Add(newVisited);
                correctPath.Add(newCorrect);

                for (int c = 0; c < board.columns; c++)
                {
                    visited[r].Add(false);
                    correctPath[r].Add(Direction.DontGo);
                }  
            }

            if (FindPath((int)transform.position.x, (int)transform.position.y) != Direction.DontGo)
                canMove = true;
            else
                canMove = false;

        }

        public Direction FindPath(int x, int y)
        {
            if (x == (int)target.transform.position.x && y == (int)target.transform.position.y)
                return Direction.Finish; // If you reached the end

            if (visited[x][y])
                return Direction.DontGo; // If you already were here

            visited[x][y] = true;
            Tile currentTile = board.GetTile(x, y);

            if (x != 0 && currentTile.left == false) // Checks if not on left edge or walled
                if (FindPath(x - 1, y) != Direction.DontGo)
                { // Recalls method one to the left
                    correctPath[x][y] = Direction.GoLeft; // Sets that path value to true;                    
                    return Direction.GoLeft;
                }
            if (x != board.columns - 1 && currentTile.right == false) // Checks if not on right edge or walled
                if (FindPath(x + 1, y) != Direction.DontGo)
                { // Recalls method one to the right
                    correctPath[x][y] = Direction.GoRight;
                    return Direction.GoRight;
                }
            if (y != 0 && currentTile.bottom == false) // Checks if not on bottom edge or walled
                if (FindPath(x, y - 1) != Direction.DontGo)
                { // Recalls method one up
                    correctPath[x][y] = Direction.GoBot;
                    return Direction.GoBot;
                } 
            if (y != board.rows - 1 && currentTile.top == false) // Checks if not on top edge or walled
                if (FindPath(x, y + 1) != Direction.DontGo)
                { // Recalls method one down
                    correctPath[x][y] = Direction.GoTop;
                    return Direction.GoTop;
                }
            return Direction.DontGo;
        }

        public enum Direction
        {
            DontGo,
            GoTop,
            GoRight,
            GoLeft,
            GoBot,
            Finish
        }
    }
}

