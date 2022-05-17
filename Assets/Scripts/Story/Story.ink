INCLUDE globals.ink

{pokemonName == "": -> main | ->already_chosen}

===main===
Hello and Welcome
Which Pokemon do you choose? 
    +[Charmander]
    ->pokemon("Charmander", 2)
    +[Bulbasaur]
    ->pokemon("Bulbasaur", 1)  
    +[Pikachu]
    ->pokemon("Pikachu", 3)
    
===pokemon(pokemonn, index)=== 
~pokemonName = pokemonn
~storyIndex = index
You chose {pokemonName} 
->DONE

===already_chosen===
You've already chosen {pokemonName}
->DONE