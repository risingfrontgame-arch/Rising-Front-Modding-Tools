using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
[CreateAssetMenu(fileName = "New Deployable", menuName = "Deployables")]
public class Deployables : ScriptableObject
{

    public GameObject prefab;
    public int chanceToSpawnSecondaryPrefabs = 0;
    public GameObject[] secondaryPrefabs;
    public Sprite icon;
    public GameObject mapIcon;
    public string name;
    public bool isGreenTeam = false;
    public int price = 50;



    [Space]
    [Header("ID Settings")]
    public string ID = "-1";
    [Tooltip("Check this to generate a random ID, its important no objects have the same ID")]
    public bool generateRandomID = false;
    public bool useCustomID = false;

    private void OnValidate()
    {
        if (ID == "-1" && useCustomID == false || generateRandomID == true && useCustomID == false)
        {
            ID = GenerateRandomString(15);
        }
    }




    private string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; // Pool of characters to choose from


    public string GenerateRandomString(int length)
    {
        generateRandomID = false;
        StringBuilder sb = new StringBuilder(); // Use StringBuilder for efficient string concatenation
        for (int i = 0; i < length; i++)
        {
            int randomIndex = Random.Range(0, characters.Length); // Get a random index within the character pool
            sb.Append(characters[randomIndex]); // Append the character at the random index
        }
        return sb.ToString();
    }
}