using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public Image carImage;
    public Text priceText;
    public Text totalScoreText;
    public Button actionButton;
    public Button nextButton;
    public Button previousButton;

    public Sprite[] carSprites;
    public int[] carPrices;
    public string[] carNames;

    private int currentIndex = 0;
    private int totalScore = 0;
    private bool[] ownedCars;

    private string ownedCarsKey = "OwnedCars";

    private void Start()
    {
        totalScore = PlayerPrefs.GetInt("TotalScore", 0);
        totalScoreText.text = "Total Score: " + totalScore.ToString();

        ownedCars = new bool[carSprites.Length];
        for(int i = 0; i < carSprites.Length; i++)
        {
            ownedCars[i] = PlayerPrefs.GetInt(ownedCarsKey + i,0) == 1 ? true : false;
        }

        UpdateCarDisplay();

        nextButton.onClick.AddListener(NextCar);
        previousButton.onClick.AddListener(PreviousButton);
        actionButton.onClick.AddListener(ActionButtonPressed);
    }

    public void UpdateCarDisplay()
    {
        carImage.sprite = carSprites[currentIndex];

        if (ownedCars[currentIndex])
        {
            actionButton.GetComponentInChildren<Text>().text = carNames[currentIndex];
            actionButton.onClick.RemoveAllListeners();
            actionButton.onClick.AddListener(SelectCar);
            priceText.text = "";
        }
        else
        {
            actionButton.GetComponentInChildren<Text>().text = "Sat�n Al";
            actionButton.onClick.RemoveAllListeners();
            actionButton.onClick.AddListener(BuyCar);
            priceText.text = "Price: " + carPrices[currentIndex].ToString();

            if(totalScore >= carPrices[currentIndex])
            {
                actionButton.interactable = true;
            }
            else
            {
                actionButton.interactable = false;
            }
        }
    }

    public void NextCar()
    {
        currentIndex = (currentIndex +1) % carPrices.Length;
        UpdateCarDisplay();
    }
    public void PreviousButton() {
        currentIndex = (currentIndex - 1 + carSprites.Length) % carPrices.Length;
        UpdateCarDisplay();
    }
    public void BuyCar()
    {
        if(totalScore >= carPrices[currentIndex] && !ownedCars[currentIndex])
        {
            totalScore -= carPrices[currentIndex];
            PlayerPrefs.SetInt("TotalScore", totalScore);
            totalScoreText.text = "Total Score: " +totalScore.ToString();

            ownedCars[currentIndex] = true;
            PlayerPrefs.SetInt(ownedCarsKey + currentIndex, 1);

            UpdateCarDisplay();
        }
    }

    public void SelectCar()
    {
        if (ownedCars[currentIndex])
        {
            string selectedCarPrefabName = carNames[currentIndex];
            PlayerPrefs.SetString("SelectedCarPrefabName", selectedCarPrefabName);

            Debug.Log("Yar�� i�in se�ilen ara�: " + selectedCarPrefabName);
        }
    }

    private void ActionButtonPressed()
    {
        if (ownedCars[currentIndex])
        {
            SelectCar();
        }
        else
        {
            BuyCar();
        }
    }
}
