using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public enum CursorLock
    {
        Confined,
        Locked,
        None
    }
    public CursorLock CursorLockedState;

	// Use this for initialization
	private void Start ()
    {
        switch(CursorLockedState)
        {
            case CursorLock.Confined:
                Cursor.lockState = CursorLockMode.Confined;
                break;

            case CursorLock.Locked:
                Cursor.lockState = CursorLockMode.Locked;
                break;

            case CursorLock.None:
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            Quit();
        }
    }

    private void OnMouseDown()
    {
        switch (CursorLockedState)
        {
            case CursorLock.Confined:
                Cursor.lockState = CursorLockMode.Confined;
                break;

            case CursorLock.Locked:
                Cursor.lockState = CursorLockMode.Locked;
                break;

            case CursorLock.None:
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }

    public void Quit()
    {
        // Quit the game
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void OnGUI()
    {
        // show the framerate
        GUI.Label(new Rect(0, 0, 100, 100), (1.0f / Time.smoothDeltaTime).ToString());
    }
}
