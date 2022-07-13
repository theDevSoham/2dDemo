 INCLUDE globals.ink

{descType == "": ->secondPhase | ->alreadyChosenDesc}

=== secondPhase ===
{childName == "": ... | {childName == "Megan": ->chooseDesc("She's having trouble with the class", "She's getting bullied at your class")} {childName == "Jade": ->chooseDesc("She's having trouble with the class", "She's getting bullied at your class")} {childName == "Amber": ->chooseDesc("She's having trouble with the class", "She's getting bullied at your class")}}
-> DONE

=== chooseDesc(opt1, opt2) ===
~changeBlockPos = false
Parent: Hello, is this Mr. Cooper?
Mr. Cooper: Yes Ma'am. Who am I speaking to?
Parent: I'm {childName}'s Mom. I'm here to talk about her performance recently. Could you please spare me some time?
Mr. Cooper: What seems to be the problem?
Parent: 
    +[{opt1}]
        ->showDesc(opt1)
    +[{opt2}]
        ->showDesc(opt2)
-> DONE

=== showDesc(option) ===
~descType = option
~storyPhase = storyPhase + 1
~changeBlockPos = true
{option == "She's having trouble with the class": ->troubleWithClass | ->bullying.}
-> DONE

===troubleWithClass()===
Parent: She's having some trouble in your class.
Mr. Cooper: Please tell me about it.
Parent: Some kids have been bullying her recently in your class.
Mr.Cooper: Well why didn't she inform this to me herself?
Parent: Well, according to {childName}, you didn't help either.
Mr. Cooper: Are you serious?
Parent: Yes {childName} says the kids tease her online and mock her in your class on the basis of the online conversations.
Mr. Cooper: Well I heard nothing of that sort. 
Parent: 
    +[Sorry I'll ask her again]
        Parent: I'll ask her about the matter again.
        ~changeBlockPos = false
        ->DONE
    +[You should be more careful]
        Parent: Please be more carefull in your class.
        ~changeBlockPos = false
        ->DONE
->DONE

===bullying()===
Parent: Some of the kids in your class have really been bullying her a lot.
Mr. Cooper: What do you mean?
Parent: Well, um, they’ve been teasing her a lot about her appearance, and then, the other day, you didn’t help things.
Mr. Cooper: What?
Parent: Yeah, these things happen online and they mock {childName} in your class.
Mr. Cooper: Well I don't know about these matters.
Parent: You should be more careful in your class.
Mr. Cooper: Ok I'll see to it. The fact is I'm really busy. So, if you please excuse me.
Parent: Okay Fine.

->DONE


=== alreadyChosenDesc ===
DND
-> DONE

