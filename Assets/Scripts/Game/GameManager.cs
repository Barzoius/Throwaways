using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager sInstance;
    
    public TMP_Text textHP;

    public static int currentHP = 10;
    private static int maxHP = 10;

    private static float baseMS = 5.0f;
    private static float currentMS = 5.0f;
    private static float maxMS;
    private static float minMS;

    private static float baseFireRate = 0.5f;
    private static float currentFireRate = 0.5f;
    private static float maxFireRate = 3.0f;
    private static float minFireRate = 0.25f;

    public static int HitPoints { get => currentHP; set => currentHP = value; }

    public static int MaxHP { get => maxHP; set => maxHP = value; }

    public static float CurrentMS { get => currentHP; set => currentMS = value; }

    public static float CurrentFireRate { get => currentFireRate; set => currentFireRate = value; }


    public void Awake()
    {
        if (sInstance == null)
        {
            sInstance = this;
        }
    }


    void Update()
    {
        textHP.text = "HP: " + currentHP;
    }


    public static void DamagePlayer(int dmg)
    {
        currentHP -= dmg;

        if(HitPoints <= 0)
        {
            KillPlayer();
        }

    }

    private static void KillPlayer()
    {

    }

    public static void HealPlayer(int hp)
    {
        currentHP = Mathf.Min(maxHP, currentHP + hp);
    }
}
