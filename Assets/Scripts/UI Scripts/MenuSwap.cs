using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwap : MonoBehaviour
{

    public void MenuDisable(GameObject menuToDisable)
    {
        menuToDisable.SetActive(false);
    }

    public void MenuEnable(GameObject menuToEnable)
    {
        menuToEnable.SetActive(true);
    }
}
