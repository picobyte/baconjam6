﻿using UnityEngine;
using System.Collections;

public class PlayerItem : MonoBehaviour
{
	private static Item currentItem;
    public static ItemType type
    {
        get
        {
            return currentItem ? currentItem.type : ItemType.None;
        }
    }

	public void OnReceiveItem(Item item)
	{
		if(currentItem)
		{
			currentItem.transform.parent = null;
            currentItem.Return();
		}
		currentItem = item;
		currentItem.transform.parent = transform;
		currentItem.transform.localPosition = Vector3.zero;
	}
}
