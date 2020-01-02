using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownAssigner : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    void Start()
    {
        StartCoroutine("InitializeDropdown");
    }

    IEnumerator InitializeDropdown()
    {
        yield return new WaitUntil(() => PongMaster.instance.loadSettings);

        dropdown.onValueChanged.AddListener(delegate
        {
            PongMaster.instance.SetDifficulty(dropdown.value);
        });

        StopCoroutine("InitializeDropDown");
    }
}
