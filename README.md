# VARJ - the game
School assignment

A game engine and RPG-game written in C#

The game engine is based on the entity component system architecture. 
It has six different managers that helps the game developer organize the game.

## EntityManager
The entity manager is responsible for generating new entity ids.

## ComponentManager
The component manager is responsible for storing, retrieving and removing components
given the entity id. It has two dictionaries that stores the components, one that stores the
components based on the entity id and one that stores the components based on
component type. When the developer wants to remove an entity the component manager
adds that id to a list and at the end of the update cycle the component manager deletes it.
When adding components the developer can choose to either add one component or a list
of components, for convenience. The component manager is implemented by the Singleton
pattern so that is the same everywhere.

## SystemManager
The system manager handles all the systems in the game, both ordinary systems and render
systems. The game developer adds all the systems that he needs, either one by one or in a
list. The system manager then has the functionality to remove, get, update and update
render. When updating the game developer can choose to just update some systems, good
for testing, or all systems. The system manager implements the Singleton pattern.

## ResourceManager
The resource manager is responsible for storing all content. When creating some entity that
needs a sprite or a sound the game developer calls the resource manager and asks for it. It
contains a dictionary with a string key to the resources. The resource manager implements
the Singleton pattern.


