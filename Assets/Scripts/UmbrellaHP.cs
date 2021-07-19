using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaHP : MonoBehaviour
{
    // Start is called before the first frame update
    public float baseHP = 10;
    private float HP;

    private PlayerController playerController;

    public HealthBar healthBar;

    void Start()
    {
        HP = baseHP;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        healthBar.SetMaxHealth(baseHP);
    }

    public void doDamage(float dmg)
    {
        HP -= dmg;
        healthBar.SetHealth(HP);

        if (HP <= 0)
        {
            playerController.IsUmbrellaAvailable = false;
            playerController.getPlayerUmbrella().SetActive(false);
            HP = baseHP;    //initializing for future use
        }
    }


}
