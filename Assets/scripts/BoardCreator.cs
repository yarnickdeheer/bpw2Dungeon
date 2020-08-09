using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    // these are the 2 types of tiles 
    public enum TileType
    {
        Wall, Floor,
    }

    public int columns = 100;                                   // grid width
    public int rows = 100;                                      // grid height    
    public IntRange numRooms = new IntRange( 4, 10);            // ammount of rooms
    public IntRange roomWidth = new IntRange(3, 10);            // room width
    public IntRange roomHeight = new IntRange(3, 10);           // room height
    public IntRange corridorLength = new IntRange(6, 10);       // corridor length
    public GameObject[] floorTiles;                             // floor tile variations
    public GameObject[] outerWallTiles;                         // wall tile variations

    private TileType[][] tiles;                                 // tyle type 
    private Room[] rooms;                                       // link to room script 
    private Corridor[] corridors;                               // link to room script                  
    private GameObject boardHolder;                             // parrent for the board        
    public GameObject enemy;                                    // enemy prefab
    public GameObject Player;                                   // player prefab   
    public GameObject chest;                                    // chest prefab
    public GameObject stair;                                    // stair prefab
   // private int R;

    void Awake()
    {
        boardHolder = new GameObject("BoardHolder");            //make boardholder object
        SetupTilesArray();                                      //setup for tiles
        CreateRoomsAndCorridors();                              //create rooms and corridors create and set player/chest position 
        SetTilesValuesForRooms();                               // set tile values for rooms and create enemys
        SetTilesValuesForCorridors();                           // set tile values for corridors        
        InstantiateTiles();                                     // create tiles
    }

    void SetupTilesArray()
    {
        tiles = new TileType[columns][];
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new TileType[rows];
        }
    }
    void CreateRoomsAndCorridors() 
    {
        rooms = new Room[numRooms.Random];                                                                  //create rooms the amount of rooms wil be determant my room script
        corridors = new Corridor[rooms.Length - 1];                                                         //create rooms the amount of rooms wil be determant my room script
        int random = Random.Range(1, rooms.Length);                                                         // create random to set the chest position in a random room
        int random2 = Random.Range(1, rooms.Length-1);                                                      // create random to set the stair position in a random room
        rooms[0] = new Room();                                                                              // create the first room     
        corridors[0] = new Corridor();                                                                      // create the first corridor

        rooms[0].SetupRoom(roomWidth,roomHeight,columns,rows);                                              // setup the first room   

        corridors[0].SetupCorridor(rooms[0],corridorLength ,roomWidth, roomHeight, columns, rows,true);     // setup the first corridor  

        for (int i = 1; i < rooms.Length;i++)
        {
            rooms[i] = new Room();                                                                           // create the rooms based on the amount you filled in  

            rooms[i].SetupRoom(roomWidth,roomHeight,columns,rows,corridors[i - 1]);
          
               
            if (i < corridors.Length)                               
            {
                corridors[i] = new Corridor();                                                              // create the corridors based on the amount you filled in  

                corridors[i].SetupCorridor(rooms[i], corridorLength,roomWidth,roomHeight,columns,rows,false);
            }
            if (i == 5)                                                                                    // create and sets the player in the 5th room
            {
                Vector3 Playerpos= new Vector3(rooms[i].xPos, rooms[i].yPos, 0);
                Instantiate(Player,Playerpos,Quaternion.identity);
            }
            if (i == random)                                                                              // create and sets the chest in a random room     
            {
                Vector3 chestpos = new Vector3(rooms[i].xPos + Random.Range(0,10), rooms[i].yPos + Random.Range(0, 10), 0);
                Instantiate(chest, chestpos, Quaternion.identity);
            }
             if(i == random2)                                                                        // create and sets the stairs in a random room    
            {
                Vector3 stairpos = new Vector3(rooms[i].xPos + Random.Range(0, 10), rooms[i].yPos + Random.Range(0, 10), 0);
                Instantiate(stair, stairpos, Quaternion.identity);
            }
        }

    }
    void SetTilesValuesForRooms() 
    {

        for (int i = 0; i < rooms.Length; i++)
        {
            Room currentRoom = rooms[i];
            if (i==0)
            {                               // create and sets the enemy in the first room

                GameObject E = Instantiate(enemy, new Vector3(currentRoom.xPos + Random.Range(0, 10), currentRoom.yPos + Random.Range(0, 10), 0), Quaternion.identity) as GameObject;

            }

            else if(i !=5)                // create and sets the enemy in the other rooms exept the room the player start room
            { 
                GameObject E = Instantiate(enemy, new Vector3(currentRoom.xPosEnemyspawn, currentRoom.yPosEnemyspawn, 0), Quaternion.identity) as GameObject;
            }

            for (int j = 0; j < currentRoom.roomWidth; j++)                         //set the tile type to floor ont here positions
            {
                int xCoord = currentRoom.xPos + j;

                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;

                    tiles[xCoord][yCoord] = TileType.Floor;
                }
         
            }
        }
    }
    
    void SetTilesValuesForCorridors()                 
    {

        // setup for the corridor tile types 
        for (int i = 0;i< corridors.Length; i++)
        {
            Corridor currentCorridor = corridors[i];                
            for (int j = 0; j < currentCorridor.corridorLength;j++)
            {
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                //check wich direction the corridor is going and sets the tiles in that direction to the floor type
                switch (currentCorridor.direction) 
                {
                    case Direction.North:
                        yCoord +=j;
                        break;
                    case Direction.East:
                        xCoord += j;
                        break;
                    case Direction.South:
                        yCoord -= j;
                        break;
                    case Direction.West:
                        xCoord -= j;
                        break;
                }

                tiles[xCoord][yCoord] = TileType.Floor;
            }
        }
    }
    void InstantiateTiles()                 // create the tiles
    {
        for (int i = 0; i<tiles.Length; i++) 
        {
            for (int j = 0; j < tiles[i].Length; j++)
            {
                if(tiles[i][j] == TileType.Floor)                   // create the floor tiles op the position of the tiles that were setup
                {
                    InstantiateFromArray(floorTiles, i, j);

                    // check if the tile next to the floor is a wall type create a wall there
                    if (tiles[i+1][j] == TileType.Wall )
                    {
                        InstantiateFromArray(outerWallTiles, i + 1, j);
                    }
                    if (tiles[i - 1][j] == TileType.Wall)
                    {
                        InstantiateFromArray(outerWallTiles, i - 1, j); 
                    }
                    if (tiles[i][j+1] == TileType.Wall)
                    {
                        InstantiateFromArray(outerWallTiles, i, j+1);
                    }
                    if (tiles[i][j-1] == TileType.Wall)
                    {
                        InstantiateFromArray(outerWallTiles, i, j-1);
                    }

                }

             

            }
        }
    } 
   
    void InstantiateFromArray(GameObject[] prefabs,float xCoord,float yCoord)
    {
        //instanciate the tile out of the asked array on the asked position as child of the boardholder object
        int randomIndex = Random.Range(0, prefabs.Length);
        Vector3 position = new Vector3(xCoord,yCoord,0);
        GameObject tileInstance = Instantiate(prefabs[randomIndex],position,Quaternion.identity) as GameObject;
        tileInstance.transform.parent = boardHolder.transform;
    }
 
}

