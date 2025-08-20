using UnityEngine;
using TMPro;

public class BettingSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text balanceText;
    [SerializeField] private TMP_Text prizeText;

    private int playerBalance = 1000;   // starting money
    private int currentBet = 10;        // default bet
    private int prizeValue = 0;

    void Start()
    {
        UpdateBalanceUI();
        prizeText.text = "";
    }

    // --- Set the bet when a button is clicked ---
    public void SetBet(int betAmount)
    {
        currentBet = betAmount;
        Debug.Log("Bet set to $" + currentBet);
    }

    // --- Deduct bet when spin starts ---
    public bool PlaceBet()
    {
        if (playerBalance < currentBet)
        {
            Debug.Log("Not enough balance!");
            return false;
        }

        playerBalance -= currentBet;
        UpdateBalanceUI();
        return true;
    }

    // --- Add prize after spin ends ---
    public void AwardPrize(int winType)
    {
        prizeValue = 0;

        if (winType == 3) // three-of-a-kind
            prizeValue = currentBet * 10; // e.g. bet 100 → win 1000
        else if (winType == 2) // two-of-a-kind
            prizeValue = currentBet * 2;  // e.g. bet 100 → win 200

        playerBalance += prizeValue;

        prizeText.text = prizeValue > 0 ? "Prize: $" + prizeValue : "No Win!";
        UpdateBalanceUI();
    }

    private void UpdateBalanceUI()
    {
        balanceText.text = "Balance: $" + playerBalance;
    }
}
