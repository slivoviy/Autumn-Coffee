using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCookBook : MonoBehaviour
{
    public void Animate_CookBook_ToScreen()
    {
        GetComponent<Animation>().Play("Cookbook_toScreen");
    }
    public void Animate_CookBook_OffScreen()
    {
        GetComponent<Animation>().Play("Cookbook_offScreen");
    }
}
