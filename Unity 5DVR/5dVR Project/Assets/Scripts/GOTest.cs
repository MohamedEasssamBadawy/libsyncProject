using System.Collections;
using System.Collections.Generic;
using UnityEditor.Analytics;
using UnityEngine;

public class GOTest : MonoBehaviour
{
    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update() {

        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");

        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;

        if (forwardPressed && velocityZ < currentMaxVelocity) {   
            velocityZ += Time.deltaTime * acceleration;
        }

        if (leftPressed && velocityX > -currentMaxVelocity) {
            velocityX -= Time.deltaTime * acceleration;
        }

        if (rightPressed && velocityX < currentMaxVelocity) {
            velocityX += Time.deltaTime * acceleration;
        }

        if(!forwardPressed && velocityZ > 0.0f) {
            velocityZ -= Time.deltaTime * deceleration;
        }
        
        if (!forwardPressed && velocityZ < 0.0f) {
            velocityZ = 0.0f;
        }

        if(!leftPressed && velocityX < 0.0f) {
            velocityX += Time.deltaTime * deceleration;
        }

        if (!rightPressed && velocityX > 0.0f) {
            velocityX -= Time.deltaTime * deceleration;
        }

        if(!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.5f && velocityX < 0.05f)) {
            velocityX = 0.0f;  
        }

        if(forwardPressed && runPressed && velocityZ > currentMaxVelocity) {
            velocityZ = currentMaxVelocity;
        }
        else if(forwardPressed && velocityZ > currentMaxVelocity) {
            velocityZ -= Time.deltaTime * deceleration;

            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f)) {
                velocityZ = currentMaxVelocity;
            }
        }
        else if(forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f)) {
            velocityZ = currentMaxVelocity;
        }

        animator.SetFloat("Velocity z", velocityZ);
        animator.SetFloat("Velocity x", velocityX);
    }
}


/*
 * 
 * 
 * 
 * 
 * 
 * Animator animator;
    int isWalkingHash;
    int isRunningHash;

    private void Start() {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("IsWalking");
        isRunningHash = Animator.StringToHash("IsRunning");
    }

    private void Update() {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        if (!isWalking && forwardPressed) {
            animator.SetBool(isWalkingHash, true);
        }

        if (isWalking && !forwardPressed) {
            animator.SetBool(isWalkingHash, false);
        }

        if (!isRunning && (forwardPressed && runPressed)) {
            animator.SetBool(isRunningHash, true);
        }

        if (isRunning && (!forwardPressed || !runPressed)) {
            animator.SetBool(isRunningHash, false);
        }
    }

*/