using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonView : BounceElement
{
    // Only this is necessary. Physics is doing the rest of work.
    // Callback called upon collision.
    void OnCollisionEnter() { app.Notify(ButtonNotification.ButtonPress, this); }
}
