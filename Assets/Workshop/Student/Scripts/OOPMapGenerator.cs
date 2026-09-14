using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Solution
{

    public class OOPMapGenerator : MonoBehaviour
    {
        [Header("Set MapGenerator")]
        public int X;
        public int Y;

        [Header("Set Player")]
        public OOPPlayer player;
        public Vector2Int playerStartPos;

        [Header("Set Exit")]
        public OOPExit Exit;
        [Header("Set Exit")]
        public Identity Wall;

        [Header("Set Prefab")]
        public GameObject[] floorsPrefab;
        public GameObject[] wallsPrefab;
        public GameObject[] demonWallsPrefab;
        public GameObject[] itemsPrefab;
        public GameObject[] collectItemsPrefab;
        public GameObject[] zombiePrefab;

        [Header("Set Transform")]
        public Transform floorParent;
        public Transform wallParent;
        public Transform itemParent;

        [Header("Set object Count")]
        public int obsatcleCount;
        public int itemPotionCount;
        public int colloctItemCount;
        public int zombieCount;

        public Identity[,] mapdata;

        // block types ...
        [HideInInspector]
        public string empty = "";
        [HideInInspector]
        public string demonWall = "demonWall";
        [HideInInspector]
        public string potion = "potion";
        [HideInInspector]
        public string bonuesPotion = "bonuesPotion";
        [HideInInspector]
        public string exit = "exit";
        [HideInInspector]
        public string playerOnMap = "player";
        [HideInInspector]
        public string collectItem = "collectItem";

        // Start is called before the first frame update
        void Start()
        {
            mapdata = new Identity[X, Y];
            for (int x = -1; x < X + 1; x++)
            {
                for (int y = -1; y < Y + 1; y++)
                {
                    if (x == -1 || x == X || y == -1 || y == Y)
                    {
                        int r = Random.Range(0, wallsPrefab.Length);
                        GameObject obj = Instantiate(wallsPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
                        obj.transform.parent = wallParent;
                        obj.name = "Wall_" + x + ", " + y;
                    }
                    else
                    {
                        int r = Random.Range(0, floorsPrefab.Length);
                        GameObject obj = Instantiate(floorsPrefab[r], new Vector3(x, y, 1), Quaternion.identity);
                        obj.transform.parent = floorParent;
                        obj.name = "floor_" + x + ", " + y;
                        mapdata[x, y] = null;
                    }
                }
            }

            player.mapGenerator = this;
            player.positionX = playerStartPos.x;
            player.positionY = playerStartPos.y;
            player.transform.position = new Vector3(playerStartPos.x, playerStartPos.y, -0.1f);
            mapdata[playerStartPos.x, playerStartPos.y] = player;

            int count = 0;

            int preventInfiniteLoop = 100;
            while (count < obsatcleCount)
            {
                if (--preventInfiniteLoop < 0) break;
                int x = Random.Range(0, X);
                int y = Random.Range(0, Y);
                if (mapdata[x, y] == null)
                {
                    PlaceItem(x, y, demonWallsPrefab, wallParent);
                    count++;
                }
            }

            count = 0;
            preventInfiniteLoop = 100;
            while (count < itemPotionCount)
            {
                if (--preventInfiniteLoop < 0) break;
                int x = Random.Range(0, X);
                int y = Random.Range(0, Y);
                if (mapdata[x, y] == null)
                {
                    PlaceItem(x, y, itemsPrefab, itemParent);
                    count++;
                }
            }
            count = 0;
            preventInfiniteLoop = 100;
            while (count < colloctItemCount)
            {
                if (--preventInfiniteLoop < 0) break;
                int x = Random.Range(0, X);
                int y = Random.Range(0, Y);
                if (mapdata[x, y] == null)
                {
                    PlaceItem(x, y, collectItemsPrefab, itemParent);
                    count++;
                }
            }
            count = 0;
            preventInfiniteLoop = 100;

            while (count < zombieCount)
            {
                if (--preventInfiniteLoop < 0) break;

                int x = Random.Range(0, X);
                int y = Random.Range(0, Y);

                if (mapdata[x, y] == null)
                {
                    PlaceItem(x, y, zombiePrefab, itemParent);
                    count++;
                }
            }
            mapdata[X - 1, Y - 1] = Exit;
            Exit.transform.position = new Vector3(X - 1, Y - 1, 0);
        }

        public Identity GetMapData(float x, float y)
        {
            if (x >= X || x < 0 || y >= Y || y < 0) {
                return Wall;
            }

            return mapdata[(int)x, (int)y];
        }

        public void PlaceItem(int x, int y, GameObject[] _itemsPrefab, Transform parent)
        {
            int r = Random.Range(0, _itemsPrefab.Length);

            GameObject obj = Instantiate(
                _itemsPrefab[r],
                new Vector3(x, y, 0),
                Quaternion.identity
            );

            obj.transform.parent = parent;

            mapdata[x, y] = obj.GetComponent<Identity>();

            mapdata[x, y].positionX = x;
            mapdata[x, y].positionY = y;
            mapdata[x, y].mapGenerator = this;
            obj.name = $"Item_{mapdata[x, y].Name} {x}, {y}";
        }


    }
}