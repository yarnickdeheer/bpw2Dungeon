using UnityEngine;
public class Room
{
    public int xPos;        // x value of the start of each room
    public int yPos;        // y value of the start of each room
    public int xPosEnemyspawn;  // x position of the enemy in the room
    public int yPosEnemyspawn;  // y position of the enemy in the room
    public int roomWidth;       // value of room width
    public int roomHeight;      // value of room height
    public Direction enteringCorridor;  //direction of the entering of the corridor

    public int Chestx;      // x position of the chest
    public int Chesty;      // y position of the chest

    public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows)
    {   
        roomWidth = widthRange.Random;  // set roomwidth
        roomHeight = heightRange.Random;    // set roomheight
    
        xPos = Mathf.RoundToInt(columns / 2f - roomWidth / 2f);     // set xpos to the left down cornor of the room
        yPos = Mathf.RoundToInt(rows / 2f - roomHeight / 2f);       // set ypos to the left down cornor of the room
     
        xPosEnemyspawn = xPos + Random.Range(4, roomWidth);         // set the enemy xpos spawn position  
        yPosEnemyspawn = yPos + Random.Range(4, roomHeight);        // set the enemy ypos spawn position  
        Chestx = xPos + Random.Range(2, roomWidth);                 // set the chest xpos spawn position  
        Chesty = yPos + Random.Range(2, roomHeight);                // set the chest ypos spawn position  



    }
    public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows, Corridor corridor)
    { 
        enteringCorridor = corridor.direction;  // set the enteringcorridor value to the direction in the corridor script

        roomWidth = widthRange.Random;          // set roomwidth
        roomHeight = heightRange.Random;        // set roomheight
     
      

        switch (corridor.direction) // set start positionx/y and legnth/heigth for every possible direction
        {
            case Direction.North:


                roomHeight = Mathf.Clamp(roomHeight, 1, rows - corridor.EndPositionY);

                yPos = corridor.EndPositionY;

                xPos = Random.Range(corridor.EndPositionX - roomWidth + 1 , corridor.EndPositionX);

                xPos = Mathf.Clamp(xPos,0 ,columns - roomWidth);
                break; 
            case Direction.East:
                roomWidth = Mathf.Clamp(roomWidth, 1, columns - corridor.EndPositionX);

                xPos = corridor.EndPositionX;

                yPos = Random.Range(corridor.EndPositionY - roomHeight + 1, corridor.EndPositionY);

                yPos = Mathf.Clamp(yPos, 0, rows - roomHeight);
                break; 
            case Direction.South:
                roomHeight = Mathf.Clamp(roomHeight, 1, rows - corridor.EndPositionY);

                yPos = corridor.EndPositionY - roomHeight + 1;
                 
                xPos = Random.Range(corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);

                xPos = Mathf.Clamp(xPos, 0, columns - roomWidth);
                break;
            case Direction.West:
                roomWidth = Mathf.Clamp(roomWidth, 1, columns - corridor.EndPositionX);

                xPos = corridor.EndPositionX - roomWidth + 1;

                yPos = Random.Range(corridor.EndPositionY - roomHeight + 1, corridor.EndPositionY);

                yPos = Mathf.Clamp(yPos, 0, rows - roomHeight);
                break; 
        }
        xPosEnemyspawn = xPos + Random.Range(0, roomWidth);         // set the enemy xpos spawn position
        yPosEnemyspawn = yPos + Random.Range(0, roomHeight);        // set the enemy ypos spawn position
    }

}
 