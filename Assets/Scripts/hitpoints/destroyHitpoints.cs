using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyHitpoints : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, gameObject.GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
