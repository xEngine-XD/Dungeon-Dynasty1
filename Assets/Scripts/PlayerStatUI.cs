using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
public class PlayerStatUI : MonoBehaviour
{
    [SerializeField] TMP_Text stats;
    PlayerStats player;
    private StringBuilder sb = new StringBuilder();

    public void ShowStats()
    {
        player = GameManager.instance.playerStats;
        sb.Length = 0;
        AddStat("Health", player.maxHealth);
        AddStat("Physical Damage", player.damage.GetValue());
        AddStat("Armor", player.armor.GetValue());
        AddStat("Poison Damage", player.poisonDamage.GetValue());
        AddStat("Poison Chance", player.poisonChance.GetValue());
        AddStat("Crit Multiplier", player.criticalMultiplier.GetValue());
        AddStat("Crit Chance", player.criticalChance.GetValue());
        AddStat("Piercing Damage Chance", player.piercingChance.GetValue());
        AddStat("Magical Damage", player.magicDamage.GetValue());
        AddStat("Magical Resistance", player.magicResist.GetValue());
        AddStat("Posion Resistance", player.poisonDeflect.GetValue());
        stats.text = sb.ToString();
    }
    private void AddStat(string statName, float value)
    {
        sb.Append(statName);
        sb.Append("  ");
        sb.Append(value);
        sb.AppendLine();
    }
}
