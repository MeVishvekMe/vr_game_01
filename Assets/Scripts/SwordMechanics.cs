using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMechanics : MonoBehaviour {
    public Transform swordPoint;

    public Boolean isGrabbed = false;

    void Update() {
        if(!isGrabbed) {
            transform.position = swordPoint.position;
            transform.rotation = swordPoint.rotation;
        }
        Debug.Log(isGrabbed);
    }

    public void setGrabbedTrue() {
        isGrabbed = true;
    }

    public void setGrabbedFalse() {
        isGrabbed = false;
    }

    
}
