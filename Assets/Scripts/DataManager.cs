using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

// MVC (Model), Observer
public class DataManager : MonoBehaviour
{
    private int fireCount;
    private int killCount;
    public UnityAction<int> OnFireCountChanged;
    public UnityAction<int> OnKillCountChanged;

    public int FireCount
    {
        get { return fireCount; }
        set {
            fireCount = value;
            OnFireCountChanged?.Invoke(value);
        }
    }

    public int KillCount
    {
        get { return killCount; }
        set
        {
            killCount = value;
            OnKillCountChanged?.Invoke(value);
        }
    }
}

