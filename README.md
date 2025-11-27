This is the GDW 2025 fall repository for our game Same Day Shipping.

Same day shipping is a game where the player must ship packages by boat over the ocean to different islands, 
with weather and other events trying to hinder them. 


For the GED Project 1 with May(marcus) and Erik, here is the required information:

# May(Marcus) Yorke, 100874913


<img width="243" height="322" alt="image" src="https://github.com/user-attachments/assets/04cfa6da-9aa8-4e48-b50c-959404a9591e" />


------------------------------------------------------------------------------------------------------
# Erik Anderson, 100753323


<img width="243" height="377" alt="image2" src="https://github.com/user-attachments/assets/5684d36e-4e2b-491c-948c-66aad0377d2b" />

## Singleton: 

![GED_Project_Singleton](https://github.com/user-attachments/assets/c6fd1da2-dc2f-4ddf-a8ef-97d953e6207f)

For the singleton present in the PlayerCommands.cs file I made no improvements as it does exactly what it is supposed to do.

In the script there is a variable for a static instance of PlayerCommands. On awake, the script check if the static instance is NOT equal to null. If that's true then it destroys the gameobject its attached to. If false then it sets the instance to itself and runs the DontDestroyOnLoad function. The reason for this is because this script contains the keybinds for the player. So if the player rebinds forward from W to say T then naturally we want that to be saved between scenes. So if the instance isn't null, then a version of it already exists and shouldn't be overwritten.
