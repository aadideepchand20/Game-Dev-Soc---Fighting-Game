using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{

    [HideInInspector]
    public Vector3 playerPosition;
    [HideInInspector]
    public Vector2 movementInput;
    //public Transform groundCheck;

    //public float fallSpeed;
    public float crouchShrinkFactor;
    public float jumpTime;
    public Vector2 inputMultiplier;

    public bool isCrouching = false;
    public bool isJumping = false;

    public Animator animator;

    private float timeJumped = 0;


    // Start is called before the first frame update
    void Start()
    {
        playerPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = gameObject.transform.position;

        //Set Input Values
        if(Input.GetKeyDown(KeyCode.D)) {
            movementInput.x = 1;
        } else if(Input.GetKeyDown(KeyCode.A)) {
            movementInput.x = -1;
        } else {
            movementInput.x = 0;
        }
        if(Input.GetKeyDown(KeyCode.W)) {
            movementInput.y = 1;
        } else if(Input.GetKey(KeyCode.S)) {
            movementInput.y = -1;
        } else {
            movementInput.y = 0;
        }

        if(!isJumping) {
            if(movementInput.y < 0) {
                if(!isCrouching) {
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y/crouchShrinkFactor,transform.localScale.z);
                    gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y/2), transform.position.z);
                    isCrouching = true;
                    playerPosition = gameObject.transform.position;
                }
            } else if(movementInput.y == 0){
                if(isCrouching) {
                    gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + (transform.localScale.y/2), transform.position.z);
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * crouchShrinkFactor,transform.localScale.z);
                    isCrouching = false;
                    playerPosition = gameObject.transform.position;
                }
            } else if(movementInput.y > 0) {
                if(!isCrouching) {
                    isJumping = true;
                    timeJumped = 0;
                    gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + (transform.localScale.y/2), transform.position.z);
                    playerPosition = gameObject.transform.position;
                }
            }
        } else {
            timeJumped++;
            if(timeJumped > jumpTime) {
                isJumping = false;
                gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y/2), transform.position.z);
                playerPosition = gameObject.transform.position;
            }
        }
        animator.SetFloat("xMove", movementInput.x * inputMultiplier.x);
        playerPosition += new Vector3(movementInput.x * inputMultiplier.x, 0,0);
        gameObject.transform.position = playerPosition;
    }
}
