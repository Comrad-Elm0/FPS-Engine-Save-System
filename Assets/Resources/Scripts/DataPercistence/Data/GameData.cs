using cowsins;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;

    public Vector3 playerPosition;
    public Quaternion playerRotation, playerCameraRotation;

    public float playerHealth, playerShield;
    public float maxHealth, maxShield, damageMultiplier, healMultiplier;

    public int playerLvl;
    public int coins;
    
    public int primaryWeaponAmmoCount;
    public int secondaryWeaponAmmoCount;
    public int currentWeaponInt;

    public Weapon_SO weaponOne;
    public Weapon_SO weaponTwo;

    public string timeSaved;
    
    public string currentScene;
    
    public GameData()
    {
        playerPosition = new Vector3(0, 0, 0);
        playerHealth = 100f;
        playerShield = 50f;
        maxHealth = 100f;
        maxShield = 50f;
        damageMultiplier = 1f;
        healMultiplier = 1f;
        playerRotation = Quaternion.identity;
        playerCameraRotation = Quaternion.identity;
        playerLvl = 0;
        coins = 0;
        weaponOne = null;
        weaponTwo = null;
        timeSaved = System.DateTime.Now.ToString();
        currentScene = "Level One";
    }
}