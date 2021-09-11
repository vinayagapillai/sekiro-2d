using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private GameObject playerCapsule;
    private Animator m_animator;
    private int m_currentAttack = 0;
    private float random;
    private Vector2 startTouch, endTouch;

    public GameObject hitCoin;


    // Start is called before the first frame update
    void Start()
    {
        playerCapsule = this.gameObject;
        playerCapsule.transform.position = new Vector2(-3.5f, playerCapsule.transform.position.y);
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        mobileInput();
        viewDirection();

        if (Input.GetKeyDown(KeyCode.A) || (startTouch.x - endTouch.x) > 100f)
        {
            playerCapsule.transform.position = new Vector2(-3.5f, playerCapsule.transform.position.y);

        }

        if (Input.GetKeyDown(KeyCode.D) || (endTouch.x - startTouch.x) > 100f)
        {
            playerCapsule.transform.position = new Vector2(3.5f, playerCapsule.transform.position.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (!GameManager.values.Shop.activeSelf)
            {
                GameManager.values.coins++;
                CameraShake.Instance.ShakeThat(0.5f, 0.1f);

                random = UnityEngine.Random.Range(1.3f, 1.6f);
                if (playerCapsule.transform.position.x == 3.5f)
                {
                    Instantiate(hitCoin, new Vector3(random, -2, -1f), Quaternion.identity);
                }
                else
                {
                    Instantiate(hitCoin, new Vector3(-random, -2, -1f), Quaternion.identity);
                }

                m_currentAttack++;

                // Loop back to one after third attack
                if (m_currentAttack > 3)
                    m_currentAttack = 1;

                // Call one of three attack animations "Attack1", "Attack2", "Attack3"
                m_animator.SetTrigger("Attack" + m_currentAttack);

                GameManager.values.VillainHealth -= 10;

                //Debug.Log("Space Pressed");
            }
        }
    }

    private void mobileInput()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouch = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouch = Input.GetTouch(0).position;
        }
    }

    private void viewDirection()
    {
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0 || (endTouch.x - startTouch.x) > 100f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        else if (inputX < 0 || (startTouch.x - endTouch.x) > 100f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "villainTag")
        {
            CameraShake.Instance.ShakeThat(2f, 0.1f);
            GameManager.values.reducePlayerHealth();
        }
    }

}
