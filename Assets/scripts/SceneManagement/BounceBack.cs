using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBack : Event {

    public enum Direction
    {
        down, up, left, right
    }

    [SerializeField]
    Direction direction;

    public override void trigger()
    {
        if (direction == Direction.down)
            MoveAround.instance.faceDown();
        else if (direction == Direction.up)
            MoveAround.instance.faceUp();
        else if (direction == Direction.left)
            MoveAround.instance.faceLeft();
        else if (direction == Direction.right)
            MoveAround.instance.faceRight();
    }

}
