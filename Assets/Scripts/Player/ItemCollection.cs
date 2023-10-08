using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    public static Action<int> onItemCollected;


    // Method that will trigger all of the collectables
    void OnTriggerEnter(Collider other)
    {
        string tagToFind = other.tag;       // Cache the other.tag reference
        (string key, int value) = GetKeyAndValueInDictionary(tagToFind);        // Cache the return Tuple

        if(other.tag != null)       // If other.tag string exists in the Dictionary
        {
            onItemCollected?.Invoke(value);     // Pass value through the event to subscribers
            Debug.Log("Key for tag: " + key);
            Debug.Log("Value for tag: " + value);
        }
        else
        {
            Debug.LogWarning("Tag not found:: " + other.tag);
        }

        other.gameObject.SetActive(false);  
    }

    (string key, int value) GetKeyAndValueInDictionary(string tagToFind)
    {
        foreach(KeyValuePair<string, int> kvp in ScoreManager.Instance.BonusItemsDictionary)
        {
            if (kvp.Key == tagToFind)       // If a Key matches the collider Tag
            {
                return (kvp.Key, kvp.Value);     // Return the Key and Value
            }
        }

        return (null, 0);      // Tag not found in the dictionary. Choose a value that doesn't exist so we can match with the Tuple signature.
    }
}
