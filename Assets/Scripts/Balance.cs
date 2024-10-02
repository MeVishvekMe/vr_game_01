    using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Balance : MonoBehaviour
{
    
    
    [SerializeField] private GameObject Body;
    [SerializeField] private float uprightjointspringstrength;
    [SerializeField] private float uprightjointspringdamper;

    [SerializeField] private GameObject raycast;
    [SerializeField] private ForceMode mode;
    [SerializeField] private float raycast_distance;
    [SerializeField] private float forcestrength = 50.0f;
    [SerializeField] private float springdamper = 5.0f;
    
    private Rigidbody _rigidbody;
    private Vector3 targetdirection = Vector3.up;
    private Quaternion playerrotation;
    
    private void Awake()
    {
        if (Body.TryGetComponent(out Rigidbody rb))
        {
            _rigidbody = rb;
        }
        return;
        

    }

    // Update is called once per frame
    void Update()
    {
        AddTorque();
        FloatCapsule();
    }

    private void AddTorque()
    {
            playerrotation = Body.transform.rotation;
            Quaternion uprightRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up); 
            Quaternion toGoal = GetShortestRotation(playerrotation, uprightRotation);

            Vector3 rotAxis;
            float rotDegrees;
            
            toGoal.ToAngleAxis(out rotDegrees,out rotAxis);
            rotAxis.Normalize();


            float rotRadians = rotDegrees * Mathf.Deg2Rad;
            
            _rigidbody.AddTorque((rotAxis*(rotRadians*uprightjointspringstrength)) - (_rigidbody.angularVelocity*uprightjointspringdamper));
    }

    private void FloatCapsule()
    {
        bool ishitting = Physics.Raycast(raycast.transform.position, Vector3.down, out RaycastHit hitinfo, raycast_distance);
        if (ishitting)
        {
            Debug.DrawRay(raycast.transform.position, Vector3.down,Color.red);
            float height = hitinfo.distance;
            
           // Debug.Log("height : " + height);
            float springForce = forcestrength / height;
            
            float dampingForce = -_rigidbody.velocity.y * springdamper;

            _rigidbody.AddForce(Vector3.up * (springForce + dampingForce), mode);
        }
        
    }
    public static Quaternion GetShortestRotation(Quaternion from, Quaternion to)
    {
        if (Quaternion.Dot(from, to) < 0.0f)
        {
            to = new Quaternion(-to.x, -to.y, -to.z, -to.w);
        }
        Quaternion shortestRotation = to * Quaternion.Inverse(from);

        return shortestRotation;
    }
}
