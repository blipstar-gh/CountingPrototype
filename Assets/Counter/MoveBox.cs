using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBox : MonoBehaviour
{
    float minX = 4.0f;
    float maxX = 6.0f;
    float minZ = -20;
    float maxZ = 16;
    Vector3 nextTarget;
    private float speed = 8.0f;
    private bool atTarget;

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

    // Update is called once per frame
    void Update()
    {
        if(!atTarget)transform.Translate((nextTarget-transform.position).normalized * speed * Time.deltaTime);
        if(Mathf.Abs(transform.position.x-nextTarget.x)<1 && Mathf.Abs(transform.position.z - nextTarget.z) < 1)
        {
            atTarget = true;
         }
    }

    public void CalculateTarget()
    {
        nextTarget = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        atTarget = false;
    }
}
