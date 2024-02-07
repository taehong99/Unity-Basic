using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShotCountUpdater : MonoBehaviour
{
    TextMeshProUGUI shotCount;
    int count = 0;

    void Start()
    {
        Shoot.OnBulletShoot += UpdateBulletCount;
        shotCount = GetComponent<TextMeshProUGUI>();
    }

    void UpdateBulletCount()
    {
        count++;
        shotCount.text = $"Bullets Shot: {count}";
    }

}
