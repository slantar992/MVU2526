

using UnityEngine;

[CreateAssetMenu(menuName= "Data/UI/Popup data", fileName = "MyPopupData")]
public class PopupData : ScriptableObject
{
    [TextArea(5, 10)]
    public string message;
}