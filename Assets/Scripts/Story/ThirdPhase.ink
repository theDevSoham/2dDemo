INCLUDE globals.ink

{annie == 0 : ->talkWithFriend | ->doneWithFriend}

===talkWithFriend===

Parent: Hello are you Annie, {childName}'s closest friend?
Annie: Umm...Yes ma'am
Parent: I'm {childName}'s mother. I need to talk to you about {childName}.
Annie: Uhh...yes, what about her? She's a real jerk!
Parent: You shouldn't say things like that about her. What kind of education are you getting studying in such prestigious school?
Annie: Well she behaves like she's dumb.
Parent: You say things like these in group chats on social media. She also said that her friends exclude her from group conversations and make fun of her when she tries to interact with you.
Parent: Do you know how depressed she is nowdays? She barely talks or eats, gets severe anxiety attacks, and sleeps less.
Annie: Well we were just kind of joking a little bit with her.
Parent: Well, yeah, that’s what you think, but other kids follow your example. In fact, one of the kids took a picture of her with their phone and posted it and had some real nasty comments on Facebook. It was terrible.
Annie: So, people treat her like that. It's not my fault is it?
Parent: No, don’t you get it? This is bullying; it’s cyberbullying, and adults like you are part of the problem. Forget it. I’m planning on discussing this with the principal tomorrow.
Annie: Oh, wait, wait, wait. Um, uh, oh. I’m sorry if I hurt her feelings , but . . .
Parent: I get sick and tired of people thinking that a little teasing is okay. Too many kids are killing themselves because they feel that there’s just no way to escape this. The behaviour you friends show towards {childName} is part of a cyberbullying crime called Exclusion.
Annie: Okay. Well, I guess I need to be a little bit more careful, but . . .
Parent: Yeah, you do. I really hope I can get {childName} to come to school tomorrow. She’s been really, really anxious and depressed.
Annie: Wow. Uh, I’m really sorry. Could you see if you can bring her to school tomorrow? Uh, I’d like to apologize and see what I can do to, maybe, improve the situation.
Parent: Thanks. I’d appreciate it. That would help.

~annie=1
->DONE

===doneWithFriend===

Annie: I'll see that the bullying never happens again.
Parent: Thanks!
->DONE