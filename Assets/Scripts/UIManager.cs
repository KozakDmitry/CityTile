using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Action OnRoadPlacement, OnHousePlacement, OnSpecialPlacement;
    public Button roadButton, houseButton, specialButton;

    public Color outlineColor;
    private List<Button> buttonList;


    private void Start()
    {
        buttonList = new List<Button> { roadButton, houseButton, specialButton };


        roadButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(roadButton);
            OnRoadPlacement?.Invoke();
        });
        houseButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(houseButton);
            OnHousePlacement?.Invoke();
        });
        specialButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(specialButton);
            OnSpecialPlacement?.Invoke();
        });
    }

    private void ModifyOutline(Button button)
    {
        var outline = button.GetComponent<Outline>();
        outline.effectColor = outlineColor;
        outline.enabled = true;
    }

    private void ResetButtonColor()
    {
        foreach(var button in buttonList)
        {
            button.GetComponent<Outline>().enabled = false;

        }
    }
}
