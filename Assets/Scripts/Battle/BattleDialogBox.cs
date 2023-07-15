using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    [SerializeField] Sprite highlightedImage;
    [SerializeField] Color highlightedColor;

    [SerializeField] Text dialogText;
    [SerializeField] Image dialogImage;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetails;
    [SerializeField] GameObject choiceBox;


    [SerializeField] List<Image> actionTexts;
    [SerializeField] List<Text> moveTexts;

    [SerializeField] Text ppText;
    [SerializeField] Text typeText;

    [SerializeField] Text YesText;
    [SerializeField] Text NoText;

    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";
        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        yield return new WaitForSeconds(1f);
    }

    public void EnableDialogImage(bool enabled)
    {
        dialogImage.enabled = enabled;
    }
    public void EnableDialogText(bool enabled)
    {
        dialogText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
    }

    public void EnableMoveSelector(bool enabled)
    {
        moveSelector.SetActive(enabled);
        moveDetails.SetActive(enabled);
    }
    public void EnableChoiceBox(bool enabled)
    {
        choiceBox.SetActive(enabled);
    }
    private Sprite[] originalSprites;

    void Start()
    {
        originalSprites = new Sprite[actionTexts.Count];
        for (int i = 0; i < actionTexts.Count; i++)
        {
            originalSprites[i] = actionTexts[i].sprite;
        }
    }

    public void UpdateActionSelection(int selectedAction)
    {
        for (int i = 0; i < actionTexts.Count; ++i)
        {
            if (i == selectedAction)
                actionTexts[i].sprite = highlightedImage;
            else
                actionTexts[i].sprite = originalSprites[i];
        }
    }

    public void UpdateMoveSelection(int selectedMove, Move move)
    {
        for (int i = 0; i < moveTexts.Count; ++i)
        {
            if (i == selectedMove)
                moveTexts[i].color = highlightedColor;
            else
                moveTexts[i].color = Color.black;
        }

        ppText.text = $"PP {move.PP} / {move.Base.PP}";
        typeText.text = move.Base.Type.ToString();

        if (move.PP == 0)
            ppText.color = Color.red;
        else
            ppText.color = Color.black;
    }

    public void SetMoveNames (List<Move> moves)
    {
        for (int i=0; i < moveTexts.Count; ++i)
        {
            if (i < moves.Count)
                moveTexts[i].text = moves[i].Base.name;
            else
                moveTexts[i].text = "-";
        }
    }
    public void UpdateChoiceBox(bool yesSelected)
    {
        if (yesSelected)
        {
            YesText.color = highlightedColor;
            NoText.color = Color.black;
        }
        else
        {
            YesText.color = Color.black;
            NoText.color = highlightedColor;
        }
    }
}
