using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatView : MonoBehaviour
{
    public TextMeshProUGUI sleepView;
    public TextMeshProUGUI hungryView;
    public TextMeshProUGUI funView;

    private void Update()
    {
        sleepView.text = PlayerStat.sleepStatInt.ToString();
        hungryView.text = PlayerStat.hungerStatInt.ToString();
        funView.text = PlayerStat.funStatInt.ToString();
    }
}
