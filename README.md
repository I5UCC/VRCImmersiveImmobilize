# VRCImmersiveImmobilize [![Github All Releases](https://img.shields.io/github/downloads/i5ucc/VRCImmersiveImmobilize/total.svg)](https://github.com/I5UCC/VRCImmersiveImmobilize/releases/latest) <a href='https://ko-fi.com/i5ucc' target='_blank'><img height='35' style='border:0px;height:25px;' src='https://az743702.vo.msecnd.net/cdn/kofi3.png?v=0' border='0' alt='Buy Me a Coffee at ko-fi.com' />

Workaround for the remote IK jitter issue and allows entering objects/walls in VRChat when using full body tracking. <br>

This functions similarly to [SouljaVR/AutoImmobilizeOSC](https://github.com/SouljaVR/AutoImmobilizeOSC) while not using the controllers touch capacitors to trigger immobilization, but rather the state of movement on both sticks. Hence fixing the problem while trying to make the user feel like its not even there. <br>

This project also features an easy installer Script in Unity.

### Please upvote this [VRChat Canny](https://feedback.vrchat.com/vrchat-ik-20/p/network-jitter-with-ik) to get this problem fixed on VRChats end.

### [<img src="https://assets-global.website-files.com/6257adef93867e50d84d30e2/636e0a6ca814282eca7172c6_icon_clyde_white_RGB.svg"  width="20" height="20"> Discord Support Server](https://discord.gg/rqcWHje3hn)

# Contents

- [Download](https://github.com/I5UCC/VRCImmersiveImmobilize#download)
- [Demonstration](https://github.com/I5UCC/VRCImmersiveImmobilize#demonstration)
- [Installation](https://github.com/I5UCC/VRCImmersiveImmobilize#installation)
- [ThumbparamsOSC](https://github.com/I5UCC/VRCImmersiveImmobilize#thumbparamsosc)
- [Testing](https://github.com/I5UCC/VRCImmersiveImmobilize#testing)
- [Caveats](https://github.com/I5UCC/VRCImmersiveImmobilize#caveats)

# Download

Either download the latest release from here, or add my Repository to VCC (VRChat Creator Companion):

[<img src="https://user-images.githubusercontent.com/43730681/235305688-08099e52-2ea8-4b28-b647-4cef10c4d073.png"  width="270" height="35">](https://i5ucc.github.io/vpm/VRCImmersiveImmobilize.html) <br>

or <br>

[<img src="https://user-images.githubusercontent.com/43730681/235304229-ce2b4689-4945-4282-967e-40bfbf8ebf54.png"  width="181" height="35">](https://i5ucc.github.io/vpm/main.html) <br>

<details>
  <summary>Manually adding to VCC:</summary>
  
  1. Open VCC
  2. Click "Settings" in the bottom left
  3. Click the "Packages" tab at the top
  4. Click "Add Repository" in the top right
  5. Paste `https://i5ucc.github.io/vpm/VRCImmersiveImmobilize.json` into the text field and click "Add"
  6. Click "I understand, Add Repository" in the popup after reading its contents
  7. Activate the checkbox next to the repository `VRCImmersiveImmobilize`
  
  PS: You can also add `https://i5ucc.github.io/vpm/main.json` to add all of my projects (and future ones) to VCC.
</details>

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
- Continue at [#ThumbparamsOSC](https://github.com/I5UCC/VRCImmersiveImmobilize#thumbparamsosc)

### ThumbparamsOSC
This program needs [ThumbparamsOSC](https://github.com/I5UCC/VRCThumbParamsOSC) running in the background, to capture stick movements and send that information to your Avatar via OSC.
Read the Documentation on ThumbparamsOSC, on how to set it up. For this case, we only need the two parameters `LeftStickMoved` and `RightStickMoved`. You can turn off any other parameters ThumbparamsOSC is sending to VRChat, if you wish so.

### Testing
You can check VRChats debug menu to see if the Parameter `Locomotion` switches between `Disabled` and `Enabled`, depending if you are moving or not.
If that isnt working, follow [ThumbparamsOSC Troubleshooting steps](https://github.com/I5UCC/VRCThumbParamsOSC#osc-troubleshoot).

### Caveats
- You will be unable to jump while not moving, as the system will immobilize you when you arent moving.
- When moving while calibrating and then standing still, the avatar might freeze, you can mitigate this by either:
    - not moving when loading into the avatar
    - reloading the avatar
    - pushing the right stick downwards
- Pointed out by 'Kung' (VRC Team), "If you turn 180 in playspace and then locomote, remote users will see a quick springing of the player root catching up to the new orientation while tweening. In local view it won't be visible."
