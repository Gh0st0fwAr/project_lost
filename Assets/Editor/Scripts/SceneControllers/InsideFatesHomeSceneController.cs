using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideFatesHomeSceneController : MonoBehaviour //класс, отвечающий за работу сцены "Внутри дома Фейт"
{
    public QuestController questController; 
    private DataObject _dataObject;
    private static bool inited;
    private bool _dialogCreated;
    private PersonController _personController;
    private void Awake() // запуск сцены
    {
        _dataObject = FindObjectOfType<DataObject>();
    }
    private void Start() // поиск персонажа, определение юнитов и заданий
    {
        if (!inited)
        {
            _dataObject.currentQuest.stateList.ChangeState();
            if (!_dataObject.currentQuest.completed)
            {
                _personController = FindObjectOfType<PersonController>();
                _personController.blocked = true;
                questController.CreateDialog(_personController.kylePanicSprite, null);
                _dialogCreated = true; 
                questController.dialogClosed.AddListener(() =>
                {
                    _personController.blocked = false;
                    inited = true;
                    questController.dialogClosed.RemoveAllListeners();
                });
            }
        }
    }
    
    private void Update() // проверка взаимодействия персонажа и триггеров, в данном случае квестового предмета
    {
        if (Input.GetKeyDown(KeyCode.E) && !inited && _dialogCreated)
        {
            questController.CreateDialog(_personController.kylePanicSprite, null);
        }
    }
}
