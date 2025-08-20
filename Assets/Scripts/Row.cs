using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;

public class Row : MonoBehaviour
{
    private float timeInterval;

    public bool rowStopped;
    public string stoppedSlot;

    // Array of all valid Y-positions for the symbols to stop on.
    // This is the key to the fix!
    // Array of all valid Y-positions for the symbols to stop on.
    private readonly float[] stopPositions = {-7.68f, -6.12f,-4.54f,-3.08f,-1.6f,-0.09f,1.59f,2.97f};



    void Start()
    {
        rowStopped = true;
        GameControl.HandlePulled += StartRotating;
    }

    private void StartRotating()
    {
        stoppedSlot = "";
        StartCoroutine("Rotate");
    }

    private IEnumerator Rotate()
    {
        rowStopped = false;
        timeInterval = 0.025f;

        // 1. Decide the result BEFORE the spin starts.
        int randomIndex = Random.Range(0, stopPositions.Length);
        float finalYPosition = stopPositions[randomIndex];

        // 2. Do a visual spin animation for a random duration.
        int randomSpinDuration = Random.Range(60, 100);
        for (int i = 0; i < randomSpinDuration; i++)
        {
            // This just handles the visual looping of the reel.
            if (transform.position.y <= -3.95f)
                transform.position = new Vector2(transform.position.x, 1.51f);

            transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);

            // Visual slow down effect
            if (i > Mathf.RoundToInt(randomSpinDuration * 0.75f))
                timeInterval = 0.15f;
            else if (i > Mathf.RoundToInt(randomSpinDuration * 0.5f))
                timeInterval = 0.1f;

            yield return new WaitForSeconds(timeInterval);
        }

        // 3. SNAP TO THE FINAL POSITION. This guarantees a perfect landing.
        transform.position = new Vector2(transform.position.x, finalYPosition);

        // 4. Assign the slot name based on the final position.
        if (Mathf.Approximately(transform.position.y, -7.68f))
            stoppedSlot = "Diamond";
        else if (Mathf.Approximately(transform.position.y, -6.12f))
            stoppedSlot = "Crown";
        else if (Mathf.Approximately(transform.position.y, -4.54f))
            stoppedSlot = "Melon";
        else if (Mathf.Approximately(transform.position.y, -3.08f))
            stoppedSlot = "Bar";
        else if (Mathf.Approximately(transform.position.y, -1.6f))
            stoppedSlot = "Seven";
        else if (Mathf.Approximately(transform.position.y, -0.09f))
            stoppedSlot = "Cherry";
        else if (Mathf.Approximately(transform.position.y, 1.59f))
            stoppedSlot = "Lemon";
        else if (Mathf.Approximately(transform.position.y, 2.97f))
            stoppedSlot = "Diamond";

        rowStopped = true;
        Debug.Log(gameObject.name + " SNAPPED to '" + stoppedSlot + "' (Y: " + transform.position.y + ")");
    }

    private void OnDestroy()
    {
        GameControl.HandlePulled -= StartRotating;
    }
}