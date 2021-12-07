using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;

    private Rigidbody playerRigidBody;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private int maxJumpTimes;

    private int superJumpsRemaining;


    // Start is called before the first frame update
    void Start()
    {
        maxJumpTimes =0;
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && maxJumpTimes<(1 + superJumpsRemaining))
        {
            maxJumpTimes++;
            Debug.Log(maxJumpTimes);
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        
    }

    private void FixedUpdate()
    {

        playerRigidBody.velocity = new Vector3(horizontalInput, playerRigidBody.velocity.y, 0);
        //if(Physics.OverlapSphere(groundCheckTransform.position,0.1f).Length==1)
        {
           // Debug.Log($"cllision count is {Physics.OverlapSphere(groundCheckTransform.position, 0.1f,playerMask).Length}");
        }

        if (jumpKeyWasPressed)
        {
            float jumpower = 5f;
            if(superJumpsRemaining >0)
            {
                jumpower = jumpower +3;
                superJumpsRemaining--;
            }
            playerRigidBody.AddForce(Vector3.up * jumpower, ForceMode.VelocityChange);
            jumpKeyWasPressed =false;


            Debug.Log("Fixed update" + maxJumpTimes);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        maxJumpTimes =0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
            Debug.Log("supper jumps " + superJumpsRemaining);
        }
    }
}
