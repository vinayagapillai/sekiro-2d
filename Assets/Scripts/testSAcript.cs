using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSAcript : MonoBehaviour
{

    private Animator villainAnimator;
    public GameObject playerCapsule;
    private bool playerIsLeft = false;
    private bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        villainAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerTracker();
        if (playerIsLeft && canAttack)
        {
            villainAnimator.SetTrigger("left");
            canAttack = false;
            StartCoroutine(attackWaitTime());
        }

        else if (!playerIsLeft && canAttack)
        {
            villainAnimator.SetTrigger("right");
            canAttack = false;
            StartCoroutine(attackWaitTime());
        }
    }

    private void playerTracker()
    {
        if (playerCapsule.transform.position.x == -3.5f)
        {
            //Debug.Log("Im Left");
            playerIsLeft = true;
        }
        else
        {
            //Debug.Log("Im right");
            playerIsLeft = false;
        }
    }


    IEnumerator attackWaitTime()
    {
        villainAnimator.SetTrigger("idle");
        yield return new WaitForSeconds(4);
        canAttack = true;
    }
}
