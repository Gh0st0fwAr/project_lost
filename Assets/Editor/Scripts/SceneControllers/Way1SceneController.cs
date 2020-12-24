using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Way1SceneController : MonoBehaviour //класс, отвечающий за работу сцены "Путь 1"
{
    public QuestController questController;
    
    private DataObject _dataObject;
    private static bool inited;
    private bool _dialogCreated;
    private PersonController _personController;
    [SerializeField] private GameObject portal;
    
    private void Awake()// запуск сцены
    {
        _dataObject = FindObjectOfType<DataObject>();
    }

    private void Start() // поиск персонажа, определение юнитов и заданий 
    {
        _dataObject.currentQuest.completed = true;
        _dataObject.currentQuest = null;
        _personController = FindObjectOfType<PersonController>();
        questController.dialogClosed.AddListener(() =>
            {
                _dataObject.currentQuest = _dataObject.questList.GetQuest(questController.questType);
                _personController.SetQuestText(_dataObject.currentQuest);
                _dataObject.currentQuest.stateList.ChangeState();
                portal.SetActive(true);
            });
    }

    private void Update()// проверка взаимодействия персонажа и триггеров, в данном случае диалога и квест-листа
    {
        if (questController.CollideWithCharacter && inited == false)
        {
            _dataObject.currentQuest = _dataObject.questList.GetQuest(questController.questType);
            inited = true;
        }
    }
    
    
}
