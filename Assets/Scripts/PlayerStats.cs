using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "data", menuName = "Player Stats Values", order = 1)]
public class PlayerStats : ScriptableObject
{
    // Health and Armor values
    public float health, armor;

    // Speed values for the ship and projectile velocities and fire rates
    public int shipSpeed, wpnSpeed, projSpeed, mslSpeed;

    // Timer values (maxTime is the max upgradable value, availTime is the maxavailable based on the current upgrade level
    public int maxTimeLvl, currTimeLvl, maxTime;

    // Weapon Object. Determines how projectiles spawn and how fast, what they look like and their damage output.
    // public Weapon wpn;

    private void OnEnable()
    {
        health = 100;
        armor = 2;
        shipSpeed = 1;
        wpnSpeed = 1;
        projSpeed = 1;
        mslSpeed = 1;

        maxTimeLvl = 0;
        currTimeLvl = 1;
        maxTime = 25 + (currTimeLvl * 5);
    }

    public PlayerStats()
    {
        Debug.Log("PlayerStats: " + GetInstanceID());
        Debug.Log("This message should appear without executing the game environment.");
    }
}
