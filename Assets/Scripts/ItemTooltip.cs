using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
public class ItemTooltip : MonoBehaviour
{
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemStats;
    [SerializeField] TMP_Text itemSlot;

    private StringBuilder sb = new StringBuilder();
    public void ShowTooltip(Equipment item)
    {
        gameObject.SetActive(true);
        itemName.text = item.name;
        itemSlot.text = item.equipSlot.ToString();
        sb.Length = 0;
        AddStat(item.damageModifier, "Damage");
        AddStat(item.armorModifier, "Armor");
        AddStat(item.poisonModifier, "Poison Damage");
        AddStat(item.poisonChance, "Poison Chance");
        AddStat(item.poisonLength, "Poison Effect Length");
        AddStat(item.criticalModifier, "Critical Chance");
        AddStat(item.criticalMultiplier, "Critical Damage");
        AddStat(item.piercemodifier, "Piercing Damage Chance");
        itemStats.text = sb.ToString();

    }
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
    private void AddStat(float value, string statName)
    {
        if(value != 0)
        {
            if(sb.Length > 0)
            {
                sb.AppendLine();
            }
            if (value > 0)
            {
                sb.Append("+");
            }
            sb.Append(value);
            sb.Append("  ");
            sb.Append(statName);
        }
    }
    
}
