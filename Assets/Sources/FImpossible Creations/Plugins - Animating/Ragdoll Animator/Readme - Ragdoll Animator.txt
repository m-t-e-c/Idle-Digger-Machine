__________________________________________________________________________________________

Package "Ragdoll Animator"
Version 1.0.0

Made by FImpossible Creations - Filip Moeglich
https://www.FilipMoeglich.pl
FImpossibleCreations@Gmail.com or FImpossibleGames@Gmail.com or Filip.Moeglich@Gmail.com

__________________________________________________________________________________________

Youtube: https://www.youtube.com/channel/UCDvDWSr6MAu1Qy9vX4w8jkw
Facebook: https://www.facebook.com/FImpossibleCreations
Twitter (@FimpossibleC): https://twitter.com/FImpossibleC

___________________________________________________

User Manual:

To use Ragdoll Animator add "Ragdoll Animator" component
(Add Component -> Fimpossible Creations -> Ragdoll Animator)
and setup required bone transform references which you can find inside hierarchy.
Initially Ragdoll Animator tries to find right bones using Humanoid Mecanim Animator is available.
Ragdoll Animator tries to find some bones by names, but it's important to check if right ones was selected.

After setting up all bone references, open "Ragdoll Generator" tab and switch "Generate Ragdoll".
Now you can check on your scene view how colliders are generated, with sliders you can adjust their size and physical parameters.
After that, you can select "Tweak Ragdoll Colliders" selector and tweak colliders positions/scale without need to go through hierarchy for that.
After all this things done you can hit the Gear ⚙️ button on the top left of Ragdoll Animator inspector window.

There you can setup how ragdoll should behave.
Enter with mouse cursor on the parameters (tooltips are not displayed during playmode)
to read tooltips about which parameter is responsible for.


Tweaking and fixing:

Take in mind that Ragdoll Animator is not yet supporting universal setup for the angle limiting on physical joints,
so if you encounter some of the limbs rotating in wrong ranges you have to adjust it by yourself.

If you want to keep ragdoll when switching scenes, you should enable "Persistent" toggle in Ragdoll Animator setup.
Component is generating secondary skeleton with ragdoll which shouldn't be destroyed to make component work, Persistent is calling "DontDestroyOnLoad" on the generated dummy skeleton.

Unity joints are not supporting scaling during playmode, so just initial scale is supported!

If after entering playmode you see there is clearly something wrong with your model + error logs in console,
try assigning "Skeleton Root" manually (bottom of ragdoll animator setup inspector window)

If you encounter somemething like spine jittery, try lowering configurable spring parameter, increase configurable damp,
try switching "Extended Animator Sync" modes, try different mass on colliders.

One time I encountered some error with spine jittery and I couldn't fix it for few hours, 
then I changed few scenes and went back to glitching scene and it was fixed by itself, no idea what changed, Unity mischiefs I guess.

__________________________________________________________________________________________
Description:

Setup your humanoid ragdoll instantly!
Blend ragdolled limbs with animated model!
Enable ragdoll and controll muscle power towards animator pose!

Ragdoll Animator offsers effective and clear solution for 
handling ragdolled humanoid model. (works on generic skeleton too)

Quickly prepare bones to generate colliders, rigidbodies and joints on.
Controll scale/rigidbody mass of all colliders with basic sliders.
Tweak colliders position/scale with additional scene gizmos - without need to finding bones in the hierarchy.

You can animate ragdoll in sync with keyframe animation and provide collision 
detection for arms, head and spine. 
You can smoothly enable free-fall ragdoll mode with possibility to add 
some natural motion to ragdoll with muscle power moving bones towards 
keyframe animator pose with defined power.

Package works on all SRPs! It's not shader related package.

__________________________________________________________________________________________
Changelog:

