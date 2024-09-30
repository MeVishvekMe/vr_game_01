using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterPhysicsScripty : MonoBehaviour {
    public Transform playerHead;
    //public CapsuleCollider bodyCollider;
    public Rigidbody rb;
    public float forceMag;
    public CharacterController controller;

    public float bodyMinHeight = 0.5f;
    public float bodyMaxHeight = 2f;

    private bool canJump = true;

    public InputActionReference jumpReference;
    


    private void OnDisable() {

        jumpReference.action.performed -= jump;
    }
    private void OnEnable() {
        jumpReference.action.performed += jump;
        
    }

    private void jump(InputAction.CallbackContext context) {
        Debug.Log(canJump);
        if(canJump) {
            rb.AddForce(new Vector3(0f, forceMag, 0f), ForceMode.Impulse);
        }
    }

    private void FixedUpdate() {
        controller.height = Mathf.Clamp(playerHead.localPosition.y, bodyMinHeight, bodyMaxHeight);
        controller.center = new Vector3(playerHead.localPosition.x, controller.height / 2, playerHead.localPosition.z);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Ground") {
            canJump = true;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if(collision.collider.tag == "Ground") {
            canJump = false;
        }
    }

    public void disableCharacterPhysics() {
        //controller.enabled = false;
    }

    public void enableCharacterPhysics() {
        //controller.enabled = true;
    }

}
