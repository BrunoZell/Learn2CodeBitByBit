# Learn To Code - Bit by Bit

**This is a basic sketch of one possible learning path**

```csharp
public static (partial) class Program {
    public static class Phone {
        static bool Unlcoked { get;  set; }
        static int attempts = 0; // Make it easy define global, static variables. Make it obvious that static means they will stay alive during the runtime of the program.
    }
}
```

Entsperre das Handy wenn der pin richtig eingegeben wurde. Er ist auf 4973 gesetzt.
Demo wartet auf Eingabe von Zahlen und bei der vierten Ziffer wird diese Methode automatisch ausgeführt. Es wird n=4 nicht Hinterfragt. konzentration auf die Entscheidung, ob die Pin denn die korrekte ist.

```csharp
void UnlockPhone(UInt16 pin) {
    if (pin == 4973) {
        Phone.Unlcoked = true;
    } else {
        Phone.Unlcoked = false;
    }
}
```

Nun wollen wir aber auch Pins mit mehr als füunf Stellen anzeigen (>2^16). Interne binäre Darstellung wird betrachtet und das Maximum erklärt. Nun wollen wir es umgehen.
(Bei Rückfrage, warum wir nicht die Bitanzahl erhöhen: Sind in alten Zeiten und es ist Physikalisch nit möglich einen Prozessor größer 16 bit anzufertigen).
Alle digits werden einzeln übergeben. Nutzer hat den Pin 497382.

```csharp
void UnlockPhone(UInt16 digit1, UInt16 digit2, UInt16 digit3, UInt16 digit4, UInt16 digit5, UInt16 digit6) {
    if (digit1 == 4 && digit2 == 9 && digit3 == 7 && digit4 == 3 && digit5 == 8 && digit6 == 2) {
        Phone.Unlcoked = true;
    } else {
        Phone.Unlcoked = false;
    }
}
```

Pins nur mit Zahlen sind zu unsicher - wie können wir Buchstaben darstellen (0-9a-f)?
Nutzer hat die Pin a0c5f2
In der Demo zum testen kann alles mögliche (0-9a-f) eingegeben werden, soll aber nur beim obigen Pin korrekterweise entsperren.

```csharp
void UnlockPhone(UInt16 digit1, UInt16 digit2, UInt16 digit3, UInt16 digit4, UInt16 digit5, UInt16 digit6) {
    if (digit1 == 10 && digit2 == 0 && digit3 == 12 && digit4 == 5 && digit5 == 15 && digit6 == 2) {
        Phone.Unlcoked = true;
    } else {
        Phone.Unlcoked = false;
    }
}
```

- 0 -> 0
- 1 -> 1
- 2 -> 2
- 3 -> 3
- 4 -> 4
- 5 -> 5
- 6 -> 6
- 7 -> 7
- 8 -> 8
- 9 -> 9
- a -> 10
- b -> 11
- c -> 12
- d -> 13
- e -> 14
- f -> 15

Nun kann der Benutzer die erforderte Anzahk an Zeigen im Pin erhöhen. Diese liegt nun bei n=8 und wird in Array-Form übergeben.
Dieser Nutzer hat die Pin 3ba8ff01

```csharp
void UnlockPhone(UInt16[] digits) {
    if (digits[0] == 3 && digits[1] == 11 && digits[2] == 10 && digits[3] == 8 && digits[4] == 15 && digits[5] == 15 && digits[6] == 0 && digits[7] == 1) {
        Phone.Unlcoked = true;
    } else {
        Phone.Unlcoked = false;
    }
}
```

Ist ja schwer da noch die Übersicht zu behalten - introducing char
Zeigt wie Datentypen das interpretieren der Werte erleichtern, wenn man genauer hinschaut es doch aber nur Zahlen sind.

```csharp
void UnlockPhone(char[] chars) {
    if (chars[0] == '3' && chars[1] == 'b' && chars[2] == 'a' && chars[3] == '8' && chars[4] == 'f' && chars[5] == 'f' && chars[6] == '0' && chars[7] == '1') {
        Phone.Unlcoked = true;
    } else {
        Phone.Unlcoked = false;
    }
}
```

Das ist sehr unübersichtlich. Können wir die Ergebnisse auch benennen und zwischenspeichern?
Wir lernen über den typ 'bool' (ja/nein) und wie diesre nützlich sein kann.
Wir lernen, dass wir selber Variablen definieren können.
Die Eingabelänge zum Testen ist immernoch bei n=8 hardgecoded.

```csharp
void UnlockPhone(char[] chars) {
    bool c1Correct = chars[0] == '3';
    bool c2Correct = chars[1] == 'b';
    bool c3Correct = chars[2] == 'a';
    bool c4Correct = chars[3] == '8';
    bool c5Correct = chars[4] == 'f';
    bool c6Correct = chars[5] == 'f';
    bool c7Correct = chars[6] == '0';
    bool c8Correct = chars[7] == '1';

    if (c1Correct && c2Correct && c3Correct && c4Correct && c5Correct && c6Correct && c7Correct && c8Correct) {
        Phone.Unlcoked = true;
    } else {
        Phone.Unlcoked = false;
    }
}
```

More readable and reasonable, but still very verbose - introducing string
A built in dynamic-length version of char[]

