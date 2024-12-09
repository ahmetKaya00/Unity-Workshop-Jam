using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSelectionManager : MonoBehaviour
{
    public Image sceneImage;
    public Button selectButton;
    public Button nextButton;
    public Button prevButton;

    public Sprite[] sceneSprites;
    public string[] sceneNames;

    private int currentIndex = 0;

    private void Start()
    {
        UpdateSceneDisplay();
        nextButton.onClick.AddListener(NextScene);
        nextButton.onClick.AddListener(PrevScene);
        selectButton.onClick.AddListener(SelectScene);
    }
    public void UpdateSceneDisplay()
    {
        sceneImage.sprite = sceneSprites[currentIndex];
        selectButton.GetComponentInChildren<Text>().text = sceneNames[currentIndex];
    }
    public void NextScene()
    {
        currentIndex = (currentIndex + 1) % sceneSprites.Length;
        UpdateSceneDisplay();
    }
    public void PrevScene()
    {
        currentIndex = (currentIndex - 1 + sceneSprites.Length) % sceneSprites.Length;
        UpdateSceneDisplay();
    }

    public void SelectScene()
    {
        string selectedSceneName = sceneNames[currentIndex];
        SceneManager.LoadScene(selectedSceneName);
    }
}
