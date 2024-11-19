using UnityEngine;
using System.Collections;

public class sctCharRot : MonoBehaviour
{

    public float speed = 1.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(Vector3.up * speed);
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sctCharRot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/