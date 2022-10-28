using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAmmo : MonoBehaviour
{
    [SerializeField] private Text ammoText;

    public void updateAmmo(int count)
    {
        ammoText.text = count + "/" ;

    }
}
