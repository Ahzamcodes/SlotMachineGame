using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class GameControl : MonoBehaviour
{
    public static event Action HandlePulled = delegate { };

    [SerializeField]
    private TMP_Text prizeText;

    [SerializeField]
    private Row[] rows;

    [SerializeField]
    private Transform handle;

    private int prizeValue;
    private bool resultsChecked = true; // Start as true

    // Dictionaries to hold prize values for better management
    private Dictionary<string, int> prizeValueThreeOfAKind = new Dictionary<string, int>()
    {
        { "Diamond", 200 },
        { "Crown", 400 },
        { "Melon", 600 },
        { "Bar", 800 },
        { "Seven", 1500 },
        { "Cherry", 3000 },
        { "Lemon", 5000 }
    };

    private Dictionary<string, int> prizeValueTwoOfAKind = new Dictionary<string, int>()
    {
        { "Diamond", 100 },
        { "Crown", 300 },
        { "Melon", 500 },
        { "Bar", 700 },
        { "Seven", 1000 },
        { "Cherry", 2000 },
        { "Lemon", 4000 }
    };

    void Update()
    {
        bool allStopped = rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped;
        bool allHaveResult = !string.IsNullOrEmpty(rows[0].stoppedSlot)
                          && !string.IsNullOrEmpty(rows[1].stoppedSlot)
                          && !string.IsNullOrEmpty(rows[2].stoppedSlot);

        if (allStopped && allHaveResult && !resultsChecked)
        {
            CheckResults();
            prizeText.enabled = true;
            prizeText.text = "Prize: " + prizeValue;
        }
    }


    private void OnMouseDown()
    {
        // Only allow pulling the handle if all rows are stopped
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            StartCoroutine("PullHandle");
        }
    }

    private IEnumerator PullHandle()
    {
        if (!bettingSystem.PlaceBet())
            yield break; // stop if not enough balance
        prizeValue = 0;
        prizeText.enabled = false;

        // handle pre-rotation
        for (int i = 0; i > -22.418; i -=5)
        {
            handle.Rotate(0f, 0f, 0f);
            yield return new WaitForSeconds(0.1f);
        }

        // Start rows spinning first
        HandlePulled();

        // NOW allow results checking
        resultsChecked = false;

        // handle return rotation
        for (int i = 0; i > -22.418; i -= 5)
        {
            handle.Rotate(0f, 0f, 0f);
            yield return new WaitForSeconds(0.1f);
        }
    }


    private void CheckResults()
    {
        string slot1 = rows[0].stoppedSlot;
        string slot2 = rows[1].stoppedSlot;
        string slot3 = rows[2].stoppedSlot;

        // Check for three-of-a-kind first (highest priority)
        if (slot1 == slot2 && slot2 == slot3)
        {
            if (prizeValueThreeOfAKind.ContainsKey(slot1))
            {
                prizeValue = prizeValueThreeOfAKind[slot1];
            }
        }
        // If not three-of-a-kind, check for two-of-a-kind
        else if (slot1 == slot2 || slot1 == slot3)
        {
            if (prizeValueTwoOfAKind.ContainsKey(slot1))
            {
                prizeValue = prizeValueTwoOfAKind[slot1];
            }
        }
        else if (slot2 == slot3)
        {
            if (prizeValueTwoOfAKind.ContainsKey(slot2))
            {
                prizeValue = prizeValueTwoOfAKind[slot2];
            }
        }

        resultsChecked = true;
        Debug.Log("Final Prize: " + prizeValue);
    }
}
