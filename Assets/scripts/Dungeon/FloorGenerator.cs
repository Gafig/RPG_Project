
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour {

    public GameObject floorSprite;
    public GameObject wallSprite;
    public GameObject exitSprite;
    public GameObject playerSpawner;
    public GameObject nextLevelConnectorSprite;
    public GameObject floorObj;
    public GameObject sword, placedSword;

    List<Room> rooms = new List<Room>();
    private enum DIR : int { WEST = 1, EAST, NORTH, SOUTH, NW, NE, SW, SE };
    private enum AXIS : int { X = 1, Y };
    private bool isDone = false;
    private const int ROOM_SIZE = 37;
    private int currRoom = 0, depth = 0;
    [SerializeField]
    private Sprite[] floorSprites;
    public Sprite stair;

    public bool isFinished(){
        return isDone;
    }

    public void generate(int roomAmount, int depth)
    {
        Destroy(placedSword);
        this.depth = depth;
        rooms.Clear();
        isDone = false;
        for (int i = 0; i < roomAmount; i++) {
            Room newRoom = new Room(i + depth * roomAmount);
            rooms.Add(newRoom);
        }
        placeBlocks();
        isDone = true;
    }

   void placeBlocks()
    {
        if(depth >= 4)
            placedSword = Instantiate(sword, new Vector3(18f, -100f, -0.1f), Quaternion.identity) as GameObject;
        if (floorObj == null)
            floorObj = new GameObject("floor");
        else
        {
            foreach (Transform child in floorObj.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        int yStart = 5;
        placeEntrance(0);
        foreach (Room r in rooms)
        {
            for (int y = 0; y < r.room.GetLength(0); y++)
                for (int x = 0; x < r.room.GetLength(1); x++)
                {
                    if (IsSameOrSubclass(typeof(Wall), r.room[y, x].GetType()))
                    {
                        (Instantiate(wallSprite, new Vector3(x, -(y + yStart), 0), Quaternion.identity) as GameObject).transform.parent = floorObj.transform;
                    }

                    else if (IsSameOrSubclass(typeof(Floor), r.room[y, x].GetType()))
                    {
                        placeFloorAt(x, -(y + yStart));
                    }
                }
            yStart += r.room.GetLength(0);
            if (r != rooms[rooms.Count - 1])
            {
                placePassage(-yStart);
                yStart += 4;
            }
        }
        placeExit(-yStart);
    }

    private void placeFloorAt(int x, int y)
    {
        GameObject floorBlock = (Instantiate(floorSprite, new Vector3(x, y, 0), Quaternion.identity) as GameObject);
        floorBlock.transform.parent = floorObj.transform;
        SpriteRenderer spriteRenderer = floorBlock.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = floorSprites[UnityEngine.Random.Range(0, floorSprites.Length)];
    }

    private void placeStairAt(int x, int y)
    {
        GameObject floorBlock = (Instantiate(floorSprite, new Vector3(x, y, 0), Quaternion.identity) as GameObject);
        floorBlock.transform.parent = floorObj.transform;
        SpriteRenderer spriteRenderer = floorBlock.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = stair;
    }

    private void placeEntrance(int y)
    {
        if(depth > 0)
            placeEntrancePieces(y);
        else
            placeDunExit(y);
        placePassagePieces(y - 1);
        placeTopSpawnerPieces(y - 2);
        placePassagePieces(y - 3);
        placePassagePieces(y - 4);
    }

    private void placePassage(int y)
    {
        for (int i = 0; i > -4; i--)
            placePassagePieces(i + y);
    }

    private void placeExit(int y)
    {
        if (depth < 4)
        {
            placePassagePieces(y);
            placePassagePieces(y - 1);
            placeBottomSpawnerPieces(y - 2);
            placePassagePieces(y - 3);
            placeEndPieces(y - 4);
        }
    }

    private void placeEndPieces(int y)
    {
        (Instantiate(wallSprite, new Vector3(16, y, 0), Quaternion.identity) as GameObject).transform.parent = floorObj.transform;
        setConnecterDown(Instantiate(nextLevelConnectorSprite, new Vector3(17, y, 0), Quaternion.identity) as GameObject);
        setConnecterDown(Instantiate(nextLevelConnectorSprite, new Vector3(18, y, 0), Quaternion.identity) as GameObject);
        setConnecterDown(Instantiate(nextLevelConnectorSprite, new Vector3(19, y, 0), Quaternion.identity) as GameObject);
        (Instantiate(wallSprite, new Vector3(20, y, 0), Quaternion.identity) as GameObject).transform.parent = floorObj.transform;
    }

    private void setConnecterDown(GameObject connecter)
    {
        connecter.transform.parent = floorObj.transform;
        DangeonLevelConnector dlc = connecter.GetComponent<DangeonLevelConnector>();
        dlc.id = "DunEnt" + (depth+1);
        dlc.dir = Direction.down;
    }

    private void placeEntrancePieces(int y)
    {
        (Instantiate(wallSprite, new Vector3(16, y, 0), Quaternion.identity) as GameObject).transform.parent = floorObj.transform;
        setConnecterUp(Instantiate(nextLevelConnectorSprite, new Vector3(17, y, 0), Quaternion.identity) as GameObject);
        setConnecterUp(Instantiate(nextLevelConnectorSprite, new Vector3(18, y, 0), Quaternion.identity) as GameObject);
        setConnecterUp(Instantiate(nextLevelConnectorSprite, new Vector3(19, y, 0), Quaternion.identity) as GameObject);
        (Instantiate(wallSprite, new Vector3(20, y, 0), Quaternion.identity) as GameObject).transform.parent = floorObj.transform;
    }

    private void placeDunExit(int y)
    {
        (Instantiate(wallSprite, new Vector3(16, y, 0), Quaternion.identity) as GameObject).transform.parent = floorObj.transform;
        (Instantiate(exitSprite, new Vector3(17, y, 0), Quaternion.identity) as GameObject).transform.parent = floorObj.transform;
        (Instantiate(exitSprite, new Vector3(18, y, 0), Quaternion.identity) as GameObject).transform.parent = floorObj.transform;
        (Instantiate(exitSprite, new Vector3(19, y, 0), Quaternion.identity) as GameObject).transform.parent = floorObj.transform;
        (Instantiate(wallSprite, new Vector3(20, y, 0), Quaternion.identity) as GameObject).transform.parent = floorObj.transform;
    }

    private void setConnecterUp(GameObject connecter)
    {
        connecter.transform.parent = floorObj.transform;
        DangeonLevelConnector dlc = connecter.GetComponent<DangeonLevelConnector>();
        dlc.id = "DunEnt" + (depth);
        dlc.dir = Direction.up;
    }

    private void placeTopSpawnerPieces(int y)
    {
        (Instantiate(wallSprite, new Vector3(16, y, 0), Quaternion.identity) as GameObject).transform.parent = floorObj.transform;
        placeStairAt(17, y);

        GameObject spawner = (Instantiate(playerSpawner, new Vector3(18, y, 0), Quaternion.identity) as GameObject);
        setSpawner(spawner, depth, Direction.down);

        placeStairAt(19, y);
        (Instantiate(wallSprite, new Vector3(20, y, 0), Quaternion.identity) as GameObject).transform.parent = floorObj.transform;
    }

    private void placeBottomSpawnerPieces(int y)
    {
        (Instantiate(wallSprite, new Vector3(16, y, 0), Quaternion.identity) as GameObject).transform.parent = floorObj.transform;
        placeStairAt(17, y);

        GameObject spawner = (Instantiate(playerSpawner, new Vector3(18, y, 0), Quaternion.identity) as GameObject);
        setSpawner(spawner, depth+1, Direction.up);

        placeStairAt(19, y);
        (Instantiate(wallSprite, new Vector3(20, y, 0), Quaternion.identity) as GameObject).transform.parent = floorObj.transform;
    }


    private void setSpawner(GameObject spawner ,int depth, Direction face)
    {
        spawner.transform.parent = floorObj.transform;
        PlayerSpawner ps = spawner.GetComponent<PlayerSpawner>();
        ps.id = "DunEnt" + (depth);
        ps.facing = face;
    }

    private void placePassagePieces(int y)
    {
        (Instantiate(wallSprite, new Vector3(16, y, 0), Quaternion.identity) as GameObject).transform.parent = floorObj.transform;
        placeStairAt(17, y);
        placeStairAt(18, y);
        placeStairAt(19, y);
        (Instantiate(wallSprite, new Vector3(20, y, 0), Quaternion.identity) as GameObject).transform.parent = floorObj.transform;

    }

    public bool IsSameOrSubclass(System.Type potentialBase, System.Type potentialDescendant)
    {
        return potentialDescendant.IsSubclassOf(potentialBase)
               || potentialDescendant == potentialBase;
    }

}

public class Room
{
    public class RoomWall
    {
        List<Wall.Inner> walls = new List<Wall.Inner>();
        
        //belong to
        Block[,] room;
        public Wall.Inner wall;
        public RoomWall(Wall.Inner wall, Block[,] room)
        {
            this.room = room;
            this.wall = wall;
            addWall(wall);
        }

        public void addWall(Wall.Inner wall)
        {
            walls.Add(wall);
        }

        public String toString()
        {
            return wall.toString() + " has " + walls.Count;
        }

        public void explode()
        {
            foreach(Wall.Inner wall in walls)
            {
                room[wall.y, wall.x] = new Floor.Vanilla();
            }
        }
    }

    private class RoomLeaf
    {
        List<RoomLeaf> children;
        public RoomLeaf parent;
        public bool root;
        public int num;

        public RoomLeaf(int num)
        {
            parent = null;
            root = true;
            children = new List<RoomLeaf>();
            this.num = num;
        }

        public void connect (RoomLeaf other)
        {
            if (other.getRoot().num < this.getRoot().num) 
                other.addChildren(this);
            else if (other.getRoot().num > this.getRoot().num)
                this.addChildren(other);
        }

        public RoomLeaf getRoot()
        {
            return root ? this : this.parent.getRoot();
        }

        void addChildren(RoomLeaf child)
        {
            if (!child.root)
                child.rotateTree();
            children.Add(child);
            child.parent = this;
            child.root = false;
        }

        void rotateTree()
        {
            if (!parent.root)
                parent.rotateTree();
            parent.children.Remove(this);
            parent.root = false;
            parent.parent = this;
            this.children.Add(parent);
            this.parent = null;
            this.root = true;
        }

        public bool isConnectTo(RoomLeaf other)
        {
            return this.getRoot().num == other.getRoot().num;
        }
    }

    public Block[,] room;
    private RoomLeaf[] roomLeaves;

    //own
    List<RoomWall> roomWalls = new List<RoomWall>();
    private int[,] emptyRoomTemplate =
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1},
            {1, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1},
            {1, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1},
            {1, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1},
            {1, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1},
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
            {1, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 2, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 2, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 1},
            {1, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 2, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 2, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 1},
            {1, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 2, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 2, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 1},
            {1, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 2, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 2, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 1},
            {1, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 2, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 2, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 19, 19, 19, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
        };

    private int[,] treasureRoomTemplate =
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1},
            {1, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1},
            {1, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1},
            {1, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1},
            {1, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1},
            {1, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 1},
            {1, 6, 6, 6, 6, 6, 2, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 2, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 2, 9, 9, 9, 9, 9, 1},
            {1, 6, 6, 6, 6, 6, 2, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 2, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 2, 9, 9, 9, 9, 9, 1},
            {1, 6, 6, 6, 6, 6, 2, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 2, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 2, 9, 9, 9, 9, 9, 1},
            {1, 6, 6, 6, 6, 6, 2, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 2, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 2, 9, 9, 9, 9, 9, 1},
            {1, 6, 6, 6, 6, 6, 2, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 2, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 2, 9, 9, 9, 9, 9, 1},
            {1, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 1},
            {1, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 2, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 2, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 1},
            {1, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 2, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 2, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 1},
            {1, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 2, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 2, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 1},
            {1, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 2, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 2, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 1},
            {1, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 2, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 2, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 1},
            {1, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 1},
            {1, 13, 13, 13, 13, 13, 2, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 2, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 2, 16, 16, 16, 16, 16, 1},
            {1, 13, 13, 13, 13, 13, 2, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 2, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 2, 16, 16, 16, 16, 16, 1},
            {1, 13, 13, 13, 13, 13, 2, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 2, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 2, 16, 16, 16, 16, 16, 1},
            {1, 13, 13, 13, 13, 13, 2, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 2, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 2, 16, 16, 16, 16, 16, 1},
            {1, 13, 13, 13, 13, 13, 2, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 2, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 2, 16, 16, 16, 16, 16, 1},
            {1, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 1},
            {1, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 2, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 2, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 1},
            {1, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 2, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 2, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 1},
            {1, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 2, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 2, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 1},
            {1, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 2, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 2, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 1},
            {1, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 2, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 2, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
        };

    public Room(int i)
    {
        if (i <= 13)
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
                        {
                            int min = Math.Min(emptyRoomTemplate[y - 1, x] - 2, emptyRoomTemplate[y + 1, x] - 2);
                            int max = Math.Max(emptyRoomTemplate[y - 1, x] - 2, emptyRoomTemplate[y + 1, x] - 2);
                            wall = new Wall.Inner(min, max, x, y);
                        }
                        else
                        {
                            int min = Math.Min(emptyRoomTemplate[y, x - 1] - 2, emptyRoomTemplate[y, x + 1] - 2);
                            int max = Math.Max(emptyRoomTemplate[y, x - 1] - 2, emptyRoomTemplate[y, x + 1] - 2);
                            wall = new Wall.Inner(min, max, x, y);
                        }
                        room[y, x] = wall;
                        mergeWall(wall);
                    }
                    else
                        room[y, x] = new Floor.Vanilla();
                }
            createTree();
            shuffleList();
            explodeWalls();
        }
        else
        {
            room = new Block[treasureRoomTemplate.GetLength(0), treasureRoomTemplate.GetLength(1)];
            for (int y = 0; y < treasureRoomTemplate.GetLength(0); y++)
                for (int x = 0; x < treasureRoomTemplate.GetLength(1); x++)
                {  
                    if (treasureRoomTemplate[y, x] == 1)
                        room[y, x] = new Wall.Outer();
                    else
                        room[y, x] = new Floor.Vanilla();
                }
        }
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
        roomWalls.Add(new RoomWall(wall, room));
    }

    void createTree()
    {
        roomLeaves = new RoomLeaf[17];
        for (int i = 0; i < 17; i++)
            roomLeaves[i] = new RoomLeaf(i + 1); 
    }

    void shuffleList()
    {
        Shuffle<RoomWall>(roomWalls);
    }

    void explodeWalls()
    {
        foreach (RoomWall rw in roomWalls)
        {
            //Debug.Log("top: " + rw.wall.top + " bottom:" + rw.wall.bottom);
            if (!isConnect(rw))
            {
                rw.explode();
                connect(rw);
            }
        }
    }

    bool isConnect(RoomWall rw)
    {
        //Debug.Log("top: " + roomLeaves[rw.wall.top - 1].num + "bottom: " + roomLeaves[rw.wall.bottom - 1].num + "connect: " + roomLeaves[rw.wall.top - 1].isConnectTo(roomLeaves[rw.wall.bottom - 1]));
        return roomLeaves[rw.wall.top - 1].isConnectTo(roomLeaves[rw.wall.bottom - 1]);
    }

    void connect(RoomWall rw)
    {
        roomLeaves[rw.wall.top - 1].connect(roomLeaves[rw.wall.bottom - 1]);
    }

    void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        System.Random rnd = new System.Random();
        while (n > 1)
        {
            int k = (rnd.Next(0, n));
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

public class Coordinates
{
    public int x, y;

    public Coordinates(int p1, int p2)
    {
        x = p1;
        y = p2;
    }

    public bool eql(Coordinates c2)
    {
        return x == c2.x && y == c2.y;
    }

    public override string ToString()
    {
        return x + "," + y;
    }

}