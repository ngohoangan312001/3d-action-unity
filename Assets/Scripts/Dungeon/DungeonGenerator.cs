using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AN
{
    //using Depth First Search Algorithm
    class Cell
    {
        public bool visited = false;
        
        /**
         * 0:top (north)
         * 1:down (south)
         * 2:right (east)
         * 3:left (west)
         */
        public bool[] status = new bool[4];
    }

    public class DungeonGenerator : MonoBehaviour
    {
        [Header("Size of the grid map ( size = X*Y)")]
        [SerializeField] private Vector2 size;

        [Header("List off Room ( !IMPORTANCE: first and last index is for start and end room )")] 
        [SerializeField] private GameObject[] rooms;
        
        private int startPosition = 0;
        private List<Cell> board;

        private void Start()
        {
            GeneratorGridMap();
        }

        void GeneratorDungeon()
        {
            int roomIndex = 0;
            GameObject room;
            Vector2 offSet;
            Vector3 roomPosition;
            Cell currentCell;
            int currentCellIndex;
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    currentCellIndex = Mathf.FloorToInt(i + j * size.x);
                    currentCell = board[currentCellIndex];
                    
                    if (currentCell.visited)
                    {
                        roomIndex = Random.Range(0, rooms.Length);

                        // if (currentCellIndex == 0)
                        // {
                        //     roomIndex = 0;
                        // }
                        // else if (currentCellIndex == board.Count - 1)
                        // {
                        //     roomIndex = rooms.Length -1;
                        // }
                        
                        room = rooms[roomIndex];
                        offSet = room.GetComponent<RoomBehaviour>().offSet;
                        roomPosition = new Vector3(i * offSet.x, 0, -j * offSet.y);
                        RoomBehaviour newRoom = Instantiate(room, roomPosition,Quaternion.identity,transform).GetComponent<RoomBehaviour>();
                        newRoom.UpdateRoom(currentCell.status);

                        newRoom.name += " " + i +"-"+ j;
                    }

                    currentCell = board[currentCellIndex];
                }
            }
        }
        
        void GeneratorGridMap()
        {
            board = new List<Cell>();

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    board.Add(new Cell());
                }
            }

            int currentCellIndex = startPosition;

            Stack<int> path = new Stack<int>();

            while (true)
            {
                board[currentCellIndex].visited = true;
                
                if (currentCellIndex == board.Count - 1)
                    break;


                List<int> neighbors = NeighborCheck(currentCellIndex);

                //no available neighbor
                if (neighbors.Count == 0)
                {
                    //reach the last cell
                    if (path.Count == 0)
                    {
                        break;
                    }

                    //current cell = last cell added to path
                    currentCellIndex = path.Pop();
                }
                else
                {
                    path.Push(currentCellIndex);
                    
                    //get random available neighbor
                    int newCell = neighbors[Random.Range(0, neighbors.Count)];

                    //right or bottom neighbor
                    //right: cell + 1
                    //bottom: cell + size.x
                    if (newCell > currentCellIndex)
                    {
                        //right
                        if ((newCell - 1) == currentCellIndex)
                        {
                            //current cell open right path
                            board[currentCellIndex].status[2] = true;
                            //new cell open left path
                            board[newCell].status[3] = true;
                        }
                        //bottom
                        else
                        {
                            //current cell open bottom path
                            board[currentCellIndex].status[1] = true;
                            //new cell open top path
                            board[newCell].status[0] = true;
                        }
                    }
                    //top or left neighbor
                    //top: cell - size.x
                    //left: cell -1
                    else
                    {
                        //left
                        if ((newCell + 1) == currentCellIndex)
                        {
                            //current cell open left path
                            board[currentCellIndex].status[3] = true;
                            //new cell open right path
                            board[newCell].status[2] = true;
                        }
                        //top
                        else
                        {
                            //current cell open top path
                            board[currentCellIndex].status[0] = true;
                            //new cell open bottom path
                            board[newCell].status[1] = true;
                        }
                    }
                    
                    //set current cell = new cell
                    currentCellIndex = newCell;
                }
            }

            //start generate dungeon
            GeneratorDungeon();
        }

        List<int> NeighborCheck(int cellIndex)
        {
            List<int> neighbors = new List<int>();
            
            //Check surround neighbors
            
            /*               size.x
              *  i-1           i          i+1
               ********** ************ **********
            j |          |    cell    |          | 
            - |          |     -      |          | 
            1 |          |   size.x   |          | 
               ********** ************ **********
              |          |    cell    |          | 
            j | cell - 1 |(i+j*size.x)| cell + 1 |  size.y      
              |          |            |          | 
               ********** ************ **********
            j |          |    cell    |          | 
            + |          |     +      |          |
            1 |          |   size.x   |          | 
               ********** ************ **********
             */
            
            /*
             * Ex: size is vector2 (3,3)
             * Then Board will be 3x3 = 9 cell
             * Now, get the center cell, which mean the 2nd index of x and y of for loop
             * => 1 + 1 * 3 = 4, then it will be the 5th cell in board ( array index start with 0 => 5th mean 4 in array)
            **/

            bool topCellAvailable = cellIndex - size.x >= 0 && !board[Mathf.FloorToInt(cellIndex - size.x)].visited;
            bool downCellAvailable =
                cellIndex + size.x < board.Count && !board[Mathf.FloorToInt(cellIndex + size.x)].visited;
            bool rightCellAvailable = (cellIndex + 1) % size.x != 0 && !board[Mathf.FloorToInt(cellIndex + 1)].visited;
            bool leftCellAvailable = cellIndex % size.x != 0 && !board[Mathf.FloorToInt(cellIndex - 1)].visited;
            
            //up
            //check if there is available neighbor on top and not visited
            if (topCellAvailable)
            {
                neighbors.Add(Mathf.FloorToInt(cellIndex - size.x));
            }
            
            //down
            //check if there is available neighbor below and not visited
            if (downCellAvailable)
            {
                neighbors.Add(Mathf.FloorToInt(cellIndex + size.x));
            }
            
            //right
            //check if there is available neighbor on the right and not visited
            if (rightCellAvailable)
            {
                neighbors.Add(Mathf.FloorToInt(cellIndex + 1));
            }
            
            //left
            //check if there is available neighbor on the left and not visited
            if (leftCellAvailable)
            {
                neighbors.Add(Mathf.FloorToInt(cellIndex - 1));
            }
            
            return neighbors;
        }
    }
}

