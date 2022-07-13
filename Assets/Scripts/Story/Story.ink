INCLUDE globals.ink

{childName == "": -> main | ->already_chosen}

===main===
~changeBlockPos = false
Teacher: Hi. Welcome to Parent-Teacher Conference.
Parent: Thanks.
Teacher: So, what is your child’s name?
Choose a name for the child
    +[Megan]
    ->child("Megan", 2)
    +[Jade]
    ->child("Jade", 1)  
    +[Amber]
    ->child("Amber", 3)
    
===child(childd, index)=== 
~childName = childd
~storyIndex = index
~storyPhase = 1
~changeBlockPos = true
Parent: It’s {childName}.
Teacher: {childName}. Uh, let’s see. Oh yeah, {childName}. Um, she missed the last couple of days. Has she been sick?
Parent: No, she’s been having some problems with the other kids in your class, and . . .
Teacher: Well, which class exactly?
Parent: It's the mathematics class.
Teacher: Well, I'm afraid you've to talk to the Mr. Cooper about this matter as he takes the mathematics class.
Parent: 
    +[Ok]
    Parent: Okay, Thank you.
    ~changeBlockPos = false
    ->DONE
    +[I wanna raise a complaint...]
    Parent: I want to report some harrassment done to {childName}
    Teacher: I'm sorry ma'am. I cannot help you any more. Anyway, I'm late for my class. I would strongly suggest you to take this matter to Mr. Cooper.
    ~changeBlockPos = false
    ->DONE


===already_chosen===
Teacher: Please take the matter about {childName}, to Mr. Cooper.Pardon me, but I'm already late for my class.
->DONE