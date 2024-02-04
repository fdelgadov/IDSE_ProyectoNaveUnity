using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AllScores : MonoBehaviour
{
    [SerializeField] TMP_Text allScores;
    // Start is called before the first frame update
    void Start()
    {
        int[] scores = SharedData.instance.scores;
        string txt = $"Score\n" +
            $"Level 1: {scores[0]}\n" +
            $"Level 2: {scores[1]}\n" +
            $"Level 3: {scores[2]}\n" +
            $"Level 4: {scores[3]}\n" +
            $"Level 5: {scores[4]}\n";
        allScores.text = txt;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
