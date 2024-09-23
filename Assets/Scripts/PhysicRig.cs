using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicRig : MonoBehaviour {
    public GameObject head;
    public CapsuleCollider capsuleCollider;

    void Update() {
        capsuleCollider.height = head.transform.position.y;
    }
} 
