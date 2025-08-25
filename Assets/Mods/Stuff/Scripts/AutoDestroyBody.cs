using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyBody : MonoBehaviour
{

    public float destroyTime = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBody", destroyTime);
    }

    void DestroyBody()
    {
        Destroy(gameObject);
    }
}