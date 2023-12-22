using cowsins;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;

    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public Quaternion playerCameraRotation;

    public float playerHealth;
    public float playerShield;

    public int playerLvl;
    public int coins;
    
    public int primaryWeaponAmmoCount;
    public int secondaryWeaponAmmoCount;
    public int currentWeaponInt;

    public Weapon_SO weaponOne;
    public Weapon_SO weaponTwo;
    
    public string currentScene;

    public GameData()
    {
        playerPosition = new Vector3(0, 0, 0);
        playerHealth = 100f;
        playerShield = 50f;
        playerRotation = Quaternion.identity;
        playerCameraRotation = Quaternion.identity;
        playerLvl = 0;
        coins = 0;
        weaponOne = null;
        weaponTwo = null;
        currentScene = "SampleScene";
    }
}