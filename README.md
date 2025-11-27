This is the GDW 2025 fall repository for our game Same Day Shipping.

Same day shipping is a game where the player must ship packages by boat over the ocean to different islands, 
with weather and other events trying to hinder them. 

For the GED Final Project with May(marcus) and Erik, here is the required information:
For each of the required design patterns, both of us did one per.

Skybox texture from: [Skybox-Series-Free](https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633)

All other sounds, models, textures, and art were done by the other members of our GDW group.

# May(Marcus) Yorke, 100874913


<img width="243" height="322" alt="image" src="https://github.com/user-attachments/assets/04cfa6da-9aa8-4e48-b50c-959404a9591e" />


------------------------------------------------------------------------------------------------------
# Erik Anderson, 100753323


<img width="243" height="377" alt="image2" src="https://github.com/user-attachments/assets/5684d36e-4e2b-491c-948c-66aad0377d2b" />

## Singleton

![GED_Project_Singleton](https://github.com/user-attachments/assets/c6fd1da2-dc2f-4ddf-a8ef-97d953e6207f)

For the singleton present in the PlayerCommands.cs file I made no improvements as it does exactly what it is supposed to do.

In the script there is a variable for a static instance of PlayerCommands. On awake, the script check if the static instance is NOT equal to null. If that's true then it destroys the gameobject its attached to. If false then it sets the instance to itself and runs the DontDestroyOnLoad function. The reason for this is because this script contains the keybinds for the player. So if the player rebinds forward from W to say T then naturally we want that to be saved between scenes. So if the instance isn't null, then a version of it already exists and shouldn't be overwritten.

## Command Pattern

![GED_Project_Command](https://github.com/user-attachments/assets/bd077832-d833-4e6f-a203-ad182378eaba)

For the command pattern that is also present in the PlayerCommands.cs file there hasn't been any improvements either.

Inside PlayerCommands it is rather standard for the pattern. There are currently 5 different commands that can be performed. Moving in 4 directions and interacting. Inside the PlayerCMovement.cs file if one of the keys is pressed that correspond to a command, then it executes that command. Each command has its own script that inherits from the abstract PCommand class. I do not see a reason to change this as its doing exactly what I want it to do. In the future I may look to see if I can find a better way to handle the keyrebinding; it currently has a list of the default keybinds should the player want to reset them, a list of the current keybinds, and a list of the string equivalent of the current keybind.

## Factory

![GED_Project_Factory](https://github.com/user-attachments/assets/97658952-ab88-4203-bc49-1acc2de26062)

For the factory pattern in CargoFactory.cs the only change made to it isn't related to the design pattern but simply just catching what could've caused an error (if the factory was sent 5 items to spawn while it only has 4 places to spawn them).

How the factory works is that when the player accepts a job from the board, a list of anywhere from 1 to 4 pieces of cargo (currently there are 3 different types of cargo) is sent to the factory. Then it takes each cargo type in the list and spawns each at designated spots on the boat. A factory pattern was used here as it makes it a lot easier to add and remove different things that can spawn. If I wanted I could have 100 different types of cargo. And those cargo objects can apply different effects to themselves when they spawn. Things like varying gravity, less friction, etc. Those examples provided will most likely be added to the game in the winter semester.

## Object Pooling

![GED_Project_ObjectPool](https://github.com/user-attachments/assets/d7f8d08a-1db4-4399-8e39-a402f9a865a7)

For an optimization pattern, I chose object pool as I felt that how I handle cargo objects in the game would benefit from it.

How its set up is that when the factory runs its awake function it fills a dictionary with the keys being the types of cargo (via an enum) and the values being of the class CargoObjectPool. This gives each type of cargo object its own pool. Now, I wouldn't do it like this if there were 100 different types, but as there's only 3 right now it makes it easier to know I'm getting the right type of object from the pool. It tries to get an object from its pool but if there isn't any then it makes one. But if there is then it uses that one. Then if an object would be removed (either if it falls off the boat or if the player finishes the job) the object instead is added back to the pool and is disabled.

By doing it this way I can reuse cargo objects instead of having to create new objects every single time.

## Observer

![GED_Project_Observer](https://github.com/user-attachments/assets/7cbcc874-77d2-410a-94a9-3908efb78119)

For the observer pattern, I chose to also implement this into the cargo system.

How it works is that the abstract class Cargo inherits the abstract class CargoSubject. Then the different types of cargo inherit the Cargo class. It feels a bit off to have a chain of abstract classes so in the future I may just put it all into the Cargo class. In the CargoSubject class there are the 3 functions you expect to see: Attach, Detach, and NotifyObservers. The JobManager class inherits from a CargoObserver script which only has a Notify function inside. When a cargo object is either instantiated or enabled from the object pool, it finds an object of type JobManager and then attaches it. If the cargo object would be despawned by falling out of the map it calls the NotifyObservers function. What this is set up to do is to tell the JobManager that cargo was lost and reduce the reward accordingly. Then the object is returned to its pool.

I decided to add observer this way as it feels like the easiest way. If the player loses cargo then the cargo can directly tell the JobManager. It overall makes it a lot cleaner.
