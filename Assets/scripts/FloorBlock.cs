using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

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
}

public class Wall : Block
{
    public class Inner : Wall
    {
        public int top, bottom;
        public Inner(int top, int bottom)
        {
            this.top = top;
            this.bottom = bottom;
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

