
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour {

    public GameObject floorSprite;
    public GameObject wallSprite;
    public GameObject floorObj;

    private Block[,] floor;
    List<Room> rooms = new List<Room>();
    private enum DIR : int { WEST = 1, EAST, NORTH, SOUTH, NW, NE, SW, SE };
    private enum AXIS : int { X = 1, Y };
    
    private const int ROOM_SIZE = 37;

    void generate(int roomAmount)
    {
        newEmptyFloor(roomAmount);
        placeBlocks();
    }

    void newEmptyFloor(int roomAmount)
    {
        
        floor = new Block[ROOM_SIZE, (ROOM_SIZE + 5) * roomAmount];
        for(int i = 0; i<roomAmount; i++)
            rooms.Add(new Room());
    }

   void placeBlocks()
    {
        if (floorObj == null)
            floorObj = new GameObject("floor");
        else
        {
            foreach (Transform child in floorObj.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        int yStart = 0;
        foreach (Room r in rooms)
        {
            for (int y = 0; y < r.room.GetLength(0); y++)
                for (int x = 0; x < r.room.GetLength(1); x++)
                {
                    if (IsSameOrSubclass(typeof(Wall), r.room[y, x].GetType()))
                    {
                        (Instantiate(wallSprite, new Vector3(x, y + yStart, 0), Quaternion.identity) as GameObject).transform.parent = floorObj.transform;
                    }

                    else if (IsSameOrSubclass(typeof(Floor), r.room[y, x].GetType()))
                    {
                        (Instantiate(floorSprite, new Vector3(x, y + yStart, 0), Quaternion.identity) as GameObject).transform.parent = floorObj.transform;
                    }
                }
            yStart += r.room.GetLength(0);
        }
    }

    public bool IsSameOrSubclass(System.Type potentialBase, System.Type potentialDescendant)
    {
        return potentialDescendant.IsSubclassOf(potentialBase)
               || potentialDescendant == potentialBase;
    }

    public void testGenerate()
    {
        generate(1);
    }

}

public class Room
{
    public class RoomWall
    {
        List<Wall.Inner> walls = new List<Wall.Inner>();
        public Wall.Inner wall;
        public RoomWall(Wall.Inner wall)
        {
            this.wall = wall;
            addWall(wall);
        }

        public void addWall(Wall.Inner wall)
        {
            walls.Add(wall);
        }

        public String toString()
        {
            return wall.toString() + " sas " + walls.Count;
        }
    }

    public Block[,] room;
    List<RoomWall> roomWalls = new List<RoomWall>();
    private int[,] emptyRoomTemplate =
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1},
            {1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1},
            {1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1},
            {1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1},
            {1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1},
            {1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1},
            {1, 6, 6, 6, 6, 6, 2, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 2, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 2, 9, 9, 9, 9, 9, 1},
            {1, 6, 6, 6, 6, 6, 2, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 2, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 2, 9, 9, 9, 9, 9, 1},
            {1, 6, 6, 6, 6, 6, 2, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 2, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 2, 9, 9, 9, 9, 9, 1},
            {1, 6, 6, 6, 6, 6, 2, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 2, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 2, 9, 9, 9, 9, 9, 1},
            {1, 6, 6, 6, 6, 6, 2, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 2, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 2, 9, 9, 9, 9, 9, 1},
            {1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1},
            {1, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 2, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 2, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 1},
            {1, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 2, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 2, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 1},
            {1, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 2, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 2, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 1},
            {1, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 2, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 2, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 1},
            {1, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 2, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 2, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 1},
            {1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1},
            {1, 13, 13, 13, 13, 13, 2, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 2, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 2, 16, 16, 16, 16, 16, 1},
            {1, 13, 13, 13, 13, 13, 2, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 2, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 2, 16, 16, 16, 16, 16, 1},
            {1, 13, 13, 13, 13, 13, 2, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 2, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 2, 16, 16, 16, 16, 16, 1},
            {1, 13, 13, 13, 13, 13, 2, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 2, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 2, 16, 16, 16, 16, 16, 1},
            {1, 13, 13, 13, 13, 13, 2, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 2, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 2, 16, 16, 16, 16, 16, 1},
            {1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1},
            {1, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 2, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 2, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 1},
            {1, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 2, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 2, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 1},
            {1, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 2, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 2, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 1},
            {1, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 2, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 2, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 1},
            {1, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 2, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 2, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
        };

    public Room()
    {
        room = new Block[emptyRoomTemplate.GetLength(0), emptyRoomTemplate.GetLength(1)];
        for (int y = 0; y < emptyRoomTemplate.GetLength(0); y++)
            for (int x = 0; x < emptyRoomTemplate.GetLength(1); x++)
            {
                if (emptyRoomTemplate[y, x] == 0)
                    room[y, x] = new EmptySpace();
                else if (emptyRoomTemplate[y, x] == 1)
                    room[y, x] = new Wall.Outer();
                else if (emptyRoomTemplate[y, x] == 2)
                {
                    Wall.Inner wall;
                    if (emptyRoomTemplate[y, x - 1] == 2 || emptyRoomTemplate[y, x - 1] == 1)
                        wall = new Wall.Inner(emptyRoomTemplate[y - 1, x] - 1, emptyRoomTemplate[y + 1, x] - 1);
                    else
                        wall = new Wall.Inner(emptyRoomTemplate[y, x - 1] - 1, emptyRoomTemplate[y, x + 1] - 1);
                        
                    room[y, x] = wall;
                    mergeWall(wall);
                }
                else
                    room[y, x] = new Floor.Vanilla();
            }
        Debug.Log(roomWalls.Count);
        foreach (RoomWall roomWall in roomWalls)
            Debug.Log(roomWall.toString());
    }

    void mergeWall(Wall.Inner wall)
    {
        foreach(RoomWall roomWall in roomWalls)
        {
            if (wall.isSameWall(roomWall.wall))
            {
                roomWall.addWall(wall);
                return;
            }
        }
        roomWalls.Add(new RoomWall(wall));
    }
}

public class coordinates
{
    public int x, y;

    public coordinates(int p1, int p2)
    {
        x = p1;
        y = p2;
    }

    public bool eql(coordinates c2)
    {
        return x == c2.x && y == c2.y;
    }

    public override string ToString()
    {
        return x + "," + y;
    }

}