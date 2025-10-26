using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Monsternumbermanager : MonoBehaviour
{
    public GameObject Core;
    public GameObject magicBook;
    public TextMeshProUGUI enemyCountText;
    public int enemyThreshold = 5;

    void Update()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCountText != null)
            enemyCountText.text = $"Enemy Count: {enemyCount}";

        if (enemyCount <= enemyThreshold && Core == null)
            magicBook.SetActive(true);
    }
}

