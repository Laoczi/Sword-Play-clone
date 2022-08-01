using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public static Wallet singleton;
    public float count { get; private set; }

    private void Awake()
    {
        if (singleton == null) singleton = this;
        else Destroy(this.gameObject);

        count = 0;

        if (PlayerPrefs.HasKey("Money")) count = PlayerPrefs.GetFloat("Money");
        else PlayerPrefs.SetFloat("Money", 0);
    }

    public void Add(float value)
    {
        if (value > 0)
        {
            count += value;
            PlayerPrefs.SetFloat("Money", count);
        }
    }
    public bool Get(float value)
    {
        if (value >= 0 && value <= count)
        {
            count -= value;
            PlayerPrefs.SetFloat("Money", count);
            return true;
        }
        else
        {
            return false;
        }
    }
}
