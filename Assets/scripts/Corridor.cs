
using UnityEngine;

// possible directions 
public enum Direction
{
    North,East,South,West,
}

public class Corridor
{
    public int startXPos;           //start Xposition of corridor
    public int startYPos;           //start Yposition of corridor

    public int corridorLength;      // corridor length
    public Direction direction;     // direction 

    public int EndPositionX //get the end X position of the corridor
    {
        get
        {
            if (direction == Direction.North || direction == Direction.South)
            {
                return startXPos;
            }
            if (direction == Direction.East)
            {
                return startXPos + corridorLength - 1;
            }
            return startXPos - corridorLength + 1;
        }
    }

    public int EndPositionY//get the end Y position of the corridor
    {
        get
        {
            if (direction == Direction.East || direction == Direction.West)
            {
                return startYPos;
            }
            if (direction == Direction.North)
            {
                return startYPos + corridorLength - 1;
            }
            return startYPos - corridorLength + 1;
        }
    }

    public void SetupCorridor(Room room, IntRange length, IntRange roomWidth, IntRange roomHeight,int columns,int rows,bool firstCorridor)
    {
        direction = (Direction)Random.Range(0,4); //get the direction where we are making the corridor

        Direction oppositeDirection = (Direction)(((int)room.enteringCorridor + 2) % 4); //get the opposite direction where we are making the corridor

        if (!firstCorridor && direction == oppositeDirection) // when we are not in the first corridor and corridor equels opposite dirrection sett direction +1 and use that direction
        {
            int directionInt = (int)direction; 
            directionInt++;
            directionInt = directionInt % 4;
            direction = (Direction)directionInt;
        }

        corridorLength = length.Random;             // set legnth to what you set in the inspector
        int maxLength = length.m_Max;               // max legtth is the max you have filled in

        switch (direction) // set start positionx/y and legnth for every possible direction
        {
            case Direction.North:
                startXPos = Random.Range(room.xPos,room.xPos + room.roomWidth -1);
                startYPos = room.yPos + room.roomHeight;
                maxLength = rows - startYPos - roomHeight.m_Min;
                break;
            case Direction.East:
                startXPos = room.xPos + room.roomWidth;
                startYPos = Random.Range(room.yPos, room.yPos + room.roomHeight - 1);
                maxLength = columns - startXPos - roomWidth.m_Min;
                break;
            case Direction.South:
                startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth);
                startYPos = room.yPos;
                maxLength = startYPos - roomHeight.m_Min;
                break;
            case Direction.West:
                startXPos = room.xPos;
                startYPos = Random.Range(room.yPos, room.yPos + room.roomHeight);
                maxLength = startXPos - roomWidth.m_Min;
                break;
        }
        corridorLength = Mathf.Clamp(corridorLength,1,maxLength); //clamps the corridor length between 1 and the maxlength
    }
}

