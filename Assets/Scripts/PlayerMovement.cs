using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject playerCapsule;
    private Animator swordAnimator;

    public GameObject hitCoins;
    public int playerSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        playerCapsule = this.gameObject;
        playerCapsule.transform.position = new Vector2(3.5f, playerCapsule.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerCapsule.transform.position = new Vector2(-3.5f, playerCapsule.transform.position.y);
            
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            playerCapsule.transform.position = new Vector2(3.5f, playerCapsule.transform.position.y);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CameraShake.Instance.ShakeThat(0.5f, 0.1f);
            //Debug.Log("Space Pressed");
            if(playerCapsule.transform.position.x == -3.5f)
            {
                //Debug.Log("right attack");
                swordAnimator.SetTrigger("RightSideTrigger");
                GameManager.values.VillainHealth -= 10;
            }
            else if (playerCapsule.transform.position.x == 3.5f)
            {
                //Debug.Log("Left attack");
                swordAnimator.SetTrigger("LeftSideTrigger");
                GameManager.values.VillainHealth -= 10;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "villainTag")
        {
            GameManager.values.PlayerHealth -= 10;
            CameraShake.Instance.ShakeThat(2f, 0.1f);
        }
    }

    private void FixedUpdate()
    {
        //playerCapsule.velocity = new Vector2(horizontal * playerSpeed, playerCapsule.gameObject.transform.position.y);
    }
}
