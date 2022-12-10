using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ruinum.Core;

public class AnimationPanel : MonoBehaviour
{
    //ЗАПУСК АНИМАЦИЙ
    public void Animate_Panel()//Открывает (визуально) меню настроек 
    {
        GetComponent<Animation>().Play("Settings Open");
    }
    public void Close_Panel()//Закрывает (визуально) меню настроек 
    {
        GetComponent<Animation>().Play("Settings Close");
    }

    public void Animate_Transition()//Переходит (визуально) в игру
    {
        GetComponent<Animation>().Play("Black_animation");
        TimerManager.Singleton.StartTimer(1, () => SceneTransition.Singleton.SwitchToScene("Gameplay_Core"));
    }

    
}
