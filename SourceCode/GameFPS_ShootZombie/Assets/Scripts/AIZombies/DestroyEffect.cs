using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public float timeToDestroy = 2f;
    // Start is called before the first frame update
    public void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

   
}
