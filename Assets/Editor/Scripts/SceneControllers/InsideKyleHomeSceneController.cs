using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideKyleHomeSceneController : MonoBehaviour //класс, отвечающий за работу сцены "Внутри дома Кайла"
{
    [SerializeField] private QuestController questController;

    private bool finished;
    private PersonController _personController;
    private static bool inited;
    private bool dialogCreated;
    private DataObject _dataObject;

    private void Awake() // запуск сцены
    {
        _dataObject = FindObjectOfType<DataObject>();
    }

    IEnumerator Start() // поиск персонажа, определение юнитов и заданий
    {
        if (inited)
            yield return null;
        else
        {
            _personController = FindObjectOfType<PersonController>();
            _personController.blocked = true;
            yield return new WaitForSeconds(2);
            _dataObject.currentQuest = _dataObject.questList.GetQuest(questController.questType);
            questController.CreateDialog(_personController.kylePanicSprite, _personController.robotSprite);
            dialogCreated = true;
            questController.dialogClosed.AddListener(() =>
            {
                finished = true;
                _personController.blocked = false;
                _personController.SetQuestText(_dataObject.currentQuest);
                inited = true;
            });
        }
    }

    private void Update() // проверка взаимодействия персонажа и триггеров, в данном случае диалога
    {
        if (Input.GetKeyDown(KeyCode.E) && !finished && dialogCreated)
        {
            questController.CreateDialog(_personController.kylePanicSprite, _personController.robotSprite);
        }
    }
}
