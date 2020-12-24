using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataObject : MonoBehaviour
{
   public QuestList questList;
   public Quest currentQuest = null;
   public void Awake()
   {
      DontDestroyOnLoad(this);
   }
}
