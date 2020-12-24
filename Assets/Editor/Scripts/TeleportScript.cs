using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportScript : MonoBehaviour // класс, отвечающий за механику телепортации
{
    public string nextScene;
    private void OnTriggerEnter2D(Collider2D other) // функция, проверяющая триггер "портал" и запускающая следующую сцену в случае успеха
    {
        if (other.gameObject.CompareTag("Character"))
            SceneManager.LoadScene(nextScene);
    }
}
