using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using System.Linq;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager values;
    
    
    
    public GameObject MOBILE;
    public CinemachineVirtualCamera virtualCamera;
    public Transform playerHealthParent;
    public TextMeshProUGUI coinText;
    public GameObject coinMinusAnimation;
    public GameObject heartMinusAnimation;
    public GameObject deathMinusAnimation;
    public GameObject Shop;

    public List<Transform> playerHealthIcons;

    public int PlayerHealth;
    public int VillainHealth = 1000;
    public int coins;
    private bool canDamage;

    //THESE SHOULD BE DIABLED AT MAINMENU
    public GameObject titanButtonUI;
    public GameObject shopButtonUI;
    public GameObject healthUI;
    public GameObject CoinCounterUI;
    public GameObject playerCapsule;
    public GameObject explationUI;
    public GameObject titleUI;
    public GameObject tapUI;
    public bool gameStart;


    private void Awake()
    {
        values = this;

        pauseGame();
        gameStart = false;
        Shop.SetActive(false);
        canDamage = true;
        playerHealthIcons = new List<Transform>();
        PlayerHealth = playerHealthParent.transform.childCount;
        for(int i = 0; i < PlayerHealth; i++)
        {
            playerHealthIcons.Add(playerHealthParent.transform.GetChild(i));
        }
    }

    private void Start()
    {
        if(MOBILE.activeSelf)
        {
            virtualCamera.transform.position = new Vector3(virtualCamera.transform.position.x, 1.5f, -14f);
            virtualCamera.m_Lens.OrthographicSize = 8;
        }
    }

    private void pauseGame()
    {
        titanButtonUI.gameObject.SetActive(false);
        shopButtonUI.gameObject.SetActive(false);
        healthUI.gameObject.SetActive(false);
        CoinCounterUI.gameObject.SetActive(false);
        playerCapsule.gameObject.SetActive(false);
        explationUI.gameObject.SetActive(true);
        titleUI.gameObject.SetActive(true);
        tapUI.gameObject.SetActive(true);
    }

    private void resumeGame()
    {
        gameStart = true;
        titanButtonUI.gameObject.SetActive(true);
        shopButtonUI.gameObject.SetActive(true);
        healthUI.gameObject.SetActive(true);
        CoinCounterUI.gameObject.SetActive(true);
        playerCapsule.gameObject.SetActive(true);
        explationUI.gameObject.SetActive(false);
        titleUI.gameObject.SetActive(false);
        tapUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameStart == false)
        {
            resumeGame();
        }

        coinText.SetText(coins.ToString());
    }

    public void reducePlayerHealth()
    {
        if (PlayerHealth > 0 && canDamage)
        {
            PlayerDamage();
            playerHealthIcons[PlayerHealth].gameObject.SetActive(false);
        }

        if (PlayerHealth == 0)
        {
            PlayerDead();
        }
    }

    private void PlayerDead()
    {
        //reset heart
        PlayerHealth = 2;
        for(int i = 0;  i < PlayerHealth; i++)
        {
            playerHealthIcons[i].gameObject.SetActive(true);
        }

        //reset Heart animation
        FloatingAnimation(heartMinusAnimation);

        //negative coin amination
        coinMinusAnimation.GetComponent<TextMeshProUGUI>().SetText("-" + coinText.text);
        FloatingAnimation(coinMinusAnimation);
        coins = 0;

        //dead animation
        FloatingAnimation(deathMinusAnimation);
    }

    private void FloatingAnimation(GameObject floatingAnimation) 
    {
        floatingAnimation.SetActive(true);
        floatingAnimation.GetComponent<Animator>().Play("minus");
        StartCoroutine(disableFloatingText(floatingAnimation, floatingAnimation.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length));
    }

    public void PlayerDamage()
    {
        canDamage = false;
        StartCoroutine(DamageNow());
    }

    IEnumerator DamageNow()
    {
        PlayerHealth--;
        yield return new WaitForSeconds(1f);
        canDamage = true;
    }

    IEnumerator disableFloatingText(GameObject floatingText, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        floatingText.SetActive(false);
    }

    public void TitanTab()
    {
        Time.timeScale = 1;
        virtualCamera.transform.position = new Vector3(0, 1.5f, -14f);
        Shop.SetActive(false);
    }

    public void ShopTab()
    {
        Time.timeScale = 0;
        virtualCamera.transform.position = new Vector3(50f, 1.5f, -14f);
        Shop.SetActive(true);
    }

    public void addExtraLife()
    {
        if (PlayerHealth < 3 && coins >= 20)
        {
            coins -= 20;
            PlayerHealth++;
            for (int i = 0; i < PlayerHealth; i++)
            {
                playerHealthIcons[i].gameObject.SetActive(true);
            }
        }
    }


}
