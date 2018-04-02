using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block {

}

public class Floor : Block
{
    public class Vanilla : Floor
    {
        public int roomNum;
        public Vanilla(int roomNum)
        {
            this.roomNum = roomNum;
        }

        public Vanilla()
        {
        }
    }

    public class Spawner : Floor
    {
        public Spawner()
        {
        }
    }

    public class Upstair : Floor
    {
        public int floor;
        public Upstair(int floor)
        {
            this.floor = floor;
        }
    }

    public class Downstair : Floor
    {
        public int floor;
        public Downstair(int floor)
        {
            this.floor = floor;
        }
    }
}

public class Wall : Block
{
    public class Inner : Wall
    {
        public int top, bottom, x, y;
        public Inner(int top, int bottom, int x, int y)
        {
            this.top = top;
            this.bottom = bottom;
            this.x = x;
            this.y = y;
        }

        public bool isSameWall(Wall.Inner other)
        {
            return this.top == other.top && this.bottom == other.bottom;
        }

        public string toString()
        {
            return top + ":" + bottom;
        }
    }

    public class Outer : Wall
    {
        public Outer()
        {

        }
    }
}

public class EmptySpace : Block
{
    
}

