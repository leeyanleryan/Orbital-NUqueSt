INCLUDE globals.ink
EXTERNAL QuestCompleted()
-> main

=== main ===
{tutorialSecretarySpoken: -> tutorialDone | -> tutorialNotDone }

-> END

=== tutorialNotDone ===
Secretary: Hi! You must be the new guy in town.
Secretary: I am the only secretary working here.
Secretary: I help the mayor with organising events, scheduling appointments, and more.
Ava: You may call me Ava.

~ tutorialSecretarySpoken = true

-> tutorialDone

=== tutorialDone ===
Ava: Anything you would like to ask?
    * [What is this place?]
        Ava: This is the mayor's house.
        Ava: Our village was established more than 30 years ago.
        Ava: We have many friendly villagers who usually require help as they are busy.
        -> tutorialDone
    * [What do I do now?]
        Ava: There is a vacant house south of this village.
        Ava: We believe it should be sufficient for you.
        Ava: Take a rest, and I will fill you in on what you should do.
        -> tutorialDone
    * [Leave]
        Ava: Bye!
        -> DONE

-> END