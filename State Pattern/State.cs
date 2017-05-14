using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State_Pattern
{
    public abstract class State
    {
        public int onNeighbors;
        public bool[] birthConditions;
        public bool[] survivalConditions;
        public State(int oN, bool[] birth, bool[] survive)
        {
            birthConditions = birth;
            survivalConditions = survive;
            onNeighbors = oN;
        }
        public abstract State update();
        public State setOn(bool on)
        {
            if (on)
                return new OnState(0, birthConditions, survivalConditions);
            return new OffState(0, birthConditions, survivalConditions);
        }
        public void setOnNeighbors(List<State> neighbors)
        {
            onNeighbors = 0;
            foreach(State i in neighbors)
            {
                if (i is OnState)
                    onNeighbors++;
            }
        }
    }
    public class OnState : State
    {
        public OnState(int oN, bool[] birth, bool[] survive) : base(oN, birth, survive) { }
        public override State update()
        {
            if (!survivalConditions[onNeighbors])
                return new OffState(onNeighbors, birthConditions, survivalConditions);
            return new OnState(onNeighbors, birthConditions, survivalConditions);
        }
    }
    public class OffState : State
    {
        public OffState(int oN, bool[] birth, bool[] survive) : base(oN, birth, survive) { }
        public override State update()
        {
            if (birthConditions[onNeighbors])
                return new OnState(onNeighbors, birthConditions, survivalConditions);
            return new OffState(onNeighbors, birthConditions, survivalConditions);
        }
    }
}
