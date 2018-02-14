using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour {

    public GameObject floorSprite;
    public GameObject wallSprite;
    public GameObject floorObj;

    private struct coordinates
    {
        public int x, y;

        public coordinates(int p1, int p2)
        {
            x = p1;
            y = p2;
        }

        public bool eql(coordinates c2 )
        {         
            return x == c2.x && y == c2.y;
        }

        public override string ToString()
        {
            return x + "," + y;
        }

    }
    private int[,] floor;
    private coordinates[] rooms, paths;
    private List<coordinates> queue = new List<coordinates>();
    private enum DIR : int { RIGHT = 1, LEFT, UP, DOWN };
    private enum AXIS : int { X = 1, Y };
    private enum BLOCK : int { NULL = -1, WALL};

    void generate(int sizeX, int sizeY, int roomAmount) {
        //sizeX = 51;
        //sizeY = 51;
        floor = new int[sizeX, sizeY];
        rooms = new coordinates[roomAmount];
        paths = new coordinates[roomAmount - 1];

        for (int i = 0; i < floor.GetLength(0); i++)
            for (int j = 0; j < floor.GetLength(1); j++)
                floor[i, j] = (int)BLOCK.NULL;

        for (int i = 0; i< roomAmount; i++)
        {
            int startY = sizeY / roomAmount * i;
            int endY = startY + sizeY / roomAmount;
            coordinates roomCenter = new coordinates(Random.Range(0, sizeX), Random.Range(startY, endY));
            rooms[i] = roomCenter;
            queue.Add(roomCenter);
            placeFloorAt(roomCenter,i+1);
            if (i < roomAmount - 1)
                paths[i] = roomCenter;
        }
        linkRooms();
        createFloor();
    }

    void linkRooms()
    {
        while (!finishLinkRoom()) 
        {
            for (int i = 0; i < paths.Length; i++)
            {
                coordinates currentLocation = paths[i];
                coordinates goal = rooms[i + 1];
                int xFactor = 0, yFactor = 0;

                if (!currentLocation.eql(goal))
                {
                    xFactor = System.Math.Sign(currentLocation.x.CompareTo(goal.x));
                    yFactor = System.Math.Sign(currentLocation.y.CompareTo(goal.y));

                    if (xFactor != 0 && yFactor != 0)
                    {
                        int dir = Random.Range(1, 3);
                        if (dir == (int)AXIS.X)
                            paths[i] = new coordinates(currentLocation.x - xFactor, currentLocation.y);
                        else if (dir == (int)AXIS.Y)
                            paths[i] = new coordinates(currentLocation.x, currentLocation.y - yFactor);
                    }
                    else if (xFactor != 0)
                    {
                        paths[i] = new coordinates(currentLocation.x - xFactor, currentLocation.y);
                    }
                    else if (yFactor != 0)
                    {
                        paths[i] = new coordinates(currentLocation.x, currentLocation.y - yFactor);
                    }

                    placeFloorAt(paths[i], i+1);
                }
            }
        }
    }

    bool finishLinkRoom()
    {
        bool done = true;
        for(int i = 0; i < paths.Length; i++)
            done = done && rooms[i+1].eql(paths[i]);
        
        return done;
    }

    void createFloor()
    {
        if (floorObj == null)
        {
            floorObj = new GameObject();
            floorObj.name = "Floor Sprite";
        }
        else
        {
            Transform transform = floorObj.transform;
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < floor.GetLength(0); i++)
            for (int j = 0; j < floor.GetLength(1); j++)
            {
                GameObject floorBlock = null;
                Vector3 spawnPosition = new Vector3( i - floor.GetLength(0)/2 , j - floor.GetLength(0) / 2, 0);
                if(floor[i, j] == (int)BLOCK.WALL)
                {
                    floorBlock = Instantiate(wallSprite, spawnPosition, Quaternion.identity) as GameObject;
                }
                else if (floor[i, j] != (int)BLOCK.NULL)
                {
                     floorBlock = Instantiate(floorSprite, spawnPosition, Quaternion.identity) as GameObject;
                }

                if(floorBlock != null)
                    floorBlock.transform.parent = floorObj.transform;
            }
    }

    void placeFloorAt(coordinates c, int roomNum)
    {
        int[] searchFor = { (int)BLOCK.NULL, (int)BLOCK.WALL };
        if (System.Array.IndexOf( searchFor, floor[c.x, c.y] ) > -1 )
        {
            floor[c.x, c.y] = roomNum;
            placeWallAround(c);
        }
    }

    void placeWallAround(coordinates c)
    {
        if (checkExist(c, (int)DIR.LEFT))
        {
            placeWallAt(new coordinates(c.x - 1, c.y));
        }
        if (checkExist(c, (int)DIR.RIGHT))
        {
            placeWallAt(new coordinates(c.x + 1, c.y));
        }
        if (checkExist(c, (int)DIR.DOWN))
        {
            placeWallAt(new coordinates(c.x, c.y + 1));
        }
        if (checkExist(c, (int)DIR.UP))
        {
            placeWallAt(new coordinates(c.x, c.y - 1));
        }
    }

    void placeWallAt(coordinates c)
    {
        int[] searchFor = { (int)BLOCK.NULL };
        if (System.Array.IndexOf(searchFor, floor[c.x, c.y]) > -1)
        {
            floor[c.x, c.y] = (int)BLOCK.WALL;
        }
    }

    bool checkExist(coordinates c, int direction)
    {
        if(direction == (int)DIR.LEFT)
            return (c.x - 1 > 0);
        if(direction == (int)DIR.RIGHT)
            return (c.x + 1 < floor.GetLength(0));
        if(direction == (int)DIR.DOWN)
            return (c.y + 1 < floor.GetLength(1));
        if(direction == (int)DIR.UP)
            return (c.y - 1 > 0);
        return false;
    }

    public void testGenerate()
    {
        generate(100, 100, 5);
        string str = "";
        for (int i = 0; i < floor.GetLength(0); i++)
        {

            for (int j = 0; j < floor.GetLength(1); j++)
            {
                str += " " + ((floor[j, i] == (int)BLOCK.NULL) ? "_" : "" + floor[j, i]);
            }
            str += '\n';

        }
        Debug.Log(str);
    }

}
