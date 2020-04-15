using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure; // Gamepad input plugin

// ----------------------------------------------------------------------------------------------------------------
// Button State Classes
// ----------------------------------------------------------------------------------------------------------------

// Stores states of a single gamepad button
struct XButton
{
    public ButtonState prev_state;
    public ButtonState state;
}

// Stores state of a single gamepad trigger
struct XTrigger
{
    public float prev_value;
    public float current_value;
}

public class Vibration
{
    public Vibration() { }
    public Vibration(string _name, float _left, float _right, float _time)
    {
        name = _name;
        left = _left;
        right = _right;
        time = _time;
    }

    public string name;
    public float left;
    public float right;
    public float time;
}

// ----------------------------------------------------------------------------------------------------------------
// Gamepad Class
// ----------------------------------------------------------------------------------------------------------------

public class XGamePad
{
    // ----------------------------------------------------------------------------------------------------------------
    // Button Enumerators
    // ----------------------------------------------------------------------------------------------------------------

    public enum Button
    {
        A,
        B,
        X,
        Y,
        DPad_Up,
        DPad_Down,
        DPad_Left,
        DPad_Right,
        Guide,
        Back,
        Start,
        LeftBumper,
        RightBumper,
        L3,
        R3
    }

    public enum Trigger
    {
        LeftTrigger,
        RightTrigger
    }

    public enum Stick
    {
        LeftStick,
        RightStick
    }

    // ----------------------------------------------------------------------------------------------------------------
    // Member Variables
    // ----------------------------------------------------------------------------------------------------------------

    private GamePadState prev_state; // Previous gamepad state
    private GamePadState state;      // Current gamepad state

    private int gamepadIndex;        // Numeric index (1,2,3 or 4
    private PlayerIndex playerIndex;    // XInput 'Player' index
    private List<Vibration> mVibrations; // Stores rumble events

    // Button input map (explained soon!)
    private Dictionary<string, XButton> inputMap;

    // States for all buttons/inputs supported
    private XButton A, B, X, Y; // Action (face) buttons
    private XButton DPad_Up, DPad_Down, DPad_Left, DPad_Right;

    private XButton Guide;       // Xbox logo button}
    private XButton Back, Start;
    private XButton L3, R3;      // Thumbstick buttons
    private XButton LB, RB;      // 'Bumper' (shoulder) buttons
    private XTrigger LT, RT; // Triggers

    // ----------------------------------------------------------------------------------------------------------------
    // Constructor / Destructor
    // ----------------------------------------------------------------------------------------------------------------

    public XGamePad(int index)
    {
        // Set gamepad index
        playerIndex = (PlayerIndex)index;

        // Create rumble container and input map
        mVibrations = new List<Vibration>();

        inputMap = new Dictionary<string, XButton>();
    }

    // ----------------------------------------------------------------------------------------------------------------

    void OnDestroy()
    {
        mVibrations.Clear();
        SetVibration(0.0f, 0.0f);
    }

    // ----------------------------------------------------------------------------------------------------------------
    // Generic Functionality
    // ----------------------------------------------------------------------------------------------------------------

    // Return numeric gamepad index
    public int GetIndex() { return gamepadIndex; }

    // Return gamepad connection state
    public bool IsConnected() { return state.IsConnected; }

    // ----------------------------------------------------------------------------------------------------------------
    // Update
    // ----------------------------------------------------------------------------------------------------------------

    public void Update()
    {
        // Update Inputs
        // Get current state
        state = GamePad.GetState(playerIndex);
        
        // Check gamepad is connected
        if (state.IsConnected)
        {
            A.state = state.Buttons.A;
            B.state = state.Buttons.B;
            X.state = state.Buttons.X;
            Y.state = state.Buttons.Y;

            DPad_Up.state = state.DPad.Up;
            DPad_Down.state = state.DPad.Down;
            DPad_Left.state = state.DPad.Left;
            DPad_Right.state = state.DPad.Right;

            Guide.state = state.Buttons.Guide;
            Back.state = state.Buttons.Back;
            Start.state = state.Buttons.Start;
            L3.state = state.Buttons.LeftStick;
            R3.state = state.Buttons.RightStick;
            LB.state = state.Buttons.LeftShoulder;
            RB.state = state.Buttons.RightShoulder;

            // Read trigger values into trigger states
            LT.current_value = state.Triggers.Left;
            RT.current_value = state.Triggers.Right;


            // Update Vibrations
            UpdateVibrations();
        }
        else // Controller not conected
        {
            mVibrations.Clear();
            SetVibration(0.0f, 0.0f);
        }
    }

