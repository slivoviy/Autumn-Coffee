using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Img_CB_change : MonoBehaviour
{
    public TextMeshProUGUI describe_text;
    public TextMeshProUGUI instuct_text;
    public int index_current;
    public int length;
    public Image Curr_Img;
    List<string> descriptions = new List<string>() { "Americano\n     Caffe Americano is a drink of strength to regular coffee, but with a less powerful taste." };
    List<string> instructions = new List<string>() { "Basic recipe for Americano:\n1) Add 1 portion of coffee\n2) Add 1 portion of water" };
    public Sprite img_0;
    public Sprite img_1;
    public Sprite img_2;
    public Sprite img_3;



    void Awake()
    {
        descriptions.Add("Cappuccino\n     A cappuccino is an espresso-based coffee drink. Due to milk being added into it, it has both a bold coffee taste and some sweetness.");
        descriptions.Add("Espresso\n     Espresso is generally thicker than coffee brewed by other methods, with a viscosity similar to that of warm honey. It is served on its own, and is also used as the base for various other coffee drinks.");
        descriptions.Add("Latte\n     Caffe Latte is a coffee beverage, Italian in origin. It's milky, sweet and is creamier than regular coffee.");

        instructions.Add("Basic recipe for Cappuccino:\n1) Add 2 portions of coffee\n2) Add 1 portion of milk");
        instructions.Add("Basic recipe for Espresso:\n1) Add 3 portions of coffee");
        instructions.Add("Basic recipe for Latte:\n1) Add 1 portion of coffee\n2) Add 2 portions of milks");

        instuct_text.text = instructions[0];
        describe_text.text = descriptions[0];

        length = descriptions.Count;

        index_current = 0;

        Curr_Img.sprite = img_0;
    }
    
    
    public void NextText()
    {
        if (length > 0)
        {
            index_current = (index_current + 1) % length;
            instuct_text.text = instructions[index_current];
            describe_text.text = descriptions[index_current];

            if (index_current == 3)
            {
                Curr_Img.sprite = img_3;
            }
            if (index_current == 2)
            {
                Curr_Img.sprite = img_2;
            }
            if (index_current == 1)
            {
                Curr_Img.sprite = img_1;
            }
            if (index_current == 0)
            {
                Curr_Img.sprite = img_0;
            }
        }

    }
    public void PrevText() //если листать назад, то все немного криво, ибо 2 разные кнопки, Awake срабатывает снова и все печально. я не знаю как это починить :(
    {
        index_current= index_current-1;
        if (index_current == -1)
        {
            Debug.Log("True");
            index_current = length-1;
        }
        instuct_text.text = instructions[index_current];
        describe_text.text = descriptions[index_current];

        if (index_current == 3)
        {
            Curr_Img.sprite = img_3;
        }
        if (index_current == 2)
        {
            Curr_Img.sprite = img_2;
        }
        if (index_current == 1)
        {
            Curr_Img.sprite = img_1;
        }
        if (index_current == 0)
        {
            Curr_Img.sprite = img_0;
        }

    }
}
