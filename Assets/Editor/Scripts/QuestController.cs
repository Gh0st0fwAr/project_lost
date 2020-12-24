using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.iOS;

public class QuestController : MonoBehaviour //класс, отвечающий за управление квестами, создает внутренный интерфейс для разработчика
{
    public QuestType questType;
    public bool CollideWithCharacter => _collideWithCharacter;
    public bool CollideWithJump => _collideWithJump;
    public GameObject dialogPrefab;
    public UnityEvent dialogClosed = new UnityEvent();

    //private QuestList _questList;
    private DialogController _dialogController;
    private bool _collideWithCharacter;
    private bool _collideWithJump;
    private int i = 0;
    private State _currentState;
    private DataObject _dataObject;

    public void Awake()
    {
        _dataObject = FindObjectOfType<DataObject>();
    }

    public void CreateDialog(Sprite Kyle, Sprite Opponent) // параметр диалога для интерфейса
    {
        if (_dialogController == null)
        {
            _dialogController = Instantiate(dialogPrefab).GetComponent<DialogController>();
            
            _currentState = _dataObject.currentQuest.stateList.GetCurrentState();
            if (_currentState.dialogList.Count == 0)
                return;
            
            _dialogController.StartDialog();
            
            var dialog = _currentState.dialogList;
            var left = dialog[i].characterName == "Кайл";
            _dialogController.SwitchDialog(left, dialog[i].words, dialog[i].characterName, Kyle, Opponent);
        }
        else if (i < _currentState.dialogList.Count)
        {
            var dialog = _currentState.dialogList;
            var left = dialog[i].characterName == "Кайл";
            _dialogController.SwitchDialog(left, dialog[i].words, dialog[i].characterName, Kyle, Opponent);
        }
        else
        {
            _dialogController.CloseDialog();
            i = 0;
            dialogClosed.Invoke();
            return;
        }
        i++;
    }

    private void OnTriggerEnter2D(Collider2D other)  // проверка состояния персонажа на состояние прыжка
    {
        if (other.gameObject.CompareTag("Character"))
        {
            if (gameObject.tag == "Jump")
            {
                _collideWithJump = true;
            }
            else
            {
                _collideWithCharacter = true;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other) // проверка состояния персонажа
    {
        if (other.gameObject.CompareTag("Character"))
        {
            if (gameObject.tag == "Jump")
            {
                _collideWithJump = false;
            }
            else
            {
                _collideWithCharacter = false;
            }
        }
    }
}
