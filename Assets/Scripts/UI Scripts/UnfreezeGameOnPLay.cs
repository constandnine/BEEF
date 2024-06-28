using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnfreezeGameOnPLay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    public void Play() 
    {
        Time.timeScale = 1.0f;
    }
}
