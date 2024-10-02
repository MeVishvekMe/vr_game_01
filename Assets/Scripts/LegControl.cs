using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegControl : MonoBehaviour
{
    
    public Transform leftTarget; //the Ik target for the feft leg
    public Transform rightTarget; //the Ik target for the right leg
    public Transform Lray; //the position that a raycast will be sent from to determin where the left foot should go
    public Transform Rray; //the position that a raycast will be sent from to determin where the left foot should go
    public Transform Hips; //the hips or chest
 
    public float LegStepUp; //the level that your leg should go up before you step
 
    private Vector3 ShouldBeL; //where your ik target will always be trying to lerp to
    private Vector3 ShouldBeR; //where your ik target will always be trying to lerp to
 
    private Vector3 ShouldReallyBeL; //always where the raycast hits or where the foot would end up if it stepped
    private Vector3 ShouldReallyBeR;//always where the raycast hits or where the foot would end up if it stepped
 
    public float DistL; //distance from left foot to ShouldReallyBeL
    public float DistR; //distance from right foot to ShouldReallyBeR
 
    private bool LStepping; //true when the left leg is stepping
    private bool RStepping; //true when the right leg is stepping
 
    private string LastStep; //the last foot that stepped
 
    public float wantStepAt; //the distance that you will step at
 
    public float legSpeed; //lerping speed
 
    // Start is called before the first frame update
    void Start()
    {
        ShouldBeL = leftTarget.position; //just making it not look wierd at the start
        ShouldBeR = rightTarget.position; //just making it not look wierd at the start
        LastStep = "R"; //to make your left foot step first because left is good
    }
 
    // Update is called once per frame
    void Update()
    {
        RaycastHit hitL;
        if (Physics.Raycast(Lray.position, Vector3.down, out hitL)) //send a raycast for left foot placement
        {
            ShouldReallyBeL = hitL.point;
        }
        RaycastHit hitR;
        if (Physics.Raycast(Rray.position, Vector3.down, out hitR)) //send a raycast for right foot placement
        {
            ShouldReallyBeR = hitR.point;
        }
 
        // get the distances for the feet
        DistL = Vector3.Distance(ShouldBeL, ShouldReallyBeL);
        DistR = Vector3.Distance(ShouldBeR, ShouldReallyBeR);
 
        //make stepping smooth
        leftTarget.position = Vector3.Lerp(leftTarget.position, ShouldBeL, legSpeed * Time.deltaTime);
        rightTarget.position = Vector3.Lerp(rightTarget.position, ShouldBeR, legSpeed * Time.deltaTime);
 
        //figure out wich foot should take a step
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
 
        //make the legs y rotation look normal
        leftTarget.transform.eulerAngles = new Vector3(0, Hips.eulerAngles.y, 0);
        rightTarget.transform.eulerAngles = new Vector3(0, Hips.eulerAngles.y, 0);
 
    }
 
    //left foot stepping function
    IEnumerator stepL()
    {
        LStepping = true; //tell script that we are stepping
        ShouldBeL = new Vector3(ShouldBeL.x, ShouldReallyBeL.y + LegStepUp, ShouldBeL.z); //make foot go up
        yield return new WaitForSeconds(0.3f);
        ShouldBeL = ShouldReallyBeL; //place foot in correct position
        yield return new WaitForSeconds(0.2f); //some extra time so john doesn't step imediatly after
        LStepping = false; //tell script that we have finished stepping
    }
 
    IEnumerator stepR()
    {
        RStepping = true; //tell script that we are stepping
        ShouldBeR = new Vector3(ShouldBeR.x, ShouldReallyBeR.y + LegStepUp, ShouldBeR.z); //make foot go up
        yield return new WaitForSeconds(0.3f);
        ShouldBeR = ShouldReallyBeR; //place foot in correct position
        yield return new WaitForSeconds(0.2f); //some extra time so john doesn't step imediatly after
        RStepping = false; //tell script that we have finished stepping
    }
    
}
