using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI _resourceText;

    private void Start()
    {
        _resourceText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateResourceText(PlayerInventory inventory)
    {
        _resourceText.text = inventory._resourcesCollected.ToString();
    }
}
