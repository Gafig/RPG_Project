using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialogs/Dialog")]
public class DialogBluePrint : ScriptableObject {

    public new string name;
    [TextArea(3, 10)]
    public string[] sentences;
}
