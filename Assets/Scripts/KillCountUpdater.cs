using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KillCountUpdater : MonoBehaviour
{
    TextMeshProUGUI killCountText;
    public int count = 0;
    void Start()
    {
        killCountText = GetComponent<TextMeshProUGUI>();
        Damageable.OnEnemyKill += UpdateKillCount;
    }

    void UpdateKillCount()
    {
        count++;
        killCountText.text = $"Enemies Killed: {count}";
    }
}
