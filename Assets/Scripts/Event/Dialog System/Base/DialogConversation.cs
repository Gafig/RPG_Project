﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogConversation{

    public Dialog[] dialogs;

    public DialogConversation(Dialog[] dialogs) {
        this.dialogs = dialogs;
    }
}

