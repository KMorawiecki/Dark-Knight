using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Completed
{
    public class Tile : MonoBehaviour
    {
        public bool top = false;
        public bool bottom = false;
        public bool left = false;
        public bool right = false;
        public SpriteRenderer spriteRenderer;
        public bool containItem = false;

        private BoardManager board;
        private Vector3 position;
        private GameObject instance; //a reference to the actual tile


        public void SetPosition(int x, int y)
        {
            Vector3 pos = new Vector3(x, y, 0f);

            position = pos;
        }

        public void SetBoardMAnager(BoardManager manager)
        {
            board = manager;
        }

        public int GetPositionX()
        {
            return (int) position.x;
        }

        public int GetPositionY()
        {
            return (int) position.y;
        }

        public void Create()
        {
            GameObject tile = new GameObject();

            if (top == true && bottom == true && left == true && right == false)
                tile = board.leftTopBotWallTile;
            else if (top == true && bottom == true && left == false && right == false)
                tile = board.topBotWallTile;
            else if (top == true && bottom == false && left == false && right == false)
                tile = board.topWallTile;
            else if (top == false && bottom == true && left == false && right == false)
                tile = board.botWallTile;
            else if (top == false && bottom == false && left == true && right == false)
                tile = board.leftWallTile;
            else if (top == false && bottom == false && left == false && right == true)
                tile = board.rightWallTile;
            else if (top == true && bottom == false && left == true && right == false)
                tile = board.topLeftWallTile;
            else if (top == true && bottom == false && left == false && right == true)
                tile = board.topRightWallTile;
            else if (top == false && bottom == true && left == true && right == false)
                tile = board.leftBotWallTile;
            else if (top == false && bottom == true && left == false && right == true)
                tile = board.rightBotWallTile;
            else if (top == false && bottom == false && left == true && right == true)
                tile = board.leftRightWallTile;
            else if (top == true && bottom == true && left == false && right == true)
                tile = board.topRightBotWallTile;
            else if (top == true && bottom == false && left == true && right == true)
                tile = board.leftTopRightWallTile;
            else if (top == false && bottom == true && left == true && right == true)
                tile = board.leftRightBotWallTile;
            else if (top == false && bottom == false && left == false && right == false)
                tile = board.floorTile;

            spriteRenderer = tile.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.black;

            instance =
            Instantiate(tile, position, Quaternion.identity) as GameObject;

            //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
            instance.transform.SetParent(board.GetBoardHolder());
        }

        public void SetColor(Color col)
        {
            spriteRenderer = instance.GetComponent<SpriteRenderer>();
            spriteRenderer.color = col;
        }

        public Color GetColor()
        {
            spriteRenderer = instance.GetComponent<SpriteRenderer>();
            return spriteRenderer.color;
        }
    }
}
