using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PortalSceneController : MonoBehaviour //класс, отвечающий за работу сцены "Портальная карта"
{
    public QuestController questController;
    public float fateSpeed = 1f;
    
    private DataObject _dataObject;
    private static bool inited;
    private bool _dialogCreated;
    private PersonController _personController;
    [SerializeField] private Vector2 _firstPoint;
    [SerializeField] private Vector2 _secondPoint;
    [SerializeField] private Animator _fateAnimator;
    private bool _moveFate;

    private void Awake()// запуск сцены
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
                    _dataObject.currentQuest.completed = true;
                    _dataObject.currentQuest = _dataObject.questList.GetQuest(questController.questType);
                    questController.dialogClosed.RemoveAllListeners();
                    questController.dialogClosed.AddListener(() =>
                    {
                        _personController.SetQuestText(_dataObject.currentQuest);
                        questController.dialogClosed.RemoveAllListeners();
                        _moveFate = true;
                        _fateAnimator.Play("FateWalk");
                        _fateAnimator.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    });
                });
            }
        }
    }
    
    private void Update() // проверка взаимодействия персонажа и триггеров, в данном случае диалога с Фейт и последующей активации ее анимации
    {
        if (Input.GetKeyDown(KeyCode.E) && !inited && _dialogCreated)
        {
            questController.CreateDialog(_personController.kylePanicSprite, null);
        }

        if (_moveFate)
        {
            _fateAnimator.transform.position = Vector3.MoveTowards(_fateAnimator.transform.position, _secondPoint, fateSpeed * Time.deltaTime);
            if (Vector3.Distance(_fateAnimator.transform.position, _secondPoint) <= 0.2) //Анимация Фейт
            {
                Destroy(_fateAnimator.gameObject); // уничтожение модельки Фейт после выполнения анимации
                _moveFate = false;
            }
        }
        
    }
}
