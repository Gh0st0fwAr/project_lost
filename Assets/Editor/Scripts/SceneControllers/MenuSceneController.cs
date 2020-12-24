using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneController : MonoBehaviour // класс, отвечающий за работу сцены Главное меню
{
    public Button startGameButton;
    public string nextScene;
    private void Awake() // запуск сцены
    {
        PlayerPrefs.DeleteKey("score");
        FindObjectOfType<DataObject>().questList.ResetAllQuests();

        startGameButton.onClick.AddListener(LoadNextScene);
    }

    private void LoadNextScene() // запуск следующей сцены или окна в соответствии с выбором пользователя
    {
        SceneManager.LoadScene(nextScene);
    }
}
