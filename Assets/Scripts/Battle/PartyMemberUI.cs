using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMemberUI : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpBar;

    [SerializeField] Sprite highlightedImage;
    [SerializeField] Color highlightedColor;

    [SerializeField] List<Image> partyTexts;

    Pokemon _pokemon;

    public void SetData(Pokemon pokemon)
    {
        _pokemon = pokemon;

        nameText.text = pokemon.Base.name;
        levelText.text = "Lvl " + pokemon.Level;
        hpBar.SetHP((float)pokemon.HP / pokemon.MaxHp);
    }

    private Sprite[] originalSrpites;

    void Start()
    {
        originalSrpites = new Sprite[partyTexts.Count];
        for (int i = 0; i < partyTexts.Count; i++)
        {
            originalSrpites[i] = partyTexts[i].sprite;
        }
    }

    public void SetSelected(bool selected)
    {
        int selectedIndex = selected ? 1 : 0;
        for (int i = 0; i < partyTexts.Count; ++i)
        {
            if (i == selectedIndex)
                partyTexts[i].sprite = highlightedImage;
            else
                partyTexts[i].sprite = originalSrpites[i];
        }
    }
}
