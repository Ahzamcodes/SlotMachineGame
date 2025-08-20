using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class GameControl : MonoBehaviour
{
    public static event Action HandlePulled = delegate { };

    [SerializeField] private Row[] rows;
    [SerializeField] private Transform handle;
    [SerializeField] private Animator handleAnimator;


    [SerializeField] private BettingSystem bettingSystem; // link in Inspector

    private bool resultsChecked = true;

    void Update()
    {
        bool allStopped = rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped;
        bool allHaveResult = !string.IsNullOrEmpty(rows[0].stoppedSlot)
                          && !string.IsNullOrEmpty(rows[1].stoppedSlot)
                          && !string.IsNullOrEmpty(rows[2].stoppedSlot);

        if (allStopped && allHaveResult && !resultsChecked)
        {
            CheckResults();
        }
    }

    private void OnMouseDown()
    {
        // Only allow pull if all rows are stopped
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            StartCoroutine(PullHandle());
        }
    }

    private IEnumerator PullHandle()
    {
        // Check balance
        if (!bettingSystem.PlaceBet())
            yield break;

        resultsChecked = false;

        // play handle animation
        handleAnimator.SetBool("Handle", true);

        // wait until animation finishes
        yield return new WaitForSeconds(1f); //adjust to match anim length

        // reset animator state
        handleAnimator.SetBool("Handle", false);

        // now spin reels
        HandlePulled();
    }


    private void CheckResults()
    {
        string slot1 = rows[0].stoppedSlot;
        string slot2 = rows[1].stoppedSlot;
        string slot3 = rows[2].stoppedSlot;

        int winType = 0; // 0 = no win, 2 = two-of-a-kind, 3 = three-of-a-kind

        if (slot1 == slot2 && slot2 == slot3)
        {
            winType = 3;
        }
        else if (slot1 == slot2 || slot1 == slot3 || slot2 == slot3)
        {
            winType = 2;
        }

        bettingSystem.AwardPrize(winType);

        resultsChecked = true;
        Debug.Log("WinType: " + winType);
    }
}
