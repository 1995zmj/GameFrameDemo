using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{

    public Vector3 power = Vector3.zero;
    public float offset = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 r = Random.onUnitSphere;
            r *= offset;
            r.y += power.y;
            transform.GetComponent<Rigidbody>().AddForce(r,ForceMode.Impulse);
        }
    }
}
