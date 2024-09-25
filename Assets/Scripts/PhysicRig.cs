using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicRig : MonoBehaviour {
    public Transform playerHead;
    public CapsuleCollider bodyCollider;

    public float bodyMinHeight = 0.5f;
    public float bodyMaxHeight = 2f;
    void FixedUpdate() {
        bodyCollider.height = Mathf.Clamp(playerHead.localPosition.y, bodyMinHeight, bodyMaxHeight);
        bodyCollider.center = new Vector3(playerHead.localPosition.x, bodyCollider.height / 2, playerHead.localPosition.z);
    }
} 
