using cowsins;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;

    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public Quaternion playerCameraRotation;

    //public int primaryWeapon;
    //public int secondaryWeapon;
    
    public int primaryWeaponAmmoCount;
    public int secondaryWeaponAmmoCount;
    public int currentWeaponInt;

    //public WeaponIdentification weaponOne;
    //public WeaponIdentification weaponTwo;

    public Weapon_SO weaponOne;
    public Weapon_SO weaponTwo;
    
    public string currentScene;

    public GameData()
    {
        playerPosition = new Vector3(0, 0, 0);
        playerRotation = Quaternion.identity;
        playerCameraRotation = Quaternion.identity;
        weaponOne = null;
        weaponTwo = null;
        currentScene = "SampleScene";
    }
}
