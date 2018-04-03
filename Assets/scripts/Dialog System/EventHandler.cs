using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EventHandler {

    bool NotifyGameControllerStart();
    void NotifyGameControllerFinish();
}