    // ----------------------------------------------------------------------------------------------------------------

    public void UpdatePreviousState()
    {
        // This 'saves' the current state for next update
        prev_state = state;

        // Check gamepad is connected
        if (state.IsConnected)
        {
            A.prev_state = prev_state.Buttons.A;
            B.prev_state = prev_state.Buttons.B;
            X.prev_state = prev_state.Buttons.X;
            Y.prev_state = prev_state.Buttons.Y;

            DPad_Up.prev_state = prev_state.DPad.Up;
            DPad_Down.prev_state = prev_state.DPad.Down;
            DPad_Left.prev_state = prev_state.DPad.Left;
            DPad_Right.prev_state = prev_state.DPad.Right;

            Guide.prev_state = prev_state.Buttons.Guide;
            Back.prev_state = prev_state.Buttons.Back;
            Start.prev_state = prev_state.Buttons.Start;
            L3.prev_state = prev_state.Buttons.LeftStick;
            R3.prev_state = prev_state.Buttons.RightStick;
            LB.prev_state = prev_state.Buttons.LeftShoulder;
            RB.prev_state = prev_state.Buttons.RightShoulder;

            // Read previous trigger values into trigger states
            LT.prev_value = prev_state.Triggers.Left;
            RT.prev_value = prev_state.Triggers.Right;
        }
    }

    // ----------------------------------------------------------------------------------------------------------------
    // Buttons
    // ----------------------------------------------------------------------------------------------------------------

    private XButton GetButton(Button button)
    {
        XButton xButton;

        switch (button)
        {
        case Button.A:              xButton = A;            break;
        case Button.B:              xButton = B;            break;
        case Button.X:              xButton = X;            break;
        case Button.Y:              xButton = Y;            break;
        case Button.DPad_Up:        xButton = DPad_Up;      break;
        case Button.DPad_Down:      xButton = DPad_Down;    break;
        case Button.DPad_Left:      xButton = DPad_Left;    break;
        case Button.DPad_Right:     xButton = DPad_Right;   break;
        case Button.Guide:          xButton = Guide;        break;
        case Button.Back:           xButton = Back;         break;
        case Button.Start:          xButton = Start;        break;
        case Button.LeftBumper:     xButton = LB;           break;
        case Button.RightBumper:    xButton = RB;           break;
        case Button.L3:             xButton = L3;           break;
        case Button.R3:             xButton = R3;           break;
        default: Debug.Log("BUTTON ERROR"); xButton = A;    break;    // Should never hit here
        }

        return xButton;
    }

    // ----------------------------------------------------------------------------------------------------------------

    public bool GetButtonDown(Button button)
    {
        if (!state.IsConnected) return false;

        XButton xButton = GetButton(button);
        return xButton.state == ButtonState.Pressed;
    }

    // ----------------------------------------------------------------------------------------------------------------

    public bool GetButtonUp(Button button)
    {
        if (!state.IsConnected) return false;

        XButton xButton = GetButton(button);
        return xButton.state == ButtonState.Released;
    }

    // ----------------------------------------------------------------------------------------------------------------

    public bool GetButtonPressed(Button button)
    {
        if (!state.IsConnected) return false;

        XButton xButton = GetButton(button);
        return (xButton.state ==      ButtonState.Pressed ) &&
               (xButton.prev_state == ButtonState.Released);
    }

    // ----------------------------------------------------------------------------------------------------------------

    public bool GetButtonReleased(Button button)
    {
        if (!state.IsConnected) return false;

        XButton xButton = GetButton(button);
        return (xButton.state == ButtonState.Released) &&
               (xButton.prev_state == ButtonState.Pressed);
    }

    // ----------------------------------------------------------------------------------------------------------------
    // Triggers
    // ----------------------------------------------------------------------------------------------------------------

    private XTrigger GetTrigger(Trigger trigger)
    {
        XTrigger xTrigger;
        switch (trigger)
        {
            case Trigger.LeftTrigger:   xTrigger = LT;  break;
            case Trigger.RightTrigger:   xTrigger = RT; break;
            default: Debug.Log("TRIGGER ERROR"); xTrigger = LT; break;
        }
        return xTrigger;
    }

    // ----------------------------------------------------------------------------------------------------------------

    public float GetTriggerValue(Trigger trigger)
    {
        return GetTrigger(trigger).current_value;
    }

    // ----------------------------------------------------------------------------------------------------------------

