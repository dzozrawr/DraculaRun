using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaHP : MonoBehaviour
{

    public float baseHP = 10;
    private float HP = 0;
    private float maxHP = 0;

    private PlayerController playerController;


    public GameObject umbrellaModel;
    private Material umbrellaMaterial;

    private float maxR, maxG, maxB;

    private float hpPercent;

    void Awake()
    {
        umbrellaMaterial = umbrellaModel.GetComponent<Renderer>().material;

        maxR = umbrellaMaterial.GetColor("_Color").r;
        maxG = umbrellaMaterial.GetColor("_Color").g;
        maxB = umbrellaMaterial.GetColor("_Color").b;

        maxHP = baseHP;
    }

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void addHP() //null reference exception negde ovde
    {
        HP += baseHP;   //adding baseHP to umbrella 
        if (HP >= baseHP)
        {
            maxHP = (((int)HP / (int)baseHP) + 1) * baseHP;
        }

        hpPercent = HP / maxHP;
        if(gameObject.activeSelf) umbrellaMaterial.SetColor("_Color", new Color(hpPercent * maxR, hpPercent * maxG, hpPercent * maxB));   //the umbrella darkens based on how much HP it has
    }

    public void doDamage(float dmg)
    {
        HP -= dmg;  //damage from the sun

        hpPercent = HP / maxHP;
        umbrellaMaterial.SetColor("_Color", new Color(hpPercent * maxR, hpPercent * maxG, hpPercent * maxB));   //the umbrella darkens based on how much HP it has

        if (HP <= 0)
        {
            playerController.IsUmbrellaAvailable = false;
            playerController.getPlayerUmbrella().SetActive(false);
            HP = baseHP;    //initializing for future use
        }
    }


}