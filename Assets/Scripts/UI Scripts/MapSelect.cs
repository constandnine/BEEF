using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelect : MonoBehaviour
{
    [Header("Map")]

    [SerializeField] private GameObject[] Maps;

    [SerializeField] private int mapCount;
    [SerializeField] private int maximumMapCount;


    public void NextMap()
    {
        if(mapCount < maximumMapCount)
        {
            mapCount ++;


            Maps[mapCount].SetActive(true);

            Maps[mapCount - 1].SetActive(false);
        }
    }


    public void PreviusMap() 
    {
        if (mapCount > 0)
        {
            mapCount --;

            Maps[mapCount].SetActive(true);

            Maps[mapCount + 1].SetActive(false);
        }
    }
}
