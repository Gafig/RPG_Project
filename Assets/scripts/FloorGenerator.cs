using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour {

    

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
            /*
            Debug.Log("Debug eql");
            Debug.Log("X:" + x + ":" + c2.x + ":" + (x == c2.x));
            Debug.Log("Y:" + y + ":" + c2.y + ":" + (y == c2.y));
            Debug.Log("X-----------X");
            */            
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
    private const int X = 1, Y = 2, RIGHT = 1, LEFT = 2, UP = 3, DOWN = 4;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void generate(int sizeX, int sizeY, int roomAmount) {
        sizeX = 101;
        sizeY = 101;
        floor = new int[sizeX, sizeY];
        rooms = new coordinates[roomAmount];
        paths = new coordinates[roomAmount - 1];

        for (int i = 0; i < floor.GetLength(0); i++)
            for (int j = 0; j < floor.GetLength(1); j++)
                floor[i, j] = -1;

        for (int i = 0; i< roomAmount; i++)
        {
            int startY = sizeY / roomAmount * i;
            int endY = startY + sizeY / roomAmount;
            coordinates roomCenter = new coordinates(Random.Range(0, sizeX), Random.Range(startY, endY));
            rooms[i] = roomCenter;
            queue.Add(roomCenter);
            floor[roomCenter.x, roomCenter.y] = i+1;
            if (i < roomAmount - 1)
                paths[i] = roomCenter;
        }
        linkRooms();
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
                        if (dir == X)
                            paths[i] = new coordinates(currentLocation.x - xFactor, currentLocation.y);
                        else if (dir == Y)
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

                    if (floor[paths[i].x, paths[i].y] == -1)
                        floor[paths[i].x, paths[i].y] = i + 1;
                }
            }
        }
    }

    bool finishLinkRoom()
    {
        bool done = true;
        for(int i = 0; i < paths.Length; i++)
        {
            Debug.Log(paths[i].x + "," + paths[i].y );
            Debug.Log(rooms[i + 1].x + "," + rooms[i + 1].y);
            Debug.Log("Room" + (i+1) + " : " + (rooms[i + 1].eql(paths[i])));
            done = done && rooms[i+1].eql(paths[i]);
        }
        return done;
    }

    public void testGenerate()
    {
        generate(1, 1, 10);
        string str = "";
        for (int i = 0; i < floor.GetLength(0); i++)
        {
            
            for (int j = 0; j < floor.GetLength(1); j++)
            {
                str += " " + ( (floor[j, i] == -1) ? "_" : "" + floor[j, i]);
            }
            str += '\n';
            
        }
        Debug.Log(str);
        Debug.Log(finishLinkRoom());
    }

    /*
    void generateHelper(int x, int y)
    {
        if (checkLeft(x, y))
        {

        }
        if (checkRight(x, y))
        {

        }
        if (checkUp(x, y))
        {

        }
        if (checkDown(x, y))
        {

        }

    }

    bool checkLeft(int x, int y)
    {
        return (x - 1 > 0);
    }

    bool checkRight(int x, int y)
    {
        return (x + 1 < floor.GetLength(0));
    }

    bool checkDown(int x, int y)
    {
        return (y + 1 < floor.GetLength(1));
    }

    bool checkUp(int x, int y)
    {
        return (y - 1 > 0) ;
    }*/

}
