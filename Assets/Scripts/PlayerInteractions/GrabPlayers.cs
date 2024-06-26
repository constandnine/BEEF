using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPlayers : MonoBehaviour
{
    [Header("joints")]

    [SerializeField] private SpringJoint leftSpringJoint;
    [SerializeField] private SpringJoint rightSpringJoint;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }
    }
}
