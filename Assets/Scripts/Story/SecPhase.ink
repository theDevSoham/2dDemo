INCLUDE globals.ink

{descType == "": ->secondPhase | ->alreadyChosenDesc}

=== secondPhase ===
{pokemonName == "": ... | {pokemonName == "Charmander": ->chooseDesc("Fire", "Water", true, false)} {pokemonName == "Bulbasaur": ->chooseDesc("Grass", "Fire", true, false)} {pokemonName == "Pikachu": ->chooseDesc("Fire", "Lightning", false, true)}}
-> DONE

=== chooseDesc(opt1, opt2, corr1, corr2) ===
What type of Pokemon is {pokemonName}?
    +[{opt1}]
        ->showDesc(opt1, corr1)
    +[{opt2}]
        ->showDesc(opt2, corr2)
-> DONE

=== showDesc(option, correct) ===
~descType = option
~correctDesc = correct
~storyPhase = storyPhase + 1
{correct: You're correct! | You're wrong}
-> DONE


=== alreadyChosenDesc ===
{correctDesc:You already chose {descType} and You're correct! | You already chose {descType} but You're wrong}
-> DONE

