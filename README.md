Demo with logic extracted to non-MonoBeh classes:
 - no injections or reactive packages, pure Unity and C#
 - all entities logic is extracted and can easily be covered with tests
 - gameplay logic is not extracted intentionally (although, it should be... maybe one day it'll be fixed)
 - logic and presentation split
 - used new Unity Input System for inputs (and still there is wrapper for input actions so input system can easily be switched to any other)
 - entity skins system
 - basic entity modules system that allows to create entity with any required logical behaviours (this system has evolution in my pet-project, but it's still in WiP and not released)
