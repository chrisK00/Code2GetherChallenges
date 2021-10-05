using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvPaginated.October
{
    public class PaginatedMenu<T>
    {
        private readonly IEnumerable<T> _items;
        private readonly int _pageSize = 10;
        private readonly int _totalCount;
        private readonly int _totalPages;
        private int _currentPage = 1;

        public PaginatedMenu(IEnumerable<T> items)
        {
            _ = items ?? throw new ArgumentNullException(nameof(items));

            _items = items;
            _totalCount = items.Count();
            _totalPages = (int)Math.Ceiling(_totalCount / (double)_pageSize);
        }

        public void Show()
        {
            while (true)
            {
                PrintItems();
                Console.WriteLine($"Page: {_currentPage}/{_totalPages}\tTotal Items: {_totalCount}");
                Console.WriteLine("Go to the next or previous page using the Left and Right arrows");
                var key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        if (PageExists(_currentPage - 1)) _currentPage--;
                        break;

                    case ConsoleKey.RightArrow:
                        if (PageExists(_currentPage + 1)) _currentPage++;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Key. Press any key to continue");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }

                Console.Clear();
            }
        }

        public void PrintItems()
        {
            var sb = new StringBuilder();
            foreach (var item in _items.Skip((_currentPage - 1) * _pageSize).Take(_pageSize))
            {
                sb.Append($"{item}");
            }

            Console.WriteLine(sb.ToString());
        }

        private bool PageExists(int page) => page <= _totalPages && page > 0;
    }
}