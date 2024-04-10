using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Weapon Values")]
public class Weapon : ScriptableObject
{
    public int type;
    public string wpnName;
    public int baseFireRate, baseDmg;

    public Weapon()
    {
        // Weapon class. Determines what's available and the base damage, fire rate and projectile characteristics of the given weapon. Attached to both Player and Mob objects.

    }
}
