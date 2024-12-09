using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   public CinemachineVirtualCamera virtualCamera;
    public Transform spawnPoint;
    public GameObject selectedCar;
    private Rigidbody selectedCarRigidbody;
    private Text speedText;

    private void Start()
    {
        string selectedCarPrefabName = PlayerPrefs.GetString("SelectedCarPrefabName");

        GameObject carPrefab = Resources.Load<GameObject>(selectedCarPrefabName);
        if(carPrefab != null)
        {
            selectedCar = Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation);
            selectedCarRigidbody = selectedCar.GetComponent<Rigidbody>();
            virtualCamera.Follow = selectedCar.transform;
            virtualCamera.LookAt = selectedCar.transform;

            speedText = GameObject.Find("ibre").GetComponent<Text>();
        }
    }

    private void Update()
    {
        if(selectedCarRigidbody != null && speedText != null)
        {
            float carSpeed = selectedCarRigidbody.velocity.magnitude * 3.6f;
            speedText.text = Mathf.Round(carSpeed).ToString() + "\nkm/h";
        }
    }
}
