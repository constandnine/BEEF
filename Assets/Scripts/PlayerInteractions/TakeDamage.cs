using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// This Script is responseble for dealing damage, taking damage and every thing that comes when your health is lower than 1.
public class TakeDamage : MonoBehaviour
{
    [Header("Healt and Damage")]

    [SerializeField] private float playerHealth;
    [SerializeField] private float playerDamage;

    public bool isKnockedOut;
    public bool hasGameEnded;

    [Header("joints")]

    [SerializeField] private ConfigurableJoint configurableJoint;



    [Header("Knockout")]

    public int knockoutCount;

    [Header("Sounds")]
    [SerializeField] PlayerSounds sounds;

    [Header("Temp UI")]

    [SerializeField] private GameObject winScreenPlayer1;

    [SerializeField] private GameObject winScreenPlayer2;
    [SerializeField] private GameObject MainMenu;


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
        if(isKnockedOut == true)
        {
            sounds.source.clip = sounds.knockout;
            sounds.source.Play();
        }


        if (gameObject.tag == "Player1")
        {
            GetComponentInParent<PlaystationControllerMovement>().enabled = false;
            GetComponentInParent<WalkingAnimation>().enabled = false;
        }

        else if (gameObject.tag == "Player2")
        {
            GetComponentInParent<XboxControllerMovement>().enabled = false;
            GetComponentInParent<WalkingAnimation1>().enabled = false;
        }


        // gets the angularXDrive of the configureble joint.
        JointDrive angularXDrive = configurableJoint.angularXDrive;

        // Sets the position spring of the angularXDRive to 50.
        angularXDrive.positionSpring = 50f;

        // Sets the modified drive to the joint.
        configurableJoint.angularXDrive = angularXDrive;


        // Starts the knock out timer.
        StartCoroutine(KnockOutTime());
    }


    private void Player1Death()
    {
        hasGameEnded= true;

        winScreenPlayer2.SetActive(true);


        sounds.source.clip = sounds.win;
        sounds.source.Play();

        StartCoroutine(GoBackToMainMenu());
    }  
    

    private void Player2Death()
    {
        hasGameEnded= true;

        winScreenPlayer1.SetActive(true);

        sounds.source.clip = sounds.win;
        sounds.source.Play();

        StartCoroutine(GoBackToMainMenu());
    }


    /// This void is called every time there is a collision
    private void OnCollisionEnter(Collision collision)
    {
        // Checks if the collision was with a object that has the tag "HandHitBox".
        if (collision.gameObject.tag == "handHitBox" /*&& useArms.leftPunch == true || useArms.rightPunch == true*/)
        {
            if (!isKnockedOut)
            {
                sounds.source.clip = sounds.hit;
                sounds.source.Play();
            }

            playerHealth -= playerDamage;
        }


        // Checks if the collision was with a object that has the tag "NotThePlayErea".
        if(collision.gameObject.tag == "NotThePlayErea")
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Player1")
        {
            Player1Death();
        }

        else if (gameObject.tag == "Player2")
        {
            Player2Death();
        }
    }


    /// determens how long you will be knock out for and makes it so that the player gous knockout.
    private IEnumerator KnockOutTime()
    {
        // adds a knockout to the players knockout count
        knockoutCount ++;


        if(knockoutCount == 3 && gameObject.tag == "Player1")
        {
            Player1Death();
            yield return null;
        }  

        else if (knockoutCount == 3 && gameObject.tag == "Player2")
        {
            Player2Death();
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


            if (gameObject.tag == "Player1")
            {
                GetComponentInParent<PlaystationControllerMovement>().enabled = true;
                GetComponentInParent<WalkingAnimation>().enabled = true;
            }

            else if (gameObject.tag == "Player2")
            {
                GetComponentInParent<XboxControllerMovement>().enabled = true;
                GetComponentInParent<WalkingAnimation1>().enabled = true;
            }
        }
    }


    private IEnumerator GoBackToMainMenu()
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
