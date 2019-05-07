﻿using System.Collections;
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

            //decide which way to go
            while (runFlag)
            {
                xDir = 0;
                yDir = 0;

                int selector = Random.Range(0, 2);
                switch (selector)
                {
                    case 0:
                        xDir = Random.Range(0, 2);
                        xDir *= 2;
                        xDir -= 1;
                        break;
                    case 1:
                        yDir = Random.Range(0, 2);
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

            ////If the difference in positions is approximately zero (Epsilon) do the following:
            //if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)

            //    //If the y coordinate of the target's (player) position is greater than the y coordinate of this enemy's position set y direction 1 (to move up). If not, set it to -1 (to move down).
            //    yDir = target.position.y > transform.position.y ? 1 : -1;

            ////If the difference in positions is not approximately zero (Epsilon) do the following:
            //else
            //    //Check if target x position is greater than enemy's x position, if so set x direction to 1 (move right), if not set to -1 (move left).
            //    xDir = target.position.x > transform.position.x ? 1 : -1;

            //Call the AttemptMove function and pass in the generic parameter Player, because Enemy is moving and expecting to potentially encounter a Player
            AttemptMove<Player>(xDir, yDir);
        }
    }
}
