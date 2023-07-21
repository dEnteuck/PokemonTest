using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSelectionUI : MonoBehaviour
{
    [SerializeField] List<Text> moveTexts;
    [SerializeField] Color hightlightedColor;

    int currentSelection = 0;

    public void SetMoveData(List<MoveBase> currentMove, MoveBase newMove)
    {
        for (int i =0; i < currentMove.Count; ++i)
        {
            moveTexts[i].text = currentMove[i].name;
        }

        moveTexts[currentMove.Count].text = newMove.name;
    }

    public void HandleMoveSelection(Action<int> onSelected)
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
            ++currentSelection;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            --currentSelection;

        currentSelection = Mathf.Clamp(currentSelection, 0, PokemonBase.MaxNumOfMoves);

        UpdateMoveSelection(currentSelection);

        if (Input.GetKeyDown(KeyCode.Z))
            onSelected?.Invoke(currentSelection);

    }

    public void UpdateMoveSelection(int selection)
    {
        for (int i=0; i < PokemonBase.MaxNumOfMoves + 1; i++)
        {
            if (i == selection)
                moveTexts[i].color = hightlightedColor;
            else
                moveTexts[i].color = Color.black;
        }
    }
}
