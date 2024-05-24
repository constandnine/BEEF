using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    //public UseArms useArms;

    [Header("Healt and Damage")]

    [SerializeField] private float playerHealth;
    [SerializeField] private float playerDamage;


    private void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        //If a punch is trown and hits someone it deals damage.
        if(collision.gameObject.tag == "handHitBox" /*&& useArms.leftPunch == true || useArms.rightPunch == true*/)
        {
            playerHealth -= playerDamage;
        }

        //Makes it so that if you are no longer on the playing erea you die.
        if(collision.gameObject.tag == "NotThePlayErea")
        {
            playerHealth = 0;
        }
    }

}
