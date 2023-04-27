# VRCImmersiveImmobilize

Workaround for the remote IK jitter issue and allows entering objects/walls in VRChat when using full body tracking. <br>

This functions similarly to [SouljaVR/AutoImmobilizeOSC](https://github.com/SouljaVR/AutoImmobilizeOSC) while not using the controllers touch capacitors to trigger immobilization, but rather the state of movement on both sticks. Hence fixing the problem while trying to make the user feel like its not even there. <br>

### Please upvote this [VRChat Canny](https://feedback.vrchat.com/vrchat-ik-20/p/network-jitter-with-ik) to get this problem fixed on VRChats end.

# Download

Either download the latest release from [here](https://github.com/I5UCC/VRCImmersiveImmobilize/releases/latest), or add my Repository to VCC (VRChat Creator Companion):

1. Open VCC
2. Click `Settings` in the bottom left
3. Click the `Packages` tab at the top
4. Click `Add Repository` in the top right
5. Paste `https://i5ucc.github.io/vpm/VRCImmersiveImmobilize.json` into the text field and click `Add`
6. Click `I understand, Add Repository` in the popup after reading its contents
7. Activate the checkbox next to the repository `VRCImmersiveImmobilize`

PS: You can also add `https://i5ucc.github.io/vpm/main.json` to add all of my projects (and future ones) to VCC.

# Demonstration

[Soon TM]

# Installation

### Automatic Install
- Add the package over VCC or Unitypackage to your project.
- In the top left, navigate to `Tools/I5UCC/VRCImmersiveImmobilize`
- Drag and Drop your avatar to the empty object saying `None (VRC Avatar Descriptor)`
- Click on install <br>
![grafik](https://user-images.githubusercontent.com/43730681/234945785-ffd37e32-9619-498a-9a81-9c120c26dc38.png)
- After clicking install, a new layer named `ImmersiveImmobilize` is added to your FX controller and the parameters are added to the Expression parameters. Additionally a manual toggle is added to your Expression Menu.
- Continue at [#ThumbparamsOSC](https://github.com/I5UCC/VRCImmersiveImmobilize#thumbparamsosc)

### Manual Install
- Add the package over VCC or Unitypackage to your project.
- Merge `VRCII_FX` in `Packages/VRCImmersiveImmobilize` with your current FX layer on your avatar, using a tool like `Avatars 3.0 Manager`
- Add the following Parameters to your avatars Expression Parameters:
    - LeftStickMoved (Bool)
    - RightStickMoved (Bool)
    - Immobilize (Bool)
- Add a new Control to your Avatars Expression menu with the type `Toggle` and using the parameter `Immobilize`

### ThumbparamsOSC
This program needs [ThumbparamsOSC](https://github.com/I5UCC/VRCThumbParamsOSC) running in the background, to capture stick movements and send that information to your Avatar via OSC.
Read the Documentation on ThumbparamsOSC, on how to set it up. For this case, we only need the two parameters `LeftStickMoved` and `RightStickMoved`. You can turn off any other parameters ThumbparamsOSC is sending to VRChat, if you wish so.

### Testing if it works
You can check VRChats debug menu to see if the Parameter `Locomotion` switches between `Disabled` and `Enabled`, depending if you are moving or not.
If that isnt working, follow [ThumbparamsOSC Troubleshooting steps](https://github.com/I5UCC/VRCThumbParamsOSC#osc-troubleshoot).
