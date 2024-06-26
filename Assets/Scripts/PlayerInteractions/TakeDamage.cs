using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// This Script is responseble for dealing damage, taking damage and every thing that comes when your health is lower than 1.
public class TakeDamage : MonoBehaviour
{
    [Header("Healt and Damage")]

    [SerializeField] private float playerHealth;
    [SerializeField] private float playerDamage;

    public bool isKnockedOut;

    [Header("joints")]

    [SerializeField] private ConfigurableJoint configurableJoint;



    [Header("Knockout")]

    public int knockoutCount;


    /// This void is called every frame.
    private void Update()
    {
        if (playerHealth <= 2f)
        {
            if(!isKnockedOut)
            {
                isKnockedOut= true;
                KnockOut();
            }
        }
    }


    /// makes the player go knockout. 
    private void KnockOut()
    {
        // gets the angularXDrive of the configureble joint.
        JointDrive angularXDrive = configurableJoint.angularXDrive;

        // Sets the position spring of the angularXDRive to 50.
        angularXDrive.positionSpring = 50f;

        // Sets the modified drive to the joint.
        configurableJoint.angularXDrive = angularXDrive;


        // Starts the knock out timer.
        StartCoroutine(KnockOutTime());
    }


    private void KnockOutDeath()
    {
        if (knockoutCount == 3)
        {
            //print(knockoutCount);
            Destroy(gameObject);
        }
    }


    /// This void is called every time there is a collision
    private void OnCollisionEnter(Collision collision)
    {
        // Checks if the collision was with a object that has the tag "HandHitBox".
        if (collision.gameObject.tag == "handHitBox" /*&& useArms.leftPunch == true || useArms.rightPunch == true*/)
        {
            playerHealth -= playerDamage;
        }


        // Checks if the collision was with a object that has the tag "NotThePlayErea".
        if(collision.gameObject.tag == "NotThePlayErea")
        {
            Destroy(gameObject);
        }
    }


    /// determens how long you will be knock out for and makes it so that the player gous knockout.
    private IEnumerator KnockOutTime()
    {
        // adds a knockout to the players knockout count
        knockoutCount ++;


        if(knockoutCount == 3)
        {
            KnockOutDeath();
            yield return null;
        }  

        else
        {
            // sets the time you are knock out for
            yield return new WaitForSeconds(10f);


            // gets the angularXDrive of the configureble joint.
            JointDrive angularXDrive = configurableJoint.angularXDrive;

            // Sets the position spring of the angularXDRive to 850.
            angularXDrive.positionSpring = 850f;

            // Sets the modified drive to the joint.
            configurableJoint.angularXDrive = angularXDrive;


            isKnockedOut = false;


            // Sets the players health back to 100.
            playerHealth = 100f;
        }
    }


}
