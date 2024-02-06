using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShotCountUpdater : MonoBehaviour
{
    Text shotCount;
    int count = 0;

    void Start()
    {
        Shoot.OnBulletShoot += UpdateBulletCount;
        shotCount = GetComponent<Text>();
    }

    void UpdateBulletCount()
    {
        count++;
        shotCount.text = $"Bullets Shot: {count}";
    }

}
