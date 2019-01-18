using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public enum ElementType {
    undefined = 0,
    Song = 1,
    Contact = 2
}

[System.Serializable]
public enum ElementState
{
    notInAnyList = 0,
    isInPlaylist = 1,
    isInShortcuts = 2,
    isRemoved = 3
}

[System.Serializable]
public class ListElement {

    public ElementType elementType;
    public ElementState elementState;
    public int id;

    public ListElement()
    {
        elementState = ElementState.notInAnyList;
        elementType = ElementType.undefined;
    }
}
