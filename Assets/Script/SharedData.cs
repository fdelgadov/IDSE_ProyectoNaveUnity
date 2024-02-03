using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedData : MonoBehaviour
{
    public static SharedData instance;
    public int[] scores = new int[5];
    public int currentLevel = 0;

    void Start()
    {
        instance = this;
    }
}
