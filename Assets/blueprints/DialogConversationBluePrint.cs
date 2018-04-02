using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Dialogs/Conversation")]
public class DialogConversationBluePrint : ScriptableObject {

    public Dialog[] dialogs;
}
