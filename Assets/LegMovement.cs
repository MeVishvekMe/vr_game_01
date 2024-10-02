using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegMovement : MonoBehaviour
{

    [SerializeField] private Transform LeftRayCast;
    [SerializeField] private Transform RightRayCast;
    [SerializeField] private LayerMask mask;
     private Vector3 CurrentLeftLeg_P;
     private Vector3 CurrentRightLeg_P;
     private Vector3 UpcomingLeftLeg_P;
     private Vector3 UpcomingRightLeg_P;    

     //private Ray LeftRay;
     //private Ray RightRay;
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateCurrentPos()
    { 
        if (Physics.Raycast(LeftRayCast.position, Vector3.down, out RaycastHit LhitInfo, 10.0f,mask)) ;
        {
            CurrentLeftLeg_P = LhitInfo.point;
            Debug.DrawRay(LeftRayCast.position, Vector3.down,Color.red);
        }
        if (Physics.Raycast(RightRayCast.position, Vector3.down, out RaycastHit RhitInfo, 10.0f,mask))
        {
            CurrentRightLeg_P = RhitInfo.point;
            Debug.DrawRay(LeftRayCast.position, Vector3.down, Color.red);
        }
        
    }
}


