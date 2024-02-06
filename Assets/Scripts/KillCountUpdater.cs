using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCountUpdater : MonoBehaviour
{
    Text killCountText;
    public int count = 0;
    void Start()
    {
        killCountText = GetComponent<Text>();
        Damageable.OnEnemyKill += UpdateKillCount;
    }

    void UpdateKillCount()
    {
        count++;
        killCountText.text = $"Enemies Killed: {count}";
    }
}
