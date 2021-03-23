# Instruction

## Usage
### Setting up the interactable object
1. Delete all the files in `"CustomInteractable"` folder.
2. Create scripts heritating `InteractableBase` in the `"CustomInteractable"` foler, then override custom `Interact()` function. And add this script to the object that needs to be interact.
### Setting up UI
1. In `Canvas`, create an empty object as the parent of all UI related elements.
2. Create `Image`(as the progress bar) and `Text`(as the tooltip) object, and assign it in the parent created before.
### Connecting Input and Interaction
1. Create an extra script to receive input message
2. In that script, modify `interactionInputData.InteractRelease` and `interactionInputData.InteractClicked`