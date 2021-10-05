using System;
using System.Collections.Generic;
using System.Linq;

namespace CsvPaginated.October
{
    public class PaginatedMenu<T>
    {
        private readonly List<T> _items;
        private readonly int _pageSize = 10;
        private readonly Action<List<T>> _printItems;
        private readonly int _totalCount;
        private readonly int _totalPages;
        private int _currentPage = 1;

        public PaginatedMenu(List<T> items, Action<List<T>> PrintItems)
        {
            _items = items;
            _printItems = PrintItems;
            _totalCount = items.Count;
            _totalPages = (int)Math.Ceiling(_totalCount / (double)_pageSize);
        }

        public void Show()
        {
            while (true)
            {
                _printItems.Invoke(_items.Skip((_currentPage - 1) * _pageSize).Take(_pageSize).ToList());
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

        private bool PageExists(int page) => page <= _totalPages && page > 0;
    }
}