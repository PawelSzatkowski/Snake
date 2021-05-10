using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_v2._0
{
    public class Menu
    {
        private Dictionary<int, MenuItem> _options = new Dictionary<int, MenuItem>();

        public void AddOption(MenuItem item)
        {
            if (_options.ContainsKey(item.Key))
            {
                return;
            }
            _options.Add(item.Key, item);
        }
        public void ExecuteOption(int optionKey)
        {
            if (!_options.ContainsKey(optionKey))
            {
                Console.WriteLine("\n" + "The option you've chosen doesn't exist." + "\n");
                return;
            }
            var item = _options[optionKey];
            item.Action();
        }

        public void PrintAvailableMenuOptions()
        {
            foreach(var option in _options)
            {
                if (option.Key == 9) // jak poniżej - zabieg kosmetyczny, oddzielam wyjście z glownego menu
                {
                    Console.WriteLine();
                }
                Console.WriteLine($"{option.Key}. {option.Value.Description}");

                if (option.Key == 5) // zabieg kosmetyczny, dla oddzielenia od siebie funkcji w features menu
                {
                    Console.WriteLine();
                }
            }
        }
    }
}
