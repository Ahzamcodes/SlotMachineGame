# 🎰 Slot Machine Game (Unity)

## 📖 Game Overview
This is a simple *slot machine game* built entirely in Unity.  
Players pull the handle to spin three reels. The reels stop one by one, and the result determines the prize:

- *3 of a kind* → Jackpot 🎉  
- *2 of a kind* → Small win  
- *No match* → No win  

The game also includes a *betting system* to check balance, place bets, and award winnings.

---

## ▶ How to Play
1. Start the game in the Unity Editor (or a build).  
2. Click on the *slot machine handle* to pull it.  
3. Wait for the handle animation to finish.  
4. The reels will spin and then stop one by one.  
5. The game automatically checks results and updates your balance:  
   - 3 matches = big win  
   - 2 matches = small win  
   - No match = lose bet  

---

## ▶ How to Run (Unity Editor)
1. Clone or download this repository.  
2. Open the project in *Unity (2021+ recommended)*.  
3. Open the main scene.
## 🚀 Instructions to Run WebGL Build
1. *Build the Game*  
   - In Unity, go to File → Build Settings.  
   - Select *WebGL* as the target platform.  
   - Click *Build and Run*.
##Features
- 🎥 *Animated Handle* → Reels only spin after the handle finishes pulling.  
- 💰 *Betting System* → Checks balance before spinning, awards prizes on win.  
- 🔄 *Event System* → Uses Action HandlePulled to trigger reel spins cleanly. 

## 🧠 Thought Process / Approach
1. *Reels & Rows*  
Designed a Row class that handles individual reel behavior and reports when it has stopped.  

2. *Game Control*  
Central GameControl script listens for handle pulls, manages betting, and checks results once all reels stop.  

3. *Animation Sync*  
Used a coroutine to wait for the handle animation to *fully finish* before triggering reel spins.  

4. *Result Logic*  
Implemented simple win detection (2 of a kind, 3 of a kind) and integrated with the betting system.  

5. *Scalability*  
Wrote the code so more reels, more symbols, or different win conditions can be added later without rewriting core logic.
