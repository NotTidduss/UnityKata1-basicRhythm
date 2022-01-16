using System.Collections;
using UnityEngine;

public class Rhythm_Game_HealthMaster : MonoBehaviour
{
    [Header ("Scene References")]
    [SerializeField] private RectTransform healthBar;


    //* private vars
    private float health;               // used for keeping track of the player's health, ranges from 0 to 100.
    private float maxHealthBarHeight;   // reference to 100% size.
    private float healValuePerfect;     // heal gained from hitting a perfect note.
    private float healValueGood;        // heal gained from hitting a good note.
    private float damageValueFine;      // damage taken from hitting a fine note.
    private float damageValueMiss;      // damage taken from a missed note.


    public void initialize(Rhythm_System sys) {
        // initialize private vars
        health = 100;
        maxHealthBarHeight = healthBar.rect.height;

        healValuePerfect = sys.healingValuePerfect;
        healValueGood = sys.healingValueGood;
        damageValueFine = sys.damageValueFine;
        damageValueMiss = sys.damageValueMiss;

        // ensure that game over is not 0 when starting
        PlayerPrefs.SetInt("rhythm_gameOver", 0);

        // start fallen note check coroutine
        StartCoroutine(CheckForFallenNoteDamage());
    }


    // If a note falls too low, take damage.
    IEnumerator CheckForFallenNoteDamage() {
        while(true) {
            if (PlayerPrefs.GetInt("rhythm_fallenNoteCountForHealth") > 0) {
                damage(damageValueMiss);
                PlayerPrefs.SetInt("rhythm_fallenNoteCountForHealth", PlayerPrefs.GetInt("rhythm_fallenNoteCountForHealth") - 1);
            }
            yield return null;
        }
    }

    // Adjust health based on given judgement.
    public void handleJudgement(Rhythm_Judgement judgement) {
        switch (judgement) {
            case Rhythm_Judgement.PERFECT: handlePerfectHit(); break;
            case Rhythm_Judgement.GOOD: handleGoodHit(); break;
            case Rhythm_Judgement.FINE: handleFineHit(); break;
            case Rhythm_Judgement.MISS: handleFineHit(); break;
        }
    }

    private void handlePerfectHit() => heal(healValuePerfect);
    private void handleGoodHit() => heal(healValueGood);
    private void handleFineHit() => damage(damageValueFine);
    private void handleMissHit() => damage(damageValueMiss);

    // Deduct damage value from current health. if health is too low, game over.
    private void damage(float damage) {
        health -= damage;
        if (health < 0) 
            PlayerPrefs.SetInt("rhythm_gameOver", 1);
        setRelativeHealthBarHeight(health);
    }

    // If health is not full, add heal value to current health.
    private void heal(float gain) {
        if (health < 100) {
            health += gain;
            if (health > 100) 
                health = 100;
            setRelativeHealthBarHeight(health);
        }
    }

    // Set health bar height according to current health %.
    private void setRelativeHealthBarHeight(float health) {
        float newHeight = maxHealthBarHeight * health / 100;
        healthBar.sizeDelta = new Vector2(healthBar.rect.width, newHeight);
    }
}
