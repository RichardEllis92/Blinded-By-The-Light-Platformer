using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugController : MonoBehaviour
{
    public static DebugController instance;

    public bool showConsole;
    bool showHelp;
    bool displayHelp;

    string input;

    public static DebugCommand DOUBLE_JUMP;
    public static DebugCommand HELP;
    public static DebugCommand FIREBALL;

    public List<object> commandList;

    Vector2 scroll;
    GUI style;

    public GameObject invalidCheat;

    public void OnToggleDebug(InputValue value)
    {
        if (PlayerController.instance.IsGrounded())
        {
            showConsole = !showConsole;
        }
    }

    public void OnEnter(InputValue value)
    {
        if (showConsole)
        {
            HandleInput();
            input = "";
            displayHelp = true;
        }
    }

    private void Awake()
    {
        DOUBLE_JUMP = new DebugCommand("double_jump", "Gives the player the ability to double jump", "double_jump", () =>
        {
            PlayerController.instance.UnlockDoubleJump();
        });

        HELP = new DebugCommand("help", "Shows a list of commands", "help", () =>
        {
            showHelp = true;
        });

        FIREBALL = new DebugCommand("fireball", "Shoots fireball", "fireball", () =>
        {
            PlayerController.instance.UnlockFireBall();
        });

        commandList = new List<object>
        {
            HELP,
        };
    }

    private void Start()
    {
        instance = this;
    }

    private void OnGUI()
    {
        if (!showConsole || !PlayerController.instance.IsGrounded()) { displayHelp = false; return; }

        float y = 0f;
        if (displayHelp)
        {


            if (showHelp)
            {
                GUI.Box(new Rect(0, y, Screen.width, 100), "");

                Rect viewport = new Rect(0, 0, Screen.width - 30, 40 * commandList.Count);

                scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

                for (int i = 0; i < commandList.Count; i++)
                {
                    DebugCommandBase command = commandList[i] as DebugCommandBase;

                    string label = $"{command.commandFormat} - {command.commandDescription}";

                    Rect labelRect = new Rect(5, 40 * i, viewport.width - 100, 50);

                    GUI.skin.label.fontSize = 30;

                    GUI.Label(labelRect, label);

                }

                GUI.EndScrollView();

                y += 100;
            }
        }

        GUI.Box(new Rect(0, y, Screen.width, 50), "");
        GUI.backgroundColor = new Color(0, 0, 0);
        GUI.skin.textField.fontSize = 30;
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 50f), input);

    }

    

    private void HandleInput()
    {
        for(int i=0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.commandID))
            {
                if(commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }
                else
                {
                    StartCoroutine(InvalidCheat());
                }
            }
        }
    }

    IEnumerator InvalidCheat()
    {
        invalidCheat.SetActive(true);
        yield return new WaitForSeconds(2f);
        invalidCheat.SetActive(false);
    }

    public void AddToListDoubleJump()
    {
        commandList.Add(DOUBLE_JUMP);
    }

    public void AddToListFireBall()
    {
        commandList.Add(FIREBALL);
    } 
}
