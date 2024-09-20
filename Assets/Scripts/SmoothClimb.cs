using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SmoothClimb : ClimbProvider
{
    [SerializeField] private XRBaseInteractor Interactor;
    protected override void Awake()
    {
        
    }

    private void startClimbing(SelectEnterEventArgs arg0)
    {
        
    }

    private void OnValidate()
    {
        if (!Interactor)
            TryGetComponent(out Interactor);
    }
}
