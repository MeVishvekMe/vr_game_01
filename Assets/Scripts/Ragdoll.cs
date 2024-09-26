using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private XRDirectInteractor Linteractor;
    [SerializeField] private XRDirectInteractor Rinteractor;
    [SerializeField] private Vector3 UpwardForce = new Vector3(0,2.0f,0);
    [SerializeField] private Rigidbody Spine;
    [SerializeField] private Rigidbody Head;
    [SerializeField] private Rigidbody LeftLeg;
    [SerializeField] private Rigidbody RightLeg;
    
    private bool IsGrabbed = false;
    private Rigidbody[] parts;
    private void Awake()
    {
        
        Linteractor.selectEntered.AddListener(Disable);
        Rinteractor.selectEntered.AddListener(Disable);
       // interactor.selectExited.AddListener(Enable);
    }
    

    private void Update()
    {
        if(!IsGrabbed)
        {
            ApplyForce();
        }
        
    }

    private void ApplyForce()
    {   
        Spine.AddForce(UpwardForce,ForceMode.Force);
        Head.AddForce(UpwardForce,ForceMode.Force);
        LeftLeg.AddForce(-UpwardForce,ForceMode.Force);
        RightLeg.AddForce(-UpwardForce,ForceMode.Force);
    }
    
    
    private void Disable(SelectEnterEventArgs arg0)
    {
        IsGrabbed = true;
       if(!arg0.interactableObject.transform.TryGetComponent(out XRGrabInteractable grabable)) return;
       Head.isKinematic = true;
    }
    private void Enable(SelectExitEventArgs arg0)
    {
        IsGrabbed = false;
        if(!arg0.interactableObject.transform.TryGetComponent(out XRGrabInteractable grabable)) return;
        Head.isKinematic = false;
    }
    
    
    
    
}