```csharp
void UnlockPhone(string password) {
    bool pwCorrect = password == "3ba8ff01";

    if (pwCorrect) {
        Phone.Unlcoked = true;
    } else {
        Phone.Unlcoked = false;
    }
}
```

Todo: Make Phone phone a class level variable (or statics!) so it is not polluting the method signature. This makes it easier to introduce subs and functions as this instance does not need to get passed on.

```csharp
void UnlockPhone(string password) { ... }
void SetPassword(string newPassword) { ... }
// (Make password storable during runtime with a static property in Phone)
```

Ist ja blöd, wenn jeder das Passwort ändern kann. Stellen wir sicher, dass nur Benutzer, die das Passwort wissen auch ein neues einstellen können.
Problemdemo: Ich setze privat (ohne dass der Schüler schaut) mein Passwort. Nun lasse ich ihn versuchen mein Handy zu unlocken. Schafft er, da er einfach das Passwort ändern kann und sich dann mit dem neuen anmeldet.
Wie können wir das umgehen? Baue es so, dass nur jemand sein passwort ändern kann, wenn er auch das alte weiß.

```csharp
void UnlockPhone(string password) { ... }
void SetPassword(string oldPassword, string newPassword) { ... }
```

Demo: Standardpasswort = "". Wissen wir beide. Ich werde privat das Passwort ändern, ohne dass der Schüler guckt. Wie kann er es nun entsperren?
Only set the password when oldPassword matches the existing one.
If not, do nothing

At this point, we should have some kind of visualization for the static storage space. A field where we store the current password (plain text) and another field where we store whether the phone is locked.
To make the statement even clearer, we will go ahead and build some lockout logic

Point him to some facts about me. Point out my birth date - 20.05.1999 - oder 'mai99', ggf. '1999' mit dem Hinweis, dass er weiß ich habe einen vierstelligen Zahlencode. Zeige ihn simple Brute-Force Methoden
(wie er durch ausprobieren das Handy entsperren kann). Wie kommen wir dagegen an? Baue eine Selbstzerstörung.

```csharp
void UnlockPhone(string password) { ... }
void SetPassword(string oldPassword, string newPassword) { ... }
```

Zusätzliches feld in Phone anlegen dürfen. Ggf eine zusätzliche statische Klasse einführen beim erstmaligen visualisieren des speichers, welche man mit selbst definierten statischen variablen bestücken kann.
Not they should create somthing along the lines of 'static int retryAttampts = 0;'.
Do not unlock the phone when retryAttempts >= 5. Increment retryAttempts whenever the user puts in a wrong passwords.

As a stretch goal - reset retryAttempts to zero when a successful login happened (that is when password is correct AND retryAttempts has not yet reached 5).

This is kinda unconfortable to see the device locked down for ever - maybe trying with a lockdown period? Only stay locked for 10 seconds.
We need some understanding of time. How could we represent it in a computer? Think relative.
Introduce Unix. Maybe have a static class member with a unix time accessor - or in Java, just use the system.currentTimeMillis().
Then later intriduce DateTime and DateTime.Now (LocalTime and LocalTime.now() for Java) as another data type and how it's stored underneath.

Now, the phone is considered as secure since brute forcing is infeasable. If we however really want security, the persistent lockout after some amount of tries is still a valid option (as even bruteforc with lockouts inbetween could be successful).

However, as the password lies in memory and we know how to encode it, we can peak into it and steal it. After all, every stored variable has to be persisted somewhere physical (in RAM, for instance).
As there are many programs running concurrently on modern computers, can we write a programm which peeks at the memory of our phone and can extract the stored password?

Here the layout is known by us:

```csharp
// | unlocked? (byte) | retryAttempts (UInt16) | char | char | char | ... | char |
void EncodePassword(byte[] memory) {
    var chars = new char[memory.Length - 3];
    for (int i = 0, i < chars.Length; i++) {
        byte passwordCharAsNumber = memory[i + 3];
        char passwordChar = (char)passwordCharAsNumber;
        chars[i] = passwordChar;
    }

    // You also can print out the chars directly if you want to. With a loop.
    string password = new String(chars);
    Console.Write(password);
}
```

Here I imagine the phone running in one window where you are free to set whatever password you want (and increase the locks), and another console windows poping up acting as your password attack tool.
The memory diagnose should be an additional window (opened on demand) which uses reflection to show how the (continuous) memory structure changes when you change your static class. Also showing strings,
but numbers for each char as well. Maybe even provide a toggle for binary representations so you can get the difference between different integer sizes.

Now, unlocking the password should be managable when you see how the memory changes when interacting with the phone - and testing our code to interpret the software.
After cracking the software, what algorithm can we deploy to have the same functionality of our lock screen, but without exposing the password in plain text?
Here we could go over encryption and having a non-standard charset (maybe just shift it). However, I think a cool way to showcase the possibilities, I want to introduce hash functions and checksums.
What is the fundamental problem here? We want to test if any given string is equal to a specific previously entered string (the last set password). What we don't need is to show the saved password after
we have saved it. We just need to know if it is equal to some new input.
Introducing checksums. Let's just sum up all the chars as numbers, and store the sum. Then later, calculate the checksum on the newly entered password and check if the sum is equal - not the password string.
Implement it, and then check if we can somehow attack it. We may can go on here while we are still able to read out the memory and reconstruct a valid password. Finally we will have implemented a commonly used
hash function while learning everything about loops, scope, functions and bit logic.

to be continued..
