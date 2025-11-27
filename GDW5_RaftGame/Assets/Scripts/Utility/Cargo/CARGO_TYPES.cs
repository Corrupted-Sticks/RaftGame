using UnityEngine;

public enum CARGO_TYPES
{
    Cube,
    Stretch,
    Barrel,
    COUNT // if you add a count to enums, you can get their length super easy by just doing CARGO_TYPES.COUNT, but you gotta do -1 if you iterate all cargotypes in a loop.
}
