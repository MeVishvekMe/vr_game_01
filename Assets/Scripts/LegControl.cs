using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegControl : MonoBehaviour
{
    public Transform leftTarget; 
    public Transform rightTarget;
    public Transform Lray;  
    public Transform Rray; 
    public Transform Hips; 
 
    public float LegStepUp; 
 
    private Vector3 ShouldBeL;
    private Vector3 ShouldBeR;
 
    private Vector3 ShouldReallyBeL; 
    private Vector3 ShouldReallyBeR;
 
    public float DistL; 
    public float DistR;
 
    private bool LStepping; 
    private bool RStepping; 
 
    private string LastStep; 
 
    public float wantStepAt; 
 
    public float legSpeed; 
 
   
    void Start()
    {
        ShouldBeL = leftTarget.position; 
        ShouldBeR = rightTarget.position;
        LastStep = "R"; 
    }
 
   
    void Update()
    {
        RaycastHit hitL;
        if (Physics.Raycast(Lray.position, Vector3.down, out hitL)) 
        {
            ShouldReallyBeL = hitL.point;
        }
        RaycastHit hitR;
        if (Physics.Raycast(Rray.position, Vector3.down, out hitR)) 
        {
            ShouldReallyBeR = hitR.point;
        }
 
      
        DistL = Vector3.Distance(ShouldBeL, ShouldReallyBeL);
        DistR = Vector3.Distance(ShouldBeR, ShouldReallyBeR);
 
       
        leftTarget.position = Vector3.Lerp(leftTarget.position, ShouldBeL, legSpeed * Time.deltaTime);
        rightTarget.position = Vector3.Lerp(rightTarget.position, ShouldBeR, legSpeed * Time.deltaTime);
 
       
        if(!LStepping && !RStepping)
        {
            if(LastStep == "R")
            {
                if (DistL > wantStepAt)
                {
                    StartCoroutine(stepL());
                    LastStep = "L";
                }
            }
            else if(LastStep == "L")
            {
                if (DistR > wantStepAt)
                {
                    StartCoroutine(stepR());
                    LastStep = "R";
                }
            }
        }
 
   
        leftTarget.transform.eulerAngles = new Vector3(0, Hips.eulerAngles.y, 0);
        rightTarget.transform.eulerAngles = new Vector3(0, Hips.eulerAngles.y, 0);
 
    }
 
  
    IEnumerator stepL()
    {
        LStepping = true; 
        ShouldBeL = new Vector3(ShouldBeL.x, ShouldReallyBeL.y + LegStepUp, ShouldBeL.z); 
        yield return new WaitForSeconds(0.3f);
        ShouldBeL = ShouldReallyBeL; 
        yield return new WaitForSeconds(0.2f); 
        LStepping = false; 
    }
 
    IEnumerator stepR()
    {
        RStepping = true;
        ShouldBeR = new Vector3(ShouldBeR.x, ShouldReallyBeR.y + LegStepUp, ShouldBeR.z); 
        yield return new WaitForSeconds(0.3f);
        ShouldBeR = ShouldReallyBeR; 
        yield return new WaitForSeconds(0.2f); 
        RStepping = false; 
    }
}
