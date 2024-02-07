using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// MVC (View)
public class KillCountUI : MonoBehaviour
{
    private TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        Manager.Data.OnKillCountChanged += UpdateKillCount;
    }
    private void OnDisable()
    {
        Manager.Data.OnKillCountChanged -= UpdateKillCount;
    }
    public void UpdateKillCount(int value)
    {
        text.text = $"Enemies Killed: {value}";
    }
}
