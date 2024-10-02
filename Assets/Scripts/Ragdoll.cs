using System;
using System.Collections;
using System.Collections.Generic;
<<<<<<< Updated upstream
using Unity.VisualScripting;
=======
using System.Linq;
>>>>>>> Stashed changes
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private XRDirectInteractor Linteractor;
    [SerializeField] private XRDirectInteractor Rinteractor;
   // [SerializeField] private Vector3 UpwardForce = new Vector3(0,2.0f,0);
    //[SerializeField] private Rigidbody Spine;
    [SerializeField] private Rigidbody Head;
<<<<<<< Updated upstream
    [SerializeField] private Rigidbody LeftLeg;
    [SerializeField] private Rigidbody RightLeg;
    
    private bool IsGrabbed = false;
    private Rigidbody[] parts;

    //public Collider spherecollider;
=======
    //[SerializeField] private Rigidbody LeftLeg;
    //[SerializeField] private Rigidbody RightLeg;
    [SerializeField] private GameObject rig;
    private static bool isGrabbed = false;
    private List<Rigidbody> parts = new List<Rigidbody>();
>>>>>>> Stashed changes
    private void Awake()
    {
        
        Linteractor.selectEntered.AddListener(DisableRagdoll);
        Rinteractor.selectEntered.AddListener(DisableRagdoll);
        
        Linteractor.selectExited.AddListener(EnableRagdoll);
        Rinteractor.selectExited.AddListener(EnableRagdoll);
        GetRigidbodies(rig);
    }
    
    /*private void ApplyForce()
    {   
        Spine.AddForce(UpwardForce,ForceMode.Force);
        Head.AddForce(UpwardForce,ForceMode.Force);
        //LeftLeg.AddForce(-UpwardForce/2,ForceMode.Force);
        //RightLeg.AddForce(-UpwardForce/2,ForceMode.Force);
    }*/
    
    
    private void DisableRagdoll(SelectEnterEventArgs arg0)
    {
        isGrabbed = true;
       if(!arg0.interactableObject.transform.TryGetComponent(out XRGrabInteractable grabable)) return;
       Head.isKinematic = true;
       SetPartsKinematic(true);

    }
    private void EnableRagdoll(SelectExitEventArgs arg0)
    {
        isGrabbed = false;
        if(!arg0.interactableObject.transform.TryGetComponent(out XRGrabInteractable grabable)) return;
        SetPartsKinematic(false);
    }

    private void GetRigidbodies(GameObject parent)
    {
        if (parent == null) return;
        
        foreach (Transform  child in parent.transform)
        {
            if(child == null) continue;
            Rigidbody rb = child.gameObject.GetComponent<Rigidbody>();
            if(rb!=null) parts.Add(rb);
            GetRigidbodies(child.gameObject);
            
        }
    }
    
<<<<<<< Updated upstream
    
    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider == spherecollider)
        {
            Debug.Log("colliding");
            return;
        }
    }*/
    
=======
    private void SetPartsKinematic(bool isKinematic)
    {
        foreach (Rigidbody rb in parts)
        {
            if (rb != null)
                rb.isKinematic = isKinematic;
        }
    }
>>>>>>> Stashed changes
}
