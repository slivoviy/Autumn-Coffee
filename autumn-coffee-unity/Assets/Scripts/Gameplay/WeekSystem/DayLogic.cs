using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class DayLogic
{
    private List<IDayLogic> _dayLogics = new List<IDayLogic>();
    
    public DayType DayType;

    public DayLogic(DayType dayType)
    {
        DayType = dayType;
    }

    public void AddDayLogic(IDayLogic dayLogic)
    {
        _dayLogics.Add(dayLogic);
    }

    public void RemoveDayLogic(IDayLogic dayLogic)
    {
        _dayLogics.Remove(dayLogic);
    }

    public void InvokeDayLogics()
    {
        for (int i = 0; i < _dayLogics.Count; i++)
        {
            _dayLogics[i].DayLogic();
        }
    }
}
