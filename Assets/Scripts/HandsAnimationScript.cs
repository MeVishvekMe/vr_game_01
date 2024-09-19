using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandsAnimationScript : MonoBehaviour {
    public InputActionProperty pinchAction;
    public InputActionProperty grabAction;

    public Animator animator;
    void Update() {
        float pinchValue = pinchAction.action.ReadValue<float>();
        animator.SetFloat("Trigger", pinchValue);
        float grabValue = grabAction.action.ReadValue<float>();
        animator.SetFloat("Grip", grabValue);
    }
}
