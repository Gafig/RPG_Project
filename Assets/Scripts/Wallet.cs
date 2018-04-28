using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour {

    public static Wallet instance;
    public int balance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        balance = 1000;
    }

    public bool validToSpend(int amount)
    {
        return balance >= amount;
    }

    public void spend(int amount)
    {
        balance -= amount;
    }

    public void add(int amount)
    {
        balance += amount;
    }
}
