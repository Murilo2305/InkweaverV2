using UnityEngine;

public static class InputUtility
{
    private const string leftTriggerName = "LTrigger";
    private const string rightTriggerName = "RTrigger";
    public static bool usedL;
    public static bool usedR;
    private static bool releasedL;
    private static bool releasedR;


    

    public static bool LTriggerPulled
    {
        get
        {
            if (!usedL && Input.GetAxis(leftTriggerName) > 0)
            {
                usedL = true;
                return true;
            }
            else if (usedL && Input.GetAxis(leftTriggerName) < 0)
            {
                usedL = false;
            }
            return false;
        }
    }

    public static bool RTriggerPulled
    {
        get
        {
            if (!usedR && Input.GetAxis(rightTriggerName) > 0)
            {
                usedR = true;
                return true;
            }
            else if (usedR && Input.GetAxis(rightTriggerName) == -1)
            {
                usedR = false;
            }
            return false;
        }
    }

    public static bool LTriggerHeld
    {
        get
        {
            if (Input.GetAxis(leftTriggerName) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public static bool RTriggerHeld
    {
        get
        {
            if (Input.GetAxis(rightTriggerName) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public static bool LTriggerReleased
    {
        get
        {
            if (!releasedL && Input.GetAxis(leftTriggerName) < 0)
            {
                releasedL = true;
                return true;
            }
            else if (releasedL && Input.GetAxis(leftTriggerName) > 0)
            {
                releasedL = false;
            }
            return false;
        }
    }

    public static bool RTriggerReleased
    {
        get
        {
            if (!releasedR && Input.GetAxis(rightTriggerName) < 0)
            {
                releasedR = true;
                return true;
            }
            else if (releasedR && Input.GetAxis(rightTriggerName) > 0)
            {
                releasedR = false;
            }
            return false;
        }
    }
}