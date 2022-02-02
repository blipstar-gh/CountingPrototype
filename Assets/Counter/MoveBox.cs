using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Move the target box to a random location
// (distance from player is based on their score, the higher the score, the further away)
//
public class MoveBox : MonoBehaviour
{
    // Bounds of possible target positions
    float minX = 4.0f;
    float maxX = 6.0f;
    float minZ = -20;
    float maxZ = 16;

    Vector3 nextTarget; // next target position

    private float speed = 8.0f; // speed of box
    private bool atTarget;  // whether box has reached target

    // Start is called before the first frame update
    void Start()
    {
        CalculateTarget();
    }

    public void SetXRange(float mnX,float mxX)
    {
        minX = mnX;
        maxX = mxX;
    }

    // Check whether box has reached target position
    void Update()
    {
        
        if(!atTarget)transform.Translate((nextTarget-transform.position).normalized * speed * Time.deltaTime);
        if(Mathf.Abs(transform.position.x-nextTarget.x)<1 && Mathf.Abs(transform.position.z - nextTarget.z) < 1)
        {
            atTarget = true;
         }
    }

    // Calculate a new random target position
    public void CalculateTarget()
    {
        nextTarget = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        atTarget = false;
    }
}
