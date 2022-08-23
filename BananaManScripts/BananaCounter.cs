using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BananaCounter : MonoBehaviour{

    public TMP_Text _text;
    public PlayerStats playerStats;

    void Update(){
        _text.text=playerStats.GetBenanasCollected().ToString();

    }
}
