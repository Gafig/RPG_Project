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
    private List<List<coordinates>> queue = new List<List<coordinates>>();
    private enum DIR : int { WEST = 1, EAST, NORTH, SOUTH, NW, NE, SW, SE };
    private enum AXIS : int { X = 1, Y };
    private enum BLOCK : int { NULL = -1, WALL};

    void generate(int sizeX, int sizeY, int roomAmount) {
        floor = new int[sizeX, sizeY];
        rooms = new coordinates[roomAmount];
        paths = new coordinates[roomAmount - 1];

        for (int i = 0; i < floor.GetLength(0); i++)
            for (int j = 0; j < floor.GetLength(1); j++)
                floor[i, j] = (int)BLOCK.NULL;
        placeRoomCenter(roomAmount);
        expandRooms();
        linkRooms();
        createFloor();
    }

    void placeRoomCenter(int roomAmount)
    {
        int indent = floor.GetLength(0) / roomAmount / 2;
        int startY = 0;
        int endY = startY + floor.GetLength(1) / roomAmount;
        for (int i = 0; i < roomAmount; i++)
        {

            coordinates roomCenter = new coordinates(Random.Range(0, floor.GetLength(1)), Random.Range(startY, endY));
            rooms[i] = roomCenter;
            List<coordinates> list = new List<coordinates>();
            list.Add(roomCenter);
            addCoToQ( list );
            placeFloorAt(roomCenter, i + 1);
            if (i < roomAmount - 1)
                paths[i] = roomCenter;

            startY = endY;
            endY = startY + floor.GetLength(1) / roomAmount;
        }
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

    void expandRooms()
    {
        for(int roomSize = 0; roomSize < 15; roomSize++)
        {
            int RoomAmount = queue.Count;
            for (int room = 0; room < RoomAmount; room++)
            {
                List<coordinates> outers = queue[0], newOuter = new List<coordinates>();
                foreach (coordinates c in outers)
                {
                    List<coordinates> list = placeFloorAround(c);
                    newOuter.AddRange(list);
                }
                queue.Remove(outers);
                queue.Add(newOuter);
            }
        }
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
                Vector3 spawnPosition = new Vector3( floor.GetLength(0)/2 - i , floor.GetLength(0) / 2 - j, 0);
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

    List<coordinates> placeFloorAround(coordinates c)
    {
        int roomNum = floor[c.x, c.y];
        List<coordinates> list = new List<coordinates>();
        if (checkExist(c, (int)DIR.WEST))
        {
            coordinates co = new coordinates(c.x - 1, c.y);
            if (getRoomNumAt(co) != getRoomNumAt(c))
            {
                placeFloorAt(co, roomNum);
                list.Add(co);
            }
        }
        if (checkExist(c, (int)DIR.EAST))
        {
            coordinates co = new coordinates(c.x + 1, c.y);
            if (getRoomNumAt(co) != getRoomNumAt(c))
            {
                placeFloorAt(new coordinates(c.x + 1, c.y), roomNum);
                list.Add(co);
            }
        }
        if (checkExist(c, (int)DIR.SOUTH))
        {
            coordinates co = new coordinates(c.x, c.y + 1);
            if (getRoomNumAt(co) != getRoomNumAt(c))
            {
                placeFloorAt(new coordinates(c.x, c.y + 1), roomNum);
                list.Add(co);
            }
        }
        if (checkExist(c, (int)DIR.NORTH))
        {
            coordinates co = new coordinates(c.x, c.y - 1);
            if (getRoomNumAt(co) != getRoomNumAt(c))
            {
                placeFloorAt(new coordinates(c.x, c.y - 1), roomNum);
                list.Add(co);
            }
        }/*
        if (checkExist(c, (int)DIR.NW))
        {
            coordinates co = new coordinates(c.x - 1, c.y - 1);
            if (getRoomNumAt(co) != getRoomNumAt(c))
            {
                placeFloorAt(new coordinates(c.x - 1, c.y - 1), roomNum);
                list.Add(co);
            }
        }
        if (checkExist(c, (int)DIR.NE))
        {
            coordinates co = new coordinates(c.x + 1, c.y - 1);
            if (getRoomNumAt(co) != getRoomNumAt(c))
            {
                placeFloorAt(new coordinates(c.x + 1, c.y - 1), roomNum);
                list.Add(co);
            }
        }
        if (checkExist(c, (int)DIR.SW))
        {
            coordinates co = new coordinates(c.x - 1, c.y + 1);
            if (getRoomNumAt(co) != getRoomNumAt(c))
            {
                placeFloorAt(new coordinates(c.x - 1, c.y + 1), roomNum);
                list.Add(co);
            }
        }
        if (checkExist(c, (int)DIR.SE))
        {
            coordinates co = new coordinates(c.x + 1, c.y + 1);
            if (getRoomNumAt(co) != getRoomNumAt(c))
            {
                placeFloorAt(new coordinates(c.x + 1, c.y + 1), roomNum);
                list.Add(co);
            }
        }*/
        return list;
    }

    void placeWallAround(coordinates c)
    {
        int roomNum = getRoomNumAt(c);
        if (checkExist(c, (int)DIR.WEST))
        {
            placeWallAt(new coordinates(c.x - 1, c.y), roomNum);
        }
        if (checkExist(c, (int)DIR.EAST))
        {
            placeWallAt(new coordinates(c.x + 1, c.y), roomNum);
        }
        if (checkExist(c, (int)DIR.SOUTH))
        {
            placeWallAt(new coordinates(c.x, c.y + 1), roomNum);
        }
        if (checkExist(c, (int)DIR.NORTH))
        {
            placeWallAt(new coordinates(c.x, c.y - 1),roomNum);
        }
    }

    void placeWallAround(List<coordinates> list)
    {
        foreach (coordinates c in list)
        {
            placeWallAround(c);
        }
    }

    void placeWallAt(coordinates c, int roomNum)
    {
        int[] searchFor = { (int)BLOCK.NULL, (int)BLOCK.WALL };
        if (System.Array.IndexOf(searchFor, floor[c.x, c.y]) > -1)
        {
            if(floor[c.x, c.y] != roomNum)
            floor[c.x, c.y] = (int)BLOCK.WALL;
        }
    }

    bool checkExist(coordinates c, int direction)
    {
        if(direction == (int)DIR.WEST)
            return (c.x - 1 > 0);
        if(direction == (int)DIR.EAST)
            return (c.x + 1 < floor.GetLength(1));
        if(direction == (int)DIR.SOUTH)
            return (c.y + 1 < floor.GetLength(0));
        if(direction == (int)DIR.NORTH)
            return (c.y - 1 > 0);
        if (direction == (int)DIR.NW)
            return (c.x - 1 > 0 && c.y - 1 > 0);
        if (direction == (int)DIR.NE)
            return (c.x + 1 < floor.GetLength(0) && c.y - 1 > 0);
        if (direction == (int)DIR.SW)
            return (c.x - 1 > 0 && c.y + 1 < floor.GetLength(0));
        if (direction == (int)DIR.SE)
            return (c.x + 1 < floor.GetLength(1) && c.y + 1 < floor.GetLength(0));
        return false;
    }

    void addCoToQ(List<coordinates> list)
    {
        queue.Add(list);
    }

    int getRoomNumAt(coordinates c)
    {
        return floor[c.x, c.y];
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
