using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConsole
{
    public class Example
    {
        private readonly Dictionary<int, (string, Action)> _menuList = new Dictionary<int, (string, Action)>()
        {
            {0, ("Exit",    () => { Console.WriteLine("Exit"); }) },
            {1, ("Action1", () => { Console.WriteLine("You take action 1\n"); }) },
            {2, ("Action2", () => { Console.WriteLine("You take action 2\n"); }) },
        };

        /// <summary>
        /// This is an example of writing code exactly as a sequence of steps in a use case
        /// First step: getting a valid choice from console
        /// Second step: take action based on choice
        /// Final step: end if choosing to exit, back to main menu otherwise
        /// </summary>
        public void DoThing()
        {
            var fistStep    = Helper.ExecuteThenPointTo<int>(DisplayMenu, GetValidInputFromConsole);
            var secondStep  = Helper.ExecuteThenPointTo(fistStep, TakeActionBasedOnInput);
            var finalStep   = Helper.ExecuteThenPointTo(secondStep, GoBackToMenuOrEnd);
            finalStep();
        }
        #region private method
        private void DisplayMenu()
        {
            _menuList.Select(menu => $"{menu.Key}: {menu.Value.Item1}")
                     .ToList()
                     .ForEach(Console.WriteLine);
        }
        private int GetValidInputFromConsole()
        {
            Console.Write("Your choice: ");
            int choice = -1;
            while (!int.TryParse(Console.ReadLine(), out choice) || !IsValidChoice(choice))
            {
                Console.WriteLine("Invalid Input! Try again");
                Console.Write("Your choice: ");
            }
            return choice;
        }
        private int TakeActionBasedOnInput(int choice)
        {
            // excute the coressponding function based on choice in the menu list
            _menuList[choice].Item2.Invoke();

            return choice;
        }
        private void GoBackToMenuOrEnd(int choice)
        {
            int exitCode = 0;
            if (choice != exitCode)
            {
                DoThing();
            }
        }
        private bool IsValidChoice(int choice)
        {
            return (choice >= 0 && choice < _menuList.Count);
        }
        #endregion
    }
}
