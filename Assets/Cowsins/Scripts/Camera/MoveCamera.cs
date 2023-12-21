namespace cowsins
{
    /// <summary>
    /// This script belongs to cowsins™ as a part of the cowsins´ FPS Engine. All rights reserved. 
    /// </summary>
    using UnityEngine;

    /// <summary>
    /// Keep camera in place
    /// </summary>
    public class MoveCamera : MonoBehaviour, IDataPersistence
    {

        [Tooltip("Reference to our head = height of the camera"), SerializeField]
        private Transform head;
        private void Update() => transform.position = head.transform.position;

        public void LoadData(GameData gameData)
        {
            this.gameObject.transform.rotation = gameData.playerCameraRotation;
        }

        public void SaveData(GameData gameData)
        {
            gameData.playerCameraRotation = this.gameObject.transform.rotation;
        }


    }
}