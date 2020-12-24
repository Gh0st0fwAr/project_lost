using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSceneController : MonoBehaviour //класс, отвечающий за работу сцены "Дом Кайла"
{
    public GameObject characterPrefab;
    public Vector2 spawnPosition;
    
    // Start is called before the first frame update
    void Start() // запуск сцены
    {
        var charater = Instantiate(characterPrefab);
        charater.GetComponent<Transform>().position = spawnPosition;
    }
}
