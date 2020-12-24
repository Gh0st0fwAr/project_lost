using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour // класс, на основе класса Dialog, позволяющий создать интерфейс в Юнити под диалог
{
    public GameObject leftTopPanel;
    public GameObject rightTopPanel;

    public Image panelImage;
    public Image kyleImage;
    public Image opponentImage;
    
    public Sprite bluePanelSprite;
    public Sprite redPanelSprite;
    
    public Text text;
    public Text leftPersonName;
    public Text rightPersonName;

    private void Init() // выключение панелей диалога, чтобы не перекрывали экран
    {
        leftTopPanel.SetActive(false);
        rightTopPanel.SetActive(false);
    }

    public void StartDialog() // запуск диалога
    {
        Init();
    }

    public void SwitchDialog(bool left, string message, string name, Sprite Kyle, Sprite opponent) // условия переключения диалоговых окон
    {
        if (left) // условия появления окна Кайла
        {
            leftTopPanel.SetActive(true);
            rightTopPanel.SetActive(false);
            panelImage.sprite = bluePanelSprite;
            kyleImage.sprite = Kyle;
            leftPersonName.text = name;
        }
        else // Условия появления окна любого другого персонажа
        {
            leftTopPanel.SetActive(false);
            rightTopPanel.SetActive(true);
            panelImage.sprite = redPanelSprite;
            rightPersonName.text = name;
            opponentImage.sprite = opponent;
        }
        
        text.text = message;
    }

    public void CloseDialog() // уничтожает пройденный диалог
    {
        Destroy(gameObject);
    }
}
