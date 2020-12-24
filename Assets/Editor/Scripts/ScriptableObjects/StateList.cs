using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;


[Serializable]
public class State // класс, описывающий состояния для скриптов
{
    public string name;
    public List<Dialog> dialogList;
}
[CreateAssetMenu(fileName = "StateList", menuName = "State list", order = 0)]
public class StateList : ScriptableObject
{
    public List<State> states;

    private int i = 0;
    public State GetCurrentState()
    {
        if (states.Count == 0)
        {
            return null;
        }

        return states[i];
    }

    public void ChangeState()
    {
        if (i <= states.Count)
        {
            i++;
        }
    }

    public void ResetState()
    {
        i = 0;
    }
}
