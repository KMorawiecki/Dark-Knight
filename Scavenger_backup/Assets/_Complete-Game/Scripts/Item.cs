using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Completed
{

    public class Item : MonoBehaviour
    {
        public string name;
        private Tile itmTile;
        // Start is called before the first frame update

        private void Update()
        {
            //color item
            GetComponent<SpriteRenderer>().color = itmTile.GetColor();
        }

        public void SetTile(Tile tile)
        {
            itmTile = tile;
        }
    }

}
