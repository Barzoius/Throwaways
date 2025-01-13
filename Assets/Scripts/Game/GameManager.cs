using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager sInstance;

    private static DeadMenu deadMenu;

    public TMP_Text textHP;


    private BossBehaviour boss;

    public static int currentHP;
    private static int maxHP = 10;

    private static float baseMS = 5.0f;
    public static float currentMS = baseMS;
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
            currentHP = 10;
            sInstance = this;
        }

    }


    void Update()
    {
        textHP.text = "HP: " + currentHP;
    }

    void Start()
    {
        boss = FindObjectOfType<BossBehaviour>();
        deadMenu = FindObjectOfType<DeadMenu>();
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
        deadMenu.ShowYouDiedMenu();
    }
    public static void WinGame()
    {
        deadMenu.ShowYouWonMenu();
    }

    public static void HealPlayer(int hp)
    {
        currentHP = Mathf.Min(maxHP, currentHP + hp);
    }

    public static void IncreaseMS(float bonusMS)
    {
        currentMS += bonusMS;
    }

    public static void UpdateMaxHP(int bonusHP)
    {
        if ((maxHP += bonusHP) > 50.0f)
            maxHP = 50;
        else
            maxHP += bonusHP;
    }
}
