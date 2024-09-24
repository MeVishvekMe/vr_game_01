using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class SmoothClimb : MonoBehaviour
{
    [SerializeField] private XRBaseInteractor interactor;
    [SerializeField] private GameObject PhysicRig;
    private Rigidbody Playerrigidbody;
    private Vector3 PlayerVelocity;
    [SerializeField] private float PushFactor = 1.5f;
    [SerializeField] private float distance = 5.0f;
    [SerializeField] private float speed = 4.0f;
    private void Awake()
    {   
        OnValidate();
        if (!PhysicRig)
        {
            Debug.Log("physicRig not found");
        }

        if (PhysicRig.TryGetComponent(out Rigidbody rb))
        {
            Playerrigidbody = rb;
        }
        interactor.selectEntered.AddListener(StopForce);
        interactor.selectExited.AddListener(PushUpward);
        
    }
    private void OnValidate()
    {
        if (!interactor)
            TryGetComponent(out interactor);
        
    }

    private void Update()
    {
        Debug.Log("player velocity"+ Playerrigidbody.velocity);
    }
    private void PushUpward(SelectExitEventArgs arg0)
    {
        if (!arg0.interactableObject.transform.TryGetComponent(out ClimbInteractable climbInteractable)) return;
            if (Playerrigidbody)
            {
                PlayerVelocity = Playerrigidbody.velocity;
                if (PlayerVelocity.y < 0)
                {
                    Vector3 force = new Vector3(0, PlayerVelocity.y * PushFactor, 0);
                   // Playerrigidbody.AddForce(force, ForceMode.Force);
                    PhysicRig.transform.position = Vector3.MoveTowards(PhysicRig.transform.position, PhysicRig.transform.position + new Vector3(0,distance,0), speed*Time.deltaTime);
                    Debug.Log(" force added ");
                }
            }
    }
    private void StopForce(SelectEnterEventArgs arg0)
    {   
        if (!arg0.interactableObject.transform.TryGetComponent(out ClimbInteractable climbInteractable)) return;
        if (Playerrigidbody)
        {
            //Playerrigidbody.velocity = Vector3.zero;
        }
    }
    private void OnDestroy()
    {
         interactor.selectEntered.RemoveListener(StopForce);
         interactor.selectExited.RemoveListener(PushUpward);
    }
}

