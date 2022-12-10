using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ruinum.Core;

public class Inventory : Executable
{
    public static Inventory Singletone;

    public ItemSO[] items = new ItemSO[2];
    public ItemSO GrabItem;

    public override void Execute()
    {
        
    }

    private void TakeItem()
    {

    }

    private void Awake()
    {
        Singletone = this;
    }

    public bool AddItem(ItemSO item)
    {
        if (!items[0])
        {
            items[0] = item;
            return true;
        }
        else if (!items[1])
        {
            items[1] = item;
            return true;
        }
            return false;
    }
    
}
