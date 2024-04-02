using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterParent : MonoBehaviour
{
    private float aliveTime;
    public float lifespan = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - aliveTime > lifespan)
        {
            Destroy(gameObject);
        }
    }
}
