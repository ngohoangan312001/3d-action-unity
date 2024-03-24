using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class RoomBehaviour : MonoBehaviour
    {
        //Register the wall of the room
        //Wall will be DEACTIVATE base on the status array parameter
        //ex: walls[top, left, right, bottom], status[true, false, false, true]
        // ==> the game object being registed at top and bottom in walls array will be DEACTIVATE  
        public GameObject[] walls;
        
        //Similar to Wall but will ACTIVE the door at DEACTIVATE wall position 
        public GameObject[] doors;
        
        [Header("Distance between room ")]
        [SerializeField] public Vector2 offSet;

        public void UpdateRoom(bool[] status)
        {
            int doorCount = doors.Length;
            int wallCount = walls.Length;
            for (int i = 0; i < status.Length; i++)
            {
                if (doorCount > i)
                {
                    doors[i].SetActive(status[i]);
                }

                if (wallCount > i)
                {
                    walls[i].SetActive(!status[i]);
                }
                
            }
        }
    }
}

