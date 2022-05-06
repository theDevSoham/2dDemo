->main

===main===
Hello and Welcome
Which Pokemon do you choose? #choice
    +[Charmander] 
    ->pokemon("Charmander") 
    +[Bulbasaur]
    ->pokemon("Bulbasaur")  
    +[Pikachu]
    ->pokemon("Pikachu")
    
===pokemon(pokemonn)=== 
You chose {pokemonn} 
->DONE