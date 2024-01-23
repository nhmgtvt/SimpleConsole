using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConsole
{
    public static class Helper
    {
        #region function compose helper
        public static Func<T2> ExecuteThenPointTo<T1, T2>(Func<T1> currentAction, Func<T1, T2> nextAction)
        {
            // Define a new function that takes no arguments and returns T2
            Func<T2> composedFunc = () =>
            {
                // Execute currentAction to get the result
                T1 resultFromCurrentAction = currentAction();

                // Execute nextAction with the result from currentAction as input
                T2 resultFromNextAction = nextAction(resultFromCurrentAction);

                // Return the final result
                return resultFromNextAction;
            };

            // Return the composed function
            return composedFunc;
        }

        public static Func<T2> ExecuteThenPointTo<T2>(Action currentAction, Func<T2> nextAction)
        {
            // Define a new function that takes no arguments and returns T2
            Func<T2> composedFunc = () =>
            {
                //execute currentAction
                currentAction.Invoke();

                // Execute nextAction
                T2 resultFromNextAction = nextAction();

                // Return the final result
                return resultFromNextAction;
            };

            // Return the composed function
            return composedFunc;
        }

        public static Action ExecuteThenPointTo<T>(Func<T> currentAction, Action<T> nextAction)
        {
            // Define a new function that takes no arguments
            Action composedFunc = () =>
            {
                // Execute currentAction to get the result
                T resultFromCurrentAction = currentAction();

                // Execute nextAction with the result from currentAction
                nextAction(resultFromCurrentAction);
            };

            // Return the composed function
            return composedFunc;
        }
        #endregion 
    }
}
