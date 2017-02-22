using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMouse : BaseMouseController {
    [SerializeField]
    ArmyManager army;
    public int groupSize;
    public List<BasicAgentNavigation> units;
    [SerializeField]
    SmartTarget target;

    void Start() {
      //  units = new List<BasicAgentNavigation>(groupSize);
    }

    protected override void doubleClick() {
        SmartTarget t = (SmartTarget)Instantiate<SmartTarget>(target, positionClick, Quaternion.identity);
        foreach (BasicAgentNavigation agent in units) {
            agent.setTarget(t);
        }
    }

    protected override void mouseHold()
    {
      
    }
}
