﻿using UnityEngine;
using UnityEngine.UI;

public class Theatre : MonoBehaviour
{
    [SerializeField] Text dialogBox;
    [SerializeField] Transform actorHolder;
    [SerializeField] GameObject scene;

    Actor currentActor = null;
    Dialog currentDialog;
    System.Action onDone;

    bool IsPlaying { get { return currentDialog != null; } }

    public void Play(Dialog dialog, System.Action onDone)
    {
        this.onDone = onDone;
        scene.SetActive(true);
        currentDialog = dialog;
        currentDialog.Initialize();
        Continue();
    }

    void Continue()
    {
        var replica = currentDialog.GetNextReplica();
        if (currentActor != null)
            Destroy(currentActor.gameObject);
        currentActor = Instantiate<Actor>(replica.actor, actorHolder);
        dialogBox.text = replica.text;
        Debug.Log(replica.text);
    }

    void Update()
    {
        if (IsPlaying && Input.GetButtonDown("Next"))
        {
            if (currentDialog.HasNotEnded)
                Continue();
            else
                End();
        }
    }

    void End()
    {
        currentDialog = null;
        Destroy(currentActor.gameObject);
        scene.SetActive(false);
        if (onDone != null)
            onDone();
    }
}
