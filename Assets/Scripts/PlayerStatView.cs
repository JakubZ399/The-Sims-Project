using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hungerBar;
    [SerializeField] private TextMeshProUGUI sleepBar;

    private void Update()
    {
        hungerBar.text = "Hunger: " + PlayerStat.hungerStatInt.ToString();
        sleepBar.text = "Sleep: " + PlayerStat.sleepStatInt.ToString();
    }
}
