using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Completed
{
    public class Rat : Enemy
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            //Call the start function of our base class MovingObject.
            base.Start();
        }

        public override void MoveEnemy()
        {
            //Declare variables for X and Y axis move directions, these range from -1 to 1.
            //These values allow us to choose between the cardinal directions: up, down, left and right.
            int xDir = 0;
            int yDir = 0;
            bool runFlag = true;
            bool moveChosen = false;

            target = GameObject.FindGameObjectWithTag("Player").transform;

            if (Math.Abs(target.position.x - transform.position.x) <= 1 && target.position.y == transform.position.y)
            {
                xDir = (int)(target.position.x - transform.position.x);
                moveChosen = true;
            }

            if (Math.Abs(target.position.y - transform.position.y) <= 1 && target.position.x == transform.position.x)
            {
                yDir = (int)(target.position.y - transform.position.y);
                moveChosen = true;
            }

           if(moveChosen == false)
                while (runFlag)  //decide which way to go
                {
                    xDir = 0;
                    yDir = 0;

                    int selector = UnityEngine.Random.Range(0, 2);
                    switch (selector)
                    {
                        case 0:
                            xDir = UnityEngine.Random.Range(0, 2);
                            xDir *= 2;
                            xDir -= 1;
                            break;
                        case 1:
                            yDir = UnityEngine.Random.Range(0, 2);
                            yDir *= 2;
                            yDir -= 1;
                            break;
                    }

                    //zabezpieczenie przed skrajnymi
                    if (transform.position.x == 0 && xDir == -1)
                        continue;
                    else if (transform.position.x == board.columns - 1 && xDir == 1)
                        continue;
                    else if (transform.position.y == 0 && yDir == -1)
                        continue;
                    else if (transform.position.y == board.rows - 1 && yDir == 1)
                        continue;

                    Tile currentTile = board.GetTile((int) transform.position.x, (int) transform.position.y);

                    //zabezpieczenie przed wejsciem w sciane
                    if (xDir == -1 && currentTile.left)
                        continue;
                    else if (xDir == 1 && currentTile.right)
                        continue;
                    else if (yDir == -1 && currentTile.bottom)
                        continue;
                    else if (yDir == 1 && currentTile.top)
                        continue;

                    runFlag = false;
                }
            //Debug.Log("x: " + xDir);
            //Debug.Log("y: " + yDir);

            AttemptMove<Player>(xDir, yDir);
        }
    }
}
