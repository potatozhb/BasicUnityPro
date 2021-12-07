using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody playerRigidBody;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private int maxJumpTimes;

    // Start is called before the first frame update
    void Start()
    {
        maxJumpTimes =0;
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && maxJumpTimes<2)
        {
            maxJumpTimes++;
            Debug.Log(maxJumpTimes);
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        
    }

    private void FixedUpdate()
    {
        if (jumpKeyWasPressed)
        {
            playerRigidBody.AddForce(Vector3.up *5, ForceMode.VelocityChange);
            jumpKeyWasPressed =false;


            Debug.Log("Fixed update" + maxJumpTimes);
        }

        playerRigidBody.velocity = new Vector3(horizontalInput, playerRigidBody.velocity.y, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
            maxJumpTimes =0;
    }
}
