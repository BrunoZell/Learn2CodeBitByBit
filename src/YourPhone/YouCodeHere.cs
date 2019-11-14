using System;

public static class LockScreen
{
    public static void UnlockPhone(UInt16 pin)
    {
        // This code will execute when someone entered a four digit pin code into the lock screen.
        // Todo: Write some code to test if the entered pin equals 4973.
        // If so, set Phone.Unlocked = true
        // Otherwise, set Phone.Unlocked = false

        if (pin == 1234)
            Phone.Unlocked = true;
    }
}
