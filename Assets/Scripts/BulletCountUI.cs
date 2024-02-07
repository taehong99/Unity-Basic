using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// MVC (View)
public class BulletCountUI : MonoBehaviour
{
    TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        Manager.Data.OnFireCountChanged += UpdateBulletCount;
    }
    private void OnDisable()
    {
        Manager.Data.OnFireCountChanged -= UpdateBulletCount;
    }

    void UpdateBulletCount(int value)
    {
        text.text = $"Bullets Shot: {value}";
    }

}
