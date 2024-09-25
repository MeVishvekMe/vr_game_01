using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMechanics : MonoBehaviour {
    public Rigidbody rb;
    public Vector3 forceAmount;
    public InputActionProperty jumpAction;

    bool jump = false;

    void Update() {
        if(jumpAction.action.IsPressed() && !jump) {
            Debug.Log("Pressed");
            rb.AddForce(forceAmount * Time.deltaTime, ForceMode.Impulse);
            jump = true;
        }
        else {
            jump = false;
        }
        
    }
}