    public bool GetTriggerDown(Trigger trigger)
    {
        return GetTrigger(trigger).current_value > 0.1f;
    }

    // ----------------------------------------------------------------------------------------------------------------

    public bool GetTriggerPressed(Trigger trigger)
    {
        XTrigger xtrigger = GetTrigger(trigger);
        return (xtrigger.prev_value == 0.0f) && (xtrigger.current_value > 0.1f);
    }

    // ----------------------------------------------------------------------------------------------------------------
    // Sticks
    // ----------------------------------------------------------------------------------------------------------------

    private GamePadThumbSticks.StickValue GetStick(Stick stick)
    {
        GamePadThumbSticks.StickValue xStick;
        switch (stick)
        {
            case Stick.LeftStick:   xStick =    state.ThumbSticks.Left;         break;
            case Stick.RightStick:  xStick =    state.ThumbSticks.Right;        break;
            default: Debug.Log("STICK ERROR"); xStick = state.ThumbSticks.Left; break;
        }
        return xStick;
    }

    // ----------------------------------------------------------------------------------------------------------------

    public Vector2 GetStickValue(Stick stick)
    {
        GamePadThumbSticks.StickValue xStick = GetStick(stick);
        return new Vector2(xStick.X, xStick.Y);
    }

    // ----------------------------------------------------------------------------------------------------------------
    // Vibration
    // ----------------------------------------------------------------------------------------------------------------

    private void UpdateVibrations()
    {
        if (mVibrations != null)
        {
            float left = 0.0f;
            float right = 0.0f;

            for (int j = 0; j < mVibrations.Count; ++j)
            {
                if (mVibrations[j].time > 0.0f)
                {
                    mVibrations[j].time -= Time.deltaTime;
                    if (mVibrations[j].time < 0.0f)
                    {
                        mVibrations.RemoveAt(j);
                        --j;
                        continue;
                    }
                    left += mVibrations[j].left;
                    right += mVibrations[j].right;
                }
            }
            SetVibration(left, right);
        }
        else // No vibrations
        {
            SetVibration(0.0f, 0.0f);
        }
    }

    // ----------------------------------------------------------------------------------------------------------------

    // Set the vibration of the controller
    public void SetVibration(float left, float right)
    {
        GamePad.SetVibration(playerIndex, left, right);
    }

    // ----------------------------------------------------------------------------------------------------------------

    // Find a vibration in the list
    public Vibration FindVibration(string name)
    {
        if (mVibrations != null)
        {
            for (int i = 0; i < mVibrations.Count; ++i)
            {
                if (mVibrations[i].name == name)
                {
                    return mVibrations[i];
                }
            }
        }

        return null;
    }

    // ----------------------------------------------------------------------------------------------------------------

    // Add a vibration to the list
    public void AddVibration(Vibration vibration)
    {
        mVibrations.Add(vibration);
    }

    public void ClearVibrations()
    {
        mVibrations.Clear();
    }
}

// ----------------------------------------------------------------------------------------------------------------
// Gamepad Manager Class
// ----------------------------------------------------------------------------------------------------------------

public class GamepadManager : MonoBehaviour
{ 
    public static XGamePad[] mGamepads;
    private static bool mInialized;

    // ----------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        if (mInialized) return;

        mGamepads = new XGamePad[4];

        for(int i = 0; i < 4; ++i)
        {
            mGamepads[i] = new XGamePad(i);
        }
        DontDestroyOnLoad(gameObject);

        mInialized = true;
    }

    // ----------------------------------------------------------------------------------------------------------------

    void Update ()
    {
	    for(int i = 0; i < 4; ++i)
        {
            mGamepads[i].UpdatePreviousState();
            mGamepads[i].Update();
        }
	}

    // ----------------------------------------------------------------------------------------------------------------

    void OnApplicationQuit()
    {
        for (int i = 0; i < 4; ++i)
        {
            mGamepads[i].ClearVibrations();
        }
    }

    // ----------------------------------------------------------------------------------------------------------------

    public static void SetVibration(int index, string name, float left, float right, float time)
    {
        if (index >= 4) return;

        XGamePad gamepad = mGamepads[index];

        Vibration foundVibration = gamepad.FindVibration(name);

        if (foundVibration != null)
        {
            foundVibration.left = left;
            foundVibration.right = right;
            foundVibration.time = time;
        }
        else
        {
            Vibration newVibration = new Vibration(name, left, right, time);
            gamepad.AddVibration(newVibration);
        }
    }
}
