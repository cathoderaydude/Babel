using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Babel.StateMachines
{
    public class State
    {
        public event Action enter;
        public event Action leave;

        public void DoEnter() => enter?.Invoke();
        public void DoLeave() => leave?.Invoke();

        public State(Action enter, Action leave)
        {
            this.enter += enter;
            this.leave += leave;
        }
    }

    public class StateMachine
    {
        State current = null;

        public event Action enterNull;
        public event Action leaveNull;

        public void Change(State next)
        {
            if (current == null)
                leaveNull?.Invoke();
            else
                current.DoLeave();

            current = next;

            if (current == null)
                enterNull?.Invoke();
            else
                current.DoEnter();
        }
    }
}
