using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static float hungerStat;
    public static float sleepStat;
    public static float funStat;

    public static int hungerStatInt;
    public static int sleepStatInt;
    public static int funStatInt;


    [SerializeField] float timeToBeHunger = 100f;
    [SerializeField] float timeToBeSleep = 100f;
    [SerializeField] float timeToBeFun = 100f;

    private void Awake()
    {
        hungerStat = 100f;
        sleepStat = 100f;
        funStat = 100f;
    }

    private void Update()
    {
        HungerStat();
        SleepStat();
        FunStat();
    }

    private void HungerStat()
    {
        hungerStat = hungerStat - (Time.deltaTime * (timeToBeHunger / 10));
        hungerStatInt = Mathf.RoundToInt(hungerStat);

        if (hungerStatInt < 0)
        {
            hungerStatInt = 0;
        }
    }

    private void SleepStat()
    {
        sleepStat = sleepStat - (Time.deltaTime * (timeToBeSleep / 10));
        sleepStatInt = Mathf.RoundToInt(sleepStat);

        if (sleepStatInt < 0)
        {
            sleepStatInt = 0;
        }
    }

    private void FunStat()
    {
        {
            funStat = funStat - (Time.deltaTime * (timeToBeFun / 10));
            funStatInt = Mathf.RoundToInt(funStat);

            if (funStatInt < 0)
            {
                funStatInt = 0;
            }
        }
    }
}
