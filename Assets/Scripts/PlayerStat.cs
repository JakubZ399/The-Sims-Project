using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    private float hungerStat;
    private float sleepStat;

    public static int hungerStatInt;
    public static int sleepStatInt;

    [SerializeField] float timeToBeHunger = 100f;
    [SerializeField] float timeToBeSleep = 100f;

    private void Awake()
    {
        hungerStat = 100f;
        sleepStat = 100f;
    }

    private void Update()
    {
        HungerStat();
        SleepStat();
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
}
