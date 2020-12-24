using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

[Serializable]
public class Quest // базовый класс, оописывающий поля квеста
{
    public QuestType questType;
    public string description;
    public int award;
    public bool completed;
    public StateList stateList;
}
[CreateAssetMenu(fileName = "QuestList", menuName = "Quest list", order = 0)]
public class QuestList : ScriptableObject // класс, описывающий Юнити параметры квеста
{
    public List<Quest> quests;

    public Quest GetQuest(QuestType questType) //выдача квеста
    {
        return quests.First(x => x.questType == questType);
    }

    public void CompleteQuest(QuestType questType) // завершение квеста
    {
        quests.First(x => x.questType == questType).completed = true;
    }

    public void ResetAllQuests() // класс, удаляющий пройденный квест
    {
        foreach (var quest in quests)
        {
            quest.stateList.ResetState();
            quest.completed = false;
        }
    }
}
